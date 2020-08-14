using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.SMS
{
    public class SMSAPIConfig
    {
        public SMSAPIConfig()
        {

        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataAccess.DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSAPIConfig_GetAll");
            }
        }

        public static void Update(int APIId)
        {
            using (DataManager oDm = new DataAccess.DataManager())
            {
                oDm.Add("@pAPIId", SqlDbType.Int, APIId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSAPIConfig_Update");
            }
        }
    }
}
