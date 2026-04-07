using Ardalis.GuardClauses;
using Error.Handler.Sample.Api.Repository.Configuration;
using LiteDB;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Error.Handler.Sample.Api.Repository.Context
{
    public sealed class LiteDbContext
    {
        public LiteDbContext(LiteDbOptions options)
        {
            string dbPathComponent = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                liteDbFullPath = Path.Combine(dbPathComponent, Guard.Against.Null(Path.GetDirectoryName(options.DatabaseLocation)));

            if (!Directory.Exists(liteDbFullPath))
            {
                Directory.CreateDirectory(liteDbFullPath);
            }

            ActivityDatabase = new(Path.Combine(liteDbFullPath, Path.GetFileName(options.DatabaseLocation)));
            CollectionName = options.CollectionName;
        }

        public LiteDatabase ActivityDatabase { get; }

        public string CollectionName { get; }
    }
}
