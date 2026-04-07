using Error.Handler.Sample.Api.Model.Entity;
using Error.Handler.Sample.Api.Repository.Context;
using Error.Handler.Sample.Api.Repository.Interface;
using LiteDB;

namespace Error.Handler.Sample.Api.Repository.Implementation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseActivityRepo"/> class.
    /// Represents an implementation of the <see cref="IExerciseActivityRepo"/> interface that provides persistence operations for exercise activities in the backing store.
    /// </summary>
    /// <param name="context">The <see cref="LiteDbContext"/> instance in scope to use for persistence operations.</param>
    /// <seealso cref="IExerciseActivityRepo"/>
    public sealed class ExerciseActivityRepo(LiteDbContext context) : IExerciseActivityRepo
    {
        /// <summary>
        /// Represents the <see cref="LiteDbContext"/> instance in scope to use for persistence operations.
        /// </summary>
        private readonly LiteDbContext _context = context;

        /// <inheritdoc />
        public ExerciseActivityEntity AddExerciseActivityToStore(ExerciseActivityEntity exerciseActivityToAdd)
        {
            Guid insertedId =
                _context
                    .ActivityDatabase
                    .GetCollection<ExerciseActivityEntity>(_context.CollectionName)
                    .Insert(exerciseActivityToAdd);

            exerciseActivityToAdd.Id = insertedId;
            return exerciseActivityToAdd;
        }

        /// <inheritdoc />
        public bool DeleteExerciseActivityFromStore(Guid id) =>
            _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>()
                .Delete(new BsonValue(id));

        /// <inheritdoc />
        public List<ExerciseActivityEntity> GetExerciseActivitiesFromStore() =>
            [.. _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>(_context.CollectionName)
                .FindAll()];

        /// <inheritdoc />
        public ExerciseActivityEntity GetExerciseActivityFromStoreById(Guid id) =>
            _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>(_context.CollectionName)
                .FindById(id);

        /// <inheritdoc />
        public (ExerciseActivityEntity exerciseActivity, bool successfullyUpdated) UpdateExerciseActivityInStore(ExerciseActivityEntity exerciseActivityToUpdate)
        {
            bool successfullyUpdated = _context
                    .ActivityDatabase
                    .GetCollection<ExerciseActivityEntity>()
                    .Update(exerciseActivityToUpdate);

            return (exerciseActivityToUpdate, successfullyUpdated);
        }
    }
}
