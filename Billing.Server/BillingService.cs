using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DDDesign.Core.NServiceBus;
using Microsoft.Extensions.Hosting;
using NLog;
using NServiceBus;

namespace Billing.Server
{
    public class BillingService : IHostedService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private IEndpointInstance endpointInstance;
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Logger.Info($"Starting Billing Service, Version: {version}");
            Logger.Info($"UtcNow: {DateTime.UtcNow}, Now: {DateTime.Now}, Culture: {Thread.CurrentThread.CurrentCulture}");

            endpointInstance = await new EndpointFactory(Endpoints.Billing).RunAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await endpointInstance.Stop();
        }
    }
}