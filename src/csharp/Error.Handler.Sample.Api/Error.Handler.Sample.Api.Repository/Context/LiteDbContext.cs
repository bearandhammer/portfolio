using Ardalis.GuardClauses;
using Error.Handler.Sample.Api.Repository.Configuration;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Error.Handler.Sample.Api.Repository.Context
{
    public sealed class LiteDbContext
    {
        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            string applicationPath = Guard.Against.NullOrWhiteSpace(Path.GetDirectoryName(Environment.ProcessPath)),
                liteDbPathComponent = Guard.Against.NullOrWhiteSpace(Path.GetDirectoryName(options.Value.DatabaseLocation)),
                liteDbFullPath = Path.Combine(applicationPath, liteDbPathComponent);

            if (!Directory.Exists(liteDbFullPath))
            {
                Directory.CreateDirectory(liteDbFullPath);
            }

            ActivityDatabase = new(options.Value.DatabaseLocation);
            CollectionName = options.Value.CollectionName;
        }

        public LiteDatabase ActivityDatabase { get; }

        public string CollectionName { get; }
    }
}
