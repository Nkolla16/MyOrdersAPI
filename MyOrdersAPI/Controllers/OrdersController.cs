using System;
using Microsoft.AspNetCore.Mvc;
using MyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.Models;

namespace MyOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _manager;

        public OrdersController(IOrderManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("getordersbycustomerid")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var orders = await _manager.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpGet]
        [Route("getorderbyid")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _manager.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        [Route("createneworder")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestModel createOrderRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int newOrderId = await _manager.CreateOrderAsync(createOrderRequestModel);
            if (newOrderId <= 0) return StatusCode(500, "Could not create order.");

            return CreatedAtAction(nameof(GetById), new { orderId = newOrderId }, new { OrderId = newOrderId });
        }


        [HttpPut]
        [Route("updateorder")]
        public async Task<IActionResult> Update([FromBody] Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _manager.UpdateOrderAsync(order);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete]
        [Route("deleteorder")]
        public async Task<IActionResult> Delete(int orderId)
        {
            var success = await _manager.DeleteOrderAsync(orderId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

