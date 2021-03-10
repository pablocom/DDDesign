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
            log.Info($"Received PlaceOrder, OrderId = {message.UserId}");

            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);
                
            LogOrderCreatedInformation(message, orderId);
            
            var orderCreatedEvent = new OrderCreatedEvent
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductsIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now, // TODO: to make it testable do it through a IDateTimeProvider injecting dependency
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
    
    // this can be any database technology, it can differ between business components
    public static class Database
    {
        private static int _id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = _id++;
            return nextOrderId.ToString();
        }
    }
}