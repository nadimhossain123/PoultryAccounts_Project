using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.PurchaseOrder
{
    public  class PurchaseOrderEntry
    {
        public PurchaseOrderEntry()
        {
        }

        public int PurchaseBillId { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public decimal BillAmount { get; set; }
        public int SupplierLedgerID_FK { get; set; }
        public int PurchaseLedgerID_FK { get; set; }
        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string Narration { get; set; }
    }
}
