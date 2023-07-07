using System.Text.RegularExpressions;
using Azure.AI.FormRecognizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.Business.DataTransferObject;
using SprEmployeeReimbursement.Business.ServiceCollection;

using SprEmployeeReimbursement.Common.Enums;
using SprEmployeeReimbursement.DataAccess.Models;
using SprEmployeeReimbursement.DataAccess.SprDbContext;

namespace SprEmployeeReimbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReimbursementService _reimbursementService;
        private readonly FormRecognizerClient _formRecognizerClient;
        private readonly SprReimbursementDbContext _context;



        public ReimbursementController(IConfiguration configuration, FormRecognizerClient formRecognizerClient, SprReimbursementDbContext context, IReimbursementService reimbursementService)
        {
            _context = context;
            _configuration = configuration;
            _formRecognizerClient = formRecognizerClient;
            _reimbursementService = reimbursementService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessMultipleReceiptsReimbursement([FromForm] MultipleReceiptsReimbursementDto input)
        {
            var result = await _reimbursementService.ProcessMultipleReceiptsReimbursement(input);
            return Created($"api/v1/Reimbursement/{result}", result);
        }

        [HttpGet("hr/{id}")]
        public async Task<IActionResult> GetReimbursementForHR(int id)
        {
            return Ok(await _context.ReimbursementModels.FindAsync(id));
        }

        [HttpGet("hr/view/requests")]
        public async Task<IActionResult> GetAllReimbursements()
        {
            return Ok(await _reimbursementService.GetAllReimbursements());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReimbursement(int id, [FromBody] ReimbursementUpdateDto reimbursementDto)
        {
           await _reimbursementService.UpdateReimbursement(id, reimbursementDto);
           return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReimbursement(int id)
        {   
                var deletedReimbursement = await _reimbursementService.DeleteReimbursement(id);
                return Ok(deletedReimbursement);          
        }

        [HttpPost("hr/approve/{id}")]
        public async Task<IActionResult>ApproveReimbursement(int id)
        {
            var reimbursement = await _reimbursementService.ApproveReimbursement(id);
            return Ok(reimbursement);

        }
        [HttpPost("hr/disapprove/{id}")]
        public async Task<IActionResult> DisapproveReimbursement(int id, [FromBody] string reason)
        {
            var reimbursement = await _reimbursementService.DisapproveReimbursement(id,reason);
            return Ok(reimbursement);

        }
        [HttpGet("hr/total/employee/reimbursements/{id}")]
        public async Task<IActionResult> GetTotalMonthlyReimbursement(string id)
        {

            return Ok(await _reimbursementService.GetTotalMonthlyReimbursement(id));

        }
        [HttpGet]
        public async Task<IActionResult> GetReimbursementStatus(int id)
        {
            var reimbursementStatus = await _reimbursementService.GetReimbursementStatus(id);
            return Ok(reimbursementStatus);
        }

    }


}
