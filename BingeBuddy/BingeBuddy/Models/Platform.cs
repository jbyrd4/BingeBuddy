using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Models
{
    public class Platform
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
