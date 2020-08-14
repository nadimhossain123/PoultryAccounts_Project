using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class SMSTrigger
    {
        public SMSTrigger()
        {
        }

        public void Save(int MobNoCount)
        {
            DataAccess.SMS.SMSTrigger.Save(MobNoCount);
        }

        public DataTable GetAll(string FromDate, string ToDate)
        {
            return DataAccess.SMS.SMSTrigger.GetAll(FromDate, ToDate);
        }

        public bool IsMessageSentToday()
        {
            return DataAccess.SMS.SMSTrigger.IsMessageSentToday();
        }

        public void Unlock()
        {
            DataAccess.SMS.SMSTrigger.Unlock();
        }

        public DataTable MemberSetails_GetAll(int SMSMemberId)
        {
            return DataAccess.SMS.SMSTrigger.MemberSetails_GetAll(SMSMemberId);
        }
    }
}
