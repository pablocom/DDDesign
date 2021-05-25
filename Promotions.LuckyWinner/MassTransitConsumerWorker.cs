using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Promotions.LuckyWinner.Handlers;

namespace Promotions.LuckyWinner
{
    public class MassTransitConsumerWorker : BackgroundService
    {
        private readonly IBus _bus;

        public MassTransitConsumerWorker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new OrderCreated(), stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}