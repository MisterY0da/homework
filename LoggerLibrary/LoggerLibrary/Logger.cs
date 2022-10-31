using System.Collections.Generic;

namespace LoggerLibrary
{
    public class Logger : ILogger
    {
        public List<IAppender> appenders;   
        public Logger(List<IAppender> appenders)
        {
            this.appenders = appenders;
        }

        public void Error(string dateTime, string message)
        {
            foreach (var appender in appenders)
            {
                message = "Error: " + message;
                appender.Append(dateTime, message);
            }
        }

        public void Info(string dateTime, string message)
        {
            foreach (var appender in appenders)
            {
                appender.Append(dateTime, message);
            }
        }
    }
}
