using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.SMS
{
    public class DoctorMaster
    {
        public int Save(Entity.SMS.DoctorMaster doctor)
        {
            return DataAccess.SMS.DoctorMaster.Save(doctor);
        }

        public DataTable GetAll()
        {
            return DataAccess.SMS.DoctorMaster.GetAll();
        }

        public void Delete(int doctorId)
        {
            DataAccess.SMS.DoctorMaster.Delete(doctorId);
        }

        public Entity.SMS.DoctorMaster GetById(int doctorId)
        {
            return DataAccess.SMS.DoctorMaster.GetById(doctorId);
        }

        public DataTable GetDocNumbers(int groupId)
        {
            return DataAccess.SMS.DoctorMaster.GetDocNumbers(groupId);
        }
    }
}
