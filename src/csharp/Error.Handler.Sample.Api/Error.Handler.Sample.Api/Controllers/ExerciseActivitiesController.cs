using Error.Handler.Sample.Api.Requests;
using Error.Handler.Sample.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Error.Handler.Sample.Api.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseActivtyController"/> class.
    /// </summary>
    [ApiController]
    [Route("exercise-activities")]
    public class ExerciseActivitiesController(ILogger<ExerciseActivitiesController> logger) : ControllerBase
    {
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
                    new ExerciseActivityResponse(
                        oneOffUniqueId,
                        "A really slow walk!!!",
                        98,
                        122),
                }));
        }
    }
}
