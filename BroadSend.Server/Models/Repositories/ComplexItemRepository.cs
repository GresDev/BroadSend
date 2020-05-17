using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Models.Repositories
{
    public class ComplexItemRepository<T1, T2> : IComplexItemRepository<T1, T2>
        where T1 : class, IComplexItem
        where T2 : class, IComplexItemAlias, new()

    {
        private readonly AppDbContext _appDbContext;

        public ComplexItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<T1>> GetAllItemsAsync()
        {
            return await _appDbContext.Set<T1>().AsNoTracking().OrderBy(p => p.Name).ToListAsync();
        }

        public async Task CreateItemAsync(T1 item, T2 itemAlias)
        {
            _appDbContext.Add(item);
            await _appDbContext.SaveChangesAsync();

            var complexItem = await _appDbContext.Set<T1>().SingleAsync(p => p.Name == item.Name);
            var complexItemAlias = new T2
            {
                Alias = itemAlias.Alias,
                ParentId = complexItem.Id
            };

            _appDbContext.Set<T2>().Add(complexItemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T1> GetItemAsync(int id)
        {
            return await _appDbContext.Set<T1>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateItemAsync(T1 item)
        {
            _appDbContext.Update(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemAsync(id);
            _appDbContext.Remove(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateItemAliasAsync(T2 itemAlias)
        {
            _appDbContext.Set<T2>().Add(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T2> GetItemAliasAsync(int id)
        {
            return await _appDbContext.Set<T2>().AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<T2>> GetItemAliasesAsync(int id)
        {
            return await _appDbContext.Set<T2>().AsNoTracking().Where(a => a.ParentId == id)
                .ToListAsync();
        }

        public async Task UpdateItemAliasAsync(T2 itemAlias)
        {
            _appDbContext.Set<T2>().Update(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAliasAsync(int id)
        {
            var itemAlias = await _appDbContext.Set<T2>().AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            _appDbContext.Remove(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }
    }
}