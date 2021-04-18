using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Events;

namespace Shipping.Server
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated>
    {
        private static readonly ILog Logger = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Logger.Info($"Received OrderCreated, OrderId = {message.OrderId}");

            var shippingOrder = new ShippingOrder(message.OrderId, message.UserId, message.ShippingTypeId);
            ShippingDatabase.Save(shippingOrder);
            
            return Task.CompletedTask;
        }
    }

    public static class ShippingDatabase
    {
        private static ICollection<ShippingOrder> Orders = new List<ShippingOrder>();
        
        public static void Save(ShippingOrder shippingOrder)
        {
            Orders.Add(shippingOrder);
        }

        public static string GetCustomerAddress(string orderId)
        {
            var order = Orders.Single(x => x.OrderId == orderId);
            return $"User: {order.UserId}, Abbey Road 82";
        }
    }
    
    public class ShippingOrder
    {
        public string OrderId { get; private set; }
        public string UserId { get; private set; }
        public string ShippingTypeId { get; private set; }
        
        public ShippingOrder(string orderId, string userId, string shippingTypeId)
        {
            OrderId = orderId;
            UserId = userId;
            ShippingTypeId = shippingTypeId;
        }
    }
}