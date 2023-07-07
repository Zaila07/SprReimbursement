
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SprEmployeeReimbursement.Business.DataTransferObject;
using SprEmployeeReimbursement.Business.FormRecognizer;
using SprEmployeeReimbursement.Common.Enums;
using SprEmployeeReimbursement.DataAccess.Models;
using SprEmployeeReimbursement.DataAccess.SprDbContext;
using System.Globalization;
using System.Text.RegularExpressions;



namespace SprEmployeeReimbursement.Business.ServiceCollection
{
    public class ReimbursementService : IReimbursementService
    {
        private readonly IConfiguration _configuration;
        private readonly SprReimbursementDbContext _context;
        public ReimbursementService(IConfiguration configuration, SprReimbursementDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<decimal> ProcessMultipleReceiptsReimbursement(MultipleReceiptsReimbursementDto input)
        {
            var apikey = _configuration["AzureFormRecognizer:ApiKey"];
            var endpoint = _configuration["AzureFormRecognizer:Endpoint"];
            var imageUrl =string.Empty;
            decimal totalAmount = 0;
            var reimbursementFolderPath = Path.Combine("ReimbursementRequests", input.EmployeeId.ToString(), Guid.NewGuid().ToString());
            foreach (var image in input.ReceiptImages)
            {
                // Extract receipt text from the current receipt image
                var receiptText = await FormRecognizerHelper.ExtractReceiptTextAsync(image, apikey, endpoint);
                Console.WriteLine("Receipt Text: " + receiptText);

                // Parse the amount from the receipt text
                var amount = ParseAmountFromReceiptText(receiptText);
                Console.WriteLine("Amount:" + amount);
                totalAmount += amount;
               

                imageUrl =SaveReceiptImage(image,reimbursementFolderPath);
             }
            var reimbursement = new ReimbursementModel
            {
                EmployeeId = input.EmployeeId,
                EmployeeName = input.EmployeeName,
                Type = input.Type,
                TransactionDate = input.TransactionDate,
                FolderPath = reimbursementFolderPath,
                Amount = totalAmount,
            };
            _context.ReimbursementModels.Add(reimbursement);
             await _context.SaveChangesAsync();
                return totalAmount;
        }
        public async Task<ReimbursementModel> GetReimbursementForHR(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);
            if (reimbursement == null)
            {
                throw new InvalidOperationException("Reimbursement Not Found");
            }
            return reimbursement;
        }

        public async Task<List<ReimbursementModel>> GetAllReimbursements()
        {

            var reimbursements = await _context.ReimbursementModels.ToListAsync();

            return reimbursements;
        }

        public async Task<ReimbursementModel> UpdateReimbursement(int id, ReimbursementUpdateDto reimbursementDto)

        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);

            if (reimbursement == null)
            {
                throw new InvalidOperationException("Reimbursenment Not Found");
            }

            if (reimbursementDto.EmployeeId != null)
            {
                reimbursement.EmployeeId = reimbursementDto.EmployeeId;
            }
            if (reimbursement.EmployeeName != null)
            {
                reimbursement.EmployeeName = reimbursementDto.EmployeeName;
            }
            if (reimbursementDto.Type != null)
            {
                reimbursement.Type = (ReimbursementType)reimbursementDto.Type.Value;
            }
            if (reimbursementDto.FolderPath!= null)
            {
                reimbursement.FolderPath= reimbursementDto.FolderPath;
            }
            if (reimbursementDto.TransactionDate != null)
            {

                reimbursement.TransactionDate = reimbursementDto.TransactionDate.Value;
            }

            _context.ReimbursementModels.Update(reimbursement);
            await _context.SaveChangesAsync();

            return reimbursement;
        }

        public async Task<int> DeleteReimbursement(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);

            if (reimbursement == null)
            {
                throw new InvalidOperationException("Reimbursement Not Found");
            }

            _context.ReimbursementModels.Remove(reimbursement);
            await _context.SaveChangesAsync();

            return 0;
        }

        public async Task<MonthlyReimbursementDto> GetTotalMonthlyReimbursement(string id)
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
                .Where(r => r.EmployeeId == id && r.TransactionDate >= startDate && r.TransactionDate <= endDate)
                .ToListAsync();

            // Calculate the total amount for the employee
            var totalAmount = reimbursements.Sum(r => r.Amount);

            //Get the employee name
            var employeeName = await _context.SprEmployees
                .Where(e=>e.Id.ToString()==id)
                .Select(e=>e.Name)
                .FirstOrDefaultAsync();

            // Create a new instance of the MonthlyReimbursementDto
            var monthlyReimbursementDto = new MonthlyReimbursementDto
            {
                EmployeeName = employeeName,
                MonthlyTotal = totalAmount
            };
            return monthlyReimbursementDto;

        }
        public async Task<ReimbursementModel> ApproveReimbursement(int id)
        {
            var reimbursement = await GetReimbursementForHR(id);
            reimbursement.IsApproved = true;
            _context.ReimbursementModels.Update(reimbursement);
            await _context.SaveChangesAsync();

            return reimbursement;
        }
        public async Task<ReimbursementModel> DisapproveReimbursement(int id, string reason)
        {
            var reimbursement = await GetReimbursementForHR(id);
            reimbursement.IsApproved = false;
            reimbursement.ResponseMessage = reason;

            _context.ReimbursementModels.Update(reimbursement);
             await _context.SaveChangesAsync();
            return reimbursement;
        }
        public async Task<ReimbursementStatusDto> GetReimbursementStatus(int id)
        {
            var reimbursement = await _context.ReimbursementModels.FindAsync(id);
        
            var statusDto = new ReimbursementStatusDto
            {
                ReimbursementId = reimbursement.Id,
                EmployeeId = reimbursement.EmployeeId,
                EmployeeName = reimbursement.EmployeeName,
                IsApproved = reimbursement.IsApproved,
                ResponseMessage = reimbursement.ResponseMessage
            };

            return statusDto;
        }


        private string SaveReceiptImage(IFormFile receiptImage, string folderPath)
        {
            //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + receiptImage.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            //Create the reimbursement request folder if it doesnt exist  
            Directory.CreateDirectory(folderPath);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                receiptImage.CopyTo(stream);
            }

            return uniqueFileName;
        }


        public decimal ParseAmountFromReceiptText(string receiptText)
        {
            if (string.IsNullOrEmpty(receiptText))
                return 0;

            var keywords = new string[] { "TOTAL", "Amount Due", "Amount:", "Amount", "Total Due", "TOTAL INVOICE",
                "Total Sale", "Total:" ,"TOTAL AMOUNT DUE"};

            foreach (var keyword in keywords)   
            {
                var keywordIndex = receiptText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);

                if (keywordIndex != -1)
                {
                    var startIndex = keywordIndex + keyword.Length;
                    var amountString = receiptText.Substring(startIndex).TrimStart(':',' ').Trim();
                    var amountMatch = Regex.Match(amountString, @"(\d{1,3}(?:,\d{3})*|\d+(?:\.\d{2})?)");

                    if (amountMatch.Success && decimal.TryParse(amountMatch.Value,
                        NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture,
                        out decimal amount))
                        return amount;
                }
            }

            return 0;
        }



    }
}
