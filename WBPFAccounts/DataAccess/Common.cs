using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Common
    {
        /// <summary>
        /// This Method takes 2 parameters and Based on the input it return the Latest Customer Code or Stock Code from Database
        /// </summary>
        /// <param name="custOrStk">c= Customer or s= Stock</param>
        /// <param name="increment">T= Asking to increment the last Value or F = Not to increment just to fetch the new code</param>
        /// <returns></returns>
        public static string GetNewCode(char custOrStk, bool increment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pCodeType", SqlDbType.Char, 1, custOrStk);
                oDm.Add("@pIncrementNumber", SqlDbType.Bit, increment);
                oDm.Add("@pCode", SqlDbType.VarChar, 30, ParameterDirection.Output);

                oDm.ExecuteNonQuery("CodeNoMaster_GenerateNewCode");

                if (oDm["@pCode"].Value != null)
                    return oDm["@pCode"].Value.ToString();
                else
                    return "Error Getting Code";
            }
        }

        public static string GetScalarValue(string columnName, string tableName, string id, string value)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.Text;
                object scalarValue = oDm.ExecuteScalar("SELECT " + columnName + " FROM " + tableName + " WHERE " + id + " = " + value);
                object sacler1 = oDm.ExecuteScalar("select [dbo].[GetCurrentDate]()");
                if (scalarValue != null)
                    return scalarValue.ToString();
                else
                    return string.Empty;
            }
        }
        public static string GetCurrentDate()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.Text;
                //object scalarValue = oDm.ExecuteScalar("SELECT " + columnName + " FROM " + tableName + " WHERE " + id + " = " + value);
                object scaler = oDm.ExecuteScalar("select FORMAT([dbo].[GetCurrentDate](),'dd MMM yyyy')");
                if (scaler != null)
                    return scaler.ToString();
                else
                    return scaler.ToString(); ;
            }
        }

        public static DataSet GetStockCustomerMobEmail()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.Text;
                DataSet ds = new DataSet();
                object scalarValue = oDm.GetDataSet("select CustomerId,CustomerEmail,CustomerPrimaryContactNo from CustomerDetails;select StockId, OwnerEmail, OwnerPrimaryContactNo from StockMaster where StockId > 343;", ref ds, "Table");
                //object scalar1 = oDm.ExecuteReader("select CustomerId,CustomerEmail,CustomerPrimaryContactNo from CustomerDetails");
                //object scaler = oDm.ExecuteScalar("select CustomerId,CustomerEmail,CustomerPrimaryContactNo from CustomerDetails;select StockId, OwnerEmail, OwnerPrimaryContactNo from StockMaster;");
                return (scalarValue as DataSet);
            }
        }
        public static int SaveStockCustomerMobEmail(string Type, string Email, string MobileNo, int Id)
        {
            object scaler = 0;

            Email = (Email == null ? "NULL" : ("'" + Email + "'"));
            MobileNo = (MobileNo == null ? "NULL" : ("'" + MobileNo + "'"));

            using (DataManager oDm = new DataManager())
            {
                if (Type.ToUpper().Equals("CUSTOMER"))
                {
                    string sqlQuery = string.Format("UPDATE CustomerDetails SET CustomerEmail={0},CustomerPrimaryContactNo={1} where CustomerId={2}", Email, MobileNo, Id);
                    oDm.CommandType = CommandType.Text;
                    scaler = oDm.ExecuteNonQuery(sqlQuery);
                }
                else
                {
                    string sqlQuery = string.Format("UPDATE dbo.StockMaster SET OwnerEmail={0}, OwnerPrimaryContactNo={1} where  StockId={2}", Email, MobileNo, Id);
                    oDm.CommandType = CommandType.Text;
                    scaler = oDm.ExecuteNonQuery(sqlQuery);
                }
            }
            return Convert.ToInt32(scaler == null ? 0 : scaler);
        }

        public static void AuditTrailLogSave(int UserId, int ActionType, string ActionDesc)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pUserId", SqlDbType.Int, UserId);
                oDm.Add("@pActionType", SqlDbType.Int, ActionType);
                oDm.Add("@pActionDesc", SqlDbType.VarChar, 100, ActionDesc);
                oDm.ExecuteNonQuery("Common_ActivityLog_Save");
            }
        }

        public static DataTable GetAuditTrailLogs(int UserId, int ActionType)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pUserId", SqlDbType.Int, UserId);
                oDm.Add("@pActionType", SqlDbType.Int, ActionType);
                return oDm.ExecuteDataTable("Common_ActivityLog_GetAll");
            }
        }

        public static DataTable GetMailSMSContent(string Code,string Type)
        {
            using(DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                if(Code !=null && Code.Length>0)
                    oDm.Add("@pCode", SqlDbType.VarChar,100, Code);
                else oDm.Add("@pCode", SqlDbType.VarChar, 100, DBNull.Value);
                if (Type != null && Type.Length > 0)
                    oDm.Add("@pType", SqlDbType.VarChar, 10, Type);
                else oDm.Add("@pType", SqlDbType.VarChar, 10, DBNull.Value);
                return oDm.ExecuteDataTable("GetMailSMSContent_GetByCode");
            }
        }

        
        public static void DeleteMailSMSContent(int ContentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.Text;
                string sql = string.Format("DELETE FROM dbo.MailSMSContent where ContentId = {0}", ContentId);
                oDm.ExecuteNonQuery(sql);
            }
        }
    }
}
