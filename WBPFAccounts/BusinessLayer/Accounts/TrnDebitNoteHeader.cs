using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public  class TrnDebitNoteHeader
    {
        public TrnDebitNoteHeader()
        {
        }

        public int Save(Entity.Accounts.TrnDebitNoteHeader DebitNote)
        {
            return DataAccess.Accounts.TrnDebitNoteHeader.Save(DebitNote);
        }

        public DataTable GetAll(int CompanyID, int FinYearID, int BranchID, string DNVoucherNo, string FromDate, string ToDate)
        {
            return DataAccess.Accounts.TrnDebitNoteHeader.GetAll(CompanyID, FinYearID, BranchID, DNVoucherNo, FromDate, ToDate);
        }

        public DataTable GetAllById(int DNHeaderID)
        {
            return DataAccess.Accounts.TrnDebitNoteHeader.GetAllById(DNHeaderID);
        }

        public int Delete(int DNHeaderID)
        {
            return DataAccess.Accounts.TrnDebitNoteHeader.Delete(DNHeaderID);
        }

        public DataTable GetAll(int DNHeaderID)
        {
            return DataAccess.Accounts.TrnDebitNoteHeader.GetAll(DNHeaderID);
        }
    }
}
