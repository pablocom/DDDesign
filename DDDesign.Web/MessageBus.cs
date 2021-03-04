using System.Threading.Tasks;
using NServiceBus;
using Sales.Messages.Commands;

namespace DDDesign.Web
{
    public class MessageBus : IMessageBus
    {
        private readonly IEndpointInstance endpointInstance;
        
        public MessageBus()
        {
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrderCommand), "Sales");
            
            endpointConfiguration.SendOnly();

            endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false).GetAwaiter().GetResult();    
            
            
        }

        public Task Send(IMessage message)
        {
            return endpointInstance.Send(message);
        }
    }
}