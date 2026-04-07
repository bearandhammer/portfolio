using Error.Handler.Sample.Api.Base;

namespace Error.Handler.Sample.Api.Response
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseActivityResponse"/> class.
    /// </summary>
    /// <param name="UniqueId">The unique identifier of the exercise activity.</param>
    /// <param name="Description">The description of the exercise activity (directly from the user).</param>
    /// <param name="AverageHeartRate">The average heart rate recorded during the activity.</param>
    /// <param name="MaximumHeartRate">The maximum heart rate recorded during the activity.</param>
    /// <param name="StartDateTime">The instant when the exercise activity started.</param>
    /// <param name="EndDateTime">The instant when the exercise activity ended.</param>
    public sealed record ExerciseActivityResponse(
            Guid UniqueId,
            string Description,
            int AverageHeartRate,
            int MaximumHeartRate,
            DateTimeOffset StartDateTime,
            DateTimeOffset EndDateTime)
        : BaseExerciseActivity(Description, AverageHeartRate, MaximumHeartRate, StartDateTime, EndDateTime);
}
