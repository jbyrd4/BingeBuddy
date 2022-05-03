using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BingeBuddy.Models;
using BingeBuddy.Utils;

namespace BingeBuddy.Repositories
{
    public class UserShowRepository : BaseRepository, IUserShowRepository
    {
        public UserShowRepository(IConfiguration config) : base(config) { }

        public List<UserShow> GetUserShowsByUserProfileId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT us.Id,
                                                us.LastReleasedEpisode, 
                                                us.LastReleasedSeason, 
                                                us.LastWatchedEpisode, 
                                                us.LastWatchedSeason, 
                                                us.DateUpdated, 
                                                us.Note, 
                                                s.Title, 
                                                s.CoverImage, 
                                                s.Cancelled, 
                                                s.Approved, 
                                                s.Id AS ShowId, 
                                                up.Id AS UserProfileId, 
                                                up.[Admin], 
                                                up.Email, 
                                                up.FirebaseUserId, 
                                                up.[Name] AS UserProfileName, 
                                                up.UserName, 
                                                p.Id AS PlatformId, 
                                                p.[Name] AS PlatformName, 
                                                c.Id AS CategoryId, 
                                                c.[Name] AS CategoryName
                                        FROM UserShow us
                                        JOIN Show s ON us.ShowId = s.Id
                                        JOIN UserProfile up ON us.UserId = up.Id
                                        JOIN [Platform] p ON us.PlatformId = p.Id
                                        JOIN Category c ON us.CategoryId = c.Id
                                        WHERE up.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    List<UserShow> userShows = new List<UserShow>();

