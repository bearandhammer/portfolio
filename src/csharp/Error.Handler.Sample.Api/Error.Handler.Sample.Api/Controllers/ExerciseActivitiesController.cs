using Error.Handler.Sample.Api.Requests;
using Error.Handler.Sample.Api.Responses;
using Error.Handler.Sample.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

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
            Guid oneOffUniqueId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            // TODO: Implement service logic to obtain an ExerciseActivityResponse (awaitable). Also, implement OneOf...
            _logger.LogInformation("Adding exercise activity: {@AddRequest}", addRequest);
            return Ok(await Task.FromResult(
                new ExerciseActivityResponse(
                    oneOffUniqueId,
                    addRequest.Description,
                    addRequest.AverageHeartRate,
                    addRequest.MaximumHeartRate)));
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
                        122),
                }));
        }
    }
}
