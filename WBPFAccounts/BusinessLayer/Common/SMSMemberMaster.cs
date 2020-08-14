using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class SMSMemberMaster
    {
        public SMSMemberMaster()
        {
        }

        public int Save(Entity.Common.SMSMember smsmember, DateTime StartDate, DateTime EndDate, string txtRemarks)
        {
            return DataAccess.Common.SMSMemberMaster.Save(smsmember, StartDate, EndDate, txtRemarks);
        }

        public Entity.Common.SMSMember GetSMSMemberById(int sMSMemberId)
        {
            return DataAccess.Common.SMSMemberMaster.GetSMSMemberById(sMSMemberId);
        }

        public void Delete(int sMSMemberId)
        {
            DataAccess.Common.SMSMemberMaster.Delete(sMSMemberId);
        }

        public DataTable GetAllMember(string MemberName,string MobileNo,int MemberType,int DistrictId, int MemberCategoryId)
        {
            return DataAccess.Common.SMSMemberMaster.GetAllMember(MemberName, MobileNo, MemberType, DistrictId, MemberCategoryId);
        }

        public DataTable GetAllMemberForExpireDetails(string MemberName, string MobileNo, int MemberType, int DistrictId, int MemberCategoryId,DateTime FromDate,DateTime ToDate )
        {
            return DataAccess.Common.SMSMemberMaster.GetAllMemberFroExpireDetails(MemberName, MobileNo, MemberType, DistrictId, MemberCategoryId,FromDate,ToDate);
        }

        public DataTable GetMobileNumbersForSMS()
        {
            return DataAccess.Common.SMSMemberMaster.GetMobileNumbersForSMS();
        }

        public DataTable GetAllDeviceIdForNotification()
        {
            return DataAccess.Common.SMSMemberMaster.GetAllDeviceIdForNotification();
        }




        public DataTable GetAllMobileNumbersForSMS()
        {
            return DataAccess.Common.SMSMemberMaster.GetAllMobileNumbersForSMS();
        }

        public DataTable GetAllMemberLogDetails(string MemberName, string MobileNo, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.SMSMemberMaster.GetAllMemberLogDetails(MemberName, MobileNo, FromDate, ToDate);
        }
        
        public DataTable MemberTypeWiseMobileNumbersForSMS(int MemberType)
        {
            return DataAccess.Common.SMSMemberMaster.MemberTypeWiseMobileNumbersForSMS(MemberType);
        }
    }
}
