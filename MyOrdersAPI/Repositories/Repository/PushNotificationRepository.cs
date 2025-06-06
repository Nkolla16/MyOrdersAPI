using System.Threading.Tasks;
using MyOrdersAPI.Repositories.IRepositories;
using MyOrdersAPI.DataService;

namespace MyOrdersAPI.Repositories
{
    public class PushNotificationRepository : IPushNotificationRepository
    {
        private readonly IDataService _dataService;

        public PushNotificationRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<bool> SaveTokenAsync(string token)
        {
            var parameters = new { Token = token };

            int result = await _dataService.ExecuteAsync("PushNotificationToken", parameters);

            return result == -1 || result > 0;
        }

        public async Task<string> GetLatestTokenAsync()
        {
            var tokens = await _dataService.QueryRawSqlAsync<string>(
                "SELECT Token FROM PushNotificationTokens WHERE IsToken_Active = 1"
            );

            return tokens.FirstOrDefault();
        }
    }
}
