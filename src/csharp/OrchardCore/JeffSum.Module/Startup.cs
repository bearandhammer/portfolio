using Jeff.Services.Interfaces;
using Jeff.Services.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace JeffSum.Module
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJeffIpsumService, JeffIpsumService>();
        }

        public void Configure(IEndpointRouteBuilder endpoints)
        {

        }
    }
}
