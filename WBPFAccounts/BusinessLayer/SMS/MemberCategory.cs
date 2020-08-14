using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class MemberCategory
    {
        public MemberCategory()
        {
        }

        public DataTable GetAll()
        {
            return DataAccess.SMS.MemberCategory.GetAll();
        }
    }
}
