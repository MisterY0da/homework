using Confluent.Kafka;

namespace Producer.Services
{
    public class KafkaService : BackgroundService
    {
        private ILogger<KafkaService> _logger;

        private static readonly string[] Fruits = new[]
        {
            "Apple", "Mandarin", "Banana", "Pineapple", "Pomegranate", "Peach", "Pear", "Kiwi"
        };

        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = "kafka:9092"
        };

        public KafkaService(ILogger<KafkaService> logger)
        {
            _logger = logger;
        }

        public async Task SendMessageAsync(string message)
        {
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("mytopic", new Message<Null, string> { Value = message });
                    _logger.LogInformation($"message sent to Kafka: {message}");
                }
                catch (ProduceException<Null, string> e)
                {
                    _logger.LogError($"delivery failed: {e.Error.Reason}");
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var rng = new Random();

            var message = Fruits[rng.Next(Fruits.Length)];

            await SendMessageAsync(message);
        }
    }
}
