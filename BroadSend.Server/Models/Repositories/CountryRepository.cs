using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class CountryRepository : SimpleItemRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}