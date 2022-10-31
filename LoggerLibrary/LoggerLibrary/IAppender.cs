namespace LoggerLibrary
{
    public interface IAppender
    {
        public void Append(string dateTime, string message);
    }
}
