using Error.Handler.Sample.Api.Model.Entity;

namespace Error.Handler.Sample.Api.Repository.Interface
{
    /// <summary>
    /// Defines persistence operations for exercise activities in the backing store.
    /// </summary>
    public interface IExerciseActivityRepo
    {
        /// <summary>
        /// Inserts a new exercise activity and assigns the generated identifier to the entity.
        /// </summary>
        /// <param name="exerciseActivityToAdd">The exercise activity to persist. The <see cref="ExerciseActivityEntity.Id"/> may be updated to match the store-generated key.</param>
        /// <returns>The same <paramref name="exerciseActivityToAdd"/> instance with <see cref="ExerciseActivityEntity.Id"/> set to the inserted identifier.</returns>
        ExerciseActivityEntity AddExerciseActivityToStore(ExerciseActivityEntity exerciseActivityToAdd);

        /// <summary>
        /// Deletes the exercise activity with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The unique identifier of the exercise activity to remove.</param>
        /// <returns><see langword="true"/> if a document was deleted; otherwise, <see langword="false"/>.</returns>
        bool DeleteExerciseActivityFromStore(Guid id);

        /// <summary>
        /// Retrieves all exercise activities from the backing store.
        /// </summary>
        /// <returns>A list of all persisted exercise activities. The list may be empty if none exist.</returns>
        List<ExerciseActivityEntity> GetExerciseActivitiesFromStore();

        /// <summary>
        /// Retrieves the exercise activity with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the exercise activity to load.</param>
        /// <returns>The matching <see cref="ExerciseActivityEntity"/>, or <see langword="null"/> if no document exists for <paramref name="id"/>.</returns>
        ExerciseActivityEntity GetExerciseActivityFromStoreById(Guid id);

        /// <summary>
        /// Updates an existing exercise activity in the backing store.
        /// </summary>
        /// <param name="exerciseActivityToUpdate">The exercise activity to persist. Typically includes the identifier of the document to replace.</param>
        /// <returns>
        /// A tuple where <c>exerciseActivity</c> is the same instance as <paramref name="exerciseActivityToUpdate"/>,
        /// and <c>successfullyUpdated</c> indicates whether the backing store reported a successful update.
        /// </returns>
        (ExerciseActivityEntity exerciseActivity, bool successfullyUpdated) UpdateExerciseActivityInStore(
            ExerciseActivityEntity exerciseActivityToUpdate);
    }
}
