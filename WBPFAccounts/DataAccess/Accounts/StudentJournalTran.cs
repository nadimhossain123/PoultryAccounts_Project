using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class StudentJournalTran
    {
        public static int Save(Entity.Accounts.StudentJournalTrn objStuJournal)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CVoucherNo", SqlDbType.VarChar, objStuJournal.CVoucherNo);
                //oDm.Add("@CompanyID_FK", SqlDbType.Int, objStuJournal.CompanyID_FK);
                //oDm.Add("@BranchID_FK", SqlDbType.Int, objStuJournal.BranchID_FK);
                //oDm.Add("@FinYearID_FK", SqlDbType.Int, objStuJournal.FinYearID_FK);
                oDm.Add("@CompanyID", SqlDbType.Int, objStuJournal.CompanyID_FK);
                oDm.Add("@BranchID", SqlDbType.Int, objStuJournal.BranchID_FK);
                oDm.Add("@FinYearID", SqlDbType.Int, objStuJournal.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, objStuJournal.DataFlow);
                oDm.Add("@CVoucherDate", SqlDbType.DateTime, objStuJournal.CVoucherDate);
                oDm.Add("@DailySrlNo", SqlDbType.Int, objStuJournal.DailySrlNo);
                oDm.Add("@LedgerId_FK", SqlDbType.Int, objStuJournal.LedgerId_FK);
                oDm.Add("@TransactionType", SqlDbType.VarChar, objStuJournal.TransactionType);
                oDm.Add("@ModeOfTransaction", SqlDbType.VarChar, objStuJournal.ModeOfTransaction);
                oDm.Add("@ChequeNo", SqlDbType.VarChar, objStuJournal.ChequeNo);
                oDm.Add("@ChequeDate", SqlDbType.DateTime, objStuJournal.ChequeDate);
                oDm.Add("@DrawnOn", SqlDbType.VarChar, objStuJournal.DrawnOn);
                oDm.Add("@ParentLedgerId_FK", SqlDbType.Int, objStuJournal.ParentLedgerId_FK);
                oDm.Add("@TotalAmount", SqlDbType.Decimal, objStuJournal.TotalAmount);
                oDm.Add("@CVNarration", SqlDbType.VarChar, objStuJournal.CVNarration);
                oDm.Add("@OperationBy", SqlDbType.Int, objStuJournal.OperationBy);
                oDm.Add("@PaymentDetails", SqlDbType.Xml, objStuJournal.PaymentDetailsXML);
                oDm.Add("@SemNo", SqlDbType.Int, objStuJournal.Semester);
                oDm.Add("@XMLCashBankVoucherDetails", SqlDbType.Xml, objStuJournal.XMLCashBankVoucherDetails);
                oDm.Add("@StudentId", SqlDbType.Int, objStuJournal.ParentLedgerId_FK);
                oDm.CommandType = CommandType.StoredProcedure;
                int recCnt = oDm.ExecuteNonQuery("usp_AccTransferJournal");
                 //int recCnt=   oDm.ExecuteNonQuery("usp_AccountTransfer");
                // string rtMsg  = (string)oDm.["@Message"].Value;
                //int recCnt = oDm.ExecuteNonQuery("usp_AccountTransfer");

                 return recCnt;               	
	
	
	
            }
        }
    }
}
