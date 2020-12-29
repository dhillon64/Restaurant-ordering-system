using Microsoft.EntityFrameworkCore;
using Restaurant_ordering_system.Contracts;
using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Category entity)
        {
           await _db.Categories.AddAsync(entity);
            return await Save(); 
        }

        public async Task<bool> Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int Id)
        {
            return await _db.Categories.AnyAsync(q=>q.Id==Id);
        }

        public async Task<bool> Exists(string Name)
        {
            return await _db.Categories.AnyAsync(q => q.Name == Name);
        }

        public async Task<ICollection<Category>> FindAll()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> FindById(int Id)
        {
            return await _db.Categories.FindAsync(Id);
        }

        public async Task<Category> FindByName(string Name)
        {
            return await _db.Categories.FirstOrDefaultAsync(q=>q.Name==Name);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> Update(Category entity)
        {
            _db.Categories.Update(entity);
            return await Save();
        }
    }
}
