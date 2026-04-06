using Error.Handler.Sample.Api.Base;

namespace Error.Handler.Sample.Api.Requests
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddExerciseActivityRequest"/> class.
    /// </summary>
    /// <param name="Description">The description of the exercise activity (directly from the user).</param>
    /// <param name="AverageHeartRate">The average heart rate recorded during the activity.</param>
    /// <param name="MaximumHeartRate">The maximum heart rate recorded during the activity.</param>
    public sealed record AddExerciseActivityRequest(string Description, int AverageHeartRate, int MaximumHeartRate)
        : BaseExerciseActivity(Description, AverageHeartRate, MaximumHeartRate);
}
