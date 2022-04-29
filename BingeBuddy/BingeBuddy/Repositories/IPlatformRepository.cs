using BingeBuddy.Models;
using System.Collections.Generic;

namespace BingeBuddy.Repositories
{
    public interface IPlatformRepository
    {
        List<Platform> GetAllPlatforms();
    }
}