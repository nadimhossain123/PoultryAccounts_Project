using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class SMSCategoryMaster
    {
        public SMSCategoryMaster()
        {
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.SMSCategoryMaster.GetAll();
        }
    }
}
