using NServiceBus;

namespace Billing.Messages.Events
{
    public class PaymentRejected : IEvent
    {
        public string OrderId { get; set; }

        public PaymentRejected(string orderId)
        {
            OrderId = orderId;
        }
    }
}