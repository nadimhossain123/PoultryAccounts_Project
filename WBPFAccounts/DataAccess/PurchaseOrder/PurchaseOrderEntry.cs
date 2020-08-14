using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.PurchaseOrder
{
    public class PurchaseOrderEntry
    {
        public PurchaseOrderEntry()
        {
        }

        public static int Save(Entity.PurchaseOrder.PurchaseOrderEntry Entry)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PurchaseBillId", SqlDbType.Int, Entry.PurchaseBillId);
                oDm.Add("@BillNo", SqlDbType.VarChar, 50, Entry.BillNo);
                oDm.Add("@BillDate", SqlDbType.DateTime, Entry.BillDate);
                oDm.Add("@BillAmount", SqlDbType.Decimal, Entry.BillAmount);
                oDm.Add("@SupplierLedgerID_FK", SqlDbType.Int, Entry.SupplierLedgerID_FK);
                oDm.Add("@PurchaseLedgerID_FK", SqlDbType.Int, Entry.PurchaseLedgerID_FK);

                oDm.Add("@CompanyID_FK", SqlDbType.Int, Entry.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, Entry.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, Entry.FinYearID_FK);
                oDm.Add("@Narration", SqlDbType.VarChar, Entry.Narration);
                oDm.Add("@DataFlow", SqlDbType.Int, Entry.DataFlow);
                oDm.Add("@CreatedBy", SqlDbType.Int, Entry.CreatedBy);
                oDm.Add("@ModifiedBy", SqlDbType.Int, Entry.ModifiedBy);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_PurchaseBillEntry_Save");
            }
        }

        public static void PurchaseBillDelete(int PurchaseBillId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@PurchaseBillId", SqlDbType.Int, PurchaseBillId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_PurchaseBill_Delete");
            }
        }

        public static DataTable GetAll(string BillNo, string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (BillNo.Trim().Length == 0)
                {
                    oDm.Add("@BillNo", SqlDbType.VarChar, 50, DBNull.Value);
                }
                else { oDm.Add("@BillNo", SqlDbType.VarChar, 50, BillNo); }

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

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PurchaseBillEntry_GetAll");
            }
        }

        

        public static Entity.PurchaseOrder.PurchaseOrderEntry GetAllById(int PurchaseBillId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@PurchaseBillId", SqlDbType.Int, PurchaseBillId);
                SqlDataReader dr = oDm.ExecuteReader("usp_PurchaseBillEntry_GetAllById");
                Entity.PurchaseOrder.PurchaseOrderEntry Entry = new Entity.PurchaseOrder.PurchaseOrderEntry();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Entry.PurchaseBillId = PurchaseBillId;
                        Entry.BillNo = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                        Entry.BillDate = (dr[2] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[2].ToString());
                        Entry.BillAmount = (dr[3] == DBNull.Value) ? 0 : decimal.Parse(dr[3].ToString());
                        Entry.SupplierLedgerID_FK = (dr[4] == DBNull.Value) ? 0 : int.Parse(dr[4].ToString());
                        Entry.PurchaseLedgerID_FK = (dr[5] == DBNull.Value) ? 0 : int.Parse(dr[5].ToString());
                        Entry.Narration = (dr[6] == DBNull.Value) ? "" : (dr[6].ToString());
                    }
                }
                return Entry;
            }
        }

        public static DataTable GetPurchaseBill()
        {
            using (DataManager oDm = new DataManager())
            {
               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteDataTable("usp_getPurchaseBill");
            }
        }
    }
}
