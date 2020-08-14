using BusinessLayer.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Accounts
{
    public partial class OpeningBalanceEntry : System.Web.UI.Page
    {
        public int OpBalId
        {
            get { return Convert.ToInt32(ViewState["OpBalId"]); }
            set { ViewState["OpBalId"] = value; }
        }
        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strParams;
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ResetControl();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    //populateFeesDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    OpBalId = Convert.ToInt32(Request.QueryString["id"].ToString());
                    PopulateOpeningBalance();
                }
                Message.Show = false;
            }
        }

        private void ResetControl()
        {
            LoadLedger();
            ddlLedger.SelectedValue = "0";
            ddlLedger.Enabled = true;
            txtOpeningBalance.Text = "";
            ddlOpeningBalanceType.SelectedValue = "0";
            LoadOpeningBalance();
            OpBalId = 0;
            btnSave.Text = "Save";
            lblActualOpeningBalance.Visible = false;
        }

        private void LoadLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType NOT IN ('BNK', 'CASH')";
            if (dv != null)
            {
                ddlLedger.DataSource = dv;
                ddlLedger.DataBind();
            }
            ddlLedger.Items.Insert(0, li);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Entity.Accounts.OpeningBalanceMaster OpBal = new Entity.Accounts.OpeningBalanceMaster();
            OpBal.OpBalId = OpBalId;
            OpBal.CompanyId = int.Parse(Session["CompanyId"].ToString());
            OpBal.LedgerId = Convert.ToInt32(ddlLedger.SelectedValue);
            OpBal.FinancialYrId = Convert.ToInt32(Session["FinYrID"].ToString());
            OpBal.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
            OpBal.OpeningBalanceType = ddlOpeningBalanceType.SelectedValue;
            OpBal.CreatedBy = int.Parse(Session["UserId"].ToString());
            BusinessLayer.Accounts.OpeningBalanceMaster objOpBal = new OpeningBalanceMaster();
            int MessageCode = objOpBal.OpeningBalanceSave(OpBal);
            if (MessageCode >= 0)
            {
                Message.IsSuccess = true;
                if (MessageCode == 0)
                { Message.Text = "Updated Successfully!"; }
                else { Message.Text = "Opening Balance Saved Successfully!"; }
            }
            else
            {
                Message.IsSuccess = false;
                if (MessageCode == -1)
                    Message.Text = "Sorry!! Already Exists OR Duplicate Entry is not allowed";
            }
            Message.Show = true;
            ResetControl();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
            Message.Show = false;
        }
        
        protected void btnCopyPrevClosing_Click(object sender, EventArgs e)
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["UserId"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("UpdateOpeningBalanceforAllLedgers", strParams));
            Message.Text = "All the previous closing are successfully copied as current opening ";
            LoadOpeningBalance();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadOpeningBalance();
            Message.Show = false;
        }

        protected void LoadOpeningBalance()
        {
            string LedgerName = txtLedgerName.Text.Trim();
            int FinYrId = int.Parse(Session["FinYrID"].ToString());
            BusinessLayer.Accounts.OpeningBalanceMaster objOpBal = new OpeningBalanceMaster();
            DataView DV = new DataView(objOpBal.GetAll());
            
            if (LedgerName != "")
            {
                DV.RowFilter = "LedgerName like '" + LedgerName + "%' AND FinancialYrId='" + FinYrId + "'";
            }
            else DV.RowFilter = "FinancialYrId='" + FinYrId + "'";
            if (DV.ToTable().Rows.Count > 0)
            {
                dgvOpeningBalance.DataSource = DV;
                dgvOpeningBalance.DataBind();
            }
        }

        protected void dgvOpeningBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOpeningBalance.PageIndex = e.NewPageIndex;
            LoadOpeningBalance();
        }

        protected void dgvOpeningBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgvOpeningBalance_RowEditing(object sender, GridViewEditEventArgs e)
        {
            OpBalId = Convert.ToInt32(dgvOpeningBalance.DataKeys[e.NewEditIndex].Value);
            PopulateOpeningBalance();
        }

        protected void PopulateOpeningBalance()
        {
            BusinessLayer.Accounts.OpeningBalanceMaster OpBal = new OpeningBalanceMaster();
            Entity.Accounts.OpeningBalanceMaster EntOpBal = OpBal.GetById(OpBalId);
            LoadLedger();
            ddlLedger.SelectedValue = EntOpBal.LedgerId.ToString();
            ddlLedger.Enabled = false;
            txtOpeningBalance.Text = EntOpBal.OpeningBalance.ToString();
            ddlOpeningBalanceType.SelectedValue = EntOpBal.OpeningBalanceType;
            btnSave.Text = "Update";
        }

        protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LedgerId = Convert.ToInt32(ddlLedger.SelectedValue);
            BusinessLayer.Accounts.OpeningBalanceMaster objOpBal = new OpeningBalanceMaster();
            DataView DV = new DataView(objOpBal.GetAll());
            DV.RowFilter = "LedgerId='"+ LedgerId+"' AND FinancialYrId ='" + int.Parse(Session["FinYrID"].ToString()) + "'";
            if (DV.ToTable().Rows.Count > 0)
            {
                lblActualOpeningBalance.Text = "Actual OPBAL: "+DV.ToTable().Rows[0]["ActualOpeningBalance"].ToString();
                lblActualOpeningBalance.Visible = true;
                ddlOpeningBalanceType.SelectedValue = DV.ToTable().Rows[0]["OpeningBalanceType"].ToString();
            }
        }
    }
}