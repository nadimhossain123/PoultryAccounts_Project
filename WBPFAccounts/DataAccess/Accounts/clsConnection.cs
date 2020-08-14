using System;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess.Accounts
{
	/// <summary>
	/// Summary description for clsConnection.
	/// </summary>
	public  class clsConnection
	{
		public SqlConnection conn = new SqlConnection();
		
		public clsConnection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void openConnection()
		{
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = WebConfig.connectionstring;
                conn.Open();
            }
		}

		public void closeConnection()
		{
			conn.Close();
		}

	}
}
