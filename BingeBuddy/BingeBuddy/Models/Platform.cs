using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Models
{
    public class Platform
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Platform")]
        public string Name { get; set; }
    }
}
