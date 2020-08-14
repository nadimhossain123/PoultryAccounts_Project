using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public  class PurchaseBillPayment
    {
        public PurchaseBillPayment()
        {
        }

        public DataTable GetDueBills(int SupplierLedgerID_FK, int CompanyID_FK)
        {
            return DataAccess.Accounts.PurchaseBillPayment.GetDueBills(SupplierLedgerID_FK, CompanyID_FK);
        }

        public void Save(Entity.Accounts.PurchaseBillPayment Payment)
        {
            DataAccess.Accounts.PurchaseBillPayment.Save(Payment);
        }

        public DataTable GetAll(int SupplierLedgerId_FK, DateTime? Fromdate, DateTime? Todate)
        {
            return DataAccess.Accounts.PurchaseBillPayment.GetAll(SupplierLedgerId_FK, Fromdate, Todate);
        }

        public DataTable GetAllById(int PurchaseBillId)
        {
            return DataAccess.Accounts.PurchaseBillPayment.GetAllById(PurchaseBillId);
        }
    }
}
