using Microsoft.AspNetCore.Http;
using SprEmployeeReimbursement.Common.Enums;

namespace SprEmployeeReimbursement.Business.DataTransferObject
{
    public class MultipleReceiptsReimbursementDto
    {
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public ReimbursementType Type { get; set; }
        public List<IFormFile> ReceiptImages { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
