using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly IRepository<TimeEntry> _timeEntryRepository;
        private readonly IRepository<Assignment> _assignmentRepository;
        private readonly IMapper _mapper;

        public TimeEntriesController(
            IRepository<TimeEntry> timeEntryRepository,
            IRepository<Assignment> assignmentRepository,
            IMapper mapper)
        {
            _timeEntryRepository = timeEntryRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetTimeEntries()
        {
            var timeEntries = await _timeEntryRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TimeEntryDto>>(timeEntries));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryDto>> GetTimeEntry(int id)
        {
            var timeEntry = await _timeEntryRepository.GetByIdAsync(id);

            if (timeEntry == null)
                return NotFound();

            return Ok(_mapper.Map<TimeEntryDto>(timeEntry));
        }

        [HttpPost]
        public async Task<ActionResult<TimeEntryDto>> CreateTimeEntry([FromBody] TimeEntryCreateDto dto)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(dto.AssignmentId);

            if (assignment == null)
                return BadRequest("Assignment not found");

            var timeEntry = new TimeEntry(
                assignment,
                dto.Date,
                dto.HoursWorked,
                dto.Description
            );

            await _timeEntryRepository.AddAsync(timeEntry);

            return CreatedAtAction(
                nameof(GetTimeEntry),
                new { id = timeEntry.Id },
                _mapper.Map<TimeEntryDto>(timeEntry)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeEntry(int id, [FromBody] TimeEntryUpdateDto dto)
        {
            var timeEntry = await _timeEntryRepository.GetByIdAsync(id);

            if (timeEntry == null)
                return NotFound();

            // Update through domain methods
            timeEntry.UpdateHours(dto.HoursWorked);
            timeEntry.UpdateDescription(dto.Description);

            await _timeEntryRepository.UpdateAsync(timeEntry);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeEntry(int id)
        {
            var timeEntry = await _timeEntryRepository.GetByIdAsync(id);

            if (timeEntry == null)
                return NotFound();

            await _timeEntryRepository.DeleteAsync(timeEntry);

            return NoContent();
        }
    }
}