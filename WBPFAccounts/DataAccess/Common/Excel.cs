using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Excel
    {
        public Excel()
        {

        }

        public static void BulkUpload(DataTable dtUpload, string TableName)
        {
            string constr = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].Trim();
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = TableName;

            foreach (DataColumn col in dtUpload.Columns)
            {
                objbulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            objbulk.WriteToServer(dtUpload);
            con.Close();
            con = null;

            dtUpload.Dispose();
            dtUpload = null;
        }

        public static string ValidatePaymentExcelUpload(int UserId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, UserId);
                oDm.Add("@pXmlOut", SqlDbType.Xml, ParameterDirection.InputOutput, "");

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_tempPaymentExcelUpload_Validate");
                return (string)oDm["@pXmlOut"].Value;
            }
        }

        public static string ValidateSMSPaymentExcelUpload(int UserId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pUserId", SqlDbType.Int, UserId);
                oDm.Add("@pXmlOut", SqlDbType.Xml, ParameterDirection.InputOutput, "");

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_tempSMSPaymentExcelUpload_Validate");
                return (string)oDm["@pXmlOut"].Value;
            }
        }
    }
}
