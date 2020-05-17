using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class TitleRepository : ComplexItemRepository<Title, TitleAlias>, ITitleRepository
    {
        public TitleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}