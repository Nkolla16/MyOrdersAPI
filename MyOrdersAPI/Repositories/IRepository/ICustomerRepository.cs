using System;
using MyOrdersAPI.Models;

namespace MyOrdersAPI.Repositories.IRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<int> InsertCustomerAsync(Customer customer);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(string customerId);
    }
}

