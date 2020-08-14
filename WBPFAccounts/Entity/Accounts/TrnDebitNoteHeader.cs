using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class TrnDebitNoteHeader
    {
        public TrnDebitNoteHeader()
        {
        }

        public int DNHeaderID { get; set; }
        public string DNVoucherNo { get; set; }
        public int CompanyID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public DateTime DNVoucherDate { get; set; }
        public int DailySrlNo { get; set; }
        public int SupplierLedgerID_FK { get; set; }
        public decimal TotalAmount { get; set; }
        public string DNNarration { get; set; }
        public string XMLDebitNoteDetails { get; set; }
        public int OperationBy { get; set; }
    }
}
