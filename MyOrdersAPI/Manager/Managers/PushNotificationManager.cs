using MyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.Repositories.IRepositories;
using System.Threading.Tasks;

namespace MyOrdersAPI.Manager
{
    public class PushNotificationManager : IPushNotificationManager
    {
        private readonly IPushNotificationRepository _repository;

        public PushNotificationManager(IPushNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RegisterTokenAsync(string token)
        {
            return await _repository.SaveTokenAsync(token);
        }
    }
}
