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
    public partial class BankAccount : System.Web.UI.Page
    {
        public string strFilter;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        DateTime dtDate;
        public string strValues;
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        public int BankID
        {
            get { return Convert.ToInt32(ViewState["BankID"]); }
            set { ViewState["BankID"] = value; }
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
                PopulateGrid();

            }
        }

        protected void LoadDropdown()
        {
            strValues = "" + chr.ToString() + "Main Group";
            genObj.BindAjaxDropDownColumnsBySP(ddlGroup, "spSelect_MstAccountsGroup", strValues);
            ddlGroup.Items.Insert(0, li);

            //Bank Name population
            strValues = "Bank Name";
            genObj.BindDropDownColumnsBySP(ddlBankName, "spSelect_MstGeneralItem", strValues);
            ddlBankName.Items.Insert(0, li);

            //Employee population
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(ObjEmployee.GetAll("", ""));
            dv.RowFilter = "CompanyId = " + Session["CompanyID"].ToString() + " and CompanyId is not null";

            ddlOperatedBy.DataSource = dv;
            ddlOperatedBy.DataBind();
            ddlOperatedBy.Items.Insert(0, li);
        }

        protected void ResetControls()
        {
            BankID = 0;
            Message.Show = false;
            btnSave.Text = "Save";
            btnSave.Enabled = true;

            ddlBankName.SelectedValue = "0";
            ddlACType.SelectedValue = "0";
            ddlOperatedBy.SelectedValue = "0";
            ddlGroup.SelectedValue = "0";
            ddlSubGoup.Items.Clear();
            ddlSubGoup.Items.Insert(0, li);

            ddlOpBalType.SelectedValue = "DR";
            txtACNo.Text = "";
            txtBankBranch.Text = "";
            txtACOpngDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtAddress.Text = "";
            txtBankContactNo.Text = "";
            txtContactPerson.Text = "";
            txtMobileNo.Text = "";
            txtOpBal.Text = "0.00";
            txtOpBalDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtAccountBalance.Text = "0.00";
        }

        protected void PopulatePage()
        {
            string strChkAct;
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + BankID.ToString() + chr.ToString() + Session["DataFlow"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstBankAccount", strValues);

            if (Session["FinYrID"].ToString() != ds.Tables[0].Rows[0]["FinYearID_FK"].ToString())
            {
                btnSave.Enabled = false;
                Message.IsSuccess = false;
                Message.Text = "Sorry! This Ledger is not created within current financial year.Update is not allowed.";
                Message.Show = true;
            }
            else
                Message.Show = false;

            ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["BankNameID_FK"].ToString();
            txtACNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            ddlACType.Text = ds.Tables[0].Rows[0]["AccountType"].ToString();
            txtBankBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            if (ds.Tables[0].Rows[0]["ActOpeningDate"] != DBNull.Value)
            {
                dtDate = (DateTime)ds.Tables[0].Rows[0]["ActOpeningDate"];
                txtACOpngDate.Text = dtDate.ToString("dd MMM yyyy");
            }
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtBankContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
            strChkAct = ds.Tables[0].Rows[0]["Active"].ToString();
            txtAccountBalance.Text = ds.Tables[0].Rows[0]["AccountBalance"].ToString();

            if (strChkAct == "True")
                chkActive.Checked = true;
            else
                chkActive.Checked = false;

            ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID_FK"].ToString();
            LoadSubGroup();
            if (ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "" || ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "0")
                ddlSubGoup.SelectedValue = "0";
            else
                ddlSubGoup.SelectedValue = ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString();
            strChkAct = ds.Tables[0].Rows[0]["CostCenterApplied"].ToString();
            if (strChkAct == "True")
                ChkCostCentreApplble.Checked = true;
            else
                ChkCostCentreApplble.Checked = false;

            txtOpBal.Text = ds.Tables[1].Rows[0]["OpeningBalance"].ToString();
            ddlOpBalType.SelectedValue = ds.Tables[1].Rows[0]["OpeningBalanceType"].ToString();
            txtOpBalDate.Text = (ds.Tables[0].Rows[0]["OpeningDate"] == DBNull.Value ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["OpeningDate"]).ToString("dd MMM yyyy"));
            btnSave.Text = "Update";

        }

        private void PopulateGrid()
        {
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            genObj.BindGridViewSP(gdBankAccount, "spSelect_MstBankAccount", strValues);
        }

        protected void LoadSubGroup()
        {
            strValues = "" + chr.ToString() + "Sub Group";
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
            dv.RowFilter = "GroupID_FK=" + ddlGroup.SelectedValue.ToString();

            ddlSubGoup.DataSource = dv;
            ddlSubGoup.DataBind();

            ddlSubGoup.Items.Insert(0, li);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtMsg = "";
            string strSPName = "";
            strValues = "";

            if (Validate())
            {
                if (BankID == 0)
                    strSPName = "spInsert_MstBankAccount";
                else
                {
                    strValues = BankID.ToString();
                    strSPName = "spUpdate_MstBankAccount";
                }
                // Value for BankAccount Table
                if (strValues == "")
                    strValues = Session["CompanyID"].ToString();
                else
                    strValues += chr.ToString() + Session["CompanyID"].ToString();

                strValues += chr.ToString() + Session["BranchID"].ToString();
                strValues += chr.ToString() + ddlBankName.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + txtACNo.Text.Trim().ToString();
                strValues += chr.ToString() + ddlACType.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + txtBankBranch.Text.Trim().ToString();
                strValues += chr.ToString() + txtACOpngDate.Text;
                strValues += chr.ToString() + txtAddress.Text.Trim().ToString();
                strValues += chr.ToString() + txtBankContactNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtContactPerson.Text.Trim().ToString();
                strValues += chr.ToString() + txtMobileNo.Text.Trim().ToString();
                strValues += chr.ToString() + ddlOperatedBy.SelectedValue.Trim().ToString();
                if (chkActive.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + Session["FinYrID"];
                strValues += chr.ToString() + ddlBankName.SelectedItem.Text.Trim().ToString();
                strValues += chr.ToString() + ddlGroup.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + ddlSubGoup.SelectedValue.Trim().ToString();
                if (ChkCostCentreApplble.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + txtOpBal.Text.Trim().ToString();
                strValues += chr.ToString() + ddlOpBalType.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + Session["UserId"];
                //if (intID == 0)
                strValues += chr.ToString() + Session["DataFlow"];
                strValues += chr.ToString() + txtOpBalDate.Text.Trim().ToString();
                strValues += chr.ToString() + txtAccountBalance.Text.Trim();
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
                    Message.Text = "This Account No. already exist!";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void gdBankAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdBankAccount.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }

        protected void gdBankAccount_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BankID = Convert.ToInt32(gdBankAccount.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }


    }
}
