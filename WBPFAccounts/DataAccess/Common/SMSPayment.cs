using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class SMSPayment
    {
        public SMSPayment()
        {
        }

        public static DataTable GetServiceTaxReport(int SMSCategoryId, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pSMSCategoryId", SqlDbType.Int, SMSCategoryId);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSPayment_GetServiceTaxReport");
            }
        }

        public static DataTable GetAll(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int SMSMemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (!string.IsNullOrEmpty(PaymentNo))
                    oDm.Add("@pPaymentNo", SqlDbType.VarChar, 50, PaymentNo);
                else
                    oDm.Add("@pPaymentNo", SqlDbType.VarChar, 50, DBNull.Value);

                if (CashBankLedgerId != 0)
                    oDm.Add("@pCashBankLedgerId", SqlDbType.Int, CashBankLedgerId);
                else
                    oDm.Add("@pCashBankLedgerId", SqlDbType.Int, DBNull.Value);

                if (!PaymentMode.Equals("All"))
                    oDm.Add("@pPaymentMode", SqlDbType.VarChar, 50, PaymentMode);
                else
                    oDm.Add("@pPaymentMode", SqlDbType.VarChar, 50, DBNull.Value);

                if (FromDate == DateTime.MinValue)
                    oDm.Add("@pFromDate", SqlDbType.Date, DBNull.Value);
                else
                    oDm.Add("@pFromDate", SqlDbType.Date, FromDate);

                if (ToDate == DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.Date, DBNull.Value);
                else
                    oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                if (SMSMemberId != 0)
                    oDm.Add("@pSMSMemberId", SqlDbType.Int, SMSMemberId);
                else
                    oDm.Add("@pSMSMemberId", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSPayment_GetAll");
            }
        }

        public static DataTable GetCategoryWiseDetail(int SMSMemberId, int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pSMSMemberId", SqlDbType.Int, SMSMemberId);
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSPayment_GetCategoryWiseDetail");
            }
        }

        public static string Save(Entity.Common.SMSPayment payment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, payment.PaymentId);
                oDm.Add("@pSMSMemberId", SqlDbType.Int, payment.SMSMemberId);
                oDm.Add("@pPaymentMode", SqlDbType.VarChar, 50, payment.PaymentMode);
                oDm.Add("@pPaymentDate", SqlDbType.Date, payment.PaymentDate);
                oDm.Add("@pPaymentAmount", SqlDbType.Decimal, payment.PaymentAmount);
                oDm.Add("@pNarration", SqlDbType.VarChar, 500, payment.Narration);
                oDm.Add("@pCreatedBy", SqlDbType.Int, payment.CreatedBy);
                oDm.Add("@pCreatedByUserType", SqlDbType.VarChar, 50, payment.CreatedByUserType);
                oDm.Add("@pCashBankLedgerId", SqlDbType.Int, payment.CashBankLedgerId);
                oDm.Add("@pFeesXml", SqlDbType.Xml, payment.FeesXml);
                oDm.Add("@pPaymentNo", SqlDbType.VarChar, 50, ParameterDirection.InputOutput, "");
                oDm.Add("@pSMSEndDate", SqlDbType.Date, payment.SMSEndDate);

                if (payment.IsApproved == null)
                    oDm.Add("@pIsApproved", SqlDbType.Bit, DBNull.Value);
                else
                    oDm.Add("@pIsApproved", SqlDbType.Bit, payment.IsApproved);

                if (payment.ApprovedBy == null)
                    oDm.Add("@pApprovedBy", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pApprovedBy", SqlDbType.Int, payment.ApprovedBy);

                if (payment.ApprovedDate == null)
                    oDm.Add("@pApprovedDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("@pApprovedDate", SqlDbType.DateTime, payment.ApprovedDate);

                oDm.Add("@pIsExcelUpload", SqlDbType.Bit, payment.IsExcelUpload);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSPayment_Save");
                return (string)oDm["@pPaymentNo"].Value;
            }
        }

        public static DataTable GetAllById(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSPayment_GetAllById");
            }
        }

        public static void Approve(int PaymentId, int ApprovedBy)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.Add("@pApprovedBy", SqlDbType.Int, ApprovedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSPayment_Approve");
            }
        }

        public static void Delete(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_SMSPayment_Delete");
            }
        }

        public static DataTable GetAgentWisePaymentReport(int AgentId, DateTime FromDate, DateTime ToDate, bool? IsApproved)
        {
            using (DataManager oDm = new DataManager())
            {
                if (AgentId != 0)
                    oDm.Add("@pAgentId", SqlDbType.Int, AgentId);
                else
                    oDm.Add("@pAgentId", SqlDbType.Int, DBNull.Value);

                if (FromDate == DateTime.MinValue)
                    oDm.Add("@pFromDate", SqlDbType.Date, DBNull.Value);
                else
                    oDm.Add("@pFromDate", SqlDbType.Date, FromDate);

                if (ToDate == DateTime.MinValue)
                    oDm.Add("@pToDate", SqlDbType.Date, DBNull.Value);
                else
                    oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                if (IsApproved != null)
                    oDm.Add("@pIsApproved", SqlDbType.Bit, IsApproved);
                else
                    oDm.Add("@pIsApproved", SqlDbType.Bit, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SMSPayment_GetAgentWisePaymentReport");
            }
        }
    }
}
