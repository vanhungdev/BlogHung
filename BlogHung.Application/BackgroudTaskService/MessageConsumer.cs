using BlogHung.Infrastructure.Models;
using EasyNetQ;
using Microsoft.Extensions.Hosting;

namespace BlogHung.Application.BackgroudTaskService
{
    public class MessageConsumer : BackgroundService
    {
        private readonly IBus _bus;

        public MessageConsumer(IBus bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _bus.PubSub.SubscribeAsync<MyMessage>("events1", HandleMessage, stoppingToken);
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
            }

        }
        private void HandleMessage(MyMessage message)
        {
            Console.WriteLine($"Received message: {message.Content}");
            // Handle the message as needed
        }
    }
}
