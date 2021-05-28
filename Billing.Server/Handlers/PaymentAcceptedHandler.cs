using System.Threading.Tasks;
using Billing.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing.Server.Handlers
{
    public class PaymentAcceptedHandler : IHandleMessages<PaymentAccepted>
    {
        private static readonly ILog Log = LogManager.GetLogger<OrderCreatedHandler>();

        public async Task Handle(PaymentAccepted message, IMessageHandlerContext context)
        {
            Log.Info($"Received PaymentAccepted, OrderId = {message.OrderId}");
        }
    }
}