using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Promotions.LuckyWinner.Handlers;

namespace Promotions.LuckyWinner
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
            services.AddHealthEndpoints();
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(OrderCreatedToNServiceBusBridge).Assembly);

                x.UsingInMemory((context,cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService(true);
            services.AddHostedService<MassTransitConsumerWorker>();
            services.AddHostedService<NServiceBusHandlerWorker>();
            services.AddSingleton<IMessageBus, MessageBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthEndpoint();
        }
    }
}