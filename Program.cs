using System;
using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
        string bootstrapServers = "kafka-sebi-dev-devrel-sebastien.a.aivencloud.com:22235";
        string groupId = "againother";
        string topic = "othertopic";
        string sslCaLocation = "ca.pem";
        string sslCertLocation = "service.cert";
        string sslKeyLocation = "service.key";

        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            SecurityProtocol = SecurityProtocol.Ssl,
            SslCaLocation = sslCaLocation,
            SslCertificateLocation = sslCertLocation,
            SslKeyLocation = sslKeyLocation,
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(topic);

            Console.WriteLine($"Consuming messages from topic: {topic}");

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();

                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming message: {e.Error.Reason}");
                }
            }
        }
    }
}
