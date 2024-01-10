using Confluent.Kafka;

namespace Producer.Services
{
    public class KafkaService : IKafkaService
    {
        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        public async Task SendMessageAsync(string message)
        {
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("mytopic", new Message<Null, string> { Value = message });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
