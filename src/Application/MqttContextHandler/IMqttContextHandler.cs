using System.Threading.Tasks;

namespace Application.MqttContextHandler
{
    public interface IMqttContextHandler
    {
        Task SaveContext(ContextModel context);
    }
}