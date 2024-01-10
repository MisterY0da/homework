using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Consumer.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "mygroup",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                consumer.Subscribe("mytopic");

                try
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
