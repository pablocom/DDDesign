using System;
using System.Threading.Tasks;
using Billing.Messages.Commands;
using Billing.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Events;

namespace Billing.Server.Handlers
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated>
    {
        private static readonly ILog Log = LogManager.GetLogger<OrderCreatedHandler>();
        
        public async Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Log.Info($"Received OrderCreated, OrderId = {message.OrderId}");
            
            var cardDetails = Database.GetCardDetailsFor(message.UserId);
            var paymentConfirmation = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);

            if (paymentConfirmation.Status == PaymentStatus.Rejected)
            {   
                await context.Publish(new PaymentRejected(message.OrderId));
                return;
            }

            await context.SendLocal(new RecordPaymentAttempt
            {
                OrderId = message.OrderId,
                Status = paymentConfirmation.Status
            });
        }
    }

    public static class PaymentProvider
    {
        public static PaymentConfirmation ChargeCreditCard(object cardDetails, double messageAmount)
        {
            var paymentProviderIsUnavailable = new Random().Next(1, 5) == 1;
            if (paymentProviderIsUnavailable) 
                throw new Exception($"{nameof(PaymentProvider)} is unavailable");


            var paymentIsRejected = new Random().Next(1, 5) == 1;
            return paymentIsRejected
                ? new PaymentConfirmation {Status = PaymentStatus.Rejected}
                : new PaymentConfirmation {Status = PaymentStatus.Accepted};
        }
    }
    public class PaymentConfirmation
    {
        public PaymentStatus Status { get; set; }
    }
    public static class Database
    {
        public static object GetCardDetailsFor(string userId)
        {
            return new CardDetails();
        }
    }
    public class CardDetails
    {
    }
}