using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class LanguageRepository : RepositorySimpleItem<Language>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}