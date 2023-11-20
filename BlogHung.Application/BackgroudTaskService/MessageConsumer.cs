using BlogHung.Infrastructure.Kafka;
using BlogHung.Infrastructure.Logging;
using BlogHung.Infrastructure.Models;
using BlogHung.Infrastructure.Utilities;
using Confluent.Kafka;
using EasyNetQ;
using Microsoft.Extensions.Hosting;

namespace BlogHung.Application.BackgroudTaskService
{
    public class MessageConsumer : BackgroundService
    {
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(Kafka.Config).Build())
                {
                    consumer.Subscribe(Kafka.Topic);

                    try
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            var consumeResult = consumer.Consume(stoppingToken);
                            string mesValue = consumeResult.Message.Value;
                            if (!string.IsNullOrEmpty(mesValue))
                            {
                                consumer.Commit(consumeResult);
                            }
                        }
                    }
                    catch (OperationCanceledException oe) //cancellationToken is cancel
                    {
                        string exx = oe.Message;
                    }
                    finally
                    {
                        consumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string exx2 = ex.Message;
            }
        }
        private void HandleMessage(MyMessage message)
        {
            Console.WriteLine($"Received message: {message.Content}");
            // Handle the message as needed
        }
    }
}
