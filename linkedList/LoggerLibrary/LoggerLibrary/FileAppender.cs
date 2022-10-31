namespace LoggerLibrary
{
    public class FileAppender : IAppender
    {
        public ILayout layout;
        public LogFile file;
        public FileAppender(ILayout layout, LogFile file)
        {
            this.layout = layout;
            this.file = file;
        }

        public void Append(string dateTime, string message)
        {            
            file.fileData += layout.FormatMessage(dateTime, message);
        }
    }
}
