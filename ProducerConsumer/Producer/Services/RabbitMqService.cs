using System.Text;
using RabbitMQ.Client;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace Producer.Services
{
    public class RabbitMqService : BackgroundService
    {
        private IConnectionFactory _factory;
        private ILogger<RabbitMqService> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public RabbitMqService(ILogger<RabbitMqService> logger) 
        {
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _logger = logger;
        }

        public void SendMessage(string message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "myqueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: "myqueue",
                               basicProperties: null,
                               body: body);

                _logger.LogInformation($"message sent to RabbitMQ: {message}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var rng = new Random();

            var message = Summaries[rng.Next(Summaries.Length)];

            SendMessage(message);

            return Task.CompletedTask;
        }
    }
}
