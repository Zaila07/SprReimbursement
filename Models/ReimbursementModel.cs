using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprEmployeeReimbursement.Models
{
    public class ReimbursementModel
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string EmployeeName { get; set; } = "";
        public string EmployeeId { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        //Add Enum data type attribute
        [EnumDataType (typeof(ReimbursementType))] 
        public ReimbursementType Type { get; set; }
        public string ReceiptImageUrl { get; set; } = "";

        [Column(TypeName="decimal(18,2)")]
        public decimal? Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
    }
}
