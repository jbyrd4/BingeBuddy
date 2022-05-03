using System;
using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Models
{
    public class UserShow
    {
        public int Id { get; set; }
        public int LastWatchedSeason { get; set; }
        public int LastWatchedEpisode { get; set; }
        public int LastReleasedSeason { get; set; }
        public int LastReleasedEpisode { get; set; }
        [Display(Name = "Updated")]
        public DateTime DateUpdated { get; set; }
        public string Note { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int ShowId { get; set; }
        public Show Show { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserProfile UserProfile { get; set; }
        [Display(Name = "Last Released")]
        public string LastReleased
        {
            get
            {
                return "S"+LastReleasedSeason+"E"+LastReleasedEpisode;
            }
        }
        [Display(Name = "Last Watched")]
        public string LastWatched
        {
            get
            {
                return "S" + LastWatchedSeason + "E" + LastWatchedEpisode;
            }
        }
    }
}
