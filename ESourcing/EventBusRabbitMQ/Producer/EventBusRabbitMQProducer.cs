using EventBusRabbitMQ.Events.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {
        #region Fields
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection;
        private readonly ILogger<EventBusRabbitMQProducer> _logger;
        private readonly int _retryCount;
        #endregion

        #region Ctor
        public EventBusRabbitMQProducer(
            IRabbitMQPersistentConnection rabbitMQPersistentConnection,
            ILogger<EventBusRabbitMQProducer> logger,
            int retryCount)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection;
            _logger = logger;
            _retryCount = retryCount;
        }
        #endregion

        #region Methods
        public void Publish(string queueName, IEvent @event)
        {
            if (!_rabbitMQPersistentConnection.IsConnected)
            {
                _rabbitMQPersistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.RequestId, $"{time.TotalSeconds:n1}", ex.Message);
            });

            using var channel = _rabbitMQPersistentConnection.CreateModel(); //kanal oluşturdum

            //kuyruk oluşturdum.
            channel.QueueDeclare(
                queue: queueName, //kuyruk ismi
                durable: false, // mesajları false ise bellekte tutuyor, true ise fiziksel olarak rabbitmq sunucusunda tutuyor.
                exclusive: false, //true: tek bir consumer buraya connect olabilir, o consumerda silindiğinde connection kapanır.
                autoDelete: false, //true ise son subscriber da bağlantıyı keserse kuyruk silinir.
                arguments: null
                );

            string message = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(message);

            //publish işlemini bir kere deneyip bırakmaması için retry count kadar denemeli.
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMQ");
                    //implement ack handle
                };
            });
        }
        #endregion
    }
}
