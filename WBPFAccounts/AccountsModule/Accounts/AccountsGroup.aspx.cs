using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinessLayer.Accounts;
using DataAccess.Accounts;

namespace CollegeERP.Accounts
{
    public partial class AccountsGroup : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        public string strFilter;
        public string strValues;
        clsConnection objConn = new clsConnection();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        public int GroupID
        {
            get { return Convert.ToInt32(ViewState["GroupID"]); }
            set { ViewState["GroupID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                ResetControls();
                PopulateGrid();
                Message.Show = false;
            }
        }

        protected void PopulatePage()
        {
            ddlUnderGroup.Items.Clear();

            strValues = GroupID.ToString() + chr.ToString() + "";
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstAccountsGroup", strValues);

            txtGroupName.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();
            ddlGroupType.SelectedValue = ds.Tables[0].Rows[0]["GroupType"].ToString();
            if (ddlGroupType.SelectedValue == "Sub Group")
            {
                strValues = "" + chr.ToString() + "Main Group";
                genObj.BindAjaxDropDownColumnsBySP(ddlUnderGroup, "spSelect_MstAccountsGroup", strValues);
                ddlUnderGroup.Items.Insert(0, li);

                if (ds.Tables[0].Rows[0]["GroupID_FK"].ToString() == "")
                    ddlUnderGroup.SelectedValue = "0";
                else
                {
                    ddlUnderGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID_FK"].ToString();
                }

            }
            else if (ddlGroupType.SelectedValue == "Main Group")
            {
                ddlUnderGroup.Items.Insert(0, li);
            }

            LoadFirstAccountType();
            ddlFirstAccountType.SelectedValue = ds.Tables[0].Rows[0]["ActType1"].ToString();
            LoadSecondAccountType();
            ddlSecondAccountType.SelectedValue = ds.Tables[0].Rows[0]["ActType2"].ToString();
            LoadThirdAccountType();
            ddlThirdAccountType.SelectedValue = ds.Tables[0].Rows[0]["ActType3"].ToString();
            btnSave.Text = "Update";


            Message.Show = false;
        }

        private void PopulateGrid()
        {
            strValues = "" + chr.ToString() + "";
            strFilter = "";
            if (txtSearchVal.Text.Trim().Length > 0)
                strFilter = "GroupName Like '" + txtSearchVal.Text + "%'";

            genObj.BindGridViewSP(gdAccountGr, "spSelect_MstAccountsGroup", strValues, strFilter);
        }

        private void ResetControls()
        {
            GroupID = 0;
            ddlGroupType.SelectedValue = "0";

            ddlUnderGroup.Items.Clear();
            ddlUnderGroup.Items.Insert(0, li);

            ddlFirstAccountType.Items.Clear();
            ddlFirstAccountType.Items.Insert(0, li);

            ddlSecondAccountType.Items.Clear();
            ddlSecondAccountType.Items.Insert(0, li);

            ddlThirdAccountType.Items.Clear();
            ddlThirdAccountType.Items.Insert(0, li);

            txtGroupName.Text = "";
            btnSave.Text = "Save";
            Message.Show = false;

        }


        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUnderGroup.Items.Clear();
            ddlFirstAccountType.Items.Clear();

            if (ddlGroupType.SelectedValue == "Sub Group")
            {
                strValues = "" + chr.ToString() + "Main Group";
                genObj.BindAjaxDropDownColumnsBySP(ddlUnderGroup, "spSelect_MstAccountsGroup", strValues);
                ddlFirstAccountType.Items.Insert(0, li);
            }
            else if (ddlGroupType.SelectedValue == "Main Group")
            {
                LoadFirstAccountType();
            }

            ddlUnderGroup.Items.Insert(0, li);
        }

        protected void ddlUnderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlUnderGroup.SelectedValue != "0")
            {
                strValues = ddlUnderGroup.SelectedValue.ToString();
                DataSet ds = genObj.ExecuteSelectSP("spSelect_MainGroupActType", strValues);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LoadFirstAccountType();
                    ddlFirstAccountType.SelectedValue = ds.Tables[0].Rows[0]["GroupTypeID"].ToString();

                    LoadSecondAccountType();
                    ddlSecondAccountType.SelectedValue = ds.Tables[0].Rows[1]["GroupTypeID"].ToString();

                    LoadThirdAccountType();
                    ddlThirdAccountType.SelectedValue = ds.Tables[0].Rows[2]["GroupTypeID"].ToString();

                }
            }
            else
            {
                ddlFirstAccountType.Items.Clear();
                ddlFirstAccountType.Items.Insert(0, li);
                LoadSecondAccountType();
            }

        }

        protected void ddlFirstAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSecondAccountType();
        }

        protected void LoadFirstAccountType()
        {
            strValues = "1" + chr.ToString() + "";
            DataTable dt = genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroupType", strValues);
            DataRow dr = dt.NewRow();
            dr["GroupTypeID"] = "0";
            dr["GroupType"] = "Select";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            ddlFirstAccountType.DataSource = dt;
            ddlFirstAccountType.DataBind();

            LoadSecondAccountType();
        }

        protected void LoadSecondAccountType()
        {
            strValues = "2" + chr.ToString() + ddlFirstAccountType.SelectedValue.ToString();
            DataTable dt = genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroupType", strValues);
            DataRow dr = dt.NewRow();
            dr["GroupTypeID"] = "0";
            dr["GroupType"] = "Select";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            ddlSecondAccountType.DataSource = dt;
            ddlSecondAccountType.DataBind();

            LoadThirdAccountType();
        }

        protected void LoadThirdAccountType()
        {
            strValues = "3" + chr.ToString() + ddlSecondAccountType.SelectedValue.ToString();
            DataTable dt = genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroupType", strValues);
            DataRow dr = dt.NewRow();
            dr["GroupTypeID"] = "0";
            dr["GroupType"] = "Select";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            ddlThirdAccountType.DataSource = dt;
            ddlThirdAccountType.DataBind();
        }

        protected void ddlSecondAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThirdAccountType();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtMsg = "";
            string strSPName = "";
            strValues = "";


            if (GroupID == 0)
                strSPName = "spInsert_MstAccountsGroup";
            else
            {
                strValues = GroupID.ToString();
                strSPName = "spUpdate_MstAccountsGroup";
            }
            if (strValues == "")
                strValues = Session["CompanyId"].ToString();
            else
                strValues += chr.ToString() + Session["CompanyId"].ToString();

            strValues += chr.ToString() + txtGroupName.Text.Trim();
            strValues += chr.ToString() + ddlGroupType.SelectedValue.Trim();
            if (ddlGroupType.SelectedValue == "Main Group")
                strValues += chr.ToString() + "";
            else
                strValues += chr.ToString() + ddlUnderGroup.SelectedValue.Trim();

            strValues += chr.ToString() + ddlFirstAccountType.SelectedValue.Trim();
            strValues += chr.ToString() + ddlSecondAccountType.SelectedValue.Trim();
            strValues += chr.ToString() + ddlThirdAccountType.SelectedValue.Trim();
            strValues += chr.ToString() + Session["BranchID"];
            strValues += chr.ToString() + Session["FinYrID"];
            strValues += chr.ToString() + Session["UserId"];
            rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);
            if (rtMsg == "True")
            {
                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";

                ResetControls();
                PopulateGrid();

            }
            else if (rtMsg == "Duplicate")
            {
                Message.IsSuccess = false;
                Message.Text = "This Account Group already exist!";

            }

            Message.Show = true;
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateGrid();
            gdAccountGr.PageIndex = 0;
        }


        protected void gdAccountGr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdAccountGr.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }

        protected void gdAccountGr_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GroupID = Convert.ToInt32(gdAccountGr.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }



    }
}
