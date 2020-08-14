using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Report
    {
        public Report()
        {
        }
        public DataTable LoadDataTable(string SQLSTR)
        {
            DataAccess.Common.Report ReportDataAccess = new DataAccess.Common.Report();
            return ReportDataAccess.LoadDataTable(SQLSTR);
        }

        public DataTable GetMonthlyPFRegister(int Month, int Year, int CompanyId)
        {
            return DataAccess.Common.Report.GetMonthlyPFRegister(Month, Year,CompanyId);
        }

        public DataTable GetMonthlyESIRegister(int Month, int Year, int CompanyId)
        {
            return DataAccess.Common.Report.GetMonthlyESIRegister(Month, Year, CompanyId);
        }

        public DataTable GetMonthlyPTaxRegister(int Month, int Year, int CompanyId)
        {
            return DataAccess.Common.Report.GetMonthlyPTaxRegister(Month, Year, CompanyId);
        }
    }
}
