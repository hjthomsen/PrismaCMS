using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrismaCMS.API.DTOs;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Application.Common.Mappings;
using PrismaCMS.Domain.Entities;
using PrismaCMS.Domain.ValueObjects;

namespace PrismaCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerCreateDto dto)
        {
            var contactInfo = new ContactInfo(
                dto.Email,
                dto.Phone,
                dto.Address,
                dto.City,
                dto.PostalCode,
                dto.Country
            );

            var customer = new Customer(dto.Name, dto.OrgNumber, contactInfo);

            await _customerRepository.AddAsync(customer);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.Id },
                _mapper.Map<CustomerDto>(customer)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            var contactInfo = new ContactInfo(
                dto.Email,
                dto.Phone,
                dto.Address,
                dto.City,
                dto.PostalCode,
                dto.Country
            );

            // Update the customer through domain methods
            customer.UpdateContactInfo(contactInfo);

            await _customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}