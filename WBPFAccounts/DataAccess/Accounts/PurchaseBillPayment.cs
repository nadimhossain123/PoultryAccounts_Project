using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class PurchaseBillPayment
    {
        public PurchaseBillPayment()
        {
        }

        public static DataTable GetDueBills(int SupplierLedgerID_FK, int CompanyID_FK)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@SupplierLedgerID_FK", SqlDbType.Int, SupplierLedgerID_FK);
                oDm.Add("@CompanyID_FK", SqlDbType.Int, CompanyID_FK);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PurchaseBillPayment_GetDueBills");
            }
        }

        public static void Save(Entity.Accounts.PurchaseBillPayment Payment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PurchaseBillPaymentId", SqlDbType.Int, ParameterDirection.Input, Payment.PurchaseBillPaymentId);
                oDm.Add("@SupplierLedgerId_FK", SqlDbType.Int, ParameterDirection.Input, Payment.SupplierLedgerId_FK);
                oDm.Add("@CashBankLedgerID", SqlDbType.Int, ParameterDirection.Input, Payment.CashBankLedgerID);
                oDm.Add("@TransactionType", SqlDbType.VarChar,40, ParameterDirection.Input, Payment.TransactionType);
                oDm.Add("@AmountPaid", SqlDbType.Decimal, ParameterDirection.Input, Payment.AmountPaid);
                oDm.Add("@AmountDeducted", SqlDbType.Decimal, ParameterDirection.Input, Payment.AmountDeducted);
                oDm.Add("@PaymentDate", SqlDbType.DateTime, ParameterDirection.Input, Payment.PaymentDate);
                oDm.Add("@ModeOfPayment", SqlDbType.VarChar,15, ParameterDirection.Input, Payment.ModeOfPayment);
                oDm.Add("@ChequeNo", SqlDbType.VarChar,30, ParameterDirection.Input, Payment.ChequeNo);

                if (Payment.ChequeDate == null)
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value);
                else 
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, ParameterDirection.Input, Payment.ChequeDate); 

                oDm.Add("@DrawnOn", SqlDbType.VarChar,50, ParameterDirection.Input, Payment.DrawnOn);
                oDm.Add("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, Payment.CreatedBy);
                oDm.Add("@CompanyId", SqlDbType.Int, ParameterDirection.Input, Payment.CompanyId);
                oDm.Add("@BranchId", SqlDbType.Int, ParameterDirection.Input, Payment.BranchId);
                oDm.Add("@FinYrId", SqlDbType.Int, ParameterDirection.Input, Payment.FinYrId);
                oDm.Add("@Narration", SqlDbType.NVarChar, 500, Payment.Narration);
                oDm.Add("@XMLCashBankVoucherDetails", SqlDbType.Xml, ParameterDirection.Input, Payment.XMLCashBankVoucherDetails);
                oDm.Add("@PaymentDetailsXML", SqlDbType.Xml, ParameterDirection.Input, Payment.PaymentDetailsXML);

                if (Payment.DeductionDetailsXML.Trim().Length > 0)
                    oDm.Add("@DeductionDetailsXML", SqlDbType.Xml, ParameterDirection.Input, Payment.DeductionDetailsXML);
                else
                    oDm.Add("@DeductionDetailsXML", SqlDbType.Xml, ParameterDirection.Input, DBNull.Value);

                oDm.Add("@CBVHeaderID", SqlDbType.Int, ParameterDirection.InputOutput, 0);
                
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_PurchaseBillPayment_Save");
                Payment.CBVHeaderID = (int)oDm["@CBVHeaderID"].Value;
            }
        }

        public static DataTable GetAll(int SupplierLedgerId_FK, DateTime? Fromdate, DateTime? Todate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (SupplierLedgerId_FK == 0)
                {
                    oDm.Add("@SupplierLedgerId_FK", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@SupplierLedgerId_FK", SqlDbType.Int, SupplierLedgerId_FK); }

                if (Fromdate == null)
                { oDm.Add("@FromDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value); }
                else { oDm.Add("@FromDate", SqlDbType.DateTime, ParameterDirection.Input, Fromdate); }

                if (Todate == null)
                { oDm.Add("@ToDate", SqlDbType.DateTime, ParameterDirection.Input, DBNull.Value); }
                else { oDm.Add("@ToDate", SqlDbType.DateTime, ParameterDirection.Input, Todate); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PurchaseBillPayment_GetAll");
            }
        }

        public static DataTable GetAllById(int PurchaseBillId)
        {
            using (DataManager oDm = new DataManager())
            {
               oDm.Add("@PurchaseBillId", SqlDbType.Int, ParameterDirection.Input, PurchaseBillId);
               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteDataTable("usp_PurchaseBillPayment_GeyAllById");
            }
        }
    }
}
