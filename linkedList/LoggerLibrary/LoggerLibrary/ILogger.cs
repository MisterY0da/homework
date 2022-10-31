namespace LoggerLibrary
{
    public interface  ILogger
    {
        public void Error(string dateTime, string message);
        public void Info(string dateTime, string message);
    }
}
