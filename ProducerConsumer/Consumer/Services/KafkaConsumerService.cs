using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Consumer.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "mygroup",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                consumer.Subscribe("mytopic");

                try
                {
                    var consumeResult = consumer.Consume();
                    _logger.LogInformation($"message recieved from Kafka: {consumeResult.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Error occurred: {e.Error.Reason}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
