using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BingeBuddy.Auth.Models
{
    public class Registration
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}