namespace Error.Handler.Sample.Api.Model.Entity
{
    public sealed class ExerciseActivityEntity
    {
        public int AverageHeartRate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public int MaximumHeartRate { get; set; }
        public List<string>? Tags { get; set; }
    }
}
