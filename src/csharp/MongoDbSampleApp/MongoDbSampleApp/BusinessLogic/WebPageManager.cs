using MongoDbSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbSampleApp.BusinessLogic
{
    public class WebPageManager
    {
        private readonly MongoConnector connector;

        public bool ExitApp { get; private set; }

        public WebPageManager()
        {
            connector = new MongoConnector("PagesDb");
        }

        public void ProcessCommand(string commandTriggered)
        {
            if (string.IsNullOrWhiteSpace(commandTriggered))
            {
                Console.WriteLine("Invalid command entered, please try again.");
            }
            else
            {
                Console.WriteLine();
                switch (commandTriggered.ToLowerInvariant().Trim())
                {
                    case "-help":
                        WriteCommandList();
                        break;

                    case "-i":
                        InsertNewWebPageDocument();
                        break;

                    case "-u":
                        UpsertWebPageDocumentById();
                        break;

                    case "-d":
                        DeleteWebPageDocumentById();
                        break;

                    case "-g":
                        ShowDetailsForWebPageDocumentById();
                        break;

                    case "-ga":
                        ShowDetailsForAllWebPageDocuments();
                        break;

                    case "-e":
                        ExitApp = true;
                        break;

                    default:
                        break;
                }
                Console.WriteLine();
            }
        }

        private void UpsertWebPageDocumentById()
        {
            // TODO
        }

        private void DeleteWebPageDocumentById()
        {
            Console.Write("Guid (ID): ");
            Guid id = new Guid(Console.ReadLine().Trim());

            connector.DeleteRecord<WebPage>("Pages", id);
            Console.WriteLine($"{ Environment.NewLine }Document deleted");
        }

        private void ShowDetailsForWebPageDocumentById()
        {
            Console.Write("Guid (ID): ");
            Guid id = new Guid(Console.ReadLine().Trim());

            WebPage page = connector.LoadRecordById<WebPage>("Pages", id);

            if (page != null)
            {
                Console.WriteLine($"{ Environment.NewLine }Page ID: { page.Id } | Header: { page.Header } | Main Image URL: { page.MainImageUrl }{ Environment.NewLine }");
                Console.WriteLine("Query complete...");
            }
        }

        private void ShowDetailsForAllWebPageDocuments()
        {
            List<WebPage> pages = connector.LoadRecords<WebPage>("Pages");

            if (pages?.Count > 0)
            {
                StringBuilder pageDetails = new StringBuilder();
                pages.ForEach(page => pageDetails.AppendLine($"Page ID: { page.Id } | Header: { page.Header } | Main Image URL: { page.MainImageUrl }"));
                Console.WriteLine(pageDetails.ToString());
                Console.WriteLine("Query complete...");
            }
        }

        private void InsertNewWebPageDocument()
        {
            WebPage newWebPageDocument = new WebPage();

            Console.Write("Header: ");
            string header = Console.ReadLine();

            Console.Write("Main Image URL: ");
            string mainImageUrl = Console.ReadLine();

            Console.Write("Paragraphs (pipe separated): ");
            string paragraphString = Console.ReadLine();
            List<string> paragraphs = new List<string>();

            if (!string.IsNullOrWhiteSpace(paragraphString))
            {
                paragraphs.AddRange(paragraphString.Split('|'));
            }

            Console.Write("Add Widgets (Y/N): ");

            List<Widget> widgets = new List<Widget>();
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                string widgetName, targetUrl;

                do
                {
                    Console.Write($"{ Environment.NewLine }Widget Name: ");
                    widgetName = Console.ReadLine();

                    Console.Write("Target URL: ");
                    targetUrl = Console.ReadLine();

                    widgets.Add(new Widget
                    {
                        Name = widgetName,
                        TargetUrl = targetUrl
                    });

                    Console.Write("Add another (Y/N)?: ");
                } while (Console.ReadKey().Key == ConsoleKey.Y);
            }

            newWebPageDocument.Header = header;
            newWebPageDocument.MainImageUrl = mainImageUrl;

            if (paragraphs?.Count > 0)
            {
                newWebPageDocument.Paragraphs.AddRange(paragraphs);
            }

            if (widgets?.Count > 0)
            {
                newWebPageDocument.Widgets.AddRange(widgets);
            }

            connector.InsertRecord("Pages", newWebPageDocument);
            Console.WriteLine($"{ Environment.NewLine }{ Environment.NewLine }Document added!");
        }

        private void WriteCommandList()
        {
            Console.WriteLine(new StringBuilder()
                .AppendLine("'-help' = lists of of the available commands.")
                .AppendLine("'-i' = insert a new web page document.")
                .AppendLine("'-u' = update an existing web page document (by id)")
                .AppendLine("'-g' = get details an existing web page document (by id)")
                .AppendLine("'-ga' = get details on all existing web page documents").ToString());
        }
    }
}