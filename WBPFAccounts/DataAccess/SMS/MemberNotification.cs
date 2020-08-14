using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
   public class MemberNotification
    {
       public MemberNotification()
       {
       }

       public static DataTable GetNotificationMobNos()
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteDataTable("usp_GetNotificationMobNos");
           }
       }

       public static void UpdateNotificationDate(int MemberId)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@MemberId", SqlDbType.Int, MemberId);
               oDm.CommandType = CommandType.StoredProcedure;
               oDm.ExecuteNonQuery("usp_UpdateNotificationDate");
           }
       }

    }
}
