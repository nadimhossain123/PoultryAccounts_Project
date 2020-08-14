using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class District
	{
		public District()
		{

		}

		public void Save(Entity.Common.District district)
		{
            DataAccess.Common.District.Save(district);
		}

        public DataTable GetAll(int stateid)
		{
            return DataAccess.Common.District.GetAll(stateid);
		}

        public Entity.Common.District GetDistrictById(int districtId)
		{
            return DataAccess.Common.District.GetDistrictById(districtId);
		}

		public void Delete(int districtId)
		{
            DataAccess.Common.District.Delete(districtId);
		}

        public DataTable SMSDistrictGetAll()
        {
            return DataAccess.Common.District.SMSDistrictGetAll();
        }
	}
}