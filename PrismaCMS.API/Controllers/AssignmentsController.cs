using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IRepository<Assignment> _assignmentRepository;
        private readonly IRepository<FinancialStatement> _financialStatementRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<TimeEntry> _timeEntryRepository;
        private readonly IMapper _mapper;

        public AssignmentsController(
            IRepository<Assignment> assignmentRepository,
            IRepository<FinancialStatement> financialStatementRepository,
            IRepository<Employee> employeeRepository,
            IRepository<TimeEntry> timeEntryRepository,
            IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _financialStatementRepository = financialStatementRepository;
            _employeeRepository = employeeRepository;
            _timeEntryRepository = timeEntryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments()
        {
            var assignments = await _assignmentRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(assignments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDto>> GetAssignment(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);

            if (assignment == null)
                return NotFound();

            return Ok(_mapper.Map<AssignmentDto>(assignment));
        }

        [HttpGet("{id}/timeentries")]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetAssignmentTimeEntries(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);
            if (assignment == null)
                return NotFound("Assignment not found");

            var timeEntries = await _timeEntryRepository.GetAllAsync();
            var assignmentTimeEntries = timeEntries.Where(te => te.AssignmentId == id);

            return Ok(_mapper.Map<IEnumerable<TimeEntryDto>>(assignmentTimeEntries));
        }

        [HttpPost]
        public async Task<ActionResult<AssignmentDto>> CreateAssignment([FromBody] AssignmentCreateDto dto)
        {
            var financialStatement = await _financialStatementRepository.GetByIdAsync(dto.FinancialStatementId);
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (financialStatement == null)
                return BadRequest("Financial statement not found");

            if (employee == null)
                return BadRequest("Employee not found");

            var assignment = new Assignment(
                financialStatement,
                employee,
                dto.AllocatedHours
            );

            await _assignmentRepository.AddAsync(assignment);

            return CreatedAtAction(
                nameof(GetAssignment),
                new { id = assignment.Id },
                _mapper.Map<AssignmentDto>(assignment)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] AssignmentUpdateDto dto)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);

            if (assignment == null)
                return NotFound();

            // Update through domain methods
            assignment.UpdateAllocatedHours(dto.AllocatedHours);

            await _assignmentRepository.UpdateAsync(assignment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);

            if (assignment == null)
                return NotFound();

            await _assignmentRepository.DeleteAsync(assignment);

            return NoContent();
        }
    }
}