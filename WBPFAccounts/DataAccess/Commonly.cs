using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public partial class Commonly : IDisposable
    {
        static string cs = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(cs);
        SqlCommand SqlCmd; public System.Data.CommandType CommandType;
        ~Commonly()
        {
            Dispose(false);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            SqlCmd.Parameters.Clear();
            if (SqlCmd.Connection != null)
            {
                if (SqlCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    //try{
                    SqlCmd.Connection.Close();
                    //}catch{}
                }
            }
            //try{
            SqlCmd.Dispose();
            //}catch{}
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public void ExecuteQuery(string ExecuteString)
        {
            SqlCmd.CommandText = ExecuteString;
            con.Open();
            SqlCmd.ExecuteNonQuery();
        }
        public SqlParameter Add(string parameterName, System.Data.SqlDbType sqlDbType, int size, System.Data.ParameterDirection direction, object value)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType, size), direction, value);
        }
        private SqlParameter Add(SqlParameter SqlPar, System.Data.ParameterDirection direction, object value)
        {
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlPar.Value = value;
            SqlPar.Direction = direction;
            return SqlCmd.Parameters.Add(SqlPar);
        }





        public void CallExecuteQuery(string FirstName, string LastName, string Email)
        {
            DataAccess.Commonly cm = new Commonly();
            //using ()
            //{
            cm.Add("@FirstName", SqlDbType.VarChar, 200, ParameterDirection.Input, FirstName);
            cm.Add("@LastName", SqlDbType.VarChar, 200, ParameterDirection.Input, LastName);
            cm.Add("@Email", SqlDbType.VarChar, 200, ParameterDirection.Input, Email);
            cm.CommandType = CommandType.StoredProcedure;
            cm.ExecuteQuery("sp_district_save");
            //}
        }
    }
}
