﻿using BossServer.Models;
using BossServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BossServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customerService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer newCustomer)
        {
            await _customerService.CreateAsync(newCustomer);

            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Customer updatedCustomer)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            updatedCustomer.Id = customer.Id;

            await _customerService.UpdateAsync(id, updatedCustomer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.RemoveAsync(id);

            return NoContent();
        }
    }

}
