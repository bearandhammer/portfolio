using DinoIpsum.Services;
using DinoIpsum.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DinoIpsum
{
    public class Startup
    {        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDinoIpsumService, DinoIpsumService>();
        }
    }
}
