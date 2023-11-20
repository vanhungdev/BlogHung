using BlogHung.Infrastructure.Utilities;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Kafka
{
    public class Kafka
    {
        public static ConsumerConfig Config { get; }
        public static readonly string Topic = "quickstart-events";

        static Kafka()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
/*                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest*/
            };
            Config = config;
        }
    }
}
