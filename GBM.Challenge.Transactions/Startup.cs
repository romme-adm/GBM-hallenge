namespace GBM.Challenge.Transactions
{
    using GBM.Challenge.Transactions.Application.Interfaces.Integrity;
    using GBM.Challenge.Transactions.Application.Interfaces.Repositories;
    using GBM.Challenge.Transactions.Application.Investment.Commands.PersistAccountInfo;
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders;
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders.Buy;
    using GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders.Sell;
    using GBM.Challenge.Transactions.Application.Investment.Queries.GetCurrentBalance;
    using GBM.Challenge.Transactions.Infrastructure.BackgroundTasks;
    using GBM.Challenge.Transactions.Infrastructure.Integrity;
    using GBM.Challenge.Transactions.Repositories.RedisPersister;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System.Collections.Generic;
    using static GBM.Challenge.Transactions.Application.Investment.Commands.SendOrders.OrderFactory;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GBM.Challenge.Transactions", Version = "v1" });
            });
            /*services*/
            services.AddTransient<BuyOrder>();
            services.AddTransient<SellOrder>();
            /*factory orders*/
            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "BUY":
                        return serviceProvider.GetService<BuyOrder>();
                    case "SELL":
                        return serviceProvider.GetService<SellOrder>();
                    default:
                        throw new KeyNotFoundException(); //
                }
            });
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<IGetCurrentBalance, GetCurrentBalance>();
            services.AddScoped<IDoTransactionCmd, DoTransactionCmd>();
            
            /*hosted service*/
            services.AddHostedService<InitOperationReceiver>();
            /*Redis config*/
            services.AddSingleton<IRedisCacheRepository, RedisCacheRepository>();
            services.AddScoped<IPersistInvestmentAccountCmd, PersistInvestmentAccountCmd>();
            /*Regis conn*/
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration.GetValue<string>("redisHost")}";
            });
            /*infra*/
            services.AddScoped<IGenerateIntegrity, GenerateIntegrity>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GBM.Challenge.Transactions v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
