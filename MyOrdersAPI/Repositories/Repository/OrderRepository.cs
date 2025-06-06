using System;
using MyOrdersAPI.Models;
using MyOrdersAPI.Repositories.IRepository;
using MyOrdersAPI.DataService;

namespace MyOrdersAPI.Repositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataService _dataService;

        public OrderRepository(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _dataService.QueryAsync<Order>("GetOrdersByCustomerId", new { CustomerId = customerId });
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var result = await _dataService.QueryAsync<Order>("GetOrderById", new { OrderId = orderId });
            return result.FirstOrDefault();
        }

        public async Task<int> CreateOrderAsync(CreateOrderRequestModel createOrderRequestModel)
        {
            return await _dataService.ExecuteScalarAsync("InsertNewOrder", createOrderRequestModel);
        }

        public async Task<int> UpdateOrderAsync(Order order)
        {
            return await _dataService.ExecuteAsync("UpdateOrder", order);
        }

        public async Task<int> DeleteOrderAsync(int orderId)
        {
            return await _dataService.ExecuteAsync("DeleteOrder", new { OrderId = orderId });
        }

    }
}

