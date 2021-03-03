using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class PlaceOrderCommand : ICommand
    {
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}