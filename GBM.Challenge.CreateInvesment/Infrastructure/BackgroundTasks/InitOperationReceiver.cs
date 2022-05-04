namespace GBM.Challenge.API.CreateInvesment.Infrastructure.BackgroundTasks
{
    using GBM.Challenge.API.CreateInvesment.Application.Interfaces;
    using GBM.Challenge.API.CreateInvesment.Application.Investment.Queries;
    using GBM.Challenge.API.CreateInvesment.Domain.Events.OpenOperations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="InitOperationReceiver" />.
    /// </summary>
    public class InitOperationReceiver : BackgroundService
    {
        /// <summary>
        /// Defines the _factory.
        /// </summary>
        private ConnectionFactory _factory;

        /// <summary>
        /// Defines the _connection.
        /// </summary>
        private IConnection _connection;

        /// <summary>
        /// Defines the _channel.
        /// </summary>
        private IModel _channel;

        /// <summary>
        /// Defines the _configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IServiceProvider _sp;
        /// <summary>
        /// Initializes a new instance of the <see cref="InitOperationReceiver"/> class.
        /// </summary>
        /// <param name="getInvesmentInfoQuery">The getInvesmentInfoQuery<see cref="IGetInvesmentInfoQuery"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public InitOperationReceiver(IServiceProvider sp)
        {
            _sp = sp;
            using (var scope = _sp.CreateScope())
            {
                _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            }
            const string broker = "gbm-direct-exchange";
            string host = _configuration.GetValue<string>("rabbitMQHost");
            _factory = new ConnectionFactory() { Uri = new Uri(host) };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(broker,ExchangeType.Direct);
            _channel.QueueDeclare("direct-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind("direct-queue", broker, "transations.init");
        }

        /// <summary>
        /// The ExecuteAsync.
        /// </summary>
        /// <param name="stoppingToken">The stoppingToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received +=  Consumer_Received;
            _channel.BasicConsume("direct-queue",true,consumer);
            return Task.CompletedTask;
        }

        public  void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            IGetInvesmentInfoQuery getInvesmentInfoQuery;
            IPublishEvent publishEvent;
            using (var scope = _sp.CreateScope())
            {
                getInvesmentInfoQuery = scope.ServiceProvider.GetRequiredService<IGetInvesmentInfoQuery>();
                publishEvent = scope.ServiceProvider.GetRequiredService<IPublishEvent>();
            }
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            OpenOperationEventInfo @event = JsonConvert.DeserializeObject<OpenOperationEventInfo>(message);
            InvestmentInfoQueryModel info = getInvesmentInfoQuery.Get(@event.InvesmentId);
            publishEvent.Publish(info);
        }


    }
}
