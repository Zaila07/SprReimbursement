using SprEmployeeReimbursement.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SprEmployeeReimbursement.DataAccess.Models;

public class ReimbursementModel
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string EmployeeName { get; set; } = "";
    public string EmployeeId { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    //Add Enum data type attribute
    [EnumDataType(typeof(ReimbursementType))]
    public ReimbursementType Type { get; set; }

    public string FolderPath { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime TransactionDate { get; set; }

    //Add Monthly Total
    public decimal? MonthlyTotal { get; set; }

    public bool IsApproved { get; set; }
    public string? ResponseMessage { get; set; }
}
