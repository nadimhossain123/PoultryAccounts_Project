using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS 
{
   public class ApiConfiguration
    {
        public DataTable GetAll()
        {
            return DataAccess.SMS.ApiConfiguration.GetAll();
        }

        public int Save(int SMSAPIId)
        {
            return DataAccess.SMS.ApiConfiguration.Save(SMSAPIId);
        }
    }
}
