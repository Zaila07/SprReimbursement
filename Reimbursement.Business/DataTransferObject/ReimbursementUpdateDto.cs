using SprEmployeeReimbursement.Common.Enums;

namespace SprEmployeeReimbursement.Business.DataTransferObject;

public class ReimbursementUpdateDto
{
    public string? EmployeeName { get; set; }
    public string? EmployeeId { get; set; }
    public ReimbursementType? Type { get; set; }
    public string FolderPath { get; set; }
    public DateTime? TransactionDate { get; set; }
}
