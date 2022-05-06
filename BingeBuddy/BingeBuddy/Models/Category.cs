using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
