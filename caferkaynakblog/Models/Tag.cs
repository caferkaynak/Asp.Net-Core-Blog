using System.Collections.Generic;

namespace caferkaynakblog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
