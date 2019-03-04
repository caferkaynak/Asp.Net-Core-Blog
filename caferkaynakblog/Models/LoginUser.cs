using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
