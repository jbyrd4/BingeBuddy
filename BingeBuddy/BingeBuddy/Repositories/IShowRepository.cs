using BingeBuddy.Models;
using System.Collections.Generic;

namespace BingeBuddy.Repositories
{
    public interface IShowRepository
    {
        void Add(Show show);
        void Delete(int id);
        List<Show> GetAllShows();
        List<Show> GetShowsByApproved(bool approved);
        Show GetShowById(int id);
        void Update(Show show);
    }
}