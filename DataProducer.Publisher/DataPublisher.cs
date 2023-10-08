using System;
using System.Text;
using System.Text.Json;
using RabbitMQ;
using RabbitMQ.Client;


namespace DataProducer.Publisher
{
    public class DataPublisher
    {
        public void DataSender(string jsonFile)
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

            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonFile);
            channel.BasicPublish(exchangeName, routeKey, null, jsonBytes);
            Thread.Sleep(1000);
        }

    }
}