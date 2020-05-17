using System.Threading.Tasks;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models
{
    public interface IDirectorRepository : ISimpleItemRepository<Director>
    {
        Task<bool> ItemAliasIsUniqueAsync(string alias);
    }
}