using BingeBuddy.Models;
using System.Collections.Generic;

namespace BingeBuddy.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}