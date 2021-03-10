using System;
using System.Collections.Generic;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class OrderCreatedEvent : IEvent
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<string> ProductsIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Amount { get; set; }
    }
}