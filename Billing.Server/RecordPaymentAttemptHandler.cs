using System;
using System.Threading.Tasks;
using Billing.Messages.Commands;
using Billing.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing.Server
{
    public class RecordPaymentAttemptHandler : IHandleMessages<RecordPaymentAttempt>
    {
        private static readonly ILog Log = LogManager.GetLogger<RecordPaymentAttemptHandler>();

        public async Task Handle(RecordPaymentAttempt message, IMessageHandlerContext context)
        {
            Log.Info($"Received RecordPaymentAttempt, OrderId = {message.OrderId}");
            
            Database.SavePaymentAttempt(message.OrderId, message.Status);

            if (message.Status == PaymentStatus.Accepted) 
                await context.Publish(new PaymentAccepted(message.OrderId));
        }

        private static class Database
        {
            public static void SavePaymentAttempt(string orderId, PaymentStatus status)
            {
                // .. save it to your favorite database
                
                var databaseIsUnavailable = new Random().Next(1, 5) == 1;
                if (databaseIsUnavailable) 
                    throw new Exception($"{nameof(Database)} is unavailable");
            }
        }
    }
}