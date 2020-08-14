using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberDiscountConfig
    {
        public MemberDiscountConfig()
        {
        }

        public void Save(Entity.Common.MemberDiscountConfig config)
        {
            DataAccess.Common.MemberDiscountConfig.Save(config);
        }

        public DataTable GetAll(int MemberId)
        {
            return DataAccess.Common.MemberDiscountConfig.GetAll(MemberId);
        }
    }
}
