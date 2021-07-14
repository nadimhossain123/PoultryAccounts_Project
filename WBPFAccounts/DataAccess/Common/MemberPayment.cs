using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class MemberPayment
    {
        public MemberPayment()
        {
        }

        public static DataTable GetServiceTaxReport(int FeesHeadId, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pFeesHeadId", SqlDbType.Int, FeesHeadId);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetServiceTaxReport");
            }
        }

        public static DataSet GetOutstandingReport(int MemberId, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReport", ref ds, "table");
            }
        }

        public static DataTable GetFeesHeadWisePaymentReport(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int MemberId,int BusinessTypeId)
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

                if (MemberId != 0)
                    oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                else
                    oDm.Add("@pMemberId", SqlDbType.Int, DBNull.Value);
                if (BusinessTypeId != 0)
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetFeesHeadWisePaymentReport");
            }
        }

        public static DataTable GetOutstanding(int MemberId, int PaymentId, int FinYrId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, MemberId);
                oDm.Add("@PaymentId", SqlDbType.Int, PaymentId);
                oDm.Add("@FinYrId", SqlDbType.Int, FinYrId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetOutstanding");
            }
        }

        public static string Save(Entity.Common.MemberPayment payment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, ParameterDirection.InputOutput, payment.PaymentId);
                oDm.Add("@pMemberId", SqlDbType.Int, payment.MemberId);
                oDm.Add("@pPaymentMode", SqlDbType.VarChar, 50, payment.PaymentMode);
                oDm.Add("@pPaymentDate", SqlDbType.Date, payment.PaymentDate);
                oDm.Add("@pPaymentAmount", SqlDbType.Decimal, payment.PaymentAmount);
                oDm.Add("@pNarration", SqlDbType.VarChar, 500, payment.Narration);
                oDm.Add("@pCreatedBy", SqlDbType.Int, payment.CreatedBy);
                oDm.Add("@pCreatedByUserType", SqlDbType.VarChar, 50, payment.CreatedByUserType);
                oDm.Add("@pCashBankLedgerId", SqlDbType.Int, payment.CashBankLedgerId);
                oDm.Add("@pFeesXml", SqlDbType.Xml, payment.FeesXml);
                oDm.Add("@pPaymentNo", SqlDbType.VarChar, 50, ParameterDirection.InputOutput, "");

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
                oDm.ExecuteNonQuery("usp_MemberPayment_Save");
                payment.PaymentId = (int)oDm["@pPaymentId"].Value;
                return (string)oDm["@pPaymentNo"].Value;
            }
        }

        public static void Approve(int PaymentId, int ApprovedBy)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.Add("@pApprovedBy", SqlDbType.Int, ApprovedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberPayment_Approve");
            }
        }

        public static void Delete(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberPayment_Delete");
            }
        }

        public static DataTable GetAllById(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, PaymentId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetAllById");
            }
        }

        public static DataTable GetServiceName(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberId", SqlDbType.Int, MemberId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetServiceName");
            }
        }

        public static DataSet GetOutstandingReportConsolidated(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName,string MemberCode,string MobileNo,int BusinessTypeId,int ReportType)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId.Equals(0))
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId.Equals(0))
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId.Equals(0))
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                if (MemberCode !=null && MemberCode.Length >0)
                    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, MemberCode);
                else
                    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, DBNull.Value);
                if (MobileNo!=null && MobileNo.Length > 0)
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                else
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);

                if (BusinessTypeId.Equals(0))
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);

                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                if(ReportType.Equals(2))
                return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportConsolidated_PAID", ref ds, "Table");
                else
                return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportConsolidated", ref ds, "Table");

            }
        }
        public static DataSet GetOutstandingReportConsolidatedNewForMedicineRepresentatives(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo, int BusinessTypeId, int ReportType)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId.Equals(0))
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId.Equals(0))
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId.Equals(0))
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                //if (MemberCode != null && MemberCode.Length > 0)
                //    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, MemberCode);
                //else
                //    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, DBNull.Value);
                //if (MobileNo != null && MobileNo.Length > 0)
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                //else
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);
                if (BusinessTypeId.Equals(0))
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);
                if (ReportType.Equals(0))
                    oDm.Add("@pFeesheadId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pFeesheadId", SqlDbType.Int, ReportType);
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                if (ReportType.Equals(2))
                   return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportForConsolidatedForMedicineRepresentatives", ref ds, "Table");
                else
                   return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportForConsolidated_Development", ref ds, "Table");


            }
        }
        
        public static DataSet GetOutstandingReportConsolidatedNew(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo, int BusinessTypeId, int ReportType)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId.Equals(0))
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId.Equals(0))
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId.Equals(0))
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                //if (MemberCode != null && MemberCode.Length > 0)
                //    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, MemberCode);
                //else
                //    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, DBNull.Value);
                //if (MobileNo != null && MobileNo.Length > 0)
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                //else
                //    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);
                if (BusinessTypeId.Equals(0))
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBusinessTypeId", SqlDbType.Int, BusinessTypeId);
                if (ReportType.Equals(0))
                    oDm.Add("@pFeesheadId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pFeesheadId", SqlDbType.Int, ReportType);
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                if (ReportType.Equals(2))
                    return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportForConsolidated", ref ds, "Table");
                else
                    return oDm.GetDataSet("usp_MemberPayment_GetOutstandingReportForConsolidated_Development", ref ds, "Table");


            }
        }

        public static DataTable GetMemberUptoDateReport(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate)
            {
            using (DataManager oDm = new DataManager())
            {
                if (StateId.Equals(0))
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId.Equals(0))
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId.Equals(0))
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberUptoDateReport");
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
                return oDm.ExecuteDataTable("usp_MemberPayment_GetAgentWisePaymentReport");
            }
        }

        public static DataTable GetDevelopmentFeesReportConsolidated(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, string MemberName)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId == 0)
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId == 0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId == 0)
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId == 0)
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                if (string.IsNullOrEmpty(MemberName.Trim()))
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetDevelopmentFeesReportConsolidated");
            }
        }

        public static DataTable GetDevelopmentFeesPaymentReport(string PaymentNo, int CashBankLedgerId, string PaymentMode, DateTime FromDate, DateTime ToDate, int MemberId)
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

                if (MemberId != 0)
                    oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                else
                    oDm.Add("@pMemberId", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberPayment_GetDevelopmentFeesPaymentReport");
            }
        }

        public static DataSet GetDevelopmentFeesOutstandingReport(int MemberId, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                oDm.Add("@pToDate", SqlDbType.Date, ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_MemberPayment_GetDevelopmentFeesOutstandingReport", ref ds, "table");
            }
        }

        public static DataTable GetFeeCollectionBusinessTypeReport(int DistrictId, DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if(DistrictId!=0)
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);
                else oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                oDm.Add("@pToDate", SqlDbType.Date, ToDate);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_BusinessTypeWise_GetFeeCollectionReport");
            }
        }

        public static DataTable GetExecutiveCommitteeReport(int StateId, int DistrictId, int BlockId, int MembershipCategoryId, DateTime ToDate, DateTime FromDate, string MemberName, string MemberCode, string MobileNo)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId.Equals(0))
                    oDm.Add("@pStateId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStateId", SqlDbType.Int, StateId);

                if (DistrictId.Equals(0))
                    oDm.Add("@pDistrictId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pDistrictId", SqlDbType.Int, DistrictId);

                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

                if (MembershipCategoryId.Equals(0))
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);

                oDm.Add("@pToDate", SqlDbType.Date, ToDate);
                oDm.Add("@pFromDate", SqlDbType.Date, FromDate);
                if (MemberName == "")
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, DBNull.Value);
                else
                    oDm.Add("@pMemberName", SqlDbType.VarChar, 50, MemberName);
                if (MemberCode != null && MemberCode.Length > 0)
                    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, MemberCode);
                else
                    oDm.Add("@pMemberCode", SqlDbType.VarChar, 50, DBNull.Value);
                if (MobileNo != null && MobileNo.Length > 0)
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, MobileNo);
                else
                    oDm.Add("@pMobileNo", SqlDbType.VarChar, 20, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberReport_ExecutiveCommittee");
            }
        }
        //paymet Gateway
        public static void PaymentResponseSave(Entity.Common.PaymentGateway payment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPaymentId", SqlDbType.Int, ParameterDirection.InputOutput, payment.PaymentId);
                oDm.Add("@pMemberId", SqlDbType.Int, payment.MemberId);
                oDm.Add("@pMemberType", SqlDbType.VarChar, 10, payment.MemberType);
                oDm.Add("@pOrderId", SqlDbType.VarChar, 30, payment.OrderId);
                if (payment.TrackingId != null && payment.TrackingId.Length > 0)
                    oDm.Add("@pTrackingId", SqlDbType.VarChar, 20, payment.TrackingId);
                else oDm.Add("@pTrackingId", SqlDbType.VarChar, 20, DBNull.Value);
                if (payment.BankRefNo != null && payment.BankRefNo.Length > 0)
                    oDm.Add("@pBankRefNo", SqlDbType.VarChar, 100, payment.BankRefNo);
                else oDm.Add("@pBankRefNo", SqlDbType.VarChar, 100, DBNull.Value);
                if (payment.OrderStatus != null && payment.OrderStatus.Length > 0)
                    oDm.Add("@pOrderStatus", SqlDbType.VarChar, 20, payment.OrderStatus);
                else oDm.Add("@pOrderStatus", SqlDbType.VarChar, 20, DBNull.Value);
                if (payment.FailureMessage != null && payment.FailureMessage.Length > 0)
                    oDm.Add("@pFailureMessage", SqlDbType.VarChar, 500, payment.FailureMessage);
                else oDm.Add("@pFailureMessage", SqlDbType.VarChar, 500, DBNull.Value);
                if (payment.PaymentMode != null && payment.PaymentMode.Length > 0)
                    oDm.Add("@pPaymentMode", SqlDbType.VarChar, 20, payment.PaymentMode);
                else oDm.Add("@pPaymentMode", SqlDbType.VarChar, 20, DBNull.Value);
                if (payment.CardName != null && payment.CardName.Length > 0)
                    oDm.Add("@pCardName", SqlDbType.VarChar, 30, payment.CardName);
                else oDm.Add("@pCardName", SqlDbType.VarChar, 30, DBNull.Value);
                if (payment.StatusCode != null && payment.StatusCode.Length > 0)
                    oDm.Add("@pStatusCode", SqlDbType.VarChar, 5, payment.StatusCode);
                else oDm.Add("@pStatusCode", SqlDbType.VarChar, 5, DBNull.Value);
                if (payment.StatusMessage != null && payment.StatusMessage.Length > 0)
                    oDm.Add("@pStatusMessage", SqlDbType.VarChar, 300, payment.StatusMessage);
                else oDm.Add("@pStatusMessage", SqlDbType.VarChar, 300, DBNull.Value);
                oDm.Add("@pPaymentAmount", SqlDbType.Decimal, payment.PaymentAmount);
                oDm.Add("@pCurrency", SqlDbType.VarChar, 3, payment.Currency);
                if (payment.PaymentDate == DateTime.MinValue)
                    oDm.Add("@pPaymentDate", SqlDbType.DateTime, DBNull.Value);
                else oDm.Add("@pPaymentDate", SqlDbType.DateTime, payment.PaymentDate);
                oDm.Add("@pCreatedBy", SqlDbType.Int, payment.CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberPaymentGateway_Save");
            }
        }

        public static int GenerateMemberBill_SpecialCase(int BlockId)
        {
            using (DataManager oDm = new DataManager())
            {
               
                if (BlockId.Equals(0))
                    oDm.Add("@pBlockId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pBlockId", SqlDbType.Int, BlockId);

               
                oDm.CommandType = CommandType.StoredProcedure;

               int i= oDm.ExecuteNonQuery("MemberBill_Generate_SpecialCase");

                return i;

            }
        }




    }
}
