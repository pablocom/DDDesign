using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Events;

namespace Billing.Server
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated>
    {
        private static ILog log = LogManager.GetLogger<OrderCreatedHandler>();
        
        public async Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderCreated, OrderId = {message.OrderId}");
        }
    }
}