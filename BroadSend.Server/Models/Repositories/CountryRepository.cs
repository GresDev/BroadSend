using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class CountryRepository : RepositorySimpleItem<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}