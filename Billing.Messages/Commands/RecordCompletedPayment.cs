using NServiceBus;

namespace Billing.Messages.Commands
{
    public class RecordCompletedPayment : IEvent
    {
        public string OrderId { get; set; }
        public PaymentStatus Status { get; set; }
    }
    
    public enum PaymentStatus
    {
        Accepted,
        Rejected
    }
}