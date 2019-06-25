using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDbSampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //WebPage samplePage = GetSamplePage();

            MongoConnector connector = new MongoConnector("PagesDb");
            //connector.InsertRecord("Pages", samplePage);

            List<WebPage> pages = connector.LoadRecords<WebPage>("Pages");

            pages?.ForEach(page =>
            {
                Console.WriteLine($"Id: { page.Id } - { page.Header }");
            });

            List<WebPage> pagesAgain = connector.LoadRecords<WebPage>("Pages");

            pagesAgain?.ForEach(page =>
            {
                Console.WriteLine($"Id: { page.Id } - { page.Header }");
            });

            Console.ReadLine();
        }

        private static WebPage GetSamplePage()
        {
            return new WebPage
            {
                Header = "Test Page",
                MainImageUrl = "https://imageurl.com/image.jpg",
                Paragraphs = new List<string>
                {
                    "This is the first paragraph",
                    "This is the second paragraph"
                },
                Widgets = new List<Widget>
                {
                    new Widget
                    {
                        Name = "Widget One",
                        TargetUrl = "https://widget.com/urlone?something=true"
                    },
                    new Widget
                    {
                        Name = "Widget Two",
                        TargetUrl = "https://widget.com/urltwo?something=false"
                    }
                }
            };
        }
    }

    public class Widget
    {
        public string Name { get; set; }
        public string TargetUrl { get; set; }
    }

    public class WebPage
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Header { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> Paragraphs { get; set; }
        public List<Widget> Widgets { get; set; }
    }

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