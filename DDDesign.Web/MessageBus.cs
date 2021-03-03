using System.Threading.Tasks;
using NServiceBus;

namespace DDDesign.Web
{
    public class MessageBus : IMessageBus
    {
        private readonly IEndpointInstance endpointInstance;
        
        public MessageBus()
        {
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            endpointConfiguration.UseTransport<LearningTransport>();

            endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false).GetAwaiter().GetResult();    
        }

        public Task Send(IMessage message)
        {
            return endpointInstance.Send(message);
        }
    }
}