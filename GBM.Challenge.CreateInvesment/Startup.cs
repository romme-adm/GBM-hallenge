using GBM.Challenge.API.CreateInvesment.Application.Interfaces;
using GBM.Challenge.API.CreateInvesment.Application.Interfaces.DataBase;
using GBM.Challenge.API.CreateInvesment.Application.Interfaces.Repositories;
using GBM.Challenge.API.CreateInvesment.Application.Investment.Commands;
using GBM.Challenge.API.CreateInvesment.Application.Investment.Queries;
using GBM.Challenge.API.CreateInvesment.Infrastructure.BackgroundTasks;
using GBM.Challenge.API.CreateInvesment.Infrastructure.Events.RabbitMQ;
using GBM.Challenge.API.CreateInvesment.Persistence.Database;
using GBM.Challenge.API.CreateInvesment.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GBM.Challenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GBM.Challenge.API.CreateInvestment", Version = "v1" });
            });
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<ICreateInvestmentCmd, CreateInvestmentCmd>();
            services.AddScoped<IGetInvesmentInfoQuery, GetInvestmentInfoQuery>();
            services.AddScoped<IPublishEvent, PublishEvent>();
            services.AddHostedService<InitOperationReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GBM.Challenge.CreateInvestment v1"));
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
