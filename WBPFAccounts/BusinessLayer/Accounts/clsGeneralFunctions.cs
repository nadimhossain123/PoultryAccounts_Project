using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using DataAccess.Accounts;
using System.Collections;
using System.Web;
using System.Text;

namespace BusinessLayer.Accounts
{
	/// <summary>
	/// Summary description for clsGeneralFunctions.
	/// 
	/// </summary>
    public class clsGeneralFunctions
    {
        clsConnection objConn = new clsConnection();
        Hashtable ht = new Hashtable();
        public clsGeneralFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataSet getDataForQuery(string strQuery)
        {
            DataSet dsReturn = new DataSet();
            string[] strTables ={ " " };
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.Text, strQuery, dsReturn, strTables);
            return dsReturn;
        }

        public static DataSet getData(string strQuery, string strTable)
        {
            DataSet dsReturn = new DataSet();
            string[] strTables ={ strTable };
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.Text, strQuery, dsReturn, strTables);
            return dsReturn;
        }

        //Assign ComboBox Value from data Set
        public void AssignComboText(System.Web.UI.WebControls.DropDownList ctrlList, String value)
        {
            for (int i = 0; i < ctrlList.Items.Count; i++)
            {
                if (ctrlList.Items[i].Text.ToUpper() == value.ToUpper())
                {
                    ctrlList.Items[i].Selected = true;
                    break;
                }
            }
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

        public void BindDropDownColumnsBySP(System.Web.UI.WebControls.DropDownList ctrlList, string strSPName, string strSPParams)
        {
            DataSet dsColumnList = new DataSet();
            dsColumnList = ExecuteSelectSP(strSPName, strSPParams);

            ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
            ctrlList.DataTextField = dsColumnList.Tables[0].Columns[0].ColumnName;
            ctrlList.DataValueField = dsColumnList.Tables[0].Columns[1].ColumnName;
            ctrlList.DataBind();


        }

        public void BindAjaxDropDownColumnsBySP(AjaxControlToolkit.ComboBox ctrlList, string strSPName, string strSPParams)
        {
            DataSet dsColumnList = new DataSet();
            dsColumnList = ExecuteSelectSP(strSPName, strSPParams);

            ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
            ctrlList.DataTextField = dsColumnList.Tables[0].Columns[0].ColumnName;
            ctrlList.DataValueField = dsColumnList.Tables[0].Columns[1].ColumnName;
            ctrlList.DataBind();


        }

        public DataTable GetDropDownColumnsBySP(string strSPName, string strSPParams)
        {
            DataSet dsColumnList = new DataSet();
            dsColumnList = ExecuteSelectSP(strSPName, strSPParams);
            return dsColumnList.Tables[0];
        }

        //populating combo with select as the first coloumn-------------
        public void BindDropDownColumnsBySP_Select(System.Web.UI.WebControls.DropDownList ctrlList, string strSPName, string strSPParams)
        {
            DataSet dsColumnList = new DataSet();
            dsColumnList = ExecuteSelectSP(strSPName, strSPParams);

            ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
            ctrlList.DataTextField = dsColumnList.Tables[0].Columns[0].ColumnName;
            ctrlList.DataValueField = dsColumnList.Tables[0].Columns[1].ColumnName;
            ctrlList.DataBind();
            ctrlList.Items.Insert(0, new ListItem("Select", "0"));
            ctrlList.SelectedValue = "0";


        }


        //for using old stored procedures
        public void BindDropDownColumnsByOldSP(System.Web.UI.WebControls.DropDownList ctrlList, string strSPName, string strSPParams)
        {
            DataSet dsColumnList = new DataSet();
            dsColumnList = ExecuteSelectSP(strSPName, strSPParams);

            ctrlList.DataSource = dsColumnList.Tables[0].DefaultView;
            ctrlList.DataTextField = dsColumnList.Tables[0].Columns[1].ColumnName;
            ctrlList.DataValueField = dsColumnList.Tables[0].Columns[0].ColumnName;
            ctrlList.DataBind();
        }

        //public void PopulateCombo(DataSet prmDS, System.Windows.Forms.ComboBox prmCmb, string prmDispalyField, string prmValueField)
        //{
        //    clsGlobalVariable.gBoolErrorMsg = false;
        //    try
        //    {

        //        prmCmb.DataSource = prmDS.Tables[0];

        //        if (prmValueField == "")
        //        {
        //            prmCmb.DisplayMember = prmDispalyField;

        //        }
        //        else
        //        {
        //            prmCmb.DisplayMember = prmDispalyField;
        //            prmCmb.ValueMember = prmValueField;

        //        }


        //        //prmCmb.SelectedIndex = 0;
        //    }
        //    catch (Exception ee)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = ee.Message.ToString();
        //    }

        //}

        //public void PopulateListBox(DataSet prmDS, System.Windows.Forms.CheckedListBox prmLstBox, string prmDispalyField, string prmValueField)
        //{
        //    clsGlobalVariable.gBoolErrorMsg = false;
        //    try
        //    {
        //        prmLstBox.DataSource = prmDS.Tables[0];

        //        if (prmValueField == "")
        //        {
        //            prmLstBox.DisplayMember = prmDispalyField;
        //        }
        //        else
        //        {
        //            prmLstBox.DisplayMember = prmDispalyField;
        //            prmLstBox.ValueMember = prmValueField;
        //        }
        //        //prmLstBox.SelectedIndex = 0;
        //    }
        //    catch (Exception ee)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = ee.Message.ToString();
        //    }
        //}

        //public void PopulateListBox(DataSet prmDS, System.Windows.Forms.ListBox prmLstBox, string prmDispalyField, string prmValueField)
        //{
        //    clsGlobalVariable.gBoolErrorMsg = false;
        //    try
        //    {
        //        prmLstBox.DataSource = prmDS.Tables[0];

        //        if (prmValueField == "")
        //        {
        //            prmLstBox.DisplayMember = prmDispalyField;
        //        }
        //        else
        //        {
        //            prmLstBox.DisplayMember = prmDispalyField;
        //            prmLstBox.ValueMember = prmValueField;
        //        }
        //        //prmLstBox.SelectedIndex = -1;
        //    }
        //    catch (Exception ee)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = ee.Message.ToString();
        //    }
        //}

        public void BindGridViewSP(System.Web.UI.WebControls.GridView ctrlGView, string strSPName, string strSPParams)
        {
            DataSet dsGView = new DataSet();
            dsGView = ExecuteSelectSP(strSPName, strSPParams);
            ctrlGView.DataSource = dsGView.Tables[0].DefaultView;
            ctrlGView.DataBind();
        }

        //written by Anamitra Saha - To bind grid view using row filter
        public void BindGridViewSP(System.Web.UI.WebControls.GridView ctrlGView, string strSPName, string strSPParams, string strFilter)
        {
            DataSet dsGView = new DataSet();
            dsGView = ExecuteSelectSP(strSPName, strSPParams);
            dsGView.Tables[0].DefaultView.RowFilter = strFilter;
            ctrlGView.DataSource = dsGView.Tables[0].DefaultView;
            //ctrlGView.Sort("", System.Web.UI.WebControls.SortDirection.Ascending);
            ctrlGView.DataBind();
        }

        //written by Anamitra Saha - OVERLOADED FUNCTION To bind grid view using row filter & sorting
        public void BindGridViewSP(System.Web.UI.WebControls.GridView ctrlGView, string strSPName, string strSPParams, string strFilter, string strSortExpression, string sortDir)
        {
            DataSet dsGView = new DataSet();
            dsGView = ExecuteSelectSP(strSPName, strSPParams);
            dsGView.Tables[0].DefaultView.Sort = strSortExpression + " " + sortDir;
            dsGView.Tables[0].DefaultView.RowFilter = strFilter;
            ctrlGView.DataSource = dsGView.Tables[0].DefaultView;
            ctrlGView.DataBind();
        }


        //written by Anamitra Saha - to return SQL sort ASC or DESC based on Gridview SortDirection
        public string ConvertSortDirectionToSql(SortDirection sortDir)
        {
            string strSortDir = "";
            switch (sortDir)
            {
                case SortDirection.Ascending:
                    strSortDir = "ASC";
                    break;
                case SortDirection.Descending:
                    strSortDir = "DESC";
                    break;
                default:
                    break;
            }
            return strSortDir;
        }


        //Written By :- Kuntal -Procedure to return a byte stream from Image file
        public byte[] GetByteFromImg(string strFilePath)
        {
            byte[] byteImg;
            long ImageLength;
            clsGlobalVariable.gBoolErrorMsg = false;

            FileInfo fiImage;
            FileStream fs;
            try
            {
                fiImage = new FileInfo(strFilePath);
                ImageLength = fiImage.Length;
                fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byteImg = new byte[Convert.ToInt32(ImageLength)];
                int iBytesRead = fs.Read(byteImg, 0, Convert.ToInt32(ImageLength));
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                byteImg = new byte[10];
                clsGlobalVariable.gBoolErrorMsg = true;
                clsGlobalVariable.gStrSystemErrorMsg = ex.Message.ToString();
                clsGlobalVariable.gSstrUserErrorMsg = "Error in loading picture!!";
            }
            return byteImg;
        }

        //Written By :- Kuntal -Procedure to return  Image path from byte stream
        public string GetImgFromByte(byte[] prmByte)
        {
            string strImgPath = "";
            FileStream fs;
            try
            {
                strImgPath = Convert.ToString(DateTime.Now.ToFileTimeUtc());
                fs = new FileStream(strImgPath, FileMode.CreateNew, FileAccess.Write);
                fs.Write(prmByte, 0, prmByte.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                clsGlobalVariable.gBoolErrorMsg = true;
                clsGlobalVariable.gStrSystemErrorMsg = ex.Message.ToString();
                clsGlobalVariable.gSstrUserErrorMsg = "Error in loading picture!!";
            }
            return strImgPath;
        }

        //Written By :- ANA -Procedure to return true/false for validating field in frmCreateField
        //public bool DuplicateCheck(string strTable, string strField, string strValue, string strAndQuery)
        //{
        //    clsConnection objConn = new clsConnection();
        //    bool boolCheck = false;
        //    string strSQL = "";
        //    clsGlobalVariable.gBoolErrorMsg=false;
        //    try
        //    {
        //        if(strAndQuery == "")
        //        {
        //            strSQL = "SELECT " + strField + " FROM " + strTable + " WHERE " + strField + " = '" + strValue + "'";
        //        }
        //        else
        //        {
        //            strSQL = "SELECT " + strField + " FROM " + strTable + " WHERE " + strField + " = '" + strValue + "'";
        //            strSQL = strSQL + strAndQuery;
        //        }
        //        SqlCommand cmd = new SqlCommand();
        //        SqlDataReader sdr;

        //        objConn.OpenConnection();

        //        cmd.Connection = objConn.conn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strSQL;
        //        sdr = cmd.ExecuteReader();
        //        if (sdr.Read())
        //        {
        //            boolCheck = true;
        //        }
        //        else
        //        {
        //            boolCheck = false;
        //        }
        //        objConn.CloseConnection();
        //    }
        //    catch(Exception exp)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = exp.Message.ToString();
        //    }
        //    return boolCheck;
        //}


        //Written By :- ANA -Procedure to return true/false for checking whether field has Unit
        //public bool CheckIfFieldHasUnit(string strFTableID)
        //{
        //    clsConnection objConn = new clsConnection();
        //    bool boolCheck = false;
        //    clsGlobalVariable.gBoolErrorMsg=false;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        SqlDataReader sdr;
        //        if(objConn.conn.State==ConnectionState.Closed)
        //        {
        //            objConn.OpenConnection();
        //        }
        //        cmd.Connection = objConn.conn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "spCheckIfFieldHasUnit";

        //        SqlParameter prm;

        //        prm = new SqlParameter("@FTableID", SqlDbType.Int);
        //        prm.Value = strFTableID;
        //        cmd.Parameters.Add(prm);

        //        sdr = cmd.ExecuteReader();
        //        if (sdr.Read())
        //        {
        //            boolCheck = true;
        //        }
        //        else
        //        {
        //            boolCheck = false;
        //        }
        //        objConn.CloseConnection();
        //    }
        //    catch(Exception exp)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = exp.Message.ToString();
        //    }
        //    return boolCheck;
        //}


        //Written By :- ANA -Procedure to delete temp photo files in Application path
        public void DeleteTMPFilesForPhotos(string strAppPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(strAppPath);
                FileInfo[] fi = di.GetFiles("1*");
                for (int i = 0; i < fi.Length; i++)
                {
                    File.Delete(fi[i].FullName);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void BuildConnString(string strDataSource, string strUserID, string strPassword)
        {
            if (strDataSource != "" && strUserID != "" && strPassword == "")
            {
                clsGlobalVariable.strConnString = "Persist Security Info=True;User ID=" + strUserID + ";Initial Catalog=BhandariJewelsDB;Data Source=" + strDataSource;
                //clsGlobalVariable.strConnString = "Persist Security Info=True;User ID=" + strUserID + ";Initial Catalog=JewellerySpecSolsDB;Data Source=" + strDataSource;
            }
            else if (strDataSource != "" && strUserID != "" && strPassword != "")
            {
                clsGlobalVariable.strConnString = "Persist Security Info=True;User ID=" + strUserID + ";Password=" + strPassword + ";Initial Catalog=BhandariJewelsDB;Data Source=" + strDataSource;
                //clsGlobalVariable.strConnString = "Persist Security Info=True;User ID=" + strUserID + ";Password=" + strPassword + ";Initial Catalog=JewellerySpecSolsDB;Data Source=" + strDataSource;
            }
            else if (strDataSource != "" && strUserID == "" && strPassword == "")
            {
                clsGlobalVariable.strConnString = "Persist Security Info=True;Initial Catalog=BhandariJewelsDB;Data Source=" + strDataSource;
                //clsGlobalVariable.strConnString = "Persist Security Info=True;Initial Catalog=JewellerySpecSolsDB;Data Source=" + strDataSource;
            }
        }

        //Written By :- ANA - Procedure to build connection string
        public void BuildBackupRestoreConnString(string strDataSource, string strUserID, string strPassword)
        {
            if (strDataSource != "" && strUserID != "" && strPassword == "")
            {
                clsGlobalVariable.strBackupConnString = "Persist Security Info=True;User ID=" + strUserID + ";Initial Catalog=master;Data Source=" + strDataSource;
            }
            else if (strDataSource != "" && strUserID != "" && strPassword != "")
            {
                clsGlobalVariable.strBackupConnString = "Persist Security Info=True;User ID=" + strUserID + ";Password=" + strPassword + ";Initial Catalog=master;Data Source=" + strDataSource;
            }
            else if (strDataSource != "" && strUserID == "" && strPassword == "")
            {
                clsGlobalVariable.strBackupConnString = "Persist Security Info=True;Initial Catalog=master;Data Source=" + strDataSource;
            }
        }

        //public void DatabaseBackupRestore(string strState, string strPath, string strDBName)
        //{
        //    clsConnection objConn = new clsConnection();
        //    //objConn.conn.Dispose();
        //    if(objConn.connBackup.State==ConnectionState.Closed)
        //    {
        //        objConn.OpenBackupConnection();
        //    }
        //    clsGlobalVariable.gBoolErrorMsg = false;
        //    DataSet ds = new DataSet();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "spBackUpRestore";
        //    cmd.Connection = objConn.connBackup;

        //    try
        //    {
        //        SqlParameter prm;

        //        prm = new SqlParameter("@State", SqlDbType.VarChar, 10);
        //        prm.Value = strState;
        //        cmd.Parameters.Add(prm);

        //        prm = new SqlParameter("@Path", SqlDbType.VarChar, 1000);
        //        prm.Value = strPath;
        //        cmd.Parameters.Add(prm);

        //        prm = new SqlParameter("@DBName", SqlDbType.VarChar, 50);
        //        prm.Value = strDBName;
        //        cmd.Parameters.Add(prm);

        //        cmd.ExecuteNonQuery();
        //        objConn.CloseBackupConnection();
        //    }
        //    catch(Exception ee)
        //    {
        //        clsGlobalVariable.gBoolErrorMsg = true;
        //        clsGlobalVariable.gStrSystemErrorMsg = ee.Message;
        //        throw ee;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}


        //for allowing only decimal values in text box

        public static bool setDecimal(char argValue)
        {
            bool thisValue = false;
            switch (argValue)
            {
                case (char)48:
                case (char)49:
                case (char)50:
                case (char)51:
                case (char)52:
                case (char)53:
                case (char)54:
                case (char)55:
                case (char)56:
                case (char)57:
                case (char)8:
                case (char)'.':
                    thisValue = false;
                    break;
                default:
                    thisValue = true;
                    break;
            }
            return thisValue;
        }

        //*******************************************************************
        //**************To fetch the Auto Id From AutoIdTable****************
        //*******************************************************************
        public string getAutoNumber(string tablename, string fieldName)
        {
            string strSQL = "";
            string strID = "";
            try
            {
                //clsSpecification spec = new clsSpecification();
                DataSet dsAutoID = new DataSet();

                strSQL = "select prefix,id_number from AutoIDTable where table_name = '" + tablename + "' AND field_name = '" + fieldName + "'";
                //dsAutoID = spec.FillDataSet(strSQL);
                if (dsAutoID.Tables[0].Rows.Count > 0)
                {
                    strID = dsAutoID.Tables[0].Rows[0]["prefix"].ToString() + Convert.ToInt64(dsAutoID.Tables[0].Rows[0]["id_number"]).ToString("0000");
                }
                else
                {
                    strID = "";
                }
            }
            catch (Exception exp)
            {
                clsGlobalVariable.gBoolErrorMsg = true;
                clsGlobalVariable.gStrSystemErrorMsg = exp.Message;
                throw exp;
            }
            return strID;
        }

        //************************************************************************
        //*********************To Update the AutoIdTable**************************
        //************************************************************************
        public void updateAutoIDTable(string tablename, string fieldName, SqlTransaction sTrans)
        {
            clsGlobalVariable.gBoolErrorMsg = false;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateAutoIdTable";
            cmd.Transaction = sTrans;
            cmd.Connection = sTrans.Connection;

            try
            {
                SqlParameter prm;

                prm = new SqlParameter("@table_name", SqlDbType.VarChar, 50);
                prm.Value = tablename;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@field_name", SqlDbType.VarChar, 50);
                prm.Value = fieldName;
                cmd.Parameters.Add(prm);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                clsGlobalVariable.gBoolErrorMsg = true;
                clsGlobalVariable.gStrSystemErrorMsg = ee.Message;
                throw ee;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        //Author: Anamitra Saha			Date : 11 Apr 2008
        //General Function to execute any Insert/Update/Delete Stored Procedures
        //*******************************************************************************************************
        //**************This function is to execute any Stored procedure of INSERT, UPDATE & DELETE**************
        //************************************IT RETURNS TRUE OR FALSE*******************************************
        //*************************strSPName : CARRIES THE STORED PROCEDURE NAME*********************************
        //**********************strSPParamValues : CARRIES THE PARAMETERS' VALUES OF ****************************
        //**********************STORED PROCEDURE CONCATENATED BY Convert.ToChar(130)*****************************
        //*******************************************************************************************************
        public bool ExecuteAnySP(string strSPName, string strSPParamValues)
        {
            DataSet ds = new DataSet();
            char[] chr = { Convert.ToChar(130) };
            //values of PARAMETERS of SP
            string[] strParamValues = strSPParamValues.Split(chr);
            //data TYPE of Parameter of SP
            string strDataType = "";
            //data SIZE of Parameter of SP
            string strDataSize = "";

            string[] strTable = { "SysTab" };

            objConn.openConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConn.conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //NAME OF STORED PROCEDURE
            cmd.CommandText = strSPName;

            SqlParameter prm = new SqlParameter();

            //Parameter for the general stored procedure to fetch details of the stored procedure to be executed
            SqlParameter param = new SqlParameter("@SPName", SqlDbType.VarChar, 100);
            param.Value = strSPName;
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.StoredProcedure, "spFetchSPProperties", ds, strTable, param);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // PARAMETER NAME TO be INSERTED AFTER FETCHING FROM DB
                string strParamName = ds.Tables[0].Rows[i]["ParamName"].ToString();
                strDataSize = ds.Tables[0].Rows[i]["ParamDataTypeLength"].ToString();
                strDataType = ds.Tables[0].Rows[i]["ParamDataType"].ToString();
                string strParamDirection = ds.Tables[0].Rows[i]["ParamDirection"].ToString();

                switch (strDataType)
                {
                    case "varchar":
                        prm = new SqlParameter(strParamName, SqlDbType.VarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "char":
                        prm = new SqlParameter(strParamName, SqlDbType.Char, Convert.ToInt32(strDataSize));
                        break;
                    case "datetime":
                        prm = new SqlParameter(strParamName, SqlDbType.DateTime);
                        break;
                    case "int":
                        prm = new SqlParameter(strParamName, SqlDbType.Int);
                        break;
                    case "decimal":
                        prm = new SqlParameter(strParamName, SqlDbType.Decimal);
                        break;
                    case "float":
                        prm = new SqlParameter(strParamName, SqlDbType.Float);
                        break;
                    case "nvarchar":
                        prm = new SqlParameter(strParamName, SqlDbType.NVarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "text":
                        prm = new SqlParameter(strParamName, SqlDbType.Text);
                        break;
                    case "ntext":
                        prm = new SqlParameter(strParamName, SqlDbType.NText);
                        break;
                    case "bit":
                        prm = new SqlParameter(strParamName, SqlDbType.Bit);
                        break;
                    case "money":
                        prm = new SqlParameter(strParamName, SqlDbType.Money);
                        break;
                    case "image":
                        prm = new SqlParameter(strParamName, SqlDbType.Image);
                        break;
                }
                if (strParamValues[i].ToString() != "")
                    prm.Value = strParamValues[i].ToString();
                else
                    prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
            }

            int intAffectedRows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            objConn.closeConnection();

            if (intAffectedRows > 0)
                return true;
            else
                return false;
        }

        //**************************************************************************************************


        //******************************************************************************************************************
        //**************This function is overloaded to execute any Stored procedure of INSERT, UPDATE & DELETE**************
        //*****************************************USING TRANSACTION********************************************************
        //************************************IT RETURNS TRUE OR FALSE******************************************************
        //*************************strSPName : CARRIES THE STORED PROCEDURE NAME********************************************
        //**********************strSPParamValues : CARRIES THE PARAMETERS' VALUES OF ****************************
        //**********************STORED PROCEDURE CONCATENATED BY Convert.ToChar(130)*****************************
        //******************************************************************************************************************
        public bool ExecuteAnySP(string strSPName, string strSPParamValues, SqlTransaction trans)
        {
            DataSet ds = new DataSet();
            char[] chr = { Convert.ToChar(130) };
            //values of Parameters of SP
            string[] strParamValues = strSPParamValues.Split(chr);
            //data type of Parameter of SP
            string strDataType = "";
            //data type of Parameter of SP
            string strDataSize = "";

            string[] strTable = { "SysTab" };

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = trans.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            //NAME OF STORED PROCEDURE
            cmd.CommandText = strSPName;

            SqlParameter prm = new SqlParameter();

            //Parameter for the general stored procedure to fetch details of the stored procedure to be executed
            SqlParameter param = new SqlParameter("@SPName", SqlDbType.VarChar, 100);
            param.Value = strSPName;
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.StoredProcedure, "spFetchSPProperties", ds, strTable, param);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // PARAMETER NAME TO be INSERTED AFTER FETCHING FROM DB
                string strParamName = ds.Tables[0].Rows[i]["ParamName"].ToString();
                strDataSize = ds.Tables[0].Rows[i]["ParamDataTypeLength"].ToString();
                strDataType = ds.Tables[0].Rows[i]["ParamDataType"].ToString();
                string strParamDirection = ds.Tables[0].Rows[i]["ParamDirection"].ToString();

                switch (strDataType)
                {
                    case "varchar":
                        prm = new SqlParameter(strParamName, SqlDbType.VarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "char":
                        prm = new SqlParameter(strParamName, SqlDbType.Char, Convert.ToInt32(strDataSize));
                        break;
                    case "datetime":
                        prm = new SqlParameter(strParamName, SqlDbType.DateTime);
                        break;
                    case "bigint":
                        prm = new SqlParameter(strParamName, SqlDbType.BigInt);
                        break;
                    case "int":
                        prm = new SqlParameter(strParamName, SqlDbType.Int);
                        break;
                    case "decimal":
                        prm = new SqlParameter(strParamName, SqlDbType.Decimal);
                        break;
                    case "float":
                        prm = new SqlParameter(strParamName, SqlDbType.Float);
                        break;
                    case "nvarchar":
                        prm = new SqlParameter(strParamName, SqlDbType.NVarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "text":
                        prm = new SqlParameter(strParamName, SqlDbType.Text);
                        break;
                    case "ntext":
                        prm = new SqlParameter(strParamName, SqlDbType.NText);
                        break;
                    case "bit":
                        prm = new SqlParameter(strParamName, SqlDbType.Bit);
                        break;
                    case "money":
                        prm = new SqlParameter(strParamName, SqlDbType.Money);
                        break;
                    case "image":
                        prm = new SqlParameter(strParamName, SqlDbType.Image);
                        break;
                }
                if (strParamValues[i].ToString() != "")
                    prm.Value = strParamValues[i].ToString();
                else
                    prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
            }
            cmd.Transaction = trans;
            int intAffectedRows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (intAffectedRows > 0)
                return true;
            else
                return false;
        }

        //**************************************************************************************************


        //*******************************************************************************************************
        //*****************This function is to execute any Select Stored procedure of a database*****************
        //************************************IT RETURNS A DATASET***********************************************
        //**********************strSPName : CARRIES THE STORED PROCEDURE NAME************************************
        //**********************strSPParamValues : CARRIES THE PARAMETERS' VALUES OF ****************************
        //**********************STORED PROCEDURE CONCATENATED BY Convert.ToChar(130)*****************************
        //*******************************************************************************************************
        public DataSet ExecuteSelectSP(string strSPName, string strSPParamValues)
        {
            //DATASET TO POPULATE THE RESULT OF GENERAL SP FOR ANY SP
            DataSet ds = new DataSet();
            //DATASET TO POPULATE THE SELECT SP RESULT
            DataSet dsSelect = new DataSet();

            char[] chr = { Convert.ToChar(130) };
            //values of PARAMETERS of SP
            string[] strParamValues = strSPParamValues.Split(chr);
            //data TYPE of Parameter of SP
            string strDataType = "";
            //data SIZE of Parameter of SP
            string strDataSize = "";

            string[] strTable = { "SysTab" };

            objConn.openConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConn.conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //NAME OF STORED PROCEDURE
            cmd.CommandText = strSPName;

            SqlParameter prm = new SqlParameter();

            //Parameter for the general stored procedure to fetch details of the stored procedure to be executed
            SqlParameter param = new SqlParameter("@SPName", SqlDbType.VarChar, 100);
            param.Value = strSPName;
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.StoredProcedure, "spFetchSPProperties", ds, strTable, param);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // PARAMETER NAME TO be INSERTED AFTER FETCHING FROM DB
                string strParamName = ds.Tables[0].Rows[i]["ParamName"].ToString();
                strDataSize = ds.Tables[0].Rows[i]["ParamDataTypeLength"].ToString();
                strDataType = ds.Tables[0].Rows[i]["ParamDataType"].ToString();
                string strParamDirection = ds.Tables[0].Rows[i]["ParamDirection"].ToString();
                
                switch (strDataType)
                {
                    case "varchar":
                        prm = new SqlParameter(strParamName, SqlDbType.VarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "char":
                        prm = new SqlParameter(strParamName, SqlDbType.Char, Convert.ToInt32(strDataSize));
                        break;
                    case "datetime":
                        prm = new SqlParameter(strParamName, SqlDbType.DateTime);
                        break;
                    case "int":
                        prm = new SqlParameter(strParamName, SqlDbType.Int);
                        break;
                    case "decimal":
                        prm = new SqlParameter(strParamName, SqlDbType.Decimal);
                        break;
                    case "float":
                        prm = new SqlParameter(strParamName, SqlDbType.Float);
                        break;
                    case "nvarchar":
                        prm = new SqlParameter(strParamName, SqlDbType.NVarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "text":
                        prm = new SqlParameter(strParamName, SqlDbType.Text);
                        break;
                    case "ntext":
                        prm = new SqlParameter(strParamName, SqlDbType.NText);
                        break;
                    case "bit":
                        prm = new SqlParameter(strParamName, SqlDbType.Bit);
                        break;
                    case "money":
                        prm = new SqlParameter(strParamName, SqlDbType.Money);
                        break;
                    case "image":
                        prm = new SqlParameter(strParamName, SqlDbType.Image);
                        break;
                }
                if (strParamValues[i].ToString() != "")
                    prm.Value = strParamValues[i].ToString();
                else
                    prm.Value = DBNull.Value;
                cmd.Parameters.Add(prm);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsSelect);
            cmd.Connection.Close();
            objConn.closeConnection();

            return dsSelect;
        }


        //**************************************************************************************************
        public string ExecuteAnySPOutput(string strSPName, string strSPParamValues)
        {
            string returnMessage = "";
            int i;
            char ch = Convert.ToChar(130);
            strSPParamValues += ch.ToString() + returnMessage;
            DataSet ds = new DataSet();
            char[] chr = { Convert.ToChar(130) };
            //values of PARAMETERS of SP
            string[] strParamValues = strSPParamValues.Split(chr);
            //data TYPE of Parameter of SP
            string strDataType = "";
            //data SIZE of Parameter of SP
            string strDataSize = "";
            string[] strTable = { "SysTab" };
            objConn.openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConn.conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //NAME OF STORED PROCEDURE
            cmd.CommandText = strSPName;

            SqlParameter prm = new SqlParameter();

            //Parameter for the general stored procedure to fetch details of the stored procedure to be executed
            SqlParameter param = new SqlParameter("@SPName", SqlDbType.VarChar, 100);
            param.Value = strSPName;
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.StoredProcedure, "spFetchSPProperties", ds, strTable, param);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // PARAMETER NAME TO be INSERTED AFTER FETCHING FROM DB
                string strParamName = ds.Tables[0].Rows[i]["ParamName"].ToString();
                strDataSize = ds.Tables[0].Rows[i]["ParamDataTypeLength"].ToString();
                strDataType = ds.Tables[0].Rows[i]["ParamDataType"].ToString();
                string strParamDirection = ds.Tables[0].Rows[i]["ParamDirection"].ToString();

                switch (strDataType)
                {
                    case "varchar":
                        prm = new SqlParameter(strParamName, SqlDbType.VarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "char":
                        prm = new SqlParameter(strParamName, SqlDbType.Char, Convert.ToInt32(strDataSize));
                        break;
                    case "datetime":
                        prm = new SqlParameter(strParamName, SqlDbType.DateTime);
                        break;
                    case "int":
                        prm = new SqlParameter(strParamName, SqlDbType.Int);
                        break;
                    case "decimal":
                        prm = new SqlParameter(strParamName, SqlDbType.Decimal);
                        break;
                    case "float":
                        prm = new SqlParameter(strParamName, SqlDbType.Float);
                        break;
                    case "nvarchar":
                        prm = new SqlParameter(strParamName, SqlDbType.NVarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "text":
                        prm = new SqlParameter(strParamName, SqlDbType.Text);
                        break;
                    case "ntext":
                        prm = new SqlParameter(strParamName, SqlDbType.NText);
                        break;
                    case "bit":
                        prm = new SqlParameter(strParamName, SqlDbType.Bit);
                        break;
                    case "money":
                        prm = new SqlParameter(strParamName, SqlDbType.Money);
                        break;
                    case "image":
                        prm = new SqlParameter(strParamName, SqlDbType.Image);
                        break;
                }
                if (strParamValues[i].ToString() != "")
                    prm.Value = strParamValues[i].ToString();
                else
                    prm.Value = DBNull.Value;
                if (strParamDirection == "1")
                    prm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(prm);
            }
            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            int intAffectedRows = cmd.ExecuteNonQuery();
            returnMessage = Convert.ToString(prm.Value);
            cmd.Connection.Close();
            objConn.closeConnection();
            return returnMessage;
        }

        //**************************************************************************************************
        public string[] ExecuteAnySPOutput(string strSPName, string strSPParamValues, int intNoOfOutParams)
        {
            string[] returnMessage = new string[intNoOfOutParams];
            string[] outputParams = new string[intNoOfOutParams];
            int i;
            int intOutParams = -1;
            char ch = Convert.ToChar(130);
            //strSPParamValues += ch.ToString() + returnMessage;
            DataSet ds = new DataSet();
            char[] chr = { Convert.ToChar(130) };
            //values of PARAMETERS of SP
            string[] strParamValues = strSPParamValues.Split(chr);
            //data TYPE of Parameter of SP
            string strDataType = "";
            //data SIZE of Parameter of SP
            string strDataSize = "";
            string[] strTable = { "SysTab" };
            objConn.openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConn.conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //NAME OF STORED PROCEDURE
            cmd.CommandText = strSPName;

            SqlParameter prm = new SqlParameter();

            //Parameter for the general stored procedure to fetch details of the stored procedure to be executed
            SqlParameter param = new SqlParameter("@SPName", SqlDbType.VarChar, 100);
            param.Value = strSPName;
            SQLHelper.FillDataset(WebConfig.connectionstring, CommandType.StoredProcedure, "spFetchSPProperties", ds, strTable, param);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // PARAMETER NAME TO be INSERTED AFTER FETCHING FROM DB
                string strParamName = ds.Tables[0].Rows[i]["ParamName"].ToString();
                strDataSize = ds.Tables[0].Rows[i]["ParamDataTypeLength"].ToString();
                strDataType = ds.Tables[0].Rows[i]["ParamDataType"].ToString();
                string strParamDirection = ds.Tables[0].Rows[i]["ParamDirection"].ToString();

                switch (strDataType)
                {
                    case "varchar":
                        prm = new SqlParameter(strParamName, SqlDbType.VarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "char":
                        prm = new SqlParameter(strParamName, SqlDbType.Char, Convert.ToInt32(strDataSize));
                        break;
                    case "datetime":
                        prm = new SqlParameter(strParamName, SqlDbType.DateTime);
                        break;
                    case "bigint":
                        prm = new SqlParameter(strParamName, SqlDbType.BigInt);
                        break;
                    case "int":
                        prm = new SqlParameter(strParamName, SqlDbType.Int);
                        break;
                    case "decimal":
                        prm = new SqlParameter(strParamName, SqlDbType.Decimal);
                        break;
                    case "float":
                        prm = new SqlParameter(strParamName, SqlDbType.Float);
                        break;
                    case "nvarchar":
                        prm = new SqlParameter(strParamName, SqlDbType.NVarChar, Convert.ToInt32(strDataSize));
                        break;
                    case "text":
                        prm = new SqlParameter(strParamName, SqlDbType.Text);
                        break;
                    case "ntext":
                        prm = new SqlParameter(strParamName, SqlDbType.NText);
                        break;
                    case "bit":
                        prm = new SqlParameter(strParamName, SqlDbType.Bit);
                        break;
                    case "money":
                        prm = new SqlParameter(strParamName, SqlDbType.Money);
                        break;
                    case "image":
                        prm = new SqlParameter(strParamName, SqlDbType.Image);
                        break;
                }
                if (strParamValues[i].ToString() != "")
                    prm.Value = strParamValues[i].ToString();
                else
                    prm.Value = DBNull.Value;
                if (strParamDirection == "1")
                {
                    prm.Direction = ParameterDirection.Output;
                    intOutParams++;
                    outputParams[intOutParams] = strParamName;
                }
                cmd.Parameters.Add(prm);
            }
            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            int intAffectedRows = cmd.ExecuteNonQuery();
            if (intOutParams > -1)
            {
                for (int p = 0; p <= intOutParams; p++)
                {
                    returnMessage[p] = cmd.Parameters[outputParams[p].ToString()].Value.ToString();
                }
            }
            //returnMessage = Convert.ToString(prm.Value);
            cmd.Connection.Close();
            objConn.closeConnection();
            return returnMessage;
        }


        //Update Login Status of User while Login & Logout
        public void UpdateLoginStatus(string strUserID, string strStatus)
        {
            char chr = Convert.ToChar(130);
            try
            {
                if (ExecuteAnySP("spUpdate_MstWebUserStatus", strUserID + chr.ToString() + strStatus))
                {
                }
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }

        //written by Gopa Mahato -  round off value by increment by 1 if it has decimal part 0.50
        public string ConvertRoundOff(double Amt)
        {
            ///// round off function
            string RoundoffAmt;
            int stInd = Amt.ToString("0.00").Length - 3;
            int endInd = Amt.ToString("0.00").Length - 1;
            string strAmt = Amt.ToString("0.00").Substring(stInd, 3);
            string strAmt_onlyint = Amt.ToString("0.00").Substring(0, Amt.ToString("0.00").Length - Amt.ToString("0.00").IndexOf(".") + 1);

            if (strAmt == ".50")
            {
                RoundoffAmt = Convert.ToDouble(Convert.ToDouble(strAmt_onlyint) + 1).ToString("0.00");
            }
            else
            {
                RoundoffAmt = Math.Round(Convert.ToDouble(Amt)).ToString("0.00");
            }
            return RoundoffAmt;
        }

        public void PopulateGrid(SortDirection SortDir, string strSortExpression, DataSet ds, GridView gv, string sort_Type)
        {
            try
            {
               
                ////  gf.BindGridViewSP(gvAbbreviation, "spGetUnit", "" + chr.ToString() + "N");
                //  string str = "";
                //  str += chr.ToString() + "N";

                //  ds = gf.ExecuteSelectSP("spGetUnit", str);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = strSortExpression + sort_Type;
                    gv.DataSource = dv;
                    gv.DataBind();

                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();


                }



            }
            catch (Exception)
            {

                throw;
            }
        }
        /*
        * This function Created by Satyabrata
        * 22/06/2010
        * For Populating DropDownlist through Webservice
        */

        public SqlDataReader SysFetchData(string spName, System.Collections.Hashtable ParameterName)
        {
            SqlDataReader dr;
            SqlConnection cvCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString());
            SqlCommand lvCom;
            System.Collections.IDictionaryEnumerator lvEnum = ParameterName.GetEnumerator();

            lvCom = new SqlCommand(spName, cvCon);
            if (cvCon.State == 0)
            {
                cvCon.Open();
            }
            lvCom.CommandType = CommandType.StoredProcedure;
            lvCom.CommandTimeout = 99999999;
            while (lvEnum.MoveNext())
            {
                lvCom.Parameters.AddWithValue(lvEnum.Key.ToString(), lvEnum.Value);
            }

            dr = lvCom.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }

//---Added for setting user priviledge----Tanmoy(14/07/10)----------//

        public string CheckPrivilege(string UserID, string PageName, System.Web.UI.WebControls.Button btnAdd, System.Web.UI.WebControls.Button btnEdit, System.Web.UI.WebControls.Button btnDelete, System.Web.UI.WebControls.Button btnPrint)
       {
            ht.Clear();
            ht.Add("@UserNM", UserID);
            ht.Add("@PageNM", PageName);
            SqlDataReader dr;
            string isShow = "";
            dr = SysFetchData("[spSelect_GetUserPrevilidge]", ht);
            if (dr.Read())
            {
                if (dr.GetValue(0).ToString() != "1")
                {
                    //SHOW
                    isShow = "1";
                }
                if (dr.GetValue(1).ToString() != "1")
                {
                    //ADD
                    if (btnAdd != null)
                    {

                        btnAdd.Visible = false;
                    }
                    
                }
                if (dr.GetValue(2).ToString() != "1")
                {
                    //EDIT
                    if (btnEdit != null)
                    {
                        btnEdit.Visible = false;
                    }
                    
                }
                if (dr.GetValue(3).ToString() != "1")
                {
                    //DELETE
                    if (btnDelete != null)
                    {
                       btnDelete.Visible = false;
                    }
                   
                }
                if (dr.GetValue(4).ToString() != "1")
                {
                    //PRINT
                    if (btnPrint != null)
                    {
                        btnPrint.Visible = false;
                    }
                    
                }
            }
            else
            {
                dr.Close();
                //Response.Redirect("MainMenu.aspx");
               
            }
            dr.Close();
            return isShow;
        }



        public System.Text.StringBuilder GridViewToHtmlTable(GridView gv)
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append(@"<table  align='center' border='1' cellspacing='0' width='100%'>");
            // Building table heading ...............
            strHtml.Append(@"<tr>");
            for (int i = 0; i <= gv.Columns.Count - 1; i++)
            {
                if (gv.Columns[i].HeaderText != "" && gv.Columns[i].Visible == true)
                {
                    strHtml.Append(@"<td class='header'>");
                    strHtml.Append(gv.Columns[i].HeaderText);
                    strHtml.Append(@"</td>");
                }
            }

            strHtml.Append(@"</tr>");
            for (int r = 0; r < gv.Rows.Count; r++)
            {
                strHtml.Append(@"<tr>");
                for (int c = 0; c <= gv.Columns.Count - 1; c++)
                {
                    if (gv.Columns[c].HeaderText != "" && gv.Columns[c].Visible == true)
                    {
                        strHtml.Append(@"<td class='row'>");
                        strHtml.Append(gv.Rows[r].Cells[c].Text);
                        strHtml.Append(@"</td>");
                    }
                }
                strHtml.Append(@"</tr>");
            }
            strHtml.Append(@"</table>");
            return strHtml;
        }
    }
}
