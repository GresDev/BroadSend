using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models.Repositories
{
    public class VendorRepository : SimpleItemRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}