using System.Threading.Tasks;
using NServiceBus;

namespace DDDesign.Web
{
    public interface IMessageBus
    {
        Task Send(IMessage message);
    }
}