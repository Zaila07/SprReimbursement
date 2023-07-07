using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprEmployeeReimbursement.Business.DataTransferObject
{
    public class MonthlyReimbursementDto
    {
        public string EmployeeName { get;set; }
        public decimal? MonthlyTotal { get; set; }

    }
}
