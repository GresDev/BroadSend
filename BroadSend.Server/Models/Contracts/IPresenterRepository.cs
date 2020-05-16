using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models.Contracts
{
    public interface IPresenterRepository
    {
        Task<IEnumerable<Presenter>> GetAllItemsAsync();

        Task CreateItemAsync(Presenter item, string itemAlias);

        Task<Presenter> GetItemAsync(int id);

        Task UpdateItemAsync(Presenter item);

        Task DeleteItemAsync(int id);

        Task CreateItemAliasAsync(PresenterAlias itemAlias);

        Task<PresenterAlias> GetItemAliasAsync(int id);

        Task<IEnumerable<PresenterAlias>> GetItemAliasesAsync(int id);

        Task UpdateItemAliasAsync(PresenterAlias itemAlias);

        Task DeleteItemAliasAsync(int id);

        Task<bool> ItemNameIsUniqueAsync(string name);

        Task<bool> ItemAliasIsUniqueAsync(string alias);
    }
}