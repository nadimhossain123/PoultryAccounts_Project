using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class CostCentreWiseReport
    {
        public CostCentreWiseReport() { }

        public static DataTable GetAll(int ContCenterId,int CompanyId,string FromDate,string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CostCenterId", SqlDbType.Int, ContCenterId);
                oDm.Add("@CompanyId",SqlDbType.Int,CompanyId);
                if(FromDate=="")
                    oDm.Add("@FromDate", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@FromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate));
                if(ToDate=="")
                    oDm.Add("@ToDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("@ToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate));

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("spRPT_CostCenterWiseReport");
            }
        }
    }
}
