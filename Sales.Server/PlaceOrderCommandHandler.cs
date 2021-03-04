using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Commands;

namespace Sales.Server
{
    public class PlaceOrderCommandHandler : IHandleMessages<PlaceOrderCommand>
    {
        private static ILog log = LogManager.GetLogger<PlaceOrderCommandHandler>();
        private static Random random = new Random();
        
        public Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.UserId}");

            return Task.CompletedTask;
        }
        
    }
}