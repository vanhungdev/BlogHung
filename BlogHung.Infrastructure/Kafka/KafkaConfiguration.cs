using BlogHung.Infrastructure.Utilities;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Kafka
{
    public class KafkaConfiguration
    {
        public static ConsumerConfig Config { get; }
        public static readonly string Topic = "events1";

        static KafkaConfiguration()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "spf-group-01",
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            Config = config;
        }
    }
}
