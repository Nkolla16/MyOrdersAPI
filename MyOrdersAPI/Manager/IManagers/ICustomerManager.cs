using System;
using MyOrdersAPI.Models;

namespace MMyOrdersAPI.Manager.IManagers
{
    public interface ICustomerManager
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}

