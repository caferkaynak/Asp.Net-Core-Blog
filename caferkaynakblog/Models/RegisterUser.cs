using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace caferkaynakblog.Models
{
    public class RegisterUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordT { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
