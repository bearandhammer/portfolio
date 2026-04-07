using Error.Handler.Sample.Api.Model.Entity;
using Error.Handler.Sample.Api.Model.Validation;
using Error.Handler.Sample.Api.Repository.Interface;
using Error.Handler.Sample.Api.Request;
using Error.Handler.Sample.Api.Response;
using Error.Handler.Sample.Api.Service.Interface;
using MapsterMapper;
using OneOf;

namespace Error.Handler.Sample.Api.Service.Implementation
{
    /// <summary>
    /// Represents an implementation of the <see cref="IExerciseActivityService"/> interface that provides business logic for exercise activities.
    /// </summary>
    /// <param name="exerciseActivityRepo">The <see cref="IExerciseActivityRepo"/> instance in scope for the operation.</param>
    /// <param name="mapper">The <see cref="IMapper"/> instance in scope for the operation.</param>
    /// <seealso cref="IExerciseActivityService"/>
    public sealed class ExerciseActivityService(IExerciseActivityRepo exerciseActivityRepo, IMapper mapper) : IExerciseActivityService
    {
        /// <summary>
        /// Represents the <see cref="IExerciseActivityRepo"/> instance in scope for the operation, for interacting with the backing store.
        /// </summary>
        private readonly IExerciseActivityRepo _exerciseActivityRepo = exerciseActivityRepo;

        /// <summary>
        /// Represents the <see cref="IMapper"/> instance in scope for the operation, for mapping between entities and requests/responses.
        /// </summary>
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc />
        public OneOf<ExerciseActivityResponse, ConflictingExerciseActivity> AddExerciseActivity(AddExerciseActivityRequest addExerciseActivityRequest)
        {
            ExerciseActivityEntity? conflictingExerciseActivityEntity = _exerciseActivityRepo.GetExerciseActivityFromStoreDateRange(
                addExerciseActivityRequest.StartDateTime,
                addExerciseActivityRequest.EndDateTime);

            if (conflictingExerciseActivityEntity is not null)
            {
                return new ConflictingExerciseActivity();
            }

            // If no conflicting exercise activity is found, add the new exercise activity to the store
            ExerciseActivityEntity mappedExerciseActivityEntity = _mapper.Map<ExerciseActivityEntity>(addExerciseActivityRequest);
            _ = _exerciseActivityRepo.AddExerciseActivityToStore(mappedExerciseActivityEntity);

            return _mapper.Map<ExerciseActivityResponse>(mappedExerciseActivityEntity);
        }
    }
}
