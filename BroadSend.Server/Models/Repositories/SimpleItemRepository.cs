using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Models.Repositories
{
    public class SimpleItemRepository<T> : ISimpleItemRepository<T>
        where T : class, ISimpleItem

    {
        internal readonly AppDbContext AppDbContext;

        public SimpleItemRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public async Task CreateItemAsync(T item)
        {
            AppDbContext.Add(item);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            T item = await GetItemAsync(id);
            AppDbContext.Remove(item);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await AppDbContext.Set<T>().AsNoTracking().OrderBy(n => n.Name).ToListAsync();
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await AppDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateItemAsync(T item)
        {
            AppDbContext.Set<T>().Update(item);
            await AppDbContext.SaveChangesAsync();
        }
    }
}