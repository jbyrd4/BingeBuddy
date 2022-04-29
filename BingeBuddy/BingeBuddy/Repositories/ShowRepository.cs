using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BingeBuddy.Models;
using BingeBuddy.Utils;

namespace BingeBuddy.Repositories
{
    public class ShowRepository : BaseRepository, IShowRepository
    {
        public ShowRepository(IConfiguration config) : base(config) { }

        public List<Show> GetAllShows()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, CoverImage, Cancelled, Approved
                                        FROM Show";

                    var reader = cmd.ExecuteReader();

                    var shows = new List<Show>();

                    while (reader.Read())
                    {
                        shows.Add(new Show()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            CoverImage = DbUtils.GetString(reader, "CoverImage"),
                            Cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled")),
                            Approved = reader.GetBoolean(reader.GetOrdinal("Approved")),
                        });
                    }
                    reader.Close();

                    return shows;
                }
            }
        }

        public List<Show> GetShowsByApproved(bool approved)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, CoverImage, Cancelled, Approved
                                        FROM Show
                                        WHERE Approved = @approved";

                    cmd.Parameters.AddWithValue("@approved", approved);
                    var reader = cmd.ExecuteReader();
                    List<Show> shows = new List<Show>();

                    while (reader.Read())
                    {
                        shows.Add(new Show()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            CoverImage = DbUtils.GetString(reader, "CoverImage"),
                            Cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled")),
                            Approved = reader.GetBoolean(reader.GetOrdinal("Approved")),
                        });
                    }
                    reader.Close();

                    return shows;
                }
            }
        }

        public Show GetShowById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, CoverImage, Cancelled, Approved
                                        FROM Show
                                        WHERE Show.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Show show = null;

                    if (reader.Read())
                    {
                        show = new Show()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            CoverImage = DbUtils.GetString(reader, "CoverImage"),
                            Cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled")),
                            Approved = reader.GetBoolean(reader.GetOrdinal("Approved")),
                        };
                    }
                    reader.Close();
                    return show;
                }
            }
        }

        public void Add(Show show)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Show (Title, CoverImage, Cancelled, Approved)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Title, @CoverImage, @Cancelled, @Approved)";

                    DbUtils.AddParameter(cmd, "@Title", show.Title);
                    DbUtils.AddParameter(cmd, "@CoverImage", show.CoverImage);
                    DbUtils.AddParameter(cmd, "@Cancelled", show.Cancelled);
                    DbUtils.AddParameter(cmd, "@Approved", show.Approved);

                    show.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Show WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Show show)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Show
                                        SET Title = @Title,
                                            CoverImage = @CoverImage,
                                            Cancelled = @Cancelled,
                                            Approved = @Approved
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Title", show.Title);
                    DbUtils.AddParameter(cmd, "@CoverImage", show.CoverImage);
                    DbUtils.AddParameter(cmd, "@Cancelled", show.Cancelled);
                    DbUtils.AddParameter(cmd, "@Approved", show.Approved);
                    DbUtils.AddParameter(cmd, "@Id", show.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}