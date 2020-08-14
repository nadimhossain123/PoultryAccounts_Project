using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class CostCentreWiseReport
    {
        public CostCentreWiseReport() { }

        public DataTable GetAll(int CostCenterId, int CompanyId, string FromDate, string ToDate)
        {
            return DataAccess.Accounts.CostCentreWiseReport.GetAll(CostCenterId, CompanyId, FromDate, ToDate);
        }
    }
}
