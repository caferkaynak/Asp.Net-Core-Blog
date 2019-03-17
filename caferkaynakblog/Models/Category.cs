using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace caferkaynakblog.Models
{
    public class Category
    {
        [Key,UIHint("Id")]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
