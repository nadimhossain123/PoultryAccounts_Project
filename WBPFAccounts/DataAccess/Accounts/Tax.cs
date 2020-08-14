using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Accounts
{
    public class Tax
    {
        public Tax()
        {

        }

        public static int Save(Entity.Accounts.Tax tax)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@TaxId", SqlDbType.Int, tax.TaxId);
                oDm.Add("@TaxHead", SqlDbType.VarChar, 50, tax.TaxHead);
                oDm.Add("@TaxPercent", SqlDbType.Decimal, tax.TaxPercent);
                oDm.Add("@MessageCode", SqlDbType.Int,ParameterDirection.InputOutput, 0);
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("Tax_Save");
                return (int)oDm["@MessageCode"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("Tax_GetAll");
            }
        }

        public static Entity.Accounts.Tax GetTaxById(int taxid)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@TaxId", SqlDbType.Int, ParameterDirection.Input, taxid);

                SqlDataReader dr = oDm.ExecuteReader("Tax_GetById");

                Entity.Accounts.Tax tax = new Entity.Accounts.Tax();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        tax.TaxId = taxid;
                        tax.TaxHead = (dr["TaxHead"] == DBNull.Value) ? "" : dr["TaxHead"].ToString();
                        tax.TaxPercent = (dr["TaxPercent"] == DBNull.Value) ? 0 : decimal.Parse(dr["TaxPercent"].ToString());
                    }
                }
                return tax;
            }
        }

        public static int Delete(int taxid)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@TaxId", SqlDbType.Int, taxid);

                return oDm.ExecuteNonQuery("Tax_Delete");
            }
        }

        public static DataTable Service_Tax_ByFeesHead_Report(Entity.Accounts.Tax tax)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FeesHeadId", SqlDbType.Int, tax.FeesHeadId);
                oDm.Add("@FromDate", SqlDbType.DateTime, tax.FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, tax.ToDate);
                oDm.Add("@CompanyId", SqlDbType.Int, tax.CompanyId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Service_Tax_ByFeesHead_Report");
            }
        }

        public static DataTable Service_Tax_ByLedgerId_Report(Entity.Accounts.Tax tax)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@LedgerId", SqlDbType.Int, tax.LedgerId);
                if(tax.ParentLedgerId==0)
                    oDm.Add("@ParentLedgerID", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@ParentLedgerID", SqlDbType.Int, tax.ParentLedgerId);
                oDm.Add("@FromDate", SqlDbType.DateTime, tax.FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, tax.ToDate);
                oDm.Add("@BranchId", SqlDbType.Int, tax.BranchId);
                oDm.Add("@CompanyId", SqlDbType.Int, tax.CompanyId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Service_Tax_ByLedgerId_Report");
            }
        }

        public static DataTable GetAllDistinct()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("Tax_GetAllDistinct");
            }
        }
    }
}

