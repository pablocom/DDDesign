using System;
using System.Threading.Tasks;
using NServiceBus;

namespace DDDesign.MessagingBridge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "MessagingBridge";

            var endpointConfiguration = new EndpointConfiguration("MessagingBridge");
            endpointConfiguration.Recoverability()
                .Immediate(
                    immediate => { immediate.NumberOfRetries(5); });

            _ = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}