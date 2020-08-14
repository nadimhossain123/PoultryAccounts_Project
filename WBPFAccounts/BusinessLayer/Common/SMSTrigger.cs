using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class SMSTrigger
    {
        public SMSTrigger()
        {
        }

        public void Save(int MobNoCount)
        {
            DataAccess.Common.SMSTrigger.Save(MobNoCount);
        }

        public DataTable GetAll(string FromDate, string ToDate)
        {
            return DataAccess.Common.SMSTrigger.GetAll(FromDate, ToDate);
        }

        public bool IsMessageSentToday()
        {
            return DataAccess.Common.SMSTrigger.IsMessageSentToday();
        }

        public void Unlock()
        {
            DataAccess.Common.SMSTrigger.Unlock();
        }
    }
}
