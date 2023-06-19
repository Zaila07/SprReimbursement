using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using System.Text.Json.Serialization;

namespace SprEmployeeReimbursement.Models
{
    public class ReimbursementDTO
    {
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ReimbursementType Type { get; set; }
        public IFormFile ReceiptImage { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
