using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class SMSPayment
    {
        public SMSPayment()
        {
        }

        public DataTable GetServiceTaxReport(int SMSCategoryId, DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.Common.SMSPayment.GetServiceTaxReport(SMSCategoryId, FromDate, ToDate);
        }

        public DataTable GetAll(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int SMSMemberId)
        {
            return DataAccess.Common.SMSPayment.GetAll(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, SMSMemberId);
        }

        public DataTable GetCategoryWiseDetail (int SMSMemberId, int PaymentId)
        {
            return DataAccess.Common.SMSPayment.GetCategoryWiseDetail(SMSMemberId, PaymentId);
        }

        public string Save(Entity.Common.SMSPayment payment)
        {
            return DataAccess.Common.SMSPayment.Save(payment);
        }

        public DataTable GetAllById(int PaymentId)
        {
            return DataAccess.Common.SMSPayment.GetAllById(PaymentId);
        }

        public void Approve(int PaymentId, int ApprovedBy)
        {
            DataAccess.Common.SMSPayment.Approve(PaymentId, ApprovedBy);
        }

        public void Delete(int PaymentId)
        {
            DataAccess.Common.SMSPayment.Delete(PaymentId);
        }

        public DataTable GetAgentWisePaymentReport(int AgentId, DateTime FromDate, DateTime ToDate, bool? IsApproved)
        {
            return DataAccess.Common.SMSPayment.GetAgentWisePaymentReport(AgentId, FromDate, ToDate, IsApproved);
        }
    }
}
