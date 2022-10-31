namespace LoggerLibrary
{
    public class SimpleLayout : ILayout
    {
        public string FormatMessage(string dateTime, string message)
        {
            return dateTime + "\t" + message;
        }
    }
}
