using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class TrnDebitNoteHeader
    {
        public TrnDebitNoteHeader()
        {
        }

        public static int Save(Entity.Accounts.TrnDebitNoteHeader DebitNote)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@DNHeaderID", SqlDbType.Int, ParameterDirection.InputOutput, DebitNote.DNHeaderID);
                oDm.Add("@CompanyID_FK", SqlDbType.Int, ParameterDirection.Input, DebitNote.CompanyID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, ParameterDirection.Input, DebitNote.FinYearID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, ParameterDirection.Input, DebitNote.BranchID_FK);
                oDm.Add("@DNVoucherDate", SqlDbType.DateTime, ParameterDirection.Input, DebitNote.DNVoucherDate);
                oDm.Add("@SupplierLedgerID_FK", SqlDbType.Int, ParameterDirection.Input, DebitNote.SupplierLedgerID_FK);
                oDm.Add("@TotalAmount", SqlDbType.Decimal, ParameterDirection.Input, DebitNote.TotalAmount);
                oDm.Add("@DNNarration", SqlDbType.NVarChar,4000, ParameterDirection.Input, DebitNote.DNNarration);
                oDm.Add("@OperationBy", SqlDbType.Int, ParameterDirection.Input, DebitNote.OperationBy);
                oDm.Add("@XMLDebitNoteDetails", SqlDbType.NText, ParameterDirection.Input, DebitNote.XMLDebitNoteDetails);

                oDm.CommandType = CommandType.StoredProcedure;
                int RowsAffected = oDm.ExecuteNonQuery("usp_TrnDebitNoteHeader_Save");
                DebitNote.DNHeaderID = (int)oDm["@DNHeaderID"].Value;

                return RowsAffected;
            }
        }

        public static DataTable GetAll(int CompanyID, int FinYearID, int BranchID, string DNVoucherNo, string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CompanyID", SqlDbType.Int, CompanyID);
                oDm.Add("@FinYearID", SqlDbType.Int, FinYearID);
                oDm.Add("@BranchID", SqlDbType.Int, BranchID);

                if (DNVoucherNo.Length > 0)
                    oDm.Add("@DNVoucherNo", SqlDbType.VarChar, 40, DNVoucherNo);
                else
                    oDm.Add("@DNVoucherNo", SqlDbType.VarChar, 40, DBNull.Value);

                if (FromDate.Length > 0)
                    oDm.Add("@FromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate + " 00:00:00"));
                else
                    oDm.Add("@FromDate", SqlDbType.DateTime, DBNull.Value);

                if (ToDate.Length > 0)
                    oDm.Add("@ToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate + " 23:59:59"));
                else
                    oDm.Add("@ToDate", SqlDbType.DateTime, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_TrnDebitNoteHeader_GetAll");
            }
        }

        public static DataTable GetAllById(int DNHeaderID)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@DNHeaderID", SqlDbType.Int, DNHeaderID);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_TrnDebitNoteHeader_GetAllById");
            }
        }

        public static int Delete(int DNHeaderID)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@DNHeaderID", SqlDbType.Int, DNHeaderID);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_TrnDebitNoteHeader_Delete");
            }
        }

        public static DataTable GetAll(int DNHeaderID)
        {
            using (DataManager oDm = new DataManager())
            {
                if (DNHeaderID == 0)
                    oDm.Add("@DNHeaderID", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@DNHeaderID", SqlDbType.Int, DNHeaderID);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_TrnDebitNote_GetAll");
            }
        }
    }
}
