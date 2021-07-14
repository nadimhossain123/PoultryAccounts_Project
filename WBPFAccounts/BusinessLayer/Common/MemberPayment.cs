using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberPayment
    {
        public MemberPayment()
        {
        }

        public DataTable GetServiceTaxReport(int FeesHeadId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.MemberPayment.GetServiceTaxReport(FeesHeadId, FromDate, ToDate);
        }

        public DataSet GetOutstandingReport(int MemberId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.MemberPayment.GetOutstandingReport(MemberId, FromDate, ToDate);
        }

        public DataTable GetFeesHeadWisePaymentReport(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int MemberId,int BusinessTypeId)
        {
            return DataAccess.Common.MemberPayment.GetFeesHeadWisePaymentReport(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, MemberId, BusinessTypeId);
        }

        public DataTable GetOutstanding(int MemberId, int PaymentId, int FinYrId)
        {
            return DataAccess.Common.MemberPayment.GetOutstanding(MemberId, PaymentId, FinYrId);
        }

        public string Save(Entity.Common.MemberPayment payment)
        {
            return DataAccess.Common.MemberPayment.Save(payment);
        }

        public void Approve(int PaymentId, int ApprovedBy)
        {
            DataAccess.Common.MemberPayment.Approve(PaymentId, ApprovedBy);
        }

        public void Delete(int PaymentId)
        {
            DataAccess.Common.MemberPayment.Delete(PaymentId);
        }

        public DataTable GetAllById(int PaymentId)
        {
            return DataAccess.Common.MemberPayment.GetAllById(PaymentId);
        }

        public DataTable GetServiceName(int MemberId)
        {
            return DataAccess.Common.MemberPayment.GetServiceName(MemberId);
        }

        public DataSet GetOutstandingReportConsolidated(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName,string MemberCode,string MobileNo,int BusinessTypeId,int ReportType)
        {
            return DataAccess.Common.MemberPayment.GetOutstandingReportConsolidated(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate, FromDate, MemberName,MemberCode, MobileNo,BusinessTypeId,ReportType);

        }
        public DataSet GetOutstandingReportConsolidatedNew(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo, int BusinessTypeId, int ReportType)
        {
            return DataAccess.Common.MemberPayment.GetOutstandingReportConsolidatedNew(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate, FromDate, MemberName, MemberCode, MobileNo, BusinessTypeId, ReportType);

        }
        public DataSet GetOutstandingReportConsolidatedNewForMedicineRepresentatives(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo, int BusinessTypeId, int ReportType)
        {
            return DataAccess.Common.MemberPayment.GetOutstandingReportConsolidatedNewForMedicineRepresentatives(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate, FromDate, MemberName, MemberCode, MobileNo, BusinessTypeId, ReportType);

        }

        public DataTable GetMemberUptoDateReport(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate)
        {
            return DataAccess.Common.MemberPayment.GetMemberUptoDateReport(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate);
        }

        public DataTable GetAgentWisePaymentReport(int AgentId, DateTime FromDate, DateTime ToDate, bool? IsApproved)
        {
            return DataAccess.Common.MemberPayment.GetAgentWisePaymentReport(AgentId, FromDate, ToDate, IsApproved);
        }

        public DataTable GetDevelopmentFeesReportConsolidated(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, string MemberName)
        {
            return DataAccess.Common.MemberPayment.GetDevelopmentFeesReportConsolidated(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate, MemberName);
        }

        public DataTable GetDevelopmentFeesPaymentReport(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int MemberId)
        {
            return DataAccess.Common.MemberPayment.GetDevelopmentFeesPaymentReport(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, MemberId);
        }

        public DataSet GetDevelopmentFeesOutstandingReport(int MemberId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.MemberPayment.GetDevelopmentFeesOutstandingReport(MemberId, FromDate, ToDate);
        }

        public DataTable GetFeeCollectionBusinessTypeReport(int DistrictId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.MemberPayment.GetFeeCollectionBusinessTypeReport(DistrictId, FromDate, ToDate);
        }

        public DataTable GetExecutiveCommitteeReport(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo)
        {
            return DataAccess.Common.MemberPayment.GetExecutiveCommitteeReport(StateId, DistrictId, BlockId, MembershipCategoryId, ToDate, FromDate, MemberName, MemberCode, MobileNo);
        }

        //Payment Gateway
        public void PaymentResponseSave(Entity.Common.PaymentGateway payment)
        {
            DataAccess.Common.MemberPayment.PaymentResponseSave(payment);
        }

        public int GenerateMemberBill_SpecialCase( int BlockId)
        {
            return DataAccess.Common.MemberPayment.GenerateMemberBill_SpecialCase(BlockId);

        }
    }
}
