using NServiceBus;
using System.Threading.Tasks;
using Promotions.LuckyWinner.Handlers;

namespace Promotions.LuckyWinner
{
    public class MessageBus : IMessageBus
    {
        private readonly IEndpointInstance endpointInstance;

        public MessageBus()
        {
            var endpointConfiguration = new EndpointConfiguration("LuckyWinner");
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.SendOnly();

            endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task Send(IMessage message)
        {
            await endpointInstance.Send(message);
        }

        public async Task Publish(IMessage message)
        {
            await endpointInstance.Publish(message);
        }
    }

    public interface IMessageBus
    {
        Task Send(IMessage message);
        Task Publish(IMessage message);
    }
}
