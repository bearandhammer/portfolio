namespace Error.Handler.Sample.Api.Responses
{
    public record ExerciseActivityResponse(Guid UniqueId, string Description, int AverageHeartRate, int MaximumHeartRate);
}
