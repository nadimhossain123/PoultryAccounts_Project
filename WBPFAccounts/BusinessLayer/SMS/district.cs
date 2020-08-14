using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer.SMS
{
    public class district
    {
        public district()
        {

        }
        public void Save(Entity.SMS.district Entitydistrict)
        {
            DataAccess.SMS.district.Save(Entitydistrict);

        }
        public DataTable GetAll()
        {
            return DataAccess.SMS.district.GetAll();
        }

        public Entity.SMS.district GetAllById(int DistrictId)
        {

            return DataAccess.SMS.district.GetAllById(DistrictId);
        }

        public DataTable GetAllDistrictMaster()
        {
            return DataAccess.SMS.district.GetAllDistrictMaster();
        }

        public DataTable GetAllBlock(int districtId)
        {
            return DataAccess.SMS.district.GetAllBlock(districtId);
        }
    }
}

