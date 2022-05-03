using BingeBuddy.Models;
using System.Collections.Generic;

namespace BingeBuddy.Repositories
{
    public interface IUserShowRepository
    {
        List<UserShow> GetUserShowsByUserProfileId(int id);
        List<UserShow> GetUserShowsByCategoryId(int userId, int categoryId);
        void Add(UserShow userShow);
        void Delete(int id);
        UserShow GetById(int id);
        void Edit(UserShow userShow);
    }
}