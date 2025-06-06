using System.Threading.Tasks;

namespace MyOrdersAPI.Manager.IManagers
{
    public interface IPushNotificationManager
    {
        Task<bool> RegisterTokenAsync(string token);
    }
}
