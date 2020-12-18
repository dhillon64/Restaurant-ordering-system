using Microsoft.EntityFrameworkCore;
using Restaurant_ordering_system.Contracts;
using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        public readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(MenuItem entity)
        {
            await _db.MenuItems.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(MenuItem entity)
        {
            _db.MenuItems.Remove(entity);
            return await Save();

        }

        public async Task<bool> Exists(int Id)
        {
            return await _db.MenuItems.AnyAsync(q => q.Id == Id);
        }

        public async Task<ICollection<MenuItem>> FindAll()
        {
            return await _db.MenuItems.Include(q => q.Category).ToListAsync();
        }

        public async Task<MenuItem> FindById(int Id)
        {
            return await _db.MenuItems.Include(q => q.Category).FirstOrDefaultAsync(q => q.Id == Id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> Update(MenuItem entity)
        {
            _db.MenuItems.Update(entity);
            return await Save();
        }
    }
}
