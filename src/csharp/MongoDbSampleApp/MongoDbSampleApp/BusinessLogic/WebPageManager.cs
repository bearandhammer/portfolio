using MongoDbSampleApp.Helpers;
using MongoDbSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDbSampleApp.BusinessLogic
{
    public class WebPageManager
    {
        private readonly MongoConnector connector;

        public bool ExitApp { get; private set; }

        public WebPageManager(string databaseName = Constants.PAGES_DB_NAME)
        {
            connector = new MongoConnector(databaseName);
        }

        public void ProcessCommand(string commandTriggered)
        {
            if (string.IsNullOrWhiteSpace(commandTriggered))
            {
                Console.WriteLine(Constants.INVALID_COMMAND);
            }
            else
            {
                Console.WriteLine();
                ProcessSpecificCommand(commandTriggered);
                Console.WriteLine();
            }
        }

        private void ProcessSpecificCommand(string commandTriggered)
        {
            switch (commandTriggered.ToLowerInvariant().Trim())
            {
                case Constants.HELP_COMMAND:
                    WriteCommandList();
                    break;

                case Constants.INSERT_COMMAND:
                    InsertNewWebPageDocument();
                    break;

                case Constants.UPDATE_COMMAND:
                    UpdateWebPageDocumentById();
                    break;

                case Constants.DELETE_COMMAND:
                    DeleteWebPageDocumentById();
                    break;

                case Constants.GET_COMMAND:
                    GetDetailsForWebPageDocumentById();
                    break;

                case Constants.GETALL_COMMAND:
                    GetDetailsForAllWebPageDocuments();
                    break;

                case Constants.EXIT_COMMAND:
                    ExitApp = true;
                    break;

                default:
                    Console.WriteLine(Constants.INVALID_COMMAND);
                    break;
            }
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

        #region Add Web Page Document

        private void InsertNewWebPageDocument()
        {
            if (TryAddWebPageFromUserInput(out WebPage newWebPageDocument))
            {
                connector.InsertRecord(Constants.PAGES_TABLE_NAME, newWebPageDocument);
                Console.WriteLine($"{ Environment.NewLine }{ Environment.NewLine }Document added!");
            }
            else
            {
                Console.WriteLine($"{ Environment.NewLine }{ Environment.NewLine }Due to parsing errors the new WebPage object could not be added to MongoDB.");
            }
        }

        private bool TryAddWebPageFromUserInput(out WebPage newWebPageDocument)
        {
            newWebPageDocument = new WebPage();

            if (!AddBasicPageMetadata(newWebPageDocument))
            {
                return false;
            }

            if (!AddWidgetPageMetadata(newWebPageDocument))
            {
                return false;
            }

            return true;
        }

        private bool AddBasicPageMetadata(WebPage newWebPageDocument)
        {
            if (!TryGetInput("Header: ", out string header))
            {
                return false;
            }
            else
            {
                newWebPageDocument.Header = header;
            }

            if (!TryGetInput("Main Image URL: ", out string mainImageUrl))
            {
                return false;
            }
            else
            {
                newWebPageDocument.MainImageUrl = mainImageUrl;
            }

            if (!TryGetInput("Paragraphs (pipe separated): ", out List<string> paragraphs,
                (inputValue) =>
                {
                    return !string.IsNullOrWhiteSpace(inputValue) ? inputValue.Split('|').ToList() : new List<string>();
                }))
            {
                return false;
            }
            else
            {
                newWebPageDocument.Paragraphs.AddRange(paragraphs);
            }

            return true;
        }

        private bool AddWidgetPageMetadata(WebPage newWebPageDocument)
        {
            bool widgetAdditionsSuccessful = true;

            Console.Write("Add Widgets (Y/N): ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                do
                {
                    if (TryGetWidgetFromUserInput(out Widget widget))
                    {
                        newWebPageDocument.Widgets.Add(widget);
                    }
                    else
                    {
                        widgetAdditionsSuccessful = false;
                    }

                    Console.Write("Add another (Y/N)?: ");
                } while (Console.ReadKey().Key == ConsoleKey.Y || !widgetAdditionsSuccessful);
            }

            return widgetAdditionsSuccessful;
        }

        private bool TryGetWidgetFromUserInput(out Widget widget)
        {
            widget = new Widget();

            if (!TryGetInput("Widget Name: ", out string widgetName))
            {
                return false;
            }
            else
            {
                widget.Name = widgetName;
            }

            if (!TryGetInput("Target URL: ", out string widgetTargetUrl))
            {
                return false;
            }
            else
            {
                widget.TargetUrl = widgetTargetUrl;
            }

            return true;
        }

        #endregion Add Web Page Document

        #region Update Web Page Document

        private void UpdateWebPageDocumentById()
        {
            if (TryGetInput<Guid>("Guid (ID): ", out Guid id))
            {
                WebPage page = connector.LoadRecordById<WebPage>(Constants.PAGES_TABLE_NAME, id);

                if (page != null)
                {
                    if (TryUpdateWebPageFromUserInput(page))
                    {
                        connector.UpsertRecords(Constants.PAGES_TABLE_NAME, page.Id, page);
                        Console.WriteLine($"{ Environment.NewLine }Document updated!");
                    }
                    else
                    {
                        Console.WriteLine("Page could not be updated.");
                    }
                }
                else
                {
                    Console.WriteLine($"No Page found matching ID: { id }.");
                }
            }
            else
            {
                Console.WriteLine("Update cannot be performed as the provided value is not a Guid.");
            }
        }

        private bool TryUpdateWebPageFromUserInput(WebPage existingWebPageDocument)
        {
            if (!UpdateBasicPageMetadata(existingWebPageDocument))
            {
                return false;
            }

            if (!UpdateWidgetPageMetadata(existingWebPageDocument))
            {
                return false;
            }

            return true;
        }

        private bool UpdateBasicPageMetadata(WebPage existingWebPageDocument)
        {
            if (!UserWantsToKeepValue($"Header: ({ existingWebPageDocument.Header })"))
            {
                if (!TryGetInput("Header: ", out string header))
                {
                    return false;
                }
                else
                {
                    existingWebPageDocument.Header = header;
                }
            }

            if (!UserWantsToKeepValue($"Main Image URL: ({ existingWebPageDocument.MainImageUrl })"))
            {
                if (!TryGetInput("Main Image URL: ", out string mainImageUrl))
                {
                    return false;
                }
                else
                {
                    existingWebPageDocument.MainImageUrl = mainImageUrl;
                }
            }

            if (!UserWantsToKeepValue($"Paragraphs (pipe separated): ({ string.Join("|", existingWebPageDocument.Paragraphs) })"))
            {
                if (!TryGetInput("Paragraphs (pipe separated): ", out List<string> paragraphs,
                    (inputValue) =>
                    {
                        return !string.IsNullOrWhiteSpace(inputValue) ? inputValue.Split('|').ToList() : new List<string>();
                    }))
                {
                    return false;
                }
                else
                {
                    existingWebPageDocument.Paragraphs.AddRange(paragraphs);
                }
            }

            return true;
        }

        private bool UpdateWidgetPageMetadata(WebPage existingWebPageDocument)
        {
            bool widgetAdditionsSuccessful = true;

            Console.Write("Add Widgets (Y/N): ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                do
                {
                    if (TryUpdateWidgetFromUserInput(out Widget widget))
                    {
                        existingWebPageDocument.Widgets.Add(widget);
                    }
                    else
                    {
                        widgetAdditionsSuccessful = false;
                    }

                    Console.Write("Add another (Y/N)?: ");
                } while (Console.ReadKey().Key == ConsoleKey.Y || !widgetAdditionsSuccessful);
            }

            return widgetAdditionsSuccessful;
        }

        private bool TryUpdateWidgetFromUserInput(out Widget widget)
        {
            widget = new Widget();

            if (!UserWantsToKeepValue($"Widget Name: ({ widget.Name })"))
            {
                if (!TryGetInput("Widget Name: ", out string widgetName))
                {
                    return false;
                }
                else
                {
                    widget.Name = widgetName;
                }
            }

            if (!UserWantsToKeepValue($"Target URL: ({ widget.TargetUrl })"))
            {
                if (!TryGetInput("Target URL: ", out string widgetTargetUrl))
                {
                    return false;
                }
                else
                {
                    widget.TargetUrl = widgetTargetUrl;
                }
            }

            return true;
        }

        #endregion Update Web Page Document

        #region Delete Web Page Document

        private void DeleteWebPageDocumentById()
        {
            Console.Write("Guid (ID): ");
            Guid id = new Guid(Console.ReadLine().Trim());

            connector.DeleteRecord<WebPage>(Constants.PAGES_TABLE_NAME, id);
            Console.WriteLine($"{ Environment.NewLine }Document deleted!");
        }

        #endregion Delete Web Page Document

        #region Get Web Page Document (single)

        private void GetDetailsForWebPageDocumentById()
        {
            Console.Write("Guid (ID): ");
            Guid id = new Guid(Console.ReadLine().Trim());

            WebPage page = connector.LoadRecordById<WebPage>(Constants.PAGES_TABLE_NAME, id);

            if (page != null)
            {
                Console.WriteLine($"{ Environment.NewLine }Page ID: { page.Id } | Header: { page.Header } | Main Image URL: { page.MainImageUrl }{ Environment.NewLine }");
                Console.WriteLine("Query complete!");
            }
        }

        #endregion Get Web Page Document (single)

        #region Get Web Page Document (all)

        private void GetDetailsForAllWebPageDocuments()
        {
            List<WebPage> pages = connector.LoadRecords<WebPage>(Constants.PAGES_TABLE_NAME);

            if (pages?.Count > 0)
            {
                StringBuilder pageDetails = new StringBuilder();
                pages.ForEach(page => pageDetails.AppendLine($"Page ID: { page.Id } | Header: { page.Header } | Main Image URL: { page.MainImageUrl }"));
                Console.WriteLine(pageDetails.ToString());
                Console.WriteLine("Query complete!");
            }
        }

        #endregion Get Web Page Document (all)

        #region Helpers

        private static bool UserWantsToKeepValue(string prompt)
        {
            Console.Write($"{ prompt } - Keep value (-k to keep)? ");
            return Console.ReadLine().Trim().Equals(Constants.KEEP_COMMAND, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool TryGetInput<T>(string prompt, out T parsedValue, Func<string, T> providedParser = null)
        {
            Console.Write(prompt);
            return Console.ReadLine().TryParseValueFromUser<T>(out parsedValue, providedParser);
        }

        #endregion Helpers
    }
}