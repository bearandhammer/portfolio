using Error.Handler.Sample.Api.Model.Entity;
using Error.Handler.Sample.Api.Request;
using Error.Handler.Sample.Api.Response;
using Mapster;

namespace Error.Handler.Sample.Api.Mappings
{
    public class GlobalMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Test
            config.NewConfig<AddExerciseActivityRequest, ExerciseActivityEntity>()
                .Map(dest => dest.Id, src => Guid.Empty)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.AverageHeartRate, src => src.AverageHeartRate)
                .Map(dest => dest.MaximumHeartRate, src => src.MaximumHeartRate)
                .Map(dest => dest.StartDateTime, src => src.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.EndDateTime);

            config.NewConfig<ExerciseActivityEntity, ExerciseActivityResponse>()
                .Map(dest => dest.UniqueId, src => src.Id)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.AverageHeartRate, src => src.AverageHeartRate)
                .Map(dest => dest.MaximumHeartRate, src => src.MaximumHeartRate)
                .Map(dest => dest.StartDateTime, src => src.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.EndDateTime);
        }
    }
}
