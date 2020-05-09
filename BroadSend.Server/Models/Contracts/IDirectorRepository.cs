using BroadSend.Server.Models.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models
{
    public interface IDirectorRepository : ISimpleItemRepository<Director>
    {
        Task<bool> ItemAliasIsUniqueAsync(string alias);

    }
}
