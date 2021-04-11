using System.Threading.Tasks;
using Billing.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales.Server
{
    public class PaymentRejectedHandler : IHandleMessages<PaymentRejected>
    {
        private static readonly ILog Log = LogManager.GetLogger<PaymentRejectedHandler>();

        public async Task Handle(PaymentRejected message, IMessageHandlerContext context)
        {
            Log.Info($"Received PaymentRejected, OrderId = {message.OrderId}");
        }
    }
}