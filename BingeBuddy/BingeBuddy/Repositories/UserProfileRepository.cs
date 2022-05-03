using BingeBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using BingeBuddy.Utils;

namespace BingeBuddy.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {

        private readonly IConfiguration _config;

        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Email, FirebaseUserId, Name, UserName, Admin
                                    FROM UserProfile
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Email, FirebaseUserId, Name, UserName, Admin
                                    FROM UserProfile
                                    WHERE FirebaseUserId = @FirebaseuserId";

                    cmd.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO
                                        UserProfile (Email, FirebaseUserId, [Name], UserName, Admin) 
                                        OUTPUT INSERTED.ID
                                        VALUES(@email, @firebaseUserId, @Name, @UserName, @Admin)";

                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@Name", userProfile.Name);
                    cmd.Parameters.AddWithValue("@UserName", userProfile.UserName);
                    cmd.Parameters.AddWithValue("@Admin", 0);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, FirebaseUserId, UserName, [Name], Email, [Admin]
                                        FROM UserProfile";

                    var reader = cmd.ExecuteReader();
                    var users = new List<UserProfile>();

                    while (reader.Read())
                        users.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            UserName = DbUtils.GetString(reader, "UserName"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                        });
                    reader.Close();

                    return users;
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
                    cmd.CommandText = @"DELETE FROM UserProfile
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Edit(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile
                                        SET
                                            FirebaseUserId = @fireBaseUserId,
                                            UserName = @userName,
                                            Name = @name,
                                            Email = @email,
                                            Admin = @admin
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", userProfile.Id);
                    DbUtils.AddParameter(cmd, "@fireBaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@userName", userProfile.UserName);
                    DbUtils.AddParameter(cmd, "@name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@admin", userProfile.Admin);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}