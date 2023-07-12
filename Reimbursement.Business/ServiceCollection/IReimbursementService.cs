using SprEmployeeReimbursement.Business.DataTransferObject;
using SprEmployeeReimbursement.DataAccess.Models;

namespace SprEmployeeReimbursement.Business.ServiceCollection
{
    public interface IReimbursementService
    {
        Task<decimal> ProcessMultipleReceiptsReimbursement(MultipleReceiptsReimbursementDto input);
        Task<ReimbursementModel> GetReimbursementForHR(int id);
        Task<List<ReimbursementModel>> GetAllReimbursements();
        Task<ReimbursementModel> UpdateReimbursement(int id, ReimbursementUpdateDto reimbursementDto);
        Task<int> DeleteReimbursement(int id);
        Task<MonthlyReimbursementDto> GetTotalMonthlyReimbursement(string id);
        Task<ReimbursementModel> ApproveReimbursement(int id);
        Task<ReimbursementModel> DisapproveReimbursement(int id, string reason);
        Task<ReimbursementStatusDto> GetReimbursementStatus(int id);
    }
}
