using MyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.Models;
using MyOrdersAPI.Repositories.IRepositories;
using MyOrdersAPI.Repositories.IRepository;
using MyOrdersAPI.Services;

public class OrderManager : IOrderManager
{
    private readonly IOrderRepository _repo;
    private readonly IPushNotificationService _pushService;
    private readonly IPushNotificationRepository _pushRepo;

    public OrderManager(
        IOrderRepository repo,
        IPushNotificationService pushService,
        IPushNotificationRepository pushRepo)
    {
        _repo = repo;
        _pushService = pushService;
        _pushRepo = pushRepo;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
    {
        return await _repo.GetOrdersByCustomerIdAsync(customerId);
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _repo.GetOrderByIdAsync(orderId);
    }

    public async Task<int> CreateOrderAsync(CreateOrderRequestModel createOrderRequestModel)
    {
        int newOrderId = await _repo.CreateOrderAsync(createOrderRequestModel);

        if (newOrderId > 0)
        {
            string token = await _pushRepo.GetLatestTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                var notification = new PushNotificationRequest
                {
                    DeviceToken = token,
                    Title = "New Order Created",
                    Body = $"Order #{newOrderId} for Customer {createOrderRequestModel.CustomerID} has been created."
                };

                await _pushService.SendNotificationAsync(notification);
            }
        }

        return newOrderId;
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        var result = await _repo.UpdateOrderAsync(order);
        return result > 0;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var result = await _repo.DeleteOrderAsync(orderId);
        return result > 0;
    }
}
