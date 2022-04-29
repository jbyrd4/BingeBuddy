using System.Collections.Generic;

namespace BingeBuddy.Models.ViewModels
{
    public class UserShowViewModel
    {
        public List<Category> CategoryOptions { get; set; }
        public List<Platform> PlatformOptions { get; set; }
        public List<Show> ShowOptions { get; set; }
        public UserShow userShow { get; set; }
    }
}