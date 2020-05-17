using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class PresenterRepository : ComplexItemRepository<Presenter, PresenterAlias>, IPresenterRepository
    {
        public PresenterRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}