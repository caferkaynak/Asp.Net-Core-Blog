using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace caferkaynakblog.Models
{
    public class Repository : IRepository
    {
        private ApplicationDbContext context;
        public ApplicationDbContext repository;
        public Repository(ApplicationDbContext repo,ApplicationDbContext _context)
        {
            repository = repo;
            context = _context;
        }
        public IQueryable<User> Users => repository.Users;
        public IQueryable<Entry> Entries => repository.Entries;
        public IQueryable<Tag> Tags => repository.Tags;
        public IQueryable<Category> Categories => repository.Categories;
        public IQueryable<EntryTag> EntryTags => repository.EntryTags;

        public void CreateUser(User user)
        {
            repository.Users.Add(user);
            repository.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            repository.Users.Update(user);
            repository.SaveChanges();
        }
        public void CreateCategory(Category category)
        {
            repository.Categories.Add(category);
            repository.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            repository.Categories.Update(category);
            repository.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            repository.Categories.Remove(category);
            repository.SaveChanges();
        }
        public void CreateEntry(Entry entry)
        {
            repository.Entries.Add(entry);
            repository.SaveChanges();
        }
        public void UpdateEntry(Entry entry)
        {
            repository.Entries.Update(entry);
            repository.SaveChanges();
        }
        public void DeleteEntry(Entry entry)
        {
            repository.Entries.Remove(entry);
            repository.SaveChanges();
        }
        public void CreateTag(Tag tag)
        {
            repository.Tags.Add(tag);
            repository.SaveChanges();
        }
        public void UpdateTag(Tag tag)
        {
            repository.Tags.Update(tag);
            repository.SaveChanges();
        }
        public void DeleteTag(Tag tag)
        {
            repository.Tags.Remove(tag);
            repository.SaveChanges();
        }
    }
}
