using Error.Handler.Sample.Api.Model.Entity;
using Error.Handler.Sample.Api.Repository.Context;
using Error.Handler.Sample.Api.Repository.Interface;
using LiteDB;

namespace Error.Handler.Sample.Api.Repository.Implementation
{
    public sealed class ExerciseActivityRepo(LiteDbContext context) : IExerciseActivityRepo
    {
        private readonly LiteDbContext _context = context;

        /// <inheritdoc />
        public ExerciseActivityEntity AddExerciseActivity(ExerciseActivityEntity exerciseActivityToAdd)
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
        public bool DeleteExerciseActivity(Guid id) =>
            _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>()
                .Delete(new BsonValue(id));

        /// <inheritdoc />
        public List<ExerciseActivityEntity> GetExerciseActivities() =>
            [.. _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>(_context.CollectionName)
                .FindAll()];

        /// <inheritdoc />
        public ExerciseActivityEntity GetExerciseActivityById(Guid id) =>
            _context
                .ActivityDatabase
                .GetCollection<ExerciseActivityEntity>(_context.CollectionName)
                .FindById(id);

        /// <inheritdoc />
        public (ExerciseActivityEntity exerciseActivity, bool successfullyUpdated) UpdateExerciseActivity(ExerciseActivityEntity exerciseActivityToUpdate)
        {
            bool successfullyUpdated = _context
                    .ActivityDatabase
                    .GetCollection<ExerciseActivityEntity>()
                    .Update(exerciseActivityToUpdate);

            return (exerciseActivityToUpdate, successfullyUpdated);
        }
    }
}
