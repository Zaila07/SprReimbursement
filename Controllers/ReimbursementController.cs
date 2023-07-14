using Azure.AI.FormRecognizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.Business.DataTransferObject;
using SprEmployeeReimbursement.Business.ServiceCollection;
using SprEmployeeReimbursement.DataAccess.SprDbContext;
using SprEmployeeReimbursement.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SprEmployeeReimbursement.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReimbursementService _reimbursementService;
        private readonly FormRecognizerClient _formRecognizerClient;
        private readonly SprReimbursementDbContext _context;
        private readonly ILogger<ReimbursementController> _logger;

        public ReimbursementController(IConfiguration configuration, FormRecognizerClient formRecognizerClient,
            SprReimbursementDbContext context, IReimbursementService reimbursementService, ILogger<ReimbursementController> logger)
        {
            _context = context;
            _configuration = configuration;
            _formRecognizerClient = formRecognizerClient;
            _reimbursementService = reimbursementService;
            _logger = logger;
        }

        [ProducesResponseType(typeof(decimal), statusCode: StatusCodes.Status200OK)]
        [MapToApiVersion("1")]
        [HttpPost]
        public async Task<IActionResult> ProcessMultipleReceiptsReimbursement([FromForm] MultipleReceiptsReimbursementDto input)
        {
            var result = await _reimbursementService.ProcessMultipleReceiptsReimbursement(input);
            return Created($"api/v1/Reimbursement/{result}", result);
        }

        [ProducesResponseType(typeof(ReimbursementModel), statusCode: StatusCodes.Status200OK)]
        [HttpGet("hr/{id}")]
        public async Task<IActionResult> GetReimbursementForHR(int id)
        {
            return Ok(await _context.ReimbursementModels.FindAsync(id));
        }

        [ProducesResponseType(typeof(List<ReimbursementModel>), statusCode: StatusCodes.Status200OK)]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllReimbursements()
        {
            return Ok(await _reimbursementService.GetAllReimbursements());
        }

        [ProducesResponseType(typeof(ReimbursementModel), statusCode: StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReimbursement(int id, [FromBody] ReimbursementUpdateDto reimbursementDto)
        {
            await _reimbursementService.UpdateReimbursement(id, reimbursementDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int),statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteReimbursement(int id)
        {
            var deletedReimbursement = await _reimbursementService.DeleteReimbursement(id);
            return Ok(deletedReimbursement);
        }

        [HttpPost("hr/approve/{id}")]
        [ProducesResponseType(typeof(ReimbursementModel), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult>ApproveReimbursement(int id)
        {
            var reimbursement = await _reimbursementService.ApproveReimbursement(id);
            return Ok(reimbursement);
        }
        [HttpPost("hr/disapprove/{id}")]
        [ProducesResponseType(typeof(ReimbursementModel), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> DisapproveReimbursement(int id, [FromBody] string reason)
        {
            var reimbursement = await _reimbursementService.DisapproveReimbursement(id,reason);
            return Ok(reimbursement);

        }
        [HttpGet("hr/total/employee/reimbursements/{id}")]
        [ProducesResponseType(typeof(MonthlyReimbursementDto),statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalMonthlyReimbursement(string id)
        {
            return Ok(await _reimbursementService.GetTotalMonthlyReimbursement(id));
        }
   
        [HttpGet("employee/reimbursement/status")]
        [ProducesResponseType(typeof(ReimbursementStatusDto),statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReimbursementStatus(int id)
        {
            var reimbursementStatus = await _reimbursementService.GetReimbursementStatus(id);
            return Ok(reimbursementStatus);
        }

    }

}
