using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models.Contracts
{
    public interface IComplexItemRepository<T1, T2>
    {
        Task<IEnumerable<T1>> GetAllItemsAsync();
        Task CreateItemAsync(T1 item, T2 itemAlias);
        Task<T1> GetItemAsync(int id);
        Task UpdateItemAsync(T1 item);
        Task DeleteItemAsync(int id);
        Task CreateItemAliasAsync(T2 itemAlias);
        Task<T2> GetItemAliasAsync(int id);
        Task<IEnumerable<T2>> GetItemAliasesAsync(int id);
        Task UpdateItemAliasAsync(T2 itemAlias);
        Task DeleteItemAliasAsync(int id);
        Task<bool> ItemNameIsUniqueAsync(string name);
        Task<bool> ItemAliasIsUniqueAsync(string alias);
    }
}