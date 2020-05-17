using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models.Contracts
{
    public interface ISimpleItemRepository<T>
    {
        Task<IEnumerable<T>> GetAllItemsAsync();
        Task CreateItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(int id);
        Task<bool> ItemNameIsUniqueAsync(string name);
    }
}