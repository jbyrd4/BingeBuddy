using BingeBuddy.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using BingeBuddy.Utils;

namespace BingeBuddy.Repositories
{
    public class PlatformRepository : BaseRepository, IPlatformRepository
    {
        public PlatformRepository(IConfiguration config) : base(config) { }

        public List<Platform> GetAllPlatforms()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, [Name] FROM Platform";

                    var reader = cmd.ExecuteReader();
                    var platforms = new List<Platform>();

                    while (reader.Read())
                    {
                        platforms.Add(new Platform()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();

                    return platforms;
                }
            }
        }
    }
}
