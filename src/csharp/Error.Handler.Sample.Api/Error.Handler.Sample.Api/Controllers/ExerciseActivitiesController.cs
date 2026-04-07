using Error.Handler.Sample.Api.Extensions;
using Error.Handler.Sample.Api.Model.Validation;
using Error.Handler.Sample.Api.Request;
using Error.Handler.Sample.Api.Response;
using Error.Handler.Sample.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;

namespace Error.Handler.Sample.Api.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseActivitiesController"/> class.
    /// </summary>
    /// <param name="exerciseActivityService">The <see cref="IExerciseActivityService"/> instance in scope for the operation.</param>
    /// <param name="logger">The <see cref="ILogger{ExerciseActivitiesController}"/> instance in scope for the operation.</param>
    /// <seealso cref="IExerciseActivityService"/>
    /// <seealso cref="ILogger{ExerciseActivitiesController}"/>
    [ApiController]
    [Route("exercise-activities")]
    public class ExerciseActivitiesController(
        IExerciseActivityService exerciseActivityService,
        ILogger<ExerciseActivitiesController> logger) : ControllerBase
    {
        /// <summary>
        /// Represents the service in scope for the operation.
        /// </summary>
        private readonly IExerciseActivityService _exerciseActivityService = exerciseActivityService;

        /// <summary>
        /// Represents the logger in scope for the operation.
        /// </summary>
        private readonly ILogger<ExerciseActivitiesController> _logger = logger;

        /// <summary>
        /// Adds a new exercise activity to the system, adhering to validation rules.
        /// </summary>
        /// <param name="request">The <see cref="AddExerciseActivityRequest"/> to add to the system.</param>
        /// <returns>An <see cref="ActionResult{ExerciseActivityResponse}"/> representing the added exercise activity.</returns>
        [HttpPost]
        public async Task<ActionResult<ExerciseActivityResponse>> AddExerciseActivity([FromBody] AddExerciseActivityRequest addRequest)
        {
            OneOf<ExerciseActivityResponse, ConflictingExerciseActivity> result = _exerciseActivityService.AddExerciseActivity(addRequest);

            return result.Match<ActionResult<ExerciseActivityResponse>>(
                exerciseActivityResponse => Ok(exerciseActivityResponse),
                conflictingExerciseActivity => Conflict(conflictingExerciseActivity.ToProblemDetails()));
        }

        /// <summary>
        /// Gets all exercise activities.
        /// </summary>
        /// <returns>An <see cref="ActionResult{IEnumerable{ExerciseActivityResponse}}"/> representing all logged exercise activities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseActivityResponse>>> GetExerciseActivities()
        {
            Guid oneOffUniqueId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            // TODO: Implement service logic to obtain an IEnumerable<ExerciseActivityResponse> (awaitable). Also, implement OneOf...
            _logger.LogInformation("Getting all exercise activities");
            return Ok(await Task.FromResult(
                new List<ExerciseActivityResponse>
                {
                    new(
                        oneOffUniqueId,
                        "A really slow walk!!!",
                        98,
                        122,
                        DateTimeOffset.Parse("2026-04-02T08:00:00+00:00"),
                        DateTimeOffset.Parse("2026-04-02T08:45:00+00:00")),
                }));
        }
    }
}
