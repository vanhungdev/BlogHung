using Confluent.Kafka;

namespace BlogHung.Infrastructure.Kafka.Consumers
{
    public class KafkaConsumerManager
    {
        private readonly Dictionary<string, IKafka> _consumers;
        public KafkaConsumerManager()
        {
            _consumers = new Dictionary<string, IKafka>();
        }

        /// <summary>
        /// Add a consumer thread
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="messageHandler"></param>
        /// <param name="config"></param>
        public void AddConsumer(string topic, Func<string, Task> messageHandler, ConsumerConfig config)
        {
            if (!_consumers.ContainsKey(topic))
            {
                var consumer = new Kafka(messageHandler, config);
                _consumers.Add(topic, consumer);
            }
        }

        /// <summary>
        /// Start parallel
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task StartAllConsumersAsync(CancellationToken stoppingToken)
        {
            var tasks = _consumers.Select(kv => Task.Run(() => kv.Value.StartConsuming(kv.Key, stoppingToken)));
            await Task.WhenAll(tasks);
        }
    }
}
