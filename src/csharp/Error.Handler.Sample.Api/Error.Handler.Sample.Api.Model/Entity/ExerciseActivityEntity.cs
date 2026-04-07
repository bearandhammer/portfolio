namespace Error.Handler.Sample.Api.Model.Entity
{
    /// <summary>
    /// Represents the data store model for an exercise activity.
    /// </summary>
    public sealed class ExerciseActivityEntity
    {
        /// <summary>
        /// Gets or sets the primary key for this exercise activity in the backing store.
        /// </summary>
        /// <remarks>
        /// For new entities, this value is <see cref="Guid.Empty"/> until the repository persists the document;
        /// after insert, it is set to the identifier returned by the database.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the instant when the exercise activity started.
        /// </summary>
        public required DateTimeOffset StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the instant when the exercise activity ended.
        /// </summary>
        public required DateTimeOffset EndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the description of the exercise activity.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the average heart rate during the exercise activity.
        /// </summary>
        public required int AverageHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the maximum heart rate during the exercise activity.
        /// </summary>
        public required int MaximumHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the user-defined tags associated with the exercise activity.
        /// </summary>
        public List<string>? Tags { get; set; }
    }
}
