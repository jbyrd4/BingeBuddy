using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Models
{
    public class Show
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Cover Image")]
        public string CoverImage { get; set; }
        public bool Cancelled { get; set; }
        public bool Approved { get; set; }
    }
}