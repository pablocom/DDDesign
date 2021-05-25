using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Promotions.LuckyWinner
{
    public class NServiceBusHandlerWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var endpointConfiguration = new EndpointConfiguration("MessagingBridge");
            endpointConfiguration.Recoverability()
                .Immediate(immediate => { immediate.NumberOfRetries(5); });
            _ = endpointConfiguration.UseTransport<LearningTransport>();

            await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        }
    }
}