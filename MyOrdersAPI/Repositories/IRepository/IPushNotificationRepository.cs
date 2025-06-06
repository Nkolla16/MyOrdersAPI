using System.Threading.Tasks;

namespace MyOrdersAPI.Repositories.IRepositories
{
    public interface IPushNotificationRepository
    {
        Task<bool> SaveTokenAsync(string token);
        Task<string> GetLatestTokenAsync();
    }
}
