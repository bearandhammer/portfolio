using HipsterIpsum.Services;
using HipsterIpsum.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HipsterIpsum.Module
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHipsterIpsumService, HipsterIpsumService>();
        }
    }
}
