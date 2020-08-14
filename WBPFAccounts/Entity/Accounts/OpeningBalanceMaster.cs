using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class OpeningBalanceMaster
    {
        public int OpBalId { get; set; }
        public int CompanyId { get; set; }
        public int FinancialYrId { get; set; }
        public int LedgerId { get; set; }
        public int BranchId { get; set; }
        public decimal OpeningBalance { get; set; }
        public string OpeningBalanceType { get; set; }
        public int CreatedBy { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
