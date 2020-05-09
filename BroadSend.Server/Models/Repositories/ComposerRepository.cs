using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class ComposerRepository : RepositorySimpleItem<Composer>, IComposerRepository
    {
        public ComposerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}