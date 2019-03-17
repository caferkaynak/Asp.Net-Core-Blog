using System.Collections.Generic;

namespace caferkaynakblog.Models
{
    public class EntryViewModel
    {
        public List<Category> categories{ get; set; }
        public List<Tag> tags { get; set; }
        public Entry entry { get; set; }
        public List<Entry> entries { get; set; }
        public List<UsersIdName> usersIdNames { get; set; }
    }
}
