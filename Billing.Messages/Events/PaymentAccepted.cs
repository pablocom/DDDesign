using NServiceBus;

namespace Billing.Messages.Events
{
    public class PaymentAccepted : IEvent
    {
        public string OrderId { get; set; }

        public PaymentAccepted(string orderId)
        {
            OrderId = orderId;
        }
    }
}