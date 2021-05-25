using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MassTransit;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Promotions.LuckyWinner.Handlers
{
    public class OrderCreatedToNServiceBusBridge : IConsumer<OrderCreated>
    {
        private readonly ILogger<OrderCreatedToNServiceBusBridge> logger;
        private readonly IMessageBus messageBus;

        public OrderCreatedToNServiceBusBridge(ILogger<OrderCreatedToNServiceBusBridge> logger, IMessageBus messageBus)
        {
            this.logger = logger;
            this.messageBus = messageBus;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            logger.LogInformation($"Handling OrderCreated from Mass Transit");
            await messageBus.Publish(context.Message);
        }
    }
    
    public class OrderCreated : IMessage
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public List<string> ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Amount { get; set; }

        public OrderCreated()
        {
            
        }
        
        public OrderCreated(string orderId, string userId, List<string> productIds, string shippingTypeId, DateTime timeStamp, double amount)
        {
            OrderId = orderId;
            UserId = userId;
            ProductIds = productIds;
            ShippingTypeId = shippingTypeId;
            TimeStamp = timeStamp;
            Amount = amount;
        }
    }
}