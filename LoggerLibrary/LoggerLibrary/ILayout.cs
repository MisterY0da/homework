namespace LoggerLibrary
{
    public interface ILayout
    {
        public string FormatMessage(string dateTime, string message);
    }
}
