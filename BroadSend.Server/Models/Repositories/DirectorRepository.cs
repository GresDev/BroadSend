using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Models.Repositories
{
    public class DirectorRepository : SimpleItemRepository<Director>, IDirectorRepository
    {
        public DirectorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<bool> ItemAliasIsUniqueAsync(string alias)
        {
            var result = await AppDbContext.Set<Director>().AsNoTracking().FirstOrDefaultAsync(n => n.Alias == alias);

            return result == null;
        }
    }
}