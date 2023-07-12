using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprEmployeeReimbursement.Business.DataTransferObject
{
    public  class ReimbursementStatusDto
    {
            public int ReimbursementId { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public bool IsApproved { get; set; }
            public string ResponseMessage { get; set; }
    
    }
}
