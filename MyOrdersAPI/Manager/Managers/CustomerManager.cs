using System;
using MyOrdersAPI.Models;
using MyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.Repositories.IRepository;
using MMyOrdersAPI.Manager.IManagers;

namespace MyOrdersAPI.Manager.Managers
{ 
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _repo;

        public CustomerManager(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _repo.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await _repo.GetCustomerByIdAsync(customerId);
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            var result = await _repo.InsertCustomerAsync(customer);
            return result > 0;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var result = await _repo.UpdateCustomerAsync(customer);
            return result > 0;
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {
            var result = await _repo.DeleteCustomerAsync(customerId);
            return result > 0;
        }
    }

}

