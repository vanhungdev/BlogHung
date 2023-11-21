using BlogHung.Application.OrderProcess;
using BlogHung.Infrastructure.Kafka;
using BlogHung.Infrastructure.Kafka.Consumer;
using Microsoft.Extensions.Hosting;

namespace BlogHung.Application.BackgroudTaskService
{
    public class MessageConsumer : BackgroundService
    {
        private readonly KafkaConsumerManager _consumerManager;
        private readonly IOrderProcess _orderProcess;

        public MessageConsumer(KafkaConsumerManager consumerManager, IOrderProcess orderProcess)
        {
            _consumerManager = consumerManager;
            _orderProcess = orderProcess;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var topic1 = "events1";
            var topic2 = "events2";

            _consumerManager.AddConsumer(topic1, async message => _orderProcess.CreateOrderProcess(message, topic1), KafkaConfiguration.Config); // Configuration can be changed
            _consumerManager.AddConsumer(topic2, async message => MessageProcess2(message, topic2), KafkaConfiguration.Config); // Configuration can be changed
            //await _consumerManager.StartAllConsumersAsync(stoppingToken); // Start parallel
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        public static void MessageProcess2(string message, string topic)
        {
            Console.WriteLine($"Received message from {topic}: {message}");
        }
    }
}
