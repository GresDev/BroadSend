using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Models.Repositories
{
    public class PresenterRepository : IPresenterRepository
    {
        private readonly AppDbContext _appDbContext;

        public PresenterRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Presenter>> GetAllItemsAsync()
        {
            return await _appDbContext.Set<Presenter>().AsNoTracking().OrderBy(p => p.Name).ToListAsync();
        }

        public async Task CreateItemAsync(Presenter item, string itemAlias)
        {
            _appDbContext.Add(item);
            await _appDbContext.SaveChangesAsync();

            Presenter presenter = await _appDbContext.Set<Presenter>().SingleAsync(p => p.Name == item.Name);
            PresenterAlias presenterAlias = new PresenterAlias
            {
                Alias = itemAlias,
                PresenterId = presenter.Id
            };

            _appDbContext.Set<PresenterAlias>().Add(presenterAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Presenter> GetItemAsync(int id)
        {
            return await _appDbContext.Set<Presenter>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateItemAsync(Presenter item)
        {
            _appDbContext.Update(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            Presenter item = await GetItemAsync(id);
            _appDbContext.Remove(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateItemAliasAsync(PresenterAlias itemAlias)
        {
            _appDbContext.Set<PresenterAlias>().Add(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PresenterAlias> GetItemAliasAsync(int id)
        {
            return await _appDbContext.Set<PresenterAlias>().AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<PresenterAlias>> GetItemAliasesAsync(int id)
        {
            return await _appDbContext.Set<PresenterAlias>().AsNoTracking().Where(a => a.PresenterId == id)
                .ToListAsync();
        }

        public async Task UpdateItemAliasAsync(PresenterAlias itemAlias)
        {
            _appDbContext.Set<PresenterAlias>().Update(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAliasAsync(int id)
        {
            var itemAlias = await _appDbContext.Set<PresenterAlias>().AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            _appDbContext.Remove(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ItemNameIsUniqueAsync(string name)
        {
            var result = await _appDbContext.Set<Presenter>().AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            return result == null ? true : false;
        }

        public async Task<bool> ItemAliasIsUniqueAsync(string alias)
        {
            var result = await _appDbContext.Set<PresenterAlias>().AsNoTracking()
                .FirstOrDefaultAsync(a => a.Alias == alias);
            return result == null ? true : false;
        }
    }
}