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
    public partial class GeneralLedger : System.Web.UI.Page
    {
        public string strFilter;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsConnection objConn = new clsConnection();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        public int LedgerID
        {
            get { return Convert.ToInt32(ViewState["LedgerID"]); }
            set { ViewState["LedgerID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDropdown();
                ResetControls();
                PopulateGrid(null);
            }
        }

        protected void ResetControls()
        {
            LedgerID = 0;

            ddlLedgerType.SelectedValue = "0";
            ddlGroup.SelectedValue = "0";
            ddlSubGroup.Items.Clear();
            ddlSubGroup.Items.Insert(0, li);

            ddlOpeningBalanceType.SelectedValue = "DR";
            txtLedgerName.Text = "";
            txtOpeningBalance.Text = "0.00";
            txtOpBalDate.Text = DateTime.Now.ToString("dd MMM yyyy");

            chkCostCenter.Checked = false;
            chkIsActive.Checked = false;

            Message.Show = false;
            btnSave.Text = "Save";
            btnSave.Enabled = true;
        }

        protected void LoadDropdown()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";
            // A/c group population
            strValues = "" + chr.ToString() + "Main Group";
            genObj.BindAjaxDropDownColumnsBySP(ddlGroup, "spSelect_MstAccountsGroup", strValues);
            ddlGroup.Items.Insert(0, li);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void PopulatePage()
        {
            string strChkAct;

            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + LedgerID.ToString();
            strValues += chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstGeneralLedger", strValues);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["FinYrID"].ToString() != ds.Tables[0].Rows[0]["FinYearID_FK"].ToString())
                {
                    btnSave.Enabled = false;
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! This Ledger is not created within current financial year. Update is not allowed.";
                    Message.Show = true;
                }
                else
                    Message.Show = false;

                txtLedgerName.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                ddlLedgerType.SelectedValue = ds.Tables[0].Rows[0]["LedgerType"].ToString();
                ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID_FK"].ToString();
                LoadSubGroup();

                if (ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "" || ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "0")
                    ddlSubGroup.SelectedValue = "0";
                else
                    ddlSubGroup.SelectedValue = ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString();

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    txtOpeningBalance.Text = ds.Tables[1].Rows[0]["OpeningBalance"].ToString();
                    ddlOpeningBalanceType.SelectedValue = ds.Tables[1].Rows[0]["OpeningBalanceType"].ToString();
                }

                strChkAct = ds.Tables[0].Rows[0]["CostCenterApplied"].ToString();
                if (strChkAct == "True")
                    chkCostCenter.Checked = true;
                else
                    chkCostCenter.Checked = false;
                strChkAct = ds.Tables[0].Rows[0]["Active"].ToString();
                if (strChkAct == "True" ? (chkIsActive.Checked = true) : (chkCostCenter.Checked = false));
                txtOpBalDate.Text = (ds.Tables[0].Rows[0]["OpeningDate"] == DBNull.Value ? "" : ((DateTime)ds.Tables[0].Rows[0]["OpeningDate"]).ToString("dd MMM yyyy"));

                btnSave.Text = "Update";
            }


        }

        protected void PopulateGrid(string strFilter)
        {
            char chr = Convert.ToChar(130);
            if (strFilter == "null" || strFilter == null)
                strFilter = "";
            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + "" + chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            genObj.BindGridViewSP(gdGenLedger, "spSelect_MstGeneralLedger", strValues, strFilter);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                char chr = Convert.ToChar(130);
                string strSPName = "";
                string strValues = "";

                if (LedgerID == 0)
                    strSPName = "spInsert_MstGeneralLedger";
                else
                {
                    strValues = LedgerID.ToString();
                    strSPName = "spUpdate_MstGeneralLedger";
                }
                // Value for BankAccount Table
                if (strValues == "")
                    strValues = Session["CompanyId"].ToString();
                else
                    strValues += chr.ToString() + Session["CompanyId"].ToString();

                strValues += chr.ToString() + Session["FinYrID"].ToString();
                strValues += chr.ToString() + Session["BranchID"].ToString();
                strValues += chr.ToString() + txtLedgerName.Text.Trim().ToString();
                strValues += chr.ToString() + ddlGroup.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + ddlSubGroup.SelectedValue.Trim().ToString();
                if (chkCostCenter.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + ddlLedgerType.SelectedValue.Trim().ToString();
                //strValues += chr.ToString() + "True";
                if (chkIsActive.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + txtOpeningBalance.Text.Trim().ToString();
                strValues += chr.ToString() + ddlOpeningBalanceType.SelectedValue.Trim();
                strValues += chr.ToString() + Session["UserId"];
                strValues += chr.ToString() + Session["DataFlow"].ToString();
                strValues += chr.ToString() + txtOpBalDate.Text.Trim().ToString();

                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "1";
                strValues += chr.ToString() + "1";
                strValues += chr.ToString() + "1";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";
                strValues += chr.ToString() + "";

                string rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);
                if (rtMsg == "True")
                {
                    Message.IsSuccess = true;
                    Message.Text = "Your request has been processed successfully!";
                    ResetControls();
                    PopulateGrid(null);
                }
                else if (rtMsg == "Duplicate")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This General Ledger already exist!";
                }

            }

            Message.Show = true;
        }

        protected bool Validate()
        {
            bool result = true;
            string ErrorText = "";
            if (ddlGroup.SelectedValue == "0" || ddlGroup.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Group";
            }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubGroup();
        }

        protected void LoadSubGroup()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";

            strValues = "" + chr.ToString() + "Sub Group";
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
            dv.RowFilter = "GroupID_FK=" + ddlGroup.SelectedValue.ToString();

            ddlSubGroup.DataSource = dv;
            ddlSubGroup.DataBind();

            ddlSubGroup.Items.Insert(0, li);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strFilter = "";
            if (txtLedgerNameVw.Text != "")
            {
                strFilter = "LedgerName like '" + txtLedgerNameVw.Text.Trim() + "%'";
            }
            gdGenLedger.PageIndex = 0;
            PopulateGrid(strFilter);
        }

        protected void gdGenLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdGenLedger.PageIndex = e.NewPageIndex;

            strFilter = "";
            if (txtLedgerNameVw.Text != "")
            {
                strFilter = "LedgerName like '" + txtLedgerNameVw.Text.Trim() + "%'";
            }
            PopulateGrid(strFilter);
        }

        protected void gdGenLedger_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LedgerID = Convert.ToInt32(gdGenLedger.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }

        protected void gdGenLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

    }
}
