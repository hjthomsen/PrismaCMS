using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialStatementsController : ControllerBase
    {
        private readonly IRepository<FinancialStatement> _financialStatementRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Assignment> _assignmentRepository;
        private readonly IMapper _mapper;

        public FinancialStatementsController(
            IRepository<FinancialStatement> financialStatementRepository,
            IRepository<Customer> customerRepository,
            IRepository<Assignment> assignmentRepository,
            IMapper mapper)
        {
            _financialStatementRepository = financialStatementRepository;
            _customerRepository = customerRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialStatementDto>>> GetFinancialStatements()
        {
            var financialStatements = await _financialStatementRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FinancialStatementDto>>(financialStatements));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialStatementDto>> GetFinancialStatement(int id)
        {
            var financialStatement = await _financialStatementRepository.GetByIdAsync(id);

            if (financialStatement == null)
                return NotFound();

            return Ok(_mapper.Map<FinancialStatementDto>(financialStatement));
        }

        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetFinancialStatementAssignments(int id)
        {
            var financialStatement = await _financialStatementRepository.GetByIdAsync(id);
            if (financialStatement == null)
                return NotFound("Financial statement not found");

            var assignments = await _assignmentRepository.GetAllAsync();
            var financialStatementAssignments = assignments.Where(a => a.FinancialStatementId == id);

            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(financialStatementAssignments));
        }

        [HttpPost]
        public async Task<ActionResult<FinancialStatementDto>> CreateFinancialStatement([FromBody] FinancialStatementCreateDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);

            if (customer == null)
                return BadRequest("Customer not found");

            var financialStatement = new FinancialStatement(
                dto.Year,
                customer
            );

            await _financialStatementRepository.AddAsync(financialStatement);

            return CreatedAtAction(
                nameof(GetFinancialStatement),
                new { id = financialStatement.Id },
                _mapper.Map<FinancialStatementDto>(financialStatement)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinancialStatement(int id, [FromBody] FinancialStatementUpdateDto dto)
        {
            var financialStatement = await _financialStatementRepository.GetByIdAsync(id);

            if (financialStatement == null)
                return NotFound();

            // Update through domain methods
            financialStatement.UpdateStatus(dto.Status);

            await _financialStatementRepository.UpdateAsync(financialStatement);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialStatement(int id)
        {
            var financialStatement = await _financialStatementRepository.GetByIdAsync(id);

            if (financialStatement == null)
                return NotFound();

            await _financialStatementRepository.DeleteAsync(financialStatement);

            return NoContent();
        }
    }
}