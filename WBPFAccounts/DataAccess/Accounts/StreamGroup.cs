using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class StreamGroup
    {
        public static DataTable GetParentStream(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                // oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                //oDm.Add("@int_company_id", SqlDbType.Int, stremGroup.intCompanyId);
                // oDm.Add("@stream_type", SqlDbType.Int, stremGroup.stream_type);
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }
        public static DataTable GetFeesHead(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                if (stremGroup.feesHeaderID == 0)
                    oDm.Add("@feesHeaderID", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@feesHeaderID", SqlDbType.Int, stremGroup.feesHeaderID);
                oDm.Add("@fees_name", SqlDbType.NVarChar, stremGroup.FeesName);
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }
        public static DataSet GetLoad(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, stremGroup.intCompanyId);
                oDm.Add("@CourseId", SqlDbType.Int, stremGroup.courseID);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_fees_insert", ref ds, "table");
            }
        }
        public static int SaveHeader(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@stream_type", SqlDbType.Int, stremGroup.stream_type_id);
                oDm.Add("@batchID", SqlDbType.Int, stremGroup.batch_ID);
                oDm.Add("@courseID", SqlDbType.Int, stremGroup.courseID);
                oDm.Add("@fees_name", SqlDbType.NVarChar, stremGroup.FeesName);
                oDm.Add("@feesID", SqlDbType.Int, stremGroup.feesID);
                oDm.Add("@headerId", SqlDbType.Int, ParameterDirection.InputOutput, 0);
                oDm.ExecuteNonQuery("usp_fees_insert");
                return (int)oDm["@headerId"].Value;
            }
        }
        public static int SaveDetails(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@feesID", SqlDbType.Int, stremGroup.feesID);
                oDm.Add("@feesHeaderID", SqlDbType.Int, stremGroup.feesHeaderID);
                oDm.Add("@columnName", SqlDbType.NVarChar, stremGroup.column_name);
                oDm.Add("@columnValue", SqlDbType.Money, stremGroup.column_value);

                return oDm.ExecuteNonQuery("usp_fees_insert");

                //oDm.CommandType=CommandType.Text;
                //(int) oDm.ExecuteScalar("SELECT SCOPE_IDENTITY()");
            }
        }
        public static int SaveBatch(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@batchName", SqlDbType.NVarChar, stremGroup.strBatchName);
                oDm.Add("@startDate", SqlDbType.NVarChar, stremGroup.strStartDate);
                oDm.Add("@endDate", SqlDbType.NVarChar, stremGroup.strEndDate);

                return oDm.ExecuteNonQuery("usp_fees_insert");
            }
        }

        public static DataTable GetOtherFeesHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 7);
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }


        public static int SaveFeesHead(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@feesID", SqlDbType.Int, stremGroup.feesID);
                oDm.Add("@fees", SqlDbType.NVarChar, 200, stremGroup.Fees);
                oDm.Add("@FeesHeadType", SqlDbType.VarChar, 10, stremGroup.FeesHeadType);
                oDm.Add("@AssestLedgerID_FK", SqlDbType.Int, stremGroup.AssestLedgerID_FK);
                oDm.Add("@IncomeLedgerID_FK", SqlDbType.Int, stremGroup.IncomeLedgerID_FK);
                oDm.Add("@IsRefundable", SqlDbType.Bit, stremGroup.IsRefundable);
                oDm.Add("@IsOneTimeApplicable", SqlDbType.Bit, stremGroup.IsOneTimeApplicable);
                oDm.Add("@IsYearlyFees", SqlDbType.Bit, stremGroup.IsYearlyFees);
                oDm.Add("@IsMonthlyFees", SqlDbType.Bit, stremGroup.IsMonthlyFees);
                oDm.Add("@IsTaxHead", SqlDbType.Bit, stremGroup.IsTaxHead);
                if (stremGroup.IsTaxHead == true)
                    oDm.Add("@TaxDependencyHead", SqlDbType.Int, stremGroup.TAXDependencyHead);
                else
                    oDm.Add("@TaxDependencyHead", SqlDbType.Int, DBNull.Value);
                return oDm.ExecuteNonQuery("usp_fees_insert");
            }
        }

        public static DataTable GetAllFeesHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 14);
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }

        public static DataTable GetAllFeesHeadById(int feesId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@feesID", SqlDbType.Int, feesId);
                oDm.Add("@int_mode", SqlDbType.Int, 15);
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }

        public static DataTable AllFees(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                if (stremGroup.courseID == 0)
                {
                    oDm.Add("@courseID", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@courseID", SqlDbType.Int, stremGroup.courseID); }
                if (stremGroup.intCompanyId == 0)
                {
                    oDm.Add("@int_company_id", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@int_company_id", SqlDbType.Int, stremGroup.intCompanyId); }
                return oDm.ExecuteDataTable("usp_fees_insert");
            }
        }
        public static DataTable FeesBasedOnID(Entity.Accounts.StreamGroup stremGroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, stremGroup.intMode);
                oDm.Add("@feesID", SqlDbType.Int, stremGroup.feesID);
                return oDm.ExecuteDataTable("usp_fees_insert");

            }
        }

        public static DataTable GetAllHostelFeesHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 16);
                return oDm.ExecuteDataTable("usp_fees_insert");

            }
        }

        public static DataTable GetAllSemesterFeesHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 17);
                return oDm.ExecuteDataTable("usp_fees_insert");

            }
        }


    }
}
