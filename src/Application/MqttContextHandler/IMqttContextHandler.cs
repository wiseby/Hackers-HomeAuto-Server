using System.Threading.Tasks;
using Application.Models;

namespace Application.MqttContextHandler
{
    public interface IMqttContextHandler
    {
        Task SaveContext(Context context);
    }
}