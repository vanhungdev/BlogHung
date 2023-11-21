using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.OrderProcess
{
    public class OrderProcess : IOrderProcess
    {
        public void CreateOrderProcess(string message, string topic)
        {
            Console.WriteLine($"Received message from {message}: {topic}");
        }
    }
}
