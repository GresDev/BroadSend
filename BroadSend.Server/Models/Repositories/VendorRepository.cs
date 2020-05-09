using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class VendorRepository : RepositorySimpleItem<Vendor>, IVendorRepository
    {
        public VendorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}