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
        public async Task<IActionResult> CreateReimbursement([FromForm] ReimbursementDto reimbursementDto)
        {
            
                if (reimbursementDto == null || reimbursementDto.ReceiptImage == null || reimbursementDto.ReceiptImage.Length == 0)
                {
                    return BadRequest("Invalid reimbursement data or receipt iamge.");
                }

                var receiptText = await _reimbursementService.ExtractReceiptTextAsync(reimbursementDto.ReceiptImage); //CS0103
                var Reimbursement = new ReimbursementModel
                {
                    EmployeeId = reimbursementDto.EmployeeId,

                    EmployeeName = reimbursementDto.EmployeeName,
                    Type = reimbursementDto.Type,
                    ReceiptImageUrl = _reimbursementService.SaveReceiptImage(reimbursementDto.ReceiptImage),
                    Amount = (decimal?)(reimbursementDto.Amount ?? 0),
                    TransactionDate = reimbursementDto.TransactionDate,
                };
                _context.ReimbursementModels.Add(Reimbursement);
                await _context.SaveChangesAsync();
                return Ok(Reimbursement);
           
            
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReimbursementById(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);

            if (reimbursement == null)
            {
                return NotFound();
            }

            return Ok(reimbursement);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReimbursements()
        {
            var reimbursements = await _context.ReimbursementModels.ToListAsync();

            return Ok(reimbursements);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReimbursement(int id, [FromBody] ReimbursementUpdateDto reimbursementDto)
        {
            try 
            {
                var reimbursement = await _context.ReimbursementModels.FindAsync(id);

                if (reimbursement == null)
                {
                    return NotFound();
                }

                if (reimbursementDto.EmployeeId != null)
                { 
                    reimbursement.EmployeeId = reimbursementDto.EmployeeId;
                }
                if (reimbursement.EmployeeName != null)
                {
                    reimbursement.EmployeeName = reimbursementDto.EmployeeName;
                }
                if(reimbursementDto.Type !=null)
                {
                    reimbursement.Type = (ReimbursementType)reimbursementDto.Type.Value;
                }
                if (reimbursementDto.Amount != null)
                { 
                    reimbursement.Amount = reimbursementDto.Amount.Value;
                }
                if (reimbursementDto.TransactionDate != null)
                {

                    reimbursement.TransactionDate = reimbursementDto.TransactionDate.Value;
                }

            
                _context.ReimbursementModels.Update(reimbursement);
                await _context.SaveChangesAsync();

                return Ok(reimbursement);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating reimbursement");
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReimbursement(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);

            if (reimbursement == null)
            {
                return NotFound();
            }

            _context.ReimbursementModels.Remove(reimbursement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("id/totals")]
        public async Task<IActionResult> GetMonthlyREimbursementTotalById(string id)
        { 
            //Get current month and year
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            //Calculate the start and end dates for the currrent month
            var startDate = new DateTime(currentYear, currentMonth, 1);
            var endDate = startDate.AddMonths(1).AddDays(1);

            //Query the database to get the reimbursement within the current  for the specified employee Id
            var reimbursements = await _context.ReimbursementModels
                .Where(r => r.EmployeeId ==id && r.TransactionDate >=startDate && r.TransactionDate <=endDate)
                .ToListAsync();

            // Calculate the total amount for the employee
            var totalAmount = reimbursements.Sum(r => r.Amount);

            // Create a new instance of the ReimbursementModel with the total amount
            var reimbursementModel = new ReimbursementModel
            {
                EmployeeId = id,
                MonthlyTotal = totalAmount
            };
            return Ok(reimbursementModel);

        }
 
        //private decimal? ParseAmountFromReceiptText(string receiptText)
        //{
        //    if (string.IsNullOrEmpty(receiptText))
        //    {
        //        return null;
        //    }

        //    var keywords = new string[] { "Total", "Amount", "Total Amount", "Amount Due", "Total Due" };
        //    var lines = receiptText.Split(Environment.NewLine);
        //    var totalLine = lines.FirstOrDefault(line => keywords.Any(keyword => line.Contains(keyword)));
        //    var amountString = Regex.Match(totalLine, @"\d+(\.\d+)?").Value;

        //    if (decimal.TryParse(amountString, out decimal amount))
        //    {
        //        return amount;
        //    }

        //    return null;
        //}


    }


}
