namespace GMB.Challenge.Console.InvestmentUpdater
{
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the _factory.
        /// </summary>
        private static ConnectionFactory _factory;

        /// <summary>
        /// Defines the _connection.
        /// </summary>
        private static IConnection _connection;

        /// <summary>
        /// Defines the _channel.
        /// </summary>
        private static IModel _channel;

        private static UpdateInvesmentCmd update;
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        static void Main(string[] args)
        {
            const string broker = "gbm-direct-exchange";
            string host = args[0];
            _factory = new ConnectionFactory() { Uri = new Uri(host) };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(broker, ExchangeType.Direct);
            _channel.QueueDeclare("transations-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind("transations-queue", broker, "transations.update");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume("transations-queue", true, consumer);
            update = new UpdateInvesmentCmd();
            Console.ReadKey();
        }

        /// <summary>
        /// The Consumer_Received.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="BasicDeliverEventArgs"/>.</param>
        static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine("INIT EVENT \n");
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            InvestmentEventInfo info = JsonConvert.DeserializeObject<InvestmentEventInfo>(message);
            Console.WriteLine(message);
            update.Execute(info);
            Console.WriteLine("END EVENT \n");
        }
    }
}
