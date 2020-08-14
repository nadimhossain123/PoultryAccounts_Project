using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Accounts
{
    public class StudentFeesCollection
    {
        public StudentFeesCollection()
        {
        }

        public static void Save(Entity.Accounts.StudentFeesCollection Fees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PaymentId", SqlDbType.Int, ParameterDirection.InputOutput, Fees.PaymentId);
                oDm.Add("@MemberId", SqlDbType.Int, ParameterDirection.Input, Fees.StudentId);
                oDm.Add("@Amount", SqlDbType.Decimal, ParameterDirection.Input, Fees.Amount);
                oDm.Add("@PaymentDate", SqlDbType.DateTime, ParameterDirection.Input, Fees.PaymentDate);
                oDm.Add("@CashBankLedgerID", SqlDbType.Int, ParameterDirection.Input, Fees.CashBankLedgerID);
                oDm.Add("@TransactionType", SqlDbType.VarChar, 40, ParameterDirection.Input, Fees.TransactionType);

                oDm.Add("@ModeOfPayment", SqlDbType.VarChar, 15, ParameterDirection.Input, Fees.ModeOfPayment);
                oDm.Add("@ChequeNo", SqlDbType.VarChar,30, ParameterDirection.Input, Fees.ChequeNo);

                if (Fees.ChequeDate == null)
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value);
                else 
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, Fees.ChequeDate); 

                oDm.Add("@DrawnOn", SqlDbType.VarChar, 50, ParameterDirection.Input, Fees.DrawnOn);
                oDm.Add("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, Fees.CreatedBy);

                oDm.Add("@PaymentDetails", SqlDbType.Xml, ParameterDirection.Input, Fees.PaymentDetailsXML);
                oDm.Add("@XMLCashBankVoucherDetails", SqlDbType.Xml, ParameterDirection.Input, Fees.XMLCashBankVoucherDetails);

                oDm.Add("@CompanyId", SqlDbType.Int, ParameterDirection.Input, Fees.CompanyId);
                oDm.Add("@BranchId", SqlDbType.Int, ParameterDirection.Input, Fees.BranchId);
                oDm.Add("@FinYrId", SqlDbType.Int, ParameterDirection.Input, Fees.FinYrId);
                oDm.Add("@FeesBookNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, Fees.FeesBookNumber);
                oDm.Add("@Narration", SqlDbType.NVarChar, 2000, ParameterDirection.Input, Fees.Narration);
                oDm.Add("@IsRefund", SqlDbType.Bit, Fees.IsRefund);
                
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_PaymentMaster_Save");
                Fees.PaymentId = (int)oDm["@PaymentId"].Value;

            }
        }
        public static void SaveRefundableFees(Entity.Accounts.StudentFeesCollection Fees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PaymentId", SqlDbType.Int, ParameterDirection.InputOutput, Fees.PaymentId);
                oDm.Add("@StudentId", SqlDbType.Int, ParameterDirection.Input, Fees.StudentId);
                oDm.Add("@SemNo", SqlDbType.Int, ParameterDirection.Input, Fees.SemNo);
                oDm.Add("@Amount", SqlDbType.Decimal, ParameterDirection.Input, Fees.Amount);
                oDm.Add("@PaymentDate", SqlDbType.DateTime, ParameterDirection.Input, Fees.PaymentDate);
                oDm.Add("@CashBankLedgerID", SqlDbType.Int, ParameterDirection.Input, Fees.CashBankLedgerID);
                oDm.Add("@TransactionType", SqlDbType.VarChar, 40, ParameterDirection.Input, Fees.TransactionType);

                oDm.Add("@ModeOfPayment", SqlDbType.VarChar, 15, ParameterDirection.Input, Fees.ModeOfPayment);
                oDm.Add("@ChequeNo", SqlDbType.VarChar, 30, ParameterDirection.Input, Fees.ChequeNo);

                if (Fees.ChequeDate == null)
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value);
                else
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, Fees.ChequeDate);

                oDm.Add("@DrawnOn", SqlDbType.VarChar, 50, ParameterDirection.Input, Fees.DrawnOn);
                oDm.Add("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, Fees.CreatedBy);

                oDm.Add("@PaymentDetails", SqlDbType.Xml, ParameterDirection.Input, Fees.PaymentDetailsXML);
                oDm.Add("@XMLCashBankVoucherDetails", SqlDbType.Xml, ParameterDirection.Input, Fees.XMLCashBankVoucherDetails);

                oDm.Add("@CompanyId", SqlDbType.Int, ParameterDirection.Input, Fees.CompanyId);
                oDm.Add("@BranchId", SqlDbType.Int, ParameterDirection.Input, Fees.BranchId);
                oDm.Add("@FinYrId", SqlDbType.Int, ParameterDirection.Input, Fees.FinYrId);
                oDm.Add("@FeesBookNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, Fees.FeesBookNumber);
                oDm.Add("@Narration", SqlDbType.NVarChar, 2000, ParameterDirection.Input, Fees.Narration);
                oDm.Add("@IsRefund", SqlDbType.Bit, Fees.IsRefund);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_RefundableFeesMaster_save");
                Fees.PaymentId = (int)oDm["@PaymentId"].Value;
            }
        }

        public static Entity.Accounts.StudentFeesCollection GetAllById(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@PaymentId", SqlDbType.Int, ParameterDirection.Input, PaymentId);

                SqlDataReader dr = oDm.ExecuteReader("usp_PaymentMaster_GetAllById");
                Entity.Accounts.StudentFeesCollection Fees = new Entity.Accounts.StudentFeesCollection();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Fees.PaymentId = PaymentId;
                        Fees.MoneyReceiptNo = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Fees.StudentId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        Fees.Amount = (dr[3] == DBNull.Value) ? 0 : decimal.Parse(dr[3].ToString());
                        Fees.PaymentDate = (dr[4] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[4].ToString());
                        Fees.ModeOfPayment = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                        Fees.ChequeNo = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();

                        if (dr[7] == DBNull.Value) { Fees.ChequeDate = null; }
                        else { Fees.ChequeDate = DateTime.Parse(dr[7].ToString()); }
                        Fees.DrawnOn = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();
                    }
                }
                return Fees;
            }
        }

        public static Entity.Accounts.StudentFeesCollection Refund_GetAllById( int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@PaymentId", SqlDbType.Int, ParameterDirection.Input, PaymentId);

                SqlDataReader dr = oDm.ExecuteReader("usp_RefundFeesMaster_GetAllById");
                Entity.Accounts.StudentFeesCollection Fees = new Entity.Accounts.StudentFeesCollection();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Fees.PaymentId = PaymentId;
                        Fees.MoneyReceiptNo = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Fees.StudentId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        Fees.Amount = (dr[3] == DBNull.Value) ? 0 : decimal.Parse(dr[3].ToString());
                        Fees.PaymentDate = (dr[4] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[4].ToString());
                        Fees.ModeOfPayment = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                        Fees.ChequeNo = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();

                        if (dr[7] == DBNull.Value) { Fees.ChequeDate = null; }
                        else { Fees.ChequeDate = DateTime.Parse(dr[7].ToString()); }
                        Fees.DrawnOn = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();

                        Fees.CBVHeaderId = (dr[9] == DBNull.Value) ? 0 : Convert.ToInt32(dr[9].ToString());
                    }
                }
                return Fees;
            }
        }

        public static DataSet GetStudentUnpaidTrans(int StudentId, int FinYrId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@MemberId", SqlDbType.Int, StudentId);
                if (FinYrId == 0)
                    oDm.Add("@FinYearID", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@FinYearID", SqlDbType.Int, FinYrId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_GetMemberUnpaidTrans", ref ds, "table");
            }
        }

        public static DataSet GetStudentUnpaidTransJV(int StudentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@StudentId", SqlDbType.Int, StudentId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_GetStudentUnpaidTransJV", ref ds, "table");
            }
        }

        public static DataSet GetStudentAdvanceTrans(int StudentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StudentId", SqlDbType.Int, StudentId);
                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_GetStudentAdvanceTrans", ref ds, "table");
            }
        }

        public static DataSet  GetStudentOutstandingReport(int StudentId,string FromDate,string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@MemberId", SqlDbType.Int, StudentId);
                if (FromDate.Trim().Length > 0)
                    oDm.Add("@FromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate + " 00:00:00"));
                else
                    oDm.Add("@FromDate", SqlDbType.DateTime, DBNull.Value);

                if (ToDate.Trim().Length > 0)
                    oDm.Add("@ToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate + " 23:59:59"));
                else
                    oDm.Add("@ToDate", SqlDbType.DateTime, DBNull.Value);

                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_GetMemberOutstandingReport", ref ds, "tbl");
            }
        }

        public static DataSet GetMoneyReceipt(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@PaymentId", SqlDbType.Int, PaymentId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_MoneyReceipt", ref ds, "table");
            }
        }

        public static DataSet GetRefundMoneyReceipt(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@PaymentId", SqlDbType.Int, PaymentId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_RefundableFeesMoneyReceipt", ref ds, "table");
            }
        }

        //StudentPaymentDetails GetAll Filter by CompanyId
        public static DataSet GetAll(string MoneyReceiptNo, string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                if (MoneyReceiptNo.Trim().Length == 0)
                { oDm.Add("@MoneyReceiptNo", SqlDbType.VarChar, 50, DBNull.Value); }
                else { oDm.Add("@MoneyReceiptNo", SqlDbType.VarChar, 50, MoneyReceiptNo); }

                if (FromDate.Trim().Length == 0)
                {
                    oDm.Add("@FromDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@FromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0)
                {
                    oDm.Add("@ToDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@ToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_PaymentMaster_GetAll", ref ds, "table");
            }
        }

        public static void Delete(int PaymentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PaymentId", SqlDbType.Int, PaymentId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_PaymentMaster_Delete");
            }
        }

        //Student Refundable Transaction
        public static DataSet GetStudentRefundableTrans(int StudentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StudentId", SqlDbType.Int, StudentId);
                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_GetStudentRefundableTrans", ref ds, "table");
            }
        }

        public static void DeleteCVB(int CBVHeaderId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CBVHeaderId", SqlDbType.Int, CBVHeaderId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("[usp_CashBankVoucher_Delete]");
            }
        }
    }
}
