using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class SMSAPIConfig
    {
        public SMSAPIConfig()
        {

        }

        public DataTable GetAll()
        {
            return DataAccess.SMS.SMSAPIConfig.GetAll();
        }

        public void Update(int APIId)
        {
            DataAccess.SMS.SMSAPIConfig.Update(APIId);
        }
    }
}
