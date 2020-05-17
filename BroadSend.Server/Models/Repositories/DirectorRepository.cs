using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class DirectorRepository : SimpleItemRepository<Director>, IDirectorRepository
    {
        public DirectorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}