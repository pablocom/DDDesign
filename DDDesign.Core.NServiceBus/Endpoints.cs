using System.Collections.Generic;

namespace DDDesign.Core.NServiceBus
{
    public static class Endpoints
    {
        public const string Billing = "billing";
        public const string Sales = "sales";
        public const string Shipping = "shipping";
        public const string MessagingBridge = "messagingbridge";
        
        public static IEnumerable<string> All { get; } = new[]
        {
            Billing, Sales, Shipping, MessagingBridge
        };
    }
}