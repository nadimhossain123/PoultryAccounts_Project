using System;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess.Accounts
{
	/// <summary>
	/// Summary description for clsCommanSQL.
	/// </summary>
	public class clsCommonSQL
	{
		public clsCommonSQL()
		{
		}

		public DataSet GetDataset(string strQuery)
		{
			try
			{
				// Returns a dataset of all the documents
				DataSet dsDatset = new DataSet();  
				if (strQuery.Length > 0)
				{
					 
					SqlConnection con = new SqlConnection(WebConfig.connectionstring);
					string[] strTable = {"TableMappings"};
					SQLHelper.FillDataset(con,CommandType.Text,strQuery,dsDatset,strTable);
				}
				return dsDatset;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

		public void ExecuteNonQuery(string strQuery)
		{
			// Execute a non query sql statement
			SQLHelper.ExecuteNonQuery(WebConfig.connectionstring, CommandType.Text, strQuery);
		}
	}
}
