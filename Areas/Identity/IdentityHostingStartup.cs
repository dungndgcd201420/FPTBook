using Microsoft.AspNetCore.Hosting;
[assembly: HostingStartup(typeof(FPTBook.Areas.Identity.IdentityHostingStartup))]
namespace FPTBook.Areas.Identity
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

