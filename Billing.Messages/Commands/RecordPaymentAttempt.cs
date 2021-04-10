using NServiceBus;

namespace Billing.Messages.Commands
{
    public class RecordPaymentAttempt : ICommand
    {
        public string OrderId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}