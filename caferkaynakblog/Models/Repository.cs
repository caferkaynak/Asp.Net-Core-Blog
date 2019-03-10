using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace caferkaynakblog.Models
{
    public class Repository : IRepository
    {

        public ApplicationDbContext context;
        public Repository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IQueryable<User> Users => context.Users;
        public IQueryable<Entry> Entries => context.Entries;
        public IQueryable<Tag> Tags => context.Tags;
        public IQueryable<Category> Categories => context.Categories;
        public IQueryable<EntryTag> EntryTags => context.EntryTags;

        public void CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
        public void CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
        public void CreateEntry(Entry entry)
        {
            context.Entries.Add(entry);
            context.SaveChanges();
        }
        public void UpdateEntry(Entry entry)
        {
            context.Entries.Update(entry);
            context.SaveChanges();
        }
        public void DeleteEntry(Entry entry)
        {
            context.Entries.Remove(entry);
            context.SaveChanges();
        }
        public void CreateTag(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
        }
        public void UpdateTag(Tag tag)
        {
            context.Tags.Update(tag);
            context.SaveChanges();
        }
        public void DeleteTag(Tag tag)
        {
            context.Tags.Remove(tag);
            context.SaveChanges();
        }
    }
}
