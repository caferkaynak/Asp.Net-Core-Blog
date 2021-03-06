﻿using System.Linq;

namespace caferkaynakblog.Models
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Tag> Tags { get; }
        IQueryable<Entry> Entries { get; }
        IQueryable<EntryTag> EntryTags { get; }
        void CreateUser(User user);
        void UpdateUser(User user);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        void CreateEntry(Entry entry);
        void UpdateEntry(Entry entry);
        void DeleteEntry(Entry entry);
        void CreateTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(Tag tag);
    }
}
