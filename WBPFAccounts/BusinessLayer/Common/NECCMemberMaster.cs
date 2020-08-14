using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLayer.Common
{
    public class NECCMemberMaster
    {
        public int Save(Entity.Common.NECCMemberMaster neccmember, DateTime StartDate, DateTime EndDate, string txtRemarks)
        {
            return DataAccess.Common.NECCMemberMaster.Save(neccmember, StartDate, EndDate, txtRemarks);
        }

        public Entity.Common.NECCMemberMaster GetNECCMemberById(int NECCMemberId)
        {
            return DataAccess.Common.NECCMemberMaster.GetNECCMemberById(NECCMemberId);
        }

        public void Delete(int NECCMemberId)
        {
            DataAccess.Common.NECCMemberMaster.Delete(NECCMemberId);
        }

        public DataTable GetAllMember(string MemberName, string MobileNo, int DistrictId)
        {
            return DataAccess.Common.NECCMemberMaster.GetAllMember(MemberName, MobileNo, DistrictId);
        }

        public DataTable GetMobileNumbersForNECC()
        {
            return DataAccess.Common.NECCMemberMaster.GetMobileNumbersForNECC();
        }

        public DataTable GetAllMobileNumbersForNECC()
        {
            return DataAccess.Common.NECCMemberMaster.GetAllMobileNumbersForNECC();
        }

        public DataTable GetAllMemberLogDetails(string MemberName, string MobileNo, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.NECCMemberMaster.GetAllMemberLogDetails(MemberName, MobileNo, FromDate, ToDate);
        }

        //public DataTable MemberTypeWiseMobileNumbersForNECC(int MemberType)
        //{
        //    return DataAccess.Common.NECCMemberMaster.MemberTypeWiseMobileNumbersForNECC(MemberType);
        //}
    }
}
