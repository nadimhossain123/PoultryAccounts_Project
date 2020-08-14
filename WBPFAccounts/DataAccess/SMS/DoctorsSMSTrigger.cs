using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SMS
{
    public class DoctorsSMSTrigger
    {
        public static int Save(Entity.SMS.DoctorsSMSTrigger smsTrigger)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDoctorsSMSTriggerId", SqlDbType.Int, ParameterDirection.InputOutput, smsTrigger.DoctorsSMSTriggerId);
                oDm.Add("@pUsername", SqlDbType.VarChar, 50, smsTrigger.Username);
                oDm.Add("@pGroupId", SqlDbType.Int, smsTrigger.GroupId);
                oDm.Add("@pNoofTrigger", SqlDbType.Int, smsTrigger.NoofTrigger);
                oDm.Add("@pMessageBody", SqlDbType.Text, smsTrigger.MessageBody);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_DoctorsSMSTrigger_Save");
            }
        }

        public static DataTable GetAll(int GroupId, int Month, int Year)
        {
            using (DataManager oDm = new DataManager())
            {
                if (GroupId == 0)
                    oDm.Add("@pGroupId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pGroupId", SqlDbType.Int, GroupId);
                if (Month == 0)
                    oDm.Add("@pMonth", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMonth", SqlDbType.Int, Month);
                if (Year == 0)
                    oDm.Add("@pYear", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pYear", SqlDbType.Int, Year);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_DoctorsSMSTrigger_GetAll");
            }
        }
    }
}
