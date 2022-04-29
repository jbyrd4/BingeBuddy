using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BingeBuddy.Models;
using BingeBuddy.Utils;

namespace BingeBuddy.Repositories
{
    public class UserShowRepository : BaseRepository
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
                            DateUpdated = new System.DateTime(),
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
    }
}
