using Error.Handler.Sample.Api.Model.Validation;
using Error.Handler.Sample.Api.Request;
using Error.Handler.Sample.Api.Response;
using OneOf;

namespace Error.Handler.Sample.Api.Service.Interface
{
    public interface IExerciseActivityService
    {
        OneOf<ExerciseActivityResponse, ConflictingExerciseActivity> AddExerciseActivity(AddExerciseActivityRequest addExerciseActivityRequest);
    }
}
