using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class ComposerRepository : SimpleItemRepository<Composer>, IComposerRepository
    {
        public ComposerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}