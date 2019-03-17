using System.ComponentModel.DataAnnotations;

namespace caferkaynakblog.Models
{
    public class LoginUser
    {
        [Required]
        [UIHint("username")]
        public string Username { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
