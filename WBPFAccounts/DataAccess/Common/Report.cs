using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Report
    {
        public Report()
        {
        }

        public DataTable LoadDataTable(string SQLSTR)
        {
            using (DataManager Dm = new DataManager())
            {
                Dm.CommandType = CommandType.Text;
                return Dm.ExecuteDataTable(SQLSTR);
            }
        }

        public static DataTable GetMonthlyPFRegister(int Month, int Year, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@Year", SqlDbType.Int, Year);
                oDm.Add("@CompanyId", SqlDbType.Int, CompanyId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MonthlyPFRegister");
            }
        }

        public static DataTable GetMonthlyESIRegister(int Month, int Year, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@Year", SqlDbType.Int, Year);
                oDm.Add("@CompanyId", SqlDbType.Int, CompanyId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MonthlyESIRegister");
            }
        }

        public static DataTable GetMonthlyPTaxRegister(int Month, int Year, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@Year", SqlDbType.Int, Year);
                oDm.Add("@CompanyId", SqlDbType.Int, CompanyId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MonthlyPTaxRegister");
            }
        }
    }
}
