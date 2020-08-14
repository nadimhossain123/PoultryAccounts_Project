using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class MembershipCategory
	{
		public MembershipCategory()
		{

		}

        public void Save(Entity.Common.MembershipCategory membershipcategory)
		{
            DataAccess.Common.MembershipCategory.Save(membershipcategory);
		}

		public DataTable GetAll()
		{
            return DataAccess.Common.MembershipCategory.GetAll();
		}

        public Entity.Common.MembershipCategory GetMembershipCategoryById(int membershipCategoryId)
		{
            return DataAccess.Common.MembershipCategory.GetMembershipCategoryById(membershipCategoryId);
		}

		public void Delete(int membershipCategoryId)
		{
            DataAccess.Common.MembershipCategory.Delete(membershipCategoryId);
		}
	}
}