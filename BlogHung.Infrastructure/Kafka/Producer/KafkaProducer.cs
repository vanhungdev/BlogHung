﻿using BlogHung.Infrastructure.Logging;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Kafka.Producer
{
    public class KafkaProducer : IKafkaProducer
    {
        public async Task<bool> ProducePushMessage(string topic, ProducerConfig config, object objRequest, string val)
        {
            var log = new StringBuilder();
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                var jsonObj = JsonConvert.SerializeObject(objRequest);
                var message = new Message<Null, string> { Value = jsonObj };
                var result = await producer.ProduceAsync(topic, message);

                //log.AppendLine($"Input: {jsonObj}");
                log.AppendLine($"m: {val} to offset: {result.TopicPartitionOffset.Offset.Value}");
                return true;
            }
            catch (ProduceException<Null, string> e)
            {
                log.AppendLine($"Delivery failed: {e.Error.Reason}");
                return false;
            }
            finally
            {
                Console.WriteLine(log.ToString());
                //LoggingHelper.SetLogStep(log.ToString());
            }
        }
    }
}
