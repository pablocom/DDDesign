using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Commands;
using Sales.Messages.Events;

namespace Sales.Server
{
    public class PlaceOrderCommandHandler : IHandleMessages<PlaceOrderCommand>
    {
        private static ILog log = LogManager.GetLogger<PlaceOrderCommandHandler>();
        
        public Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);
            LogOrderCreatedInformation(message, orderId);
            
            var orderCreatedEvent = new OrderCreated
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductsIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now,
                Amount = CalculateCostOf(message.ProductIds)
            };
            return context.Publish(orderCreatedEvent);
        }

        private double CalculateCostOf(IEnumerable<string> productIds)
        {
            return 1000;
        }

        private static void LogOrderCreatedInformation(PlaceOrderCommand message, string orderId)
        {
            log.Info(
                $"Order created, OrderId = {orderId} " +
                $"with shipping {message.ShippingTypeId} " +
                $"made by user {message.UserId}. {Environment.NewLine}" +
                $"products in order: {string.Join(',', message.ProductIds)}");
        }
    }
    
   
}