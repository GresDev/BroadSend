using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models.Contracts
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllItemsAsync();

        Task CreateItemAsync(Title item, string itemAlias);

        Task<Title> GetItemAsync(int id);

        Task UpdateItemAsync(Title item);

        Task DeleteItemAsync(int id);

        Task CreateItemAliasAsync(TitleAlias itemAlias);

        Task<TitleAlias> GetItemAliasAsync(int id);

        Task<IEnumerable<TitleAlias>> GetItemAliasesAsync(int id);

        Task UpdateItemAliasAsync(TitleAlias itemAlias);

        Task DeleteItemAliasAsync(int id);

        Task<bool> ItemNameIsUniqueAsync(string name);

        Task<bool> ItemAliasIsUniqueAsync(string alias);


        //IEnumerable<Title> AllTitles { get; }

        //bool TitleNameIsUnique(string fullName);
        //bool TitleAliasIsUnique(string alias);


        //void CreateTitle(Title title, string titleAlias);

        //Title GetTitle(int titleId);

        //void UpdateTitle(Title title);

        //void DeleteTitle(int titleId);


        //void CreateAlias(TitleAlias titleAlias);

        //TitleAlias GetAlias(int titleAiliasId);

        //List<TitleAlias> GetAliases(int titleId);

        //void UpdateAlias(TitleAlias titleAlias);

        //void DeleteAlias(int aliasId);
    }
}