using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BroadSend.Server.Areas.Identity.IdentityHostingStartup))]
namespace BroadSend.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}