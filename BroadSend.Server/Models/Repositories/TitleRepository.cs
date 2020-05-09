using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BroadSend.Server.Models
{
    public class TitleRepository : ITitleRepository
    {
        private readonly AppDbContext _appDbContext;

        public IEnumerable<Title> AllTitles
        {
            get { return _appDbContext.Titles.AsNoTracking().OrderBy(x => x.Name); }
        }


        public TitleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool TitleNameIsUnique(string name)
        {
            if (_appDbContext.Titles.AsNoTracking().FirstOrDefault(x => x.Name == name) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TitleAliasIsUnique(string alias)
        {
            if (_appDbContext.TitleAliases.AsNoTracking().FirstOrDefault(x => x.Alias == alias) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void CreateTitle(Title newTitle, string alias)
        {
            _appDbContext.Titles.Add(newTitle);
            _appDbContext.SaveChanges();

            Title title = _appDbContext.Titles.Single(x => x.Name == newTitle.Name);
            TitleAlias titleAlias = new TitleAlias
            {
                Alias = alias,
               TitleId = title.Id
            };

            _appDbContext.TitleAliases.Add(titleAlias);
            _appDbContext.SaveChanges();

        }

        public Title GetTitle(int id)
        {
            return _appDbContext.Titles.AsNoTracking().Single(x => x.Id == id);
        }

        public void UpdateTitle(Title title)
        {
            _appDbContext.Titles.Update(title);
            _appDbContext.SaveChanges();
        }

        public void DeleteTitle(int id)
        {

            Title title = _appDbContext.Titles.Single(x => x.Id == id);
            _appDbContext.Titles.Remove(title);
            _appDbContext.SaveChanges();
        }



        public void CreateAlias(TitleAlias titleAlias)
        {
            _appDbContext.TitleAliases.Add(titleAlias);
            _appDbContext.SaveChanges();
        }

        public List<TitleAlias> GetAliases(int titleId)
        {
            var aliasList = from alias in _appDbContext.TitleAliases where (alias.TitleId == titleId) select alias;
            return aliasList.ToList();
        }

        public TitleAlias GetAlias(int id)
        {
            return _appDbContext.TitleAliases.AsNoTracking().Single(x => x.Id == id);
        }

        public void UpdateAlias(TitleAlias titleAlias)
        {
            _appDbContext.TitleAliases.Update(titleAlias);
            _appDbContext.SaveChanges();
        }

        public void DeleteAlias(int id)
        {
            TitleAlias titleAlias = _appDbContext.TitleAliases.Single(x => x.Id == id);
            _appDbContext.TitleAliases.Remove(titleAlias);
            _appDbContext.SaveChanges();
        }
    }
}