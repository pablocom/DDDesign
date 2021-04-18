using System.Threading.Tasks;
using Billing.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping.Server
{
    public class PaymentAcceptedHandler : IHandleMessages<PaymentAccepted>
    {
        private static readonly ILog Logger = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(PaymentAccepted message, IMessageHandlerContext context)
        {
            var customerAddress = ShippingDatabase.GetCustomerAddress(message.OrderId);
            
            Logger.Info($"Arranging shipping for order {message.OrderId} for address {customerAddress}");
            
            return Task.CompletedTask;
        }
    }
}