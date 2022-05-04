using RabbitMQ.Client;
namespace GBM.Challenge.OpenTransactios.Infrastructure.Events.RabbitMQ
{
    using GBM.Challenge.OpenTransactios.Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="PublishEvent" />.
    /// </summary>
    public class PublishEvent : IPublishEvent
    {
        /// <summary>
        /// Defines the _configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishEvent"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public PublishEvent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// The Publish.
        /// </summary>
        /// <param name="@event">The event<see cref="object"/>.</param>
        public void Publish(object @event)
        {
            const string broker = "gbm-direct-exchange";
            string host = _configuration.GetValue<string>("rabbitMQHost");
            var factory = new ConnectionFactory() { Uri = new Uri(host) };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: broker,
                type: "direct");
                string message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(broker,
                 "transations.init",
                 null,
                 body);
            }
        }
    }
}
