using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDbSampleApp
{
    public class MongoConnector
    {
        private readonly IMongoDatabase database;

        public MongoConnector(string databaseName)
        {
            MongoClient client = new MongoClient();
            database = client.GetDatabase(databaseName);
        }

        public void InsertRecord<T>(string table, T record)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecords<T>(string table, Guid id, T record)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(table);

            ReplaceOneResult result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);

            DeleteResult result = collection.DeleteOne(filter);
        }
    }
}