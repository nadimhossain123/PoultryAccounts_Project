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

namespace CollegeERP.Accounts
{
    public partial class AccountGroupType : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        public string strFilter;
        public string strValues;
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                
                ViewState["ID"] = 0;
                PopulateCombo();
                strValues = "3" + chr.ToString() + "" + chr.ToString() + "";
                genObj.BindGridViewSP(dgvGroupType, "spSelect_MstAccountsGroupType_ForAccountsGroupType", strValues);
                Message.Show = false;
            }
        }

        private void PopulateCombo()
        {
            strValues = "1" + chr.ToString() + "";
            genObj.BindDropDownColumnsBySP(ddlFirstAccountType, "spSelect_MstAccountsGroupType", strValues);
            ddlFirstAccountType.SelectedValue = "0";
        }

        private void ResetControls()
        {
            ddlFirstAccountType.SelectedValue = "0";
            ddlSecondAccountType.Items.Clear();
            ddlSecondAccountType.Items.Insert(0, new ListItem("Select", "0"));
            ddlSecondAccountType.SelectedValue = "0";
            txtGroupType.Text = "";
            hdnGroupTypeID.Value = "";
            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void PopulatePage(object sender, EventArgs e, int intID)
        {

            string strValues = "";
            ViewState["SortOrder"] = " ASC";
            // ResetControls();
            //Grid View population
            strValues = "3";
            strValues += chr.ToString() + intID.ToString();
            strValues += chr.ToString() + "";
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstAccountsGroupType_ForAccountsGroupType", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGroupType.Text = ds.Tables[0].Rows[0]["GroupType"].ToString();
                DataSet ds1 = genObj.ExecuteSelectSP("spSelect_MstAccountsGroupType_ForAccountsGroupType", "2" + chr.ToString() + ds.Tables[0].Rows[0]["UnderGroupTypeID"].ToString() + chr.ToString() + "");
                ddlFirstAccountType.SelectedValue = ds1.Tables[0].Rows[0]["UnderGroupTypeID"].ToString();
                ddlSecondAccountType.Items.Clear();
                strValues = "2" + chr.ToString() + ddlFirstAccountType.SelectedValue.ToString();
                genObj.BindDropDownColumnsBySP(ddlSecondAccountType, "spSelect_MstAccountsGroupType", strValues);
                ddlSecondAccountType.Items.Insert(0, new ListItem("Select", "0"));
                ddlSecondAccountType.SelectedValue = ds.Tables[0].Rows[0]["UnderGroupTypeID"].ToString();
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }
            Message.Show = false;
        }

        protected void ddlFirstAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strValues = "";
            string strMessage = "";
            char chr = Convert.ToChar(130);

            if (ddlFirstAccountType.SelectedValue != "0")
            {
                ddlSecondAccountType.Items.Clear();
                strValues = "2" + chr.ToString() + ddlFirstAccountType.SelectedValue.ToString();
                genObj.BindDropDownColumnsBySP(ddlSecondAccountType, "spSelect_MstAccountsGroupType", strValues);
                ddlSecondAccountType.Items.Insert(0, new ListItem("Select", "0"));
                ddlSecondAccountType.SelectedValue = "0";
            }
            else
            {
                ddlSecondAccountType.Items.Clear();
                ddlSecondAccountType.Items.Insert(0, new ListItem("Select", "0"));
                ddlSecondAccountType.SelectedValue = "0";
            }
            //ViewState["Control"] = "ddlSecondAccountType";


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strMessage = "";
            string strMessageType = "";
            string rtMsg = "";
            //special character for string separation
            char chr = Convert.ToChar(130);
            string strSPName = "";
            string strValues = "";
            int intID = 0;

            if (hdnGroupTypeID.Value != "")
                intID = Convert.ToInt32(hdnGroupTypeID.Value);
            if (intID == 0)
            {
                strSPName = "spInsert_MstAccountsGroupType";
                strValues = txtGroupType.Text.Trim();
            }
            else
            {
                strValues = intID.ToString();
                strValues += chr.ToString() + txtGroupType.Text.Trim();
                strSPName = "spUpdate_MstAccountsGroupType";
            }
            // Value for Accounts Group Type Table
            //if (strValues == "")
            //    strValues = Session["SesCompanyID"].ToString();
            //else
            //    strValues += chr.ToString() + Session["SesCompanyID"].ToString();
            // strValues += chr.ToString() + txtGroupType.Text.Trim();
            strValues += chr.ToString() + ddlSecondAccountType.SelectedValue.Trim().ToString();
            strValues += chr.ToString() + Session["UserId"];
            strValues += chr.ToString() + Session["BranchID"];
            strValues += chr.ToString() + Session["FinYrID"];
            rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);
            if (rtMsg == "True")
            {
                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";

            }
            else if (rtMsg == "Duplicate")
            {
                Message.IsSuccess = false;
                Message.Text = "This Account Grp. already exist!";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Database Error: " + rtMsg + "";
            }

            //Grid View population
            strValues = "3" + chr.ToString() + "" + chr.ToString() + "";
            genObj.BindGridViewSP(dgvGroupType, "spSelect_MstAccountsGroupType_ForAccountsGroupType", strValues);
            Message.Show = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void dgvGroupType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvGroupType.PageIndex = e.NewPageIndex;
            strValues = "3";
            strValues += chr.ToString() + "";
            strValues += chr.ToString() + "";
            genObj.BindGridViewSP(dgvGroupType, "spSelect_MstAccountsGroupType_ForAccountsGroupType", strValues);

        }

        protected void dgvGroupType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("btnEdit"))
            {
                int rID = Convert.ToInt32(e.CommandArgument);
                HiddenField ib = (HiddenField)dgvGroupType.Rows[rID].FindControl("hdID");
                int intID = Convert.ToInt32(ib.Value);
                ViewState["ID"] = intID;
                hdnGroupTypeID.Value = intID.ToString();
                PopulatePage(sender, e, intID);
            }
        }

        protected void dgvGroupType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("btnEdit");
                ib.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

       
    }
}
