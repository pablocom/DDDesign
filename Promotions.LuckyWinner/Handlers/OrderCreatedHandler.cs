using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MassTransit;
using Microsoft.Extensions.Logging;

namespace Promotions.LuckyWinner.Handlers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ILogger<OrderCreatedConsumer> logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            logger.LogInformation($"Handling OrderCreated from Mass Transit");
            return Task.CompletedTask;
        }
    }
    
    public class OrderCreated
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