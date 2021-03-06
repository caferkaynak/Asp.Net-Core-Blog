﻿using System;
using System.ComponentModel.DataAnnotations;

namespace caferkaynakblog.Models
{
    public class Entry
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string UsersId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
