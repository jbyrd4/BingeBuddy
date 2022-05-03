using BingeBuddy.Models;
using System.Collections.Generic;

namespace BingeBuddy.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        List<UserProfile> GetAll();
        void Delete(int id);
        void Edit(UserProfile userProfile);
    }
}