using System.Reflection;
using Ardalis.GuardClauses;
using Error.Handler.Sample.Api.Repository.Configuration;
using Error.Handler.Sample.Api.Repository.Context;
using Error.Handler.Sample.Api.Repository.Implementation;
using Error.Handler.Sample.Api.Repository.Interface;
using Error.Handler.Sample.Api.Service.Implementation;
using Error.Handler.Sample.Api.Service.Interface;
using Mapster;
using MapsterMapper;

namespace Error.Handler.Sample.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddLiteDb(services, configuration);
            AddRepositories(services);
            AddServices(services);
            AddMappingConfigs(services);
        }

        private static void AddLiteDb(IServiceCollection services, IConfiguration configuration)
        {
            LiteDbOptions options = Guard.Against.Null(configuration.GetSection(nameof(LiteDbOptions)).Get<LiteDbOptions>());
            services.AddSingleton(options);
            services.AddSingleton<LiteDbContext>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExerciseActivityRepo, ExerciseActivityRepo>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IExerciseActivityService, ExerciseActivityService>();
        }

        private static void AddMappingConfigs(IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
