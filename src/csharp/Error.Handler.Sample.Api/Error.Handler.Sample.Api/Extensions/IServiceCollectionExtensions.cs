using Error.Handler.Sample.Api.Repository.Configuration;
using Error.Handler.Sample.Api.Repository.Implementation;
using Error.Handler.Sample.Api.Repository.Interface;
using Error.Handler.Sample.Api.Service.Implementation;
using Error.Handler.Sample.Api.Service.Interface;

namespace Error.Handler.Sample.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddLiteDb(services, configuration);
            AddRepositories(services);
            AddServices(services);
        }

        private static void AddLiteDb(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LiteDbOptions>(configuration.GetSection(nameof(LiteDbOptions)));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExerciseActivityRepo, ExerciseActivityRepo>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IExerciseActivityService, ExerciseActivityService>();
        }
    }
}
