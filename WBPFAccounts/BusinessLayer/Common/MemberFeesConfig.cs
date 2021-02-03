using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberFeesConfig
    {
        public MemberFeesConfig()
        {
        }

        public void Save(Entity.Common.MemberFeesConfig config)
        {
            DataAccess.Common.MemberFeesConfig.Save(config);
        }

        public DataTable GetAll(int MemberId)
        {
            return DataAccess.Common.MemberFeesConfig.GetAll(MemberId);
        }

        public DataTable MemberDevelopmentFeeGetAll(int MemberId)
        {
            return DataAccess.Common.MemberFeesConfig.MemberDevelopmentFeeGetAll(MemberId);
        }

        public DataSet MemberRenewalFeeGetAll(int MemberId,int FinYrId,int FromMonth,int ToMonth,int WithOutOpening)
        {
            return DataAccess.Common.MemberFeesConfig.MemberRenewalFeeGetAll(MemberId, FinYrId, FromMonth, ToMonth, WithOutOpening);
        }

        public DataSet MemberDevelopmentFeeAllMonthGetAll(int MemberId, int FinYrId, int FromMonth, int ToMonth,int WithOutOpening)
        {
            return DataAccess.Common.MemberFeesConfig.MemberDevelopmentFeeAllMonthGetAll(MemberId, FinYrId, FromMonth, ToMonth,WithOutOpening);
        }
    }
}
