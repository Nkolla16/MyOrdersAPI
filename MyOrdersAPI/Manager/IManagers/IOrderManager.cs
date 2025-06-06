using System;
using MyOrdersAPI.Models;

namespace MyOrdersAPI .Manager.IManagers
{
    public interface IOrderManager
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<int> CreateOrderAsync(CreateOrderRequestModel createOrderRequestModel);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}

