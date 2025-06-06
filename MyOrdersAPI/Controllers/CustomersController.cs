using Microsoft.AspNetCore.Mvc;
using MyOrdersAPI.Models;
using MyOrdersAPI.Repositories;
using MyOrdersAPI.Models;
using MyOrdersAPI.Manager.IManagers;
using System.Threading.Tasks;
using MMyOrdersAPI.Manager.IManagers;

namespace MyOrderAPI.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerManager _manager;

        public CustomersController(ICustomerManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("getallcustomers")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _manager.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet]
        [Route("getcustomerbyid")]
        public async Task<IActionResult> GetById(string customerId)
        {
            var customer = await _manager.GetCustomerByIdAsync(customerId);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        [Route("createcustomer")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _manager.CreateCustomerAsync(customer);
            if (!success) return StatusCode(500, "Could not create customer.");
            return CreatedAtAction(nameof(GetById), new { customerId = customer.CustomerId }, customer);
        }

        [HttpPut]
        [Route("updatecustomer")]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _manager.UpdateCustomerAsync(customer);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete]
        [Route("deletecustomer")]
        public async Task<IActionResult> Delete(string customerId)
        {
            var success = await _manager.DeleteCustomerAsync(customerId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
