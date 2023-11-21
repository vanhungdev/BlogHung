using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Kafka
{
    public interface IKafka
    {
        /// <summary>
        /// Start a consumer
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        Task StartConsuming(string topic, CancellationToken stoppingToken);

        /// <summary>
        /// Stop all
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        Task StopConsuming(CancellationToken stoppingToken);
    }
}
