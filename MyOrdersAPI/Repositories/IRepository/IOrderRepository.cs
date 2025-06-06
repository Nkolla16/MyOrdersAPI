using System;
using MyOrdersAPI.Models;

namespace MyOrdersAPI.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<int> CreateOrderAsync(CreateOrderRequestModel createOrderRequestModel);
        Task<int> UpdateOrderAsync(Order order);
        Task<int> DeleteOrderAsync(int orderId);
    }
}
