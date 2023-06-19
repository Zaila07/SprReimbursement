using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.Models;

namespace SprEmployeeReimbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly FormRecognizerClient _formRecognizerClient;
        private readonly SprReimbursementDbContext _context;



        public ReimbursementController(IConfiguration configuration, FormRecognizerClient formRecognizerClient, SprReimbursementDbContext context)
        {
            _context = context;
            _configuration = configuration;
            _formRecognizerClient = formRecognizerClient;
        }




        

    [HttpPost]
        public async Task<IActionResult> CreateReimbursement([FromForm] ReimbursementDTO reimbursementDTO)
        {
            try 
            {
                if (reimbursementDTO == null || reimbursementDTO.ReceiptImage == null || reimbursementDTO.ReceiptImage.Length == 0)
                {
                    return BadRequest("Invalid reimbursement data or receipt iamge.");
                }

                var receiptText = await ExtractReceiptTextAsync(reimbursementDTO.ReceiptImage);
                var Reimbursement = new ReimbursementModel
                {
                    EmployeeId = reimbursementDTO.EmployeeId,
                    EmployeeName = reimbursementDTO.EmployeeName,
                    Type = reimbursementDTO.Type,
                    ReceiptImageUrl = SaveReceiptImage(reimbursementDTO.ReceiptImage),
                    Amount = (decimal?)(reimbursementDTO.Amount ?? 0),

                    TransactionDate = reimbursementDTO.TransactionDate,

                };
                _context.ReimbursementModels.Add(Reimbursement);
                await _context.SaveChangesAsync();
                return Ok(Reimbursement);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occured while creating the reimbursement");
            }
            
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
        public async Task<IActionResult> UpdateReimbursement(int id, [FromBody] ReimbursementUpdateDTO reimbursementDTO)
        {
            try 
            {
                var reimbursement = await _context.ReimbursementModels.FindAsync(id);

                if (reimbursement == null)
                {
                    return NotFound();
                }

                if (reimbursementDTO.EmployeeId != null)
                { 
                    reimbursement.EmployeeId = reimbursementDTO.EmployeeId;
                }
                if (reimbursement.EmployeeName != null)
                {
                    reimbursement.EmployeeName = reimbursementDTO.EmployeeName;
                }
                if(reimbursementDTO.Type !=null)
                {
                    reimbursement.Type = reimbursementDTO.Type.Value;
                }
                if (reimbursementDTO.Amount != null)
                { 
                    reimbursement.Amount = reimbursementDTO.Amount.Value;
                }
                if (reimbursementDTO.TransactionDate != null)
                {

                    reimbursement.TransactionDate = reimbursementDTO.TransactionDate.Value;
                }

                //// Update the reimbursement object with the new data
                //reimbursement.EmployeeId = reimbursementDTO.EmployeeId;
                //reimbursement.EmployeeName = reimbursementDTO.EmployeeName;
                //reimbursement.Type = reimbursementDTO.Type;
                //reimbursement.Amount = reimbursementDTO.Amount;
                //reimbursement.TransactionDate = reimbursementDTO.TransactionDate;

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




        private async Task<string> ExtractReceiptTextAsync(IFormFile receiptImage)
        {
            var formRecognizerOptions = new AzureKeyCredential(_configuration["AzureFormRecognizer:ApiKey"]);
            var formRecognizerClient = new FormRecognizerClient(new
                Uri(_configuration["AzureFormRecognizer:Endpoint"]), formRecognizerOptions);
            using (var stream = receiptImage.OpenReadStream()) 
            {
                var form = await formRecognizerClient.StartRecognizeContentAsync(stream);
               var operationResult = await form.WaitForCompletionAsync();
                var formPage = operationResult.Value.FirstOrDefault(); 
                if (formPage != null)
                {
                    var receiptText =string.Join(" ",formPage.Lines
                        .Select(line => line.Text));
                    return receiptText;
                }
            
            }

            return string.Empty;
        }

        private string SaveReceiptImage(IFormFile receiptImage)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + receiptImage.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //Create the directory if it doesnt exist
            if (!Directory.Exists(uploadsFolder))
            { 
                Directory.CreateDirectory(uploadsFolder);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                receiptImage.CopyTo(stream);
            }

            return uniqueFileName;
        }
        private decimal? ParseAmountFromReceiptText(string receiptText)
        {
            if (string.IsNullOrEmpty(receiptText))
            {
                return null;
            }

            var keywords = new string[] { "Total", "Amount", "Total Amount", "Amount Due", "Total Due" };
            var lines = receiptText.Split(Environment.NewLine);
            var totalLine = lines.FirstOrDefault(line => keywords.Any(keyword => line.Contains(keyword)));
            var amountString = Regex.Match(totalLine, @"\d+(\.\d+)?").Value;

            if (decimal.TryParse(amountString, out decimal amount))
            {
                return amount;
            }

            return null;
        }


    }


}
