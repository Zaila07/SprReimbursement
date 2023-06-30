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

        [HttpPost("employee/request")]
        public async Task<IActionResult> ProcessMultipleReceiptsReimbursement([FromForm] MultipleReceiptsReimbursementDto input)
        {
            try
            {
                if (input.ReceiptImages == null || !input.ReceiptImages.Any())
                {
                    return BadRequest("No receipt images provided.");
                }

              

                // Call the service method to process the multiple receipts reimbursement
           decimal totalAmount = await _reimbursementService.ProcessMultipleReceiptsReimbursement(input);

                return Ok(totalAmount);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }


        [HttpGet("hr/{id}")]
        public async Task<IActionResult> GetReimbursementForHR(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);

            if (reimbursement == null)
            {
                return NotFound();
            }

            return Ok(reimbursement);
        }

        [HttpGet("hr/view/requests")]
        public async Task<IActionResult> GetAllReimbursements()
        {
           
            var reimbursements = await _reimbursementService.GetAllReimbursements();
            return Ok(reimbursements);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReimbursement(int id, [FromBody] ReimbursementUpdateDto reimbursementDto)
        {

            var updatedReimbursement = await _reimbursementService.UpdateReimbursement(id, reimbursementDto);
            if (updatedReimbursement == null)
             {
                return null;
             }

                return Ok(updatedReimbursement);
           
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReimbursement(int id)
        {
            try
            {
                var deletedReimbursement = await _reimbursementService.DeleteReimbursement(id);
                return Ok(deletedReimbursement);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            
            }
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
        public async Task<IActionResult> GetMonthlyREimbursementTotalById(string id)
        {
            try
            {
                var employeeTotalReimbursement = await _reimbursementService.GetMonthlyReimbursementTotalById(id);

                if (employeeTotalReimbursement == null)
                {
                    return NotFound(); 
                }

                var monthlyReimbursementDto = new MonthlyReimbursementDto
                {
                    EmployeeName = employeeTotalReimbursement.EmployeeName,
                    MonthlyTotal = employeeTotalReimbursement.MonthlyTotal
                };

                return Ok(monthlyReimbursementDto);
            }
            catch (Exception)
            {
                return BadRequest("Failed to retrieve monthly reimbursement total");
            }
        }




    }


}
