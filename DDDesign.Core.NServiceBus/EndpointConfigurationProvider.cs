using System.Threading.Tasks;
using NServiceBus;

namespace DDDesign.Core.NServiceBus
{
    public class EndpointFactory
    {
        private readonly string endpointName;

        public EndpointFactory(string endpointName)
        {
            this.endpointName = endpointName;
        }
        
        public async Task<IEndpointInstance> RunAsync()
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            
            // for example configure bucket for large messages
            // TODO: move to another transport optimized for production purposes
            endpointConfiguration.UseTransport<LearningTransport>();

            return await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        }
    }
}