namespace Producer.Services
{
    public interface IKafkaService
    {
        Task SendMessageAsync(string message);
    }
}
