using System;
using System.IO; 
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace DataAccess.Accounts
{
	/// <summary>
	/// Summary description for modCommon.
	/// </summary>
	/// 

	public enum ReportFormat
	{
		CrystalReport,
		Excel,
		HTML32,
		HTML40,
		PDF,
		RichText,
		Word
			
	}

	public  class  clsCommon
	{
		
		public static string getEmailTypeForLocation(string strLocationId)
		{
			string strEMailType="";
			DataSet dsReturn = new DataSet();
			string strSQL = "SELECT EMail_type FROM Lr_Location  Where Imo_Number = '" + strLocationId+"'" ;
			string[] strTables ={"+Lr_Location+"};
			
			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsReturn,strTables);
			if (dsReturn.Tables[0].Rows.Count > 0)
			{
				strEMailType = dsReturn.Tables[0].Rows[0]["EMail_type"].ToString() ;
			} 
			else
			{
				strEMailType="";
			}
			
			return strEMailType;
		}
		public static DataSet getDataForTable(string strTableName, string strSortOrder)
		{	DataSet dsReturn = new DataSet();
			string strSQL = "SELECT * FROM " + strTableName + " ORDER BY upper(" + strSortOrder + ")";
			string[] strTables ={"+strTable+"};
			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsReturn,strTables);
			return dsReturn;
 		}
		public static DataSet getDataForQuery(string strQuery)
		{
			DataSet dsReturn = new DataSet();
			string[] strTables ={" "};
			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strQuery,dsReturn,strTables);
			return dsReturn;
		}

		public static DataSet getData(string strQuery, string strTable)
		{
			DataSet dsReturn = new DataSet();
			string[] strTables ={strTable};
			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strQuery,dsReturn,strTables);
			return dsReturn;
		}

		public static string QuoateString(string strPassString)
		{
			string strReturnString;
			strReturnString = "'" + strPassString + "'";
			return strReturnString;
		}
		public static string CleanString(string strPassString)
		{
			string strReturnString;
			strReturnString = strPassString.Replace("'","''"); 
			return strReturnString;
		}
		
		public static void BindDropDownColumnsByQuery(System.Web.UI.WebControls.DropDownList ctrlList, string strQuery)
		{
			DataSet dsColumnList = new DataSet();
			dsColumnList = getDataForQuery(strQuery);

			ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
			ctrlList.DataTextField = dsColumnList.Tables[0].Columns[0].ColumnName;
			ctrlList.DataValueField = dsColumnList.Tables[0].Columns[1].ColumnName;
			ctrlList.DataBind(); 
			
		}

		public static void BindDropDownColumns(System.Web.UI.WebControls.DropDownList ctrlList, System.Data.DataSet  dsColumnList,string strTextField,string strValueField)
		{
			ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
			ctrlList.DataTextField = strTextField;
			ctrlList.DataValueField = strValueField;
			ctrlList.DataBind(); 
			
		}
		//Code ADD BY BIPLAB KUMAR SAHA
		public static void BindDropDownColumnsTextOnly(System.Web.UI.WebControls.DropDownList ctrlList, System.Data.DataSet  dsColumnList,string strTextField)
		{
			ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
			ctrlList.DataTextField = strTextField;
			ctrlList.DataBind(); 
		}
		public static string GetIdentityForServiceAgreement(string strColumnIdentity,string strColumnAlias,string strTable,string strService,string strClientCode)
		{
			
			string strSQL = "SELECT max(SUBSTR("+ strColumnIdentity+ ",14,15)) AS " + strColumnAlias + " FROM " + strTable + " Where " + strColumnIdentity + " Like '" + strService + strClientCode + "%'";
			DataSet dsResult =new DataSet();  
			int liResult;
			int liLenSAID;
			int LoopSAID;
			string strPrefix = strService + strClientCode;
			string  strResult="";
			string strZeroAppend ="";
			string strReturnVal="";
			string[] strTables ={"Lr_Service_Agreement"} ;

			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsResult,strTables);

			
			if (dsResult.Tables[0].Rows.Count >0)
			{
				foreach(DataRow row in  dsResult.Tables[0].Rows)
				{
					strResult = System.Convert.ToString( row[0]) ; 
				}
			}
			//strResult = System.Convert.ToString(objResult);  
			if (strResult != "")
			{
				liResult = System.Convert.ToInt16(strResult,10) + 1;
				strResult =System.Convert.ToString(liResult); 
				liLenSAID = strResult.Trim().Length;  
				LoopSAID = 2 - liLenSAID;
			
				for(int i=0;i<LoopSAID;i++)
				{
					strZeroAppend = strZeroAppend + "0";
				}
				strReturnVal = strPrefix+ strZeroAppend + strResult;
			}
			else
			{
				strReturnVal = strPrefix+ "01";
			}

			return strReturnVal;

		}

		public static string GetIdentity(string strColumnIdentity,string strColumnAlias,string strPrefix,string strTable)
		{						
			string strSQL = "SELECT max(SUBSTR("+ strColumnIdentity+ ",3,10)) AS " + strColumnAlias + " FROM " + strTable + " where SUBSTR("+ strColumnIdentity+ ",1,2) = '" + strPrefix.ToString() + "'" ;
			DataSet dsResult =new DataSet();  
			int liResult;
			int liLenSAID;
			int LoopSAID;
			string  strResult="";
			string strZeroAppend ="";
			string strReturnVal="";
			string[] strTables ={"+strTable+"} ;

			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsResult,strTables);

			if (dsResult.Tables[0].Rows.Count >0)
			{
				foreach(DataRow row in  dsResult.Tables[0].Rows)
				{
					strResult = System.Convert.ToString( row[0]) ; 
				}
			}

			if (strResult != "")
			{
				liResult = System.Convert.ToInt16(strResult,10) + 1;
				strResult =System.Convert.ToString(liResult); 
				liLenSAID = strResult.Trim().Length;  
				LoopSAID = 8 - liLenSAID;
			
				for(int i=0;i<LoopSAID;i++)
				{
					strZeroAppend = strZeroAppend + "0";
				}
				strReturnVal = strPrefix+ strZeroAppend + strResult;
			}
			else
			{
				strReturnVal = strPrefix+ "00000001";
			}

			return strReturnVal;
		}

		public static string GetIdentityForOrderRefNo(string strProject,string strTable,string strOrderDate)
		{			
//			string strSQL = "SELECT max(SUBSTR(DI_OrderHeader.OrdRefNo,instr(DI_OrderHeader.OrdRefNo,'-')+1,length(DI_OrderHeader.OrdRefNo))) as OrderRefNO  FROM " + strTable+ "" + 			
//				" where  SUBSTR(DI_OrderHeader.OrdRefNo,0,instr(DI_OrderHeader.OrdRefNo,'-')-4)= '"+ strProject +"'" ;		    

		//	string strSQL = "SELECT max(SUBSTR(DI_OrderHeader.OrdRefNo,instr(DI_OrderHeader.OrdRefNo,' ')+4,length(DI_OrderHeader.OrdRefNo)))  as OrderRefNO  FROM " + strTable+ "" + 			
		//	" where  SUBSTR(DI_OrderHeader.OrdRefNo,0,length(DI_OrderHeader.OrdRefNo)-12)= '"+ strProject +"'" ;		

			//Kuntal - 20 02 2007
			string strSQL = "SELECT max(SUBSTR(DI_OrderHeader.OrdRefNo,length(ORDREFNO)-7,8))  as OrderRefNO  FROM " + strTable+ "" + 			
				" where  SUBSTR(DI_OrderHeader.OrdRefNo,0,length(DI_OrderHeader.OrdRefNo)-12)= '"+ strProject +"'" ;		
			//
			DataSet dsResult =new DataSet();		
			int liResult;
			int liLenSAID;
			int LoopSAID;
			string  strResult="";
			string strZeroAppend ="";
			string strReturnVal="";
			string strYear=System.Convert.ToDateTime(strOrderDate).Year.ToString().Substring(2);
			string[] strTables ={"+strTable+"} ;

			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsResult,strTables);

			if (dsResult.Tables[0].Rows.Count >0)
			{
				foreach(DataRow row in  dsResult.Tables[0].Rows)
				{
					strResult = System.Convert.ToString(row[0].ToString());
					
				}
			}

			if (strResult != "")
			{
				strResult = strResult.Substring(0,4);
				liResult = System.Convert.ToInt16(strResult) + 1;
				strResult =System.Convert.ToString(liResult); 
				liLenSAID = strResult.Trim().Length;  
				LoopSAID = 4 - liLenSAID;
			
				for(int i=0;i<LoopSAID;i++)
				{
					strZeroAppend = strZeroAppend + "0";
				}
				strReturnVal = strProject+" "+strYear+"-"+ strZeroAppend + strResult +"-"+ "001";
			}
			else
			{
				strReturnVal = strProject+" "+strYear+"-"+ "0001"+"-"+ "001";
			}

			return strReturnVal;
		}

		public static string GetRepIdentityForOrderRefNo(string strProject,string strTable, string strOrderDate)
		{			
			string strSQL = "SELECT max(SUBSTR(DI_OrderHeader.OrdRefNo,instr(DI_OrderHeader.OrdRefNo,'-')+1,length(DI_OrderHeader.OrdRefNo))) as OrderRefNO  FROM " + strTable+ "" + 			
				" where  SUBSTR(DI_OrderHeader.OrdRefNo,0,instr(DI_OrderHeader.OrdRefNo,'-')-4)= '"+ strProject +"'" ;		    
			DataSet dsResult =new DataSet();		
			int liResult;
			int liLenSAID;
			int LoopSAID;
			string  strResult="";
			string strZeroAppend ="";
			string strReturnVal="";
			string strYear=System.Convert.ToDateTime(strOrderDate).Year.ToString().Substring(2);
			string[] strTables ={"+strTable+"} ;

			SQLHelper.FillDataset(WebConfig.connectionstring,CommandType.Text,strSQL,dsResult,strTables);

			if (dsResult.Tables[0].Rows.Count >0)
			{
				foreach(DataRow row in  dsResult.Tables[0].Rows)
				{
					strResult = System.Convert.ToString(row[0].ToString()) ;
				}
			}

			if (strResult != "")
			{
				strResult = strResult.Substring(0,4);
				liResult = System.Convert.ToInt16(strResult) + 1;
				strResult =System.Convert.ToString(liResult); 
				liLenSAID = strResult.Trim().Length;  
				LoopSAID = 4 - liLenSAID;
			
				for(int i=0;i<LoopSAID;i++)
				{
					strZeroAppend = strZeroAppend + "0";
				}
				strReturnVal = strProject+" "+strYear+"-"+ strZeroAppend + strResult+"-"+"001";
			}
			else
			{
				strReturnVal = strProject+" "+strYear+"-"+ "0001"+"-"+ "001";
			}

			return strReturnVal;
		}
		
		 // Check validity of the user		 
		public static void checkValidUser(System.Web.HttpContext  objHttpContext)
		{
			System.Web.SessionState.HttpSessionState objSession = objHttpContext.Session; 
			System.Web.HttpServerUtility objServerUtil = objHttpContext.Server ;
			string strAppUser = (String)objSession["AppUser"];
			if (strAppUser == null)
			{
				objServerUtil.Transfer("Unauthorized.aspx",false);
			}
		}

		//Export the Crystal Report
//		public static void ExportToDisk (string fileName,ReportFormat lrepFormat,ReportDocument oRpt )
//		{
//			// Declare variables and get the export options.
//			ExportOptions exportOpts = new ExportOptions();
//			DiskFileDestinationOptions diskOpts =  new DiskFileDestinationOptions();
//
//			exportOpts =oRpt.ExportOptions;
//			
//			// Set the export format.
//			switch(lrepFormat)
//			{
//				case ReportFormat.Excel:
//					exportOpts.ExportFormatType = ExportFormatType.Excel;
//					break;
//				case ReportFormat.PDF:
//					exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
//					break;
//				case ReportFormat.HTML32 :
//					exportOpts.ExportFormatType = ExportFormatType.HTML32 ;
//					break;
//				case ReportFormat.HTML40:
//					exportOpts.ExportFormatType = ExportFormatType.HTML40;
//                    break;
//				case ReportFormat.RichText:
//					exportOpts.ExportFormatType = ExportFormatType.RichText;
//                    break;
//				case ReportFormat.Word: 
//					exportOpts.ExportFormatType = ExportFormatType.WordForWindows;
//                    break;
//				default:
//					exportOpts.ExportFormatType = ExportFormatType.CrystalReport;
//				break;
//			}
//			
//			exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
//
//			// Set the disk file options.
//			diskOpts.DiskFileName = fileName;
//			exportOpts.DestinationOptions = diskOpts;
//
//			// Export the report.
//			try
//			{
//				oRpt.Export ();
//			}
//			catch(Exception e)
//			{
//				throw new Exception( e.Message);  
//			}
//		}

		// Create Folder
		public static void CreateFolder(string strFilePath)
		{
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strFilePath);
			if (!dir.Exists) 
			{	dir.Create();	}
		}	

		// Delete File
		public static void DeleteFile(string strFile)
		{
			System.IO.FileInfo file = new System.IO.FileInfo(strFile);
			if (file.Exists) 
			{  file.Delete();	}
		}	


		// Delete Folder
		public static void DeleteFolder(string strFilePath)
		{
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strFilePath);
			if (dir.Exists) 
			{	dir.Delete(true);	}
		}	

		//Copy file
//		public static void CopyFile(string strFilePath, string strFileName, string strID)
//		{
//			try
//			{
//				strID = strID.Replace("/","_");
//				string strFile = strID + "_" + strFileName;
//				string strOldFile = WebConfig.SessionRootPath + "/0_" + strFileName;
//
//				System.IO.FileInfo file = new FileInfo(strOldFile); 
//				if(file.Exists)
//				{
//					file.CopyTo(strFilePath + "/" + strFile,true);
//				}
//			}
//			catch(Exception e)
//			{
//			}
//		}

		//File Upload
		
		


		public static bool checkSpecialChar(string strName)
		{
			string strSpecialChar = "¬|@'`~;:?><,+=£)!$^&*(";
			char[] ChrSpecialChar = strSpecialChar.ToCharArray();
			char[] ChrName = strName.ToCharArray();
			for (int i=0;i<strName.Length;i++)
			{  
				for (int j=0;j<strSpecialChar.Length;j++)
				{
					if (ChrName.GetValue(i).ToString() == ChrSpecialChar.GetValue(j).ToString())
					{
						return false;
					}  
				} 
			}
			return true;
		}
		public static string MonthName(int mm)
		{
			switch(mm)
			{
				case 1:
					return "January";
				case 2:
					return "February";
				case 3:
					return "March";
				case 4:
					return "April";
				case 5:
					return "May";
				case 6:
					return "June";
				case 7:
					return "July";
				case 8:
					return "August";
				case 9:
					return	"September";
				case 10:
					return "October";
				case 11:
					return "November";
				case 12:
					return "December";
				default :
					return "";

			}
		}
       // code By Gopa 
        public static void gridview(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='mousehover'");
                if (e.Row.RowIndex % 2 == 0)
                    e.Row.Attributes.Add("onmouseout", "this.className='normal'");
                else
                    e.Row.Attributes.Add("onmouseout", "this.className='alternate'");
            }

        }
        
	}
}
