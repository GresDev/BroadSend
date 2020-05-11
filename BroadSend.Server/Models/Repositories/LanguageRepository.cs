using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class LanguageRepository : SimpleItemRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}