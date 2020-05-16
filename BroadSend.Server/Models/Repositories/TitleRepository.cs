using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Models
{
    public class TitleRepository : ITitleRepository
    {
        private readonly AppDbContext _appDbContext;

        public TitleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Title>> GetAllItemsAsync()
        {
            return await _appDbContext.Set<Title>().AsNoTracking().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task CreateItemAsync(Title item, string itemAlias)
        {
            _appDbContext.Add(item);
            await _appDbContext.SaveChangesAsync();

            Title title = await _appDbContext.Set<Title>().SingleAsync(t => t.Name == item.Name);
            TitleAlias titleAlias = new TitleAlias
            {
                Alias = itemAlias,
                TitleId = title.Id
            };

            _appDbContext.Set<TitleAlias>().Add(titleAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Title> GetItemAsync(int id)
        {
            return await _appDbContext.Set<Title>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateItemAsync(Title item)
        {
            _appDbContext.Update(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            Title item = await GetItemAsync(id);
            _appDbContext.Remove(item);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateItemAliasAsync(TitleAlias itemAlias)
        {
            _appDbContext.Set<TitleAlias>().Add(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<TitleAlias> GetItemAliasAsync(int id)
        {
            return await _appDbContext.Set<TitleAlias>().AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<TitleAlias>> GetItemAliasesAsync(int id)
        {
            return await _appDbContext.Set<TitleAlias>().AsNoTracking().Where(a => a.TitleId == id)
                .ToListAsync();
        }

        public async Task UpdateItemAliasAsync(TitleAlias itemAlias)
        {
            _appDbContext.Set<TitleAlias>().Update(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAliasAsync(int id)
        {
            var itemAlias = await _appDbContext.Set<TitleAlias>().AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            _appDbContext.Remove(itemAlias);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ItemNameIsUniqueAsync(string name)
        {
            var result = await _appDbContext.Set<Title>().AsNoTracking().FirstOrDefaultAsync(t => t.Name == name);
            return result == null ? true : false;
        }

        public async Task<bool> ItemAliasIsUniqueAsync(string alias)
        {
            var result = await _appDbContext.Set<TitleAlias>().AsNoTracking()
                .FirstOrDefaultAsync(a => a.Alias == alias);
            return result == null ? true : false;
        }



        //public IEnumerable<Title> AllTitles
        //{
        //    get { return _appDbContext.Titles.AsNoTracking().OrderBy(x => x.Name); }
        //}

        //public void CreateTitle(Title newTitle, string alias)
        //{
        //    _appDbContext.Titles.Add(newTitle);
        //    _appDbContext.SaveChanges();

        //    Title title = _appDbContext.Titles.Single(x => x.Name == newTitle.Name);
        //    TitleAlias titleAlias = new TitleAlias
        //    {
        //        Alias = alias,
        //       TitleId = title.Id
        //    };

        //    _appDbContext.TitleAliases.Add(titleAlias);
        //    _appDbContext.SaveChanges();

        //}

        //public Title GetTitle(int id)
        //{
        //    return _appDbContext.Titles.AsNoTracking().Single(x => x.Id == id);
        //}

        //public void UpdateTitle(Title title)
        //{
        //    _appDbContext.Titles.Update(title);
        //    _appDbContext.SaveChanges();
        //}

        //public void DeleteTitle(int id)
        //{

        //    Title title = _appDbContext.Titles.Single(x => x.Id == id);
        //    _appDbContext.Titles.Remove(title);
        //    _appDbContext.SaveChanges();
        //}

        //public void CreateAlias(TitleAlias titleAlias)
        //{
        //    _appDbContext.TitleAliases.Add(titleAlias);
        //    _appDbContext.SaveChanges();
        //}

        //public TitleAlias GetAlias(int id)
        //{
        //    return _appDbContext.TitleAliases.AsNoTracking().Single(x => x.Id == id);
        //}

        //public List<TitleAlias> GetAliases(int titleId)
        //{
        //    var aliasList = from alias in _appDbContext.TitleAliases where (alias.TitleId == titleId) select alias;
        //    return aliasList.ToList();
        //}

        //public void UpdateAlias(TitleAlias titleAlias)
        //{
        //    _appDbContext.TitleAliases.Update(titleAlias);
        //    _appDbContext.SaveChanges();
        //}

        //public void DeleteAlias(int id)
        //{
        //    TitleAlias titleAlias = _appDbContext.TitleAliases.Single(x => x.Id == id);
        //    _appDbContext.TitleAliases.Remove(titleAlias);
        //    _appDbContext.SaveChanges();
        //}

        //public bool TitleNameIsUnique(string name)
        //{
        //    if (_appDbContext.Titles.AsNoTracking().FirstOrDefault(x => x.Name == name) == null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool TitleAliasIsUnique(string alias)
        //{
        //    if (_appDbContext.TitleAliases.AsNoTracking().FirstOrDefault(x => x.Alias == alias) == null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


    }
}