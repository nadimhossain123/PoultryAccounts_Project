using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MonthMaster
    {
        public MonthMaster()
        {
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.MonthMaster.GetAll();
        }
    }
}
