using LoggerLibrary;
using System;
using System.Collections.Generic;

namespace LoggerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleLayout = new SimpleLayout();
            var file = new LogFile();

            var appenders = new List<IAppender>() { new ConsoleAppender(simpleLayout), new FileAppender(simpleLayout, file) };  

            var logger = new Logger(appenders);
            logger.Error("3/26/2022 2:08:11 PM", "Error parsing JSON.");
            logger.Info("3/26/2022 2:08:11 PM", "User Ivan successfully registered.");

            // show data logged in file
            Console.WriteLine("\nFileData: " + file.fileData);
        }
    }
}
