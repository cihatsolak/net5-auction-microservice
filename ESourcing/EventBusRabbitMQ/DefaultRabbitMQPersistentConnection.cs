using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        #region Fields and Properties
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private readonly int _retryCount;

        private IConnection _connection;
        private bool _disposed;
        #endregion

        #region Ctor
        public DefaultRabbitMQPersistentConnection(
            IConnectionFactory connectionFactory,
            ILogger<DefaultRabbitMQPersistentConnection> logger,
            int retryCount)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _retryCount = retryCount;
        }
        #endregion

        #region Methods and Properties
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected) throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect.");

            //SocketException veya BrokerUnreachableException hatası alırsan belirli koşullarda _retryCount kadar dene. (Polly framework)
            var policy = RetryPolicy.Handle<SocketException>()
                                    .Or<BrokerUnreachableException>()
                                    .WaitAndRetry(_retryCount, sleepDurationProvider => TimeSpan.FromSeconds(Math.Pow(2, sleepDurationProvider)), (exception, timeSpan) =>
                                    {
                                        _logger.LogWarning(exception, "RabbitMQ client could not connect after {TimeOut}s ({ExceptionMessage})", $"{timeSpan.TotalSeconds}:n1", exception.Message);
                                    });

            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");
                return false;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            try
            {
                _connection.Dispose();
            }
            catch (IOException exception)
            {
                _logger.LogCritical(exception.ToString());
            }
            finally
            {
                _disposed = true;
            }
        }
        #endregion

        #region RabbitMQ EventHandler
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");
            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");
            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");
            TryConnect();
        }
        #endregion
    }
}
