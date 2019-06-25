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
            WebPage samplePage = GetSamplePage();

            MongoConnector connector = new MongoConnector("PagesDb");
            connector.InsertRecord("Pages", samplePage);

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
    }
}