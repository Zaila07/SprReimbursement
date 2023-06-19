namespace SprEmployeeReimbursement.Models
{
    public class ReimbursementUpdateDTO
    {
        public string? EmployeeName { get; set; }
        public string? EmployeeId { get; set; }
        public ReimbursementType? Type { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
