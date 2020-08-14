using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.PurchaseOrder
{
    public class PurchaseOrderEntry
    {
        public PurchaseOrderEntry()
        {
        }

        public int Save(Entity.PurchaseOrder.PurchaseOrderEntry Entry)
        {
            return DataAccess.PurchaseOrder.PurchaseOrderEntry.Save(Entry);
        }
        public void Delete(int PurchaseBillId)
        {
            //DataAccess.PurchaseOrder.PurchaseOrderEntry.
            DataAccess.PurchaseOrder.PurchaseOrderEntry.PurchaseBillDelete(PurchaseBillId);
        }

        public DataTable GetAll(string BillNo, string FromDate, string ToDate)
        {
            return DataAccess.PurchaseOrder.PurchaseOrderEntry.GetAll(BillNo, FromDate, ToDate);
        }

        public Entity.PurchaseOrder.PurchaseOrderEntry GetAllById(int PurchaseBillId)
        {
            return DataAccess.PurchaseOrder.PurchaseOrderEntry.GetAllById(PurchaseBillId);
        }
        public DataTable GetPurchaseBill()
        {
            return DataAccess.PurchaseOrder.PurchaseOrderEntry.GetPurchaseBill();
        }
    }
}
