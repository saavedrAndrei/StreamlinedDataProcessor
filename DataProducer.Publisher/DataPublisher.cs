using System;
using System.Text;
using System.Text.Json;
using RabbitMQ;
using RabbitMQ.Client;


namespace DataProducer.Publisher
{
    public class DataPublisher
    {

        public void DataSender(List<string> healthDataJsonFiles)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost");
            factory.ClientProvidedName = "Data-Producer-Service";

            IConnection connection = factory.CreateConnection();

            IModel channel = connection.CreateModel();

            string exchangeName = "HealthData";
            string queueName = "ClevelandClinic";
            string routeKey = "StagePrediction";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routeKey, null);

            foreach (var file in healthDataJsonFiles)
            {
                byte[] encodingFile = JsonSerializer.SerializeToUtf8Bytes(file);
                channel.BasicPublish(exchangeName, routeKey, null, encodingFile);
                Thread.Sleep(1000);
            }
        }

    }
}