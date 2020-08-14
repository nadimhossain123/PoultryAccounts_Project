using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BusinessLayer.Accounts
{
	/// <summary>
	/// Summary description for clsMstFieldValue.
	/// /// Modification	:
	/// Modified By		Date		Comment/Modification details
	/// Ana				24-06-2007	New Method Added-Update 
	/// </summary>
	public class clsMstGeneral
	{
		public int GTableID;
		public string GName;
		public int GGroupID;
		public bool GIsUnit;

//		public static string strDataSource = "VRS-SERVER";
//		public static string strUserID = "sa";
//		public static string strPassword = "";

		public clsMstGeneral()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		

		public int Insert()
		{
			int intValue=0;
			clsGlobalVariable.gBoolErrorMsg = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "MstGeneral_Insert";
			//cmd.Connection = objConn.conn;
			SqlDataReader dr ;
			
			try
			{
			
                //if(objConn.conn.State==ConnectionState.Closed)
                //{
                //    objConn.OpenConnection();
                //}
				
				SqlParameter prm = new SqlParameter("@GName",SqlDbType.VarChar,50);
				prm.Value = GName;
				cmd.Parameters.Add(prm);
			
				prm = new SqlParameter("@GGroupID",SqlDbType.Int);
				prm.Value = GGroupID;
				cmd.Parameters.Add(prm);

				prm = new SqlParameter("@GIsUnit",SqlDbType.Bit);
				prm.Value = GIsUnit;
				cmd.Parameters.Add(prm);
				
				dr=cmd.ExecuteReader();

				if(dr.Read())
				{	
					intValue= Convert.ToInt32(dr[0]);
				}

				//objConn.CloseConnection();

				return intValue;
			}
			catch(SqlException se)
			{
				clsGlobalVariable.gBoolErrorMsg = true;
				clsGlobalVariable.gStrSystemErrorMsg = se.Message;
			
			}
			catch(Exception ee)
			{
				clsGlobalVariable.gBoolErrorMsg = true;
				clsGlobalVariable.gStrSystemErrorMsg = ee.Message;
			}
			return intValue;
		}
		

		public int Update()
		{
			int intValue=0;
			clsGlobalVariable.gBoolErrorMsg = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "MstGeneral_Update";
			//cmd.Connection = objConn.conn;
			SqlDataReader dr ;
			
			try
			{
                //if(objConn.conn.State==ConnectionState.Closed)
                //{
                //    objConn.OpenConnection();
                //}
				
				SqlParameter prm;

				prm = new SqlParameter("@GTableID",SqlDbType.Int);
				prm.Value = GTableID;
				cmd.Parameters.Add(prm);

				prm = new SqlParameter("@GName",SqlDbType.VarChar,50);
				prm.Value = GName;
				cmd.Parameters.Add(prm);
			
				prm = new SqlParameter("@GGroupID",SqlDbType.Int);
				prm.Value = GGroupID;
				cmd.Parameters.Add(prm);

				prm = new SqlParameter("@GIsUnit",SqlDbType.Bit);
				prm.Value = GIsUnit;
				cmd.Parameters.Add(prm);
				
				dr=cmd.ExecuteReader();

				if(dr.Read())
				{	
					intValue= Convert.ToInt32(dr[0]);
				}

				//objConn.CloseConnection();

				return intValue;
			}
			catch(SqlException se)
			{
				clsGlobalVariable.gBoolErrorMsg = true;
				clsGlobalVariable.gStrSystemErrorMsg = se.Message;
			
			}
			catch(Exception ee)
			{
				clsGlobalVariable.gBoolErrorMsg = true;
				clsGlobalVariable.gStrSystemErrorMsg = ee.Message;
			}
			return intValue;
		}


		public DataSet GetAllUnitGroups(string prmSelectOrAll)
		{
			clsGlobalVariable.gBoolErrorMsg = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "spGetAllGeneralUnit";
			//cmd.Connection = objConn.conn;
			//cmd.Parameters.Add(new SqlParameter("@SelectOrAll",SqlDbType.VarChar,10,prmSelectOrAll));
			SqlParameter prm = new SqlParameter("@SelectOrAll",SqlDbType.VarChar,10);
			prm.Value = prmSelectOrAll;
			cmd.Parameters.Add(prm);

			SqlDataAdapter da = new SqlDataAdapter(cmd);
            //if(objConn.conn.State==ConnectionState.Closed)
            //{
            //    objConn.OpenConnection();
            //}
			
			if(! clsGlobalVariable.gBoolErrorMsg)
			{
				da.Fill(ds);
			}
				
			//objConn.CloseConnection();

			return ds;
		}


		public DataSet GetAllMasterGroups(string prmSelectOrAll)
		{
			clsGlobalVariable.gBoolErrorMsg = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "spGetAllGeneralMasters";
			//cmd.Connection = objConn.conn;
			//cmd.Parameters.Add(new SqlParameter("@SelectOrAll",SqlDbType.VarChar,10,prmSelectOrAll));
			SqlParameter prm = new SqlParameter("@SelectOrAll",SqlDbType.VarChar,10);
			prm.Value = prmSelectOrAll;
			cmd.Parameters.Add(prm);

			SqlDataAdapter da = new SqlDataAdapter(cmd);
            //if(objConn.conn.State==ConnectionState.Closed)
            //{
            //    objConn.OpenConnection();
            //}
			
			if(! clsGlobalVariable.gBoolErrorMsg)
			{
				da.Fill(ds);
			}
				
			//objConn.CloseConnection();

			return ds;
		}
	
		
		public DataSet GetAllMasterGroups()
		{
			clsGlobalVariable.gBoolErrorMsg = false;
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "spGetAllGeneralMasters";
			//cmd.Connection = objConn.conn;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
            //if(objConn.conn.State==ConnectionState.Closed)
            //{
            //    objConn.OpenConnection();
            //}
			if(! clsGlobalVariable.gBoolErrorMsg)
			{
				da.Fill(ds);
			}
				
			//objConn.CloseConnection();

			return ds;
		}


		public static bool setDecimal(char argValue)
		{
			bool thisValue = false;
			switch(argValue )
			{
				case (char)48 :
				case (char)49 :
				case (char)50 :
				case (char)51 :
				case (char)52 :
				case (char)53 :
				case (char)54 :
				case (char)55 :
				case (char)56 :
				case (char)57 :
				case (char)8  :
				case (char)'.'  :
					thisValue = false;
					break;
				default :
					thisValue = true;
					break;
			}
			return thisValue;
		}


		public static bool setNumber(char argValue)
		{
			bool thisValue = false;
			switch(argValue )
			{
				case (char)48 :
				case (char)49 :
				case (char)50 :
				case (char)51 :
				case (char)52 :
				case (char)53 :
				case (char)54 :
				case (char)55 :
				case (char)56 :
				case (char)57 :
				case (char)8  :
					thisValue = false;
					break;
				default :
					thisValue = true;
					break;
			}
			return thisValue;
		}
	}
}