                    while (reader.Read())
                    {
                        userShows.Add(new UserShow()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            LastReleasedEpisode = DbUtils.GetInt(reader, "LastReleasedEpisode"),
                            LastReleasedSeason = DbUtils.GetInt(reader, "LastReleasedSeason"),
                            LastWatchedEpisode = DbUtils.GetInt(reader, "LastWatchedEpisode"),
                            LastWatchedSeason = DbUtils.GetInt(reader, "LastWatchedSeason"),
                            DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                            Note = DbUtils.GetString(reader, "Note"),
                            ShowId = DbUtils.GetInt(reader, "ShowId"),
                            UserId = DbUtils.GetInt(reader, "UserProfileId"),
                            PlatformId = DbUtils.GetInt(reader, "PlatformId"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Show = new Show()
                            {
                                Id = DbUtils.GetInt(reader, "ShowId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                CoverImage = DbUtils.GetString(reader, "CoverImage"),
                                Cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled")),
                                Approved = reader.GetBoolean(reader.GetOrdinal("Approved")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "UserProfileName"),
                                UserName = DbUtils.GetString(reader, "UserName")
                            },
                            Platform = new Platform()
                            {
                                Id = DbUtils.GetInt(reader, "PlatformId"),
                                Name = DbUtils.GetString(reader, "PlatformName")
                            },
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            }

                        });
                    }
                    return userShows;
                }
            }
        }

        public List<UserShow> GetUserShowsByCategoryId(int userId, int categoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT us.Id,
                                                us.LastReleasedEpisode, 
                                                us.LastReleasedSeason, 
                                                us.LastWatchedEpisode, 
                                                us.LastWatchedSeason, 
                                                us.DateUpdated, 
                                                us.Note, 
                                                s.Title, 
                                                s.CoverImage, 
                                                s.Cancelled, 
                                                s.Approved, 
                                                s.Id AS ShowId, 
                                                up.Id AS UserProfileId, 
                                                up.[Admin], 
                                                up.Email, 
                                                up.FirebaseUserId, 
                                                up.[Name] AS UserProfileName, 
                                                up.UserName, 
                                                p.Id AS PlatformId, 
                                                p.[Name] AS PlatformName, 
                                                c.Id AS CategoryId, 
                                                c.[Name] AS CategoryName
                                        FROM UserShow us
                                        JOIN Show s ON us.ShowId = s.Id
                                        JOIN UserProfile up ON us.UserId = up.Id
                                        JOIN [Platform] p ON us.PlatformId = p.Id
                                        JOIN Category c ON us.CategoryId = c.Id
                                        WHERE up.Id = @userId AND us.CategoryId = @categoryId ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    var reader = cmd.ExecuteReader();

                    List<UserShow> userShows = new List<UserShow>();

                    while (reader.Read())
                    {
                        userShows.Add(new UserShow()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            LastReleasedEpisode = DbUtils.GetInt(reader, "LastReleasedEpisode"),
                            LastReleasedSeason = DbUtils.GetInt(reader, "LastReleasedSeason"),
                            LastWatchedEpisode = DbUtils.GetInt(reader, "LastWatchedEpisode"),
                            LastWatchedSeason = DbUtils.GetInt(reader, "LastWatchedSeason"),
                            DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                            Note = DbUtils.GetString(reader, "Note"),
                            ShowId = DbUtils.GetInt(reader, "ShowId"),
                            UserId = DbUtils.GetInt(reader, "UserProfileId"),
                            PlatformId = DbUtils.GetInt(reader, "PlatformId"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Show = new Show()
                            {
                                Id = DbUtils.GetInt(reader, "ShowId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                CoverImage = DbUtils.GetString(reader, "CoverImage"),
                                Cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled")),
                                Approved = reader.GetBoolean(reader.GetOrdinal("Approved")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "UserProfileName"),
                                UserName = DbUtils.GetString(reader, "UserName")
                            },
                            Platform = new Platform()
                            {
                                Id = DbUtils.GetInt(reader, "PlatformId"),
                                Name = DbUtils.GetString(reader, "PlatformName")
                            },
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            }

                        });
                    }
                    return userShows;
                }
            }
        }
        public void Add(UserShow userShow)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserShow (ShowId, UserId, LastWatchedSeason, LastWatchedEpisode, LastReleasedSeason, LastReleasedEpisode, DateUpdated, Note, PlatformId, CategoryId)
                        OUTPUT INSERTED.ID
                        VALUES (@ShowId, @UserId, @LastWatchedSeason, @LastWatchedEpisode, @LastReleasedSeason, @LastReleasedEpisode, @DateUpdated, @Note, @PlatformId, @CategoryId)";

                    DbUtils.AddParameter(cmd, "@ShowId", userShow.Show.Id);
                    DbUtils.AddParameter(cmd, "@UserId", userShow.UserId);
                    DbUtils.AddParameter(cmd, "@LastWatchedSeason", userShow.LastWatchedSeason);
                    DbUtils.AddParameter(cmd, "@LastWatchedEpisode", userShow.LastWatchedEpisode);
                    DbUtils.AddParameter(cmd, "@LastReleasedEpisode", userShow.LastReleasedEpisode);
                    DbUtils.AddParameter(cmd, "@LastReleasedSeason", userShow.LastReleasedSeason);
                    DbUtils.AddParameter(cmd, "@DateUpdated", userShow.DateUpdated);
                    DbUtils.AddParameter(cmd, "@PlatformId", userShow.PlatformId);
                    DbUtils.AddParameter(cmd, "@Note", userShow.Note);
                    DbUtils.AddParameter(cmd, "@CategoryId", userShow.CategoryId);

                    userShow.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = @"DELETE FROM UserShow
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserShow GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, ShowId, UserId, LastWatchedSeason, LastWatchedEpisode, LastReleasedSeason, LastReleasedEpisode, DateUpdated, Note, PlatformId, CategoryId
                                        FROM UserShow
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {
                        UserShow userShow = new UserShow()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            ShowId = DbUtils.GetInt(reader, "ShowId"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            LastWatchedSeason = DbUtils.GetInt(reader, "LastWatchedSeason"),
                            LastWatchedEpisode = DbUtils.GetInt(reader, "LastWatchedEpisode"),
                            LastReleasedEpisode = DbUtils.GetInt(reader, "LastReleasedEpisode"),
                            LastReleasedSeason = DbUtils.GetInt(reader, "LastReleasedSeason"),
                            DateUpdated = DbUtils.GetDateTime(reader, "DateUpdated"),
                            Note = DbUtils.GetString(reader, "Note"),
                            PlatformId = DbUtils.GetInt(reader, "PlatformId"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId")
                        };

                        reader.Close();
                        return userShow;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void Edit(UserShow userShow)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserShow
                                        SET
                                            ShowId = @showId,
                                            UserId = @userId,
                                            LastWatchedSeason = @lastWatchedSeason,
                                            LastWatchedEpisode = @lastWatchedEpisode,
                                            LastReleasedSeason = @lastReleasedSeason,
                                            LastReleasedEpisode = @lastReleasedEpisode,
                                            DateUpdated = @dateUpdated,
                                            Note = @note,
                                            PlatformId = @platformId,
                                            CategoryId = @categoryId
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@showId", userShow.ShowId);
                    DbUtils.AddParameter(cmd, "@userId", userShow.UserId);
                    DbUtils.AddParameter(cmd, "@lastWatchedSeason", userShow.LastWatchedSeason);
                    DbUtils.AddParameter(cmd, "@lastWatchedEpisode", userShow.LastWatchedEpisode);
                    DbUtils.AddParameter(cmd, "@lastReleasedSeason", userShow.LastReleasedSeason);
                    DbUtils.AddParameter(cmd, "@lastReleasedEpisode", userShow.LastReleasedEpisode);
                    DbUtils.AddParameter(cmd, "@dateUpdated", userShow.DateUpdated);
                    DbUtils.AddParameter(cmd, "@note", userShow.Note);
                    DbUtils.AddParameter(cmd, "@platformId", userShow.PlatformId);
                    DbUtils.AddParameter(cmd, "@categoryId", userShow.CategoryId);
                    DbUtils.AddParameter(cmd, "@id", userShow.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}