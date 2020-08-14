using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class MemberGroup
	{
		public MemberGroup()
		{

		}

        public void Save(Entity.Common.MemberGroup membergroup)
		{
            DataAccess.Common.MemberGroup.Save(membergroup);
		}

		public DataTable GetAll()
		{
            return DataAccess.Common.MemberGroup.GetAll();
		}

        public Entity.Common.MemberGroup GetMemberGroupById(int memberGroupId)
		{
            return DataAccess.Common.MemberGroup.GetMemberGroupById(memberGroupId);
		}

		public void Delete(int memberGroupId)
		{
            DataAccess.Common.MemberGroup.Delete(memberGroupId);
		}
	}
}