using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class SMSTrigger
    {
        public SMSTrigger()
        {
        }

        public static void Save(int MobNoCount)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MobNoCount", SqlDbType.Int, MobNoCount);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSTrigger_Save");
            }
        }

        public static DataTable GetAll(string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FromDate.Trim().Length == 0)
                {
                    oDm.Add("@pFromDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pFromDate", SqlDbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(FromDate));
                }

                if (ToDate.Trim().Length == 0)
                {
                    oDm.Add("@pToDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pToDate", SqlDbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(ToDate));
                }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSTrigger_GetAll");
            }
        }

        public static bool IsMessageSentToday()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                DataTable DT = oDm.ExecuteDataTable("usp_SMSTrigger_MessageToday");

                if (DT.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public static void Unlock()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSTrigger_Unlock");
            }
        }
    }
}
