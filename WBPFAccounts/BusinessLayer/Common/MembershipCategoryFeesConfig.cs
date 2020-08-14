using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MembershipCategoryFeesConfig
    {
        public MembershipCategoryFeesConfig()
        {
        }

        public void Save(Entity.Common.MembershipCategoryFeesConfig config)
        {
            DataAccess.Common.MembershipCategoryFeesConfig.Save(config);
        }

        public DataTable GetAll(int MembershipCategoryId)
        {
            return DataAccess.Common.MembershipCategoryFeesConfig.GetAll(MembershipCategoryId);
        }
    }
}
