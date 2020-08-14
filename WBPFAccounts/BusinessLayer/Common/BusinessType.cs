using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class BusinessType
	{
		public BusinessType()
		{

		}

        public void Save(Entity.Common.BusinessType businesstype)
		{
            DataAccess.Common.BusinessType.Save(businesstype);
		}

		public DataTable GetAll()
		{
            return DataAccess.Common.BusinessType.GetAll();
		}

        public Entity.Common.BusinessType GetBusinessTypeById(int businessTypeId)
		{
            return DataAccess.Common.BusinessType.GetBusinessTypeById(businessTypeId);
		}

		public void Delete(int businessTypeId)
		{
            DataAccess.Common.BusinessType.Delete(businessTypeId);
		}
	}
}