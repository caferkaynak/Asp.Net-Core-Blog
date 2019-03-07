using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
