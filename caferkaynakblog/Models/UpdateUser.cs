using System.ComponentModel.DataAnnotations;

namespace caferkaynakblog.Models
{
    public class UpdateUser
    {
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string LastPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordT { get; set; }
    }
}
