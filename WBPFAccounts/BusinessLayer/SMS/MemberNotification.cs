using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer
{
   public class MemberNotification
    {
       public MemberNotification()
       {
       }

       public static DataTable GetNotificationMobNos()
       {
           return DataAccess.MemberNotification.GetNotificationMobNos();
       }

       public static void UpdateNotificationDate(int MemberId)
       {
           DataAccess.MemberNotification.UpdateNotificationDate(MemberId);
       }
    }
}
