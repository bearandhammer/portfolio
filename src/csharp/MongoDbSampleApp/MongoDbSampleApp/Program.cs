using MongoDbSampleApp.BusinessLogic;
using System;

namespace MongoDbSampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunApplication();
        }

        private static void RunApplication()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 35;
            WebPageManager pageManager = new WebPageManager();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Mongo Web Document Sample Application{ Environment.NewLine }");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("What would you like to do? Type -help for a full command list ('-e' to exit):");

            string commandTriggered;

            do
            {
                Console.Write("> ");

                commandTriggered = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                pageManager.ProcessCommand(commandTriggered);
                Console.ForegroundColor = ConsoleColor.White;
            } while (!pageManager.ExitApp);
        }
    }
}