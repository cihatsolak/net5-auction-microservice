using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands.OrderCreate;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ESourcing.Order.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection rabbitMQPersistentConnection, IMediator mediator, IMapper mapper)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection ?? throw new ArgumentNullException(nameof(rabbitMQPersistentConnection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Consume()
        {
            if (!_rabbitMQPersistentConnection.IsConnected)
            {
                _rabbitMQPersistentConnection.TryConnect();
            }

            var channel = _rabbitMQPersistentConnection.CreateModel(); //kanal oluşturdum

            //kuyruk oluşturdum.
            channel.QueueDeclare(
                queue: EventBusConstants.OrderCreateQueue, //kuyruk ismi
                durable: false, // mesajları false ise bellekte tutuyor, true ise fiziksel olarak rabbitmq sunucusunda tutuyor.
                exclusive: false, //true: tek bir consumer buraya connect olabilir, o consumerda silindiğinde connection kapanır.
                autoDelete: false, //true ise son subscriber da bağlantıyı keserse kuyruk silinir.
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstants.OrderCreateQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var orderCreateEvent = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

            if (e.RoutingKey.Equals(EventBusConstants.OrderCreateQueue))
            {
                var command = _mapper.Map<OrderCreateCommand>(orderCreateEvent);

                command.CreatedAt = DateTime.Now;
                command.TotalPrice = orderCreateEvent.Quantity * orderCreateEvent.Price;
                command.UnitPrice = orderCreateEvent.Price;

                await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _rabbitMQPersistentConnection.Dispose();
        }
    }
}
