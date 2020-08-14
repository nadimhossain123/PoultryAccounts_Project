using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberMaster
    {
        public MemberMaster()
        {

        }

        public void ChangePassword(int MemberId, string Password)
        {
            DataAccess.Common.MemberMaster.ChangePassword(MemberId, Password);
        }

        public void MemberActivate(int MemberId, bool IsActive)
        {
            DataAccess.Common.MemberMaster.MemberActivate(MemberId, IsActive);
        }

        public void MemberApprove(int MemberId, int CreatedBy)
        {
            DataAccess.Common.MemberMaster.MemberApprove(MemberId, CreatedBy);
        }

        public void Save(Entity.Common.MemberMaster membermaster)
        {
            DataAccess.Common.MemberMaster.Save(membermaster);
        }

        public void MemberMaster_BlockChange_Save(Entity.Common.MemberMaster membermaster)
        {
            DataAccess.Common.MemberMaster.MemberMaster_BlockChange_Save(membermaster);
        }

        public DataTable GetAll(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            return DataAccess.Common.MemberMaster.GetAll(membermaster, isMember);
        }
        public DataTable GetAllMemberWithAmount(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            //return DataAccess.Common.MemberMaster.GetAll(membermaster, isMember);
            return DataAccess.Common.MemberMaster.GetAllMemberWithAmount(membermaster, isMember);
        }

        public DataTable MemberFeesSummery_GetAll(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            return DataAccess.Common.MemberMaster.MemberFeesSummery_GetAll(membermaster, isMember);
        }

        public DataTable MemberMaster_GetAll_ForSMS(int finYearID, bool includeGovtMembers, int memberSMSCategoryId)
        {
            return DataAccess.Common.MemberMaster.MemberMaster_GetAll_ForSMS(finYearID, includeGovtMembers, memberSMSCategoryId);
        }

        public DataTable MemberMaster_GetAll_ForReport(Entity.Common.MemberMaster membermaster)
        {
            return DataAccess.Common.MemberMaster.MemberMaster_GetAll_ForReport(membermaster);
        }

        public Entity.Common.MemberMaster GetMemberMasterById(int memberId)
        {
            return DataAccess.Common.MemberMaster.GetMemberMasterById(memberId);
        }

        public void Delete(int memberId)
        {
            DataAccess.Common.MemberMaster.Delete(memberId);
        }

        public DataTable GetDistrictAndStateByBlockId(int blockid)
        {
            return DataAccess.Common.MemberMaster.GetDistrictAndStateByBlockId(blockid);
        }

        public void MemberMasterLedgerDefaultConfiguration_Update(Entity.Common.MemberMaster membermaster)
        {
            DataAccess.Common.MemberMaster.MemberMasterLedgerDefaultConfiguration_Update(membermaster);
        }

        public Entity.Common.MemberMaster MemberMasterLedgerDefaultConfiguration_Get()
        {
            return DataAccess.Common.MemberMaster.MemberMasterLedgerDefaultConfiguration_Get();
        }

        public int MemberMaster_Priority_Update(int memberid, bool ispriority)
        {
            return DataAccess.Common.MemberMaster.MemberMaster_Priority_Update(memberid, ispriority);
        }

        public int SMSSubscription_Save(Entity.Common.MemberMaster membermaster)
        {
            return DataAccess.Common.MemberMaster.SMSSubscription_Save(membermaster);
        }

        public DataTable SMSSubscription_GetAll(int finyearid)
        {
            return DataAccess.Common.MemberMaster.SMSSubscription_GetAll(finyearid);
        }

        public int SMSSubscription_Block(int memberId, bool isBlock)
        {
            return DataAccess.Common.MemberMaster.SMSSubscription_Block(memberId, isBlock);
        }

        public DataTable GetAllOutstandingReport(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            return DataAccess.Common.MemberMaster.GetAllOutstandingReport(membermaster, isMember);
        }

        public DataTable DevelopmentMemberGetAll(int StateId, int DistrictId, int BlockId, int CategoryId, string MemberName, string MobileNo,int BusinessTypeId,DateTime FromDate,DateTime ToDate)
        {
            return DataAccess.Common.MemberMaster.DevelopmentMemberGetAll(StateId, DistrictId, BlockId, CategoryId, MemberName, MobileNo, BusinessTypeId, FromDate,ToDate);
        }
        public DataTable RenewalMemberGetAll(int StateId, int DistrictId, int BlockId, int CategoryId, string MemberName, string MobileNo, int BusinessTypeId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.MemberMaster.RenewalMemberGetAll(StateId, DistrictId, BlockId, CategoryId, MemberName, MobileNo, BusinessTypeId, FromDate, ToDate);
        }
    }
}