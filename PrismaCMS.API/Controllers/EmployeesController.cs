using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Assignment> _assignmentRepository;
        private readonly IMapper _mapper;

        public EmployeesController(
            IRepository<Employee> employeeRepository,
            IRepository<Assignment> assignmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetEmployeeAssignments(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound("Employee not found");

            var assignments = await _assignmentRepository.GetAllAsync();
            var employeeAssignments = assignments.Where(a => a.EmployeeId == id);

            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(employeeAssignments));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeCreateDto dto)
        {
            var employee = new Employee(dto.Name, dto.Email, dto.Role);

            await _employeeRepository.AddAsync(employee);

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.Id },
                _mapper.Map<EmployeeDto>(employee)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            // Update through domain methods
            employee.UpdateRole(dto.Role);

            await _employeeRepository.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            await _employeeRepository.DeleteAsync(employee);

            return NoContent();
        }
    }
}