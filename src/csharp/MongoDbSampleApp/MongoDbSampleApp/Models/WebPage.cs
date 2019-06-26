using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoDbSampleApp.Models
{
    public class WebPage
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Header { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> Paragraphs { get; set; }
        public List<Widget> Widgets { get; set; }

        public WebPage()
        {
            Paragraphs = new List<string>();
            Widgets = new List<Widget>();
        }
    }
}