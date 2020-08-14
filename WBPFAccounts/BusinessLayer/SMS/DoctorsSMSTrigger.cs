using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class DoctorsSMSTrigger
    {
        public int Save(Entity.SMS.DoctorsSMSTrigger smsTrigger)
        {
            return DataAccess.SMS.DoctorsSMSTrigger.Save(smsTrigger);
        }

        public DataTable GetAll(int GroupId,int Month,int Year)
        {
            return DataAccess.SMS.DoctorsSMSTrigger.GetAll(GroupId, Month, Year);
        }
    }
}
