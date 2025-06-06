using System;
using MyOrdersAPI.Models;
using MyOrdersAPI.Repositories.IRepository;
using MyOrdersAPI.DataService;

namespace MyOrdersAPI.Repositories.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataService _dataService;

        public CustomerRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dataService.QueryAsync<Customer>("GetAllCustomers");
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            var result = await _dataService.QueryAsync<Customer>("GetCustomerById", new { CustomerId = customerId });
            return result.FirstOrDefault();
        }

        public async Task<int> InsertCustomerAsync(Customer customer)
        {
            return await _dataService.ExecuteAsync("InsertCustomer", customer);
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            return await _dataService.ExecuteAsync("UpdateCustomer", customer);
        }

        public async Task<int> DeleteCustomerAsync(string customerId)
        {
            return await _dataService.ExecuteAsync("DeleteCustomer", new { CustomerId = customerId });
        }
    }
}

