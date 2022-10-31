using System;

namespace LoggerLibrary
{
    public class ConsoleAppender : IAppender
    {
        public ILayout layout;
        public ConsoleAppender(ILayout layout)
        {
            this.layout = layout;
        }

        public void Append(string dateTime, string message)
        {
            Console.WriteLine(layout.FormatMessage(dateTime, message));
        }
    }
}
