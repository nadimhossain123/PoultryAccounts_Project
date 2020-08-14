using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class MemberConsolidatedOutstandingReport : System.Web.UI.Page
    {
        ListItem li = new ListItem("---SELECT---", "0");
        decimal OpBal=0, BillAmt = 0, BillTax=0, PaidAmt = 0, PaidTax=0, DueAmt = 0, TaxDueAmt=0, TotalBill=0, TotalPaid=0, TotalDue=0;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                //if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CONSOLIDATED_OUTSTANDING_REPORT)) && (Session["SuperAdmin"] == null))
                //     Response.Redirect("../Unauthorized.aspx");

                 
                LoadFeesHead();
                PopulateDropDownLists();
                dgvBill.DataSource = null;
                dgvBill.DataBind();
                btnDownload.Visible = false;
            }
        }

        private void LoadFeesHead()
        {
            BusinessLayer.Accounts.StreamGroup ObjFees = new BusinessLayer.Accounts.StreamGroup();
            DataTable DT = ObjFees.GetAllFeesHead();

            ddlFeesHead.DataSource = DT;
            ddlFeesHead.DataBind();
            ddlFeesHead.Items.Insert(0, li);
        }


        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadYear();
            LoadStates();
            LoadDistricts();
            LoadBlock();
            LoadMembershipCategory();
        }

        protected void LoadYear()
        {
            //for (int i = 2000; i <= 2050; i++)
            //    ddlSubscriptionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            //InsertFisrtItem(ddlSubscriptionYear, "Select");
        }

        protected void LoadStates()
        {

            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            DataTable dtState = new DataTable();
            dtState = objState.GetAll();
            if (dtState != null)
            {
                ddlState.DataSource = dtState;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
            }
            InsertFisrtItem(ddlState, "Select");
        }

        protected void LoadDistricts()
        {

            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dtDistrict = new DataTable();
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            dtDistrict = objDistrict.GetAll(stateid);
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
            InsertFisrtItem(ddlDistrict, "Select");
        }

        protected void LoadBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            int districtid = (ddlDistrict.SelectedIndex == 0) ? 0 : int.Parse(ddlDistrict.SelectedValue);
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            DataTable dt = objBlock.GetAll(districtid, stateid);
            if (dt != null)
            {
                ddlBlock.DataSource = dt;
                ddlBlock.DataTextField = "BlockName";
                ddlBlock.DataValueField = "BlockId";
                ddlBlock.DataBind();
            }
            InsertFisrtItem(ddlBlock, "Select");
        }

        protected void LoadMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            DataTable dt = objMembershipCategory.GetAll();
            if (dt != null)
            {
                ddlMembershipCategory.DataSource = dt;
                ddlMembershipCategory.DataTextField = "CategoryName";
                ddlMembershipCategory.DataValueField = "MembershipCategoryId";
                ddlMembershipCategory.DataBind();
            }
            InsertFisrtItem(ddlMembershipCategory, "Select");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OpBal = 0;
            BillAmt = 0;
            BillTax = 0;
            PaidAmt = 0;
            PaidTax = 0;
            DueAmt = 0;
            TaxDueAmt = 0;
            TotalBill = 0;
            TotalPaid = 0;
            TotalDue = 0;

            BusinessLayer.Common.SemFeesGeneration ObjSemFees = new BusinessLayer.Common.SemFeesGeneration();
            Entity.Common.SemFeesGeneration SemFees = new Entity.Common.SemFeesGeneration();

            if (txtFromDate.Text == "")
                SemFees.FromDate = null;
            else
                SemFees.FromDate = Convert.ToDateTime(txtFromDate.Text);

            if (txtToDate.Text == "")
                SemFees.ToDate = null;
            else
                SemFees.ToDate = Convert.ToDateTime(txtToDate.Text);

            SemFees.MembershipCategoryId = int.Parse(ddlMembershipCategory.SelectedValue.Trim());
            SemFees.BlockId = int.Parse(ddlBlock.SelectedValue.Trim());
            SemFees.DistrictId = int.Parse(ddlDistrict.SelectedValue.Trim());
            SemFees.StateId = int.Parse(ddlState.SelectedValue.Trim());
            SemFees.Month = "";// (ddlSubscriptionMonth.SelectedIndex == 0) ? string.Empty : ddlSubscriptionMonth.SelectedValue.Trim();
            SemFees.Year = 0;// (ddlSubscriptionYear.SelectedIndex == 0) ? 0 : int.Parse(ddlSubscriptionYear.SelectedValue.Trim());
            SemFees.FeesHeadId = int.Parse(ddlFeesHead.SelectedValue.Trim());

            DataTable dt = ObjSemFees.GetConsolidated_StudentOutstandingReport(SemFees);
            if (dt != null)
            {
                dgvBill.DataSource = dt;
                dgvBill.DataBind();
            }

            if (dt.Rows.Count > 0)
            {
                btnDownload.Visible = true;
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotOpBalAmt")).Text = "<b>" + OpBal.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotBillAmt")).Text = "<b>" + BillAmt.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotTaxBillAmt")).Text = "<b>" + BillTax.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotPaidAmt")).Text = "<b>" + PaidAmt.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotTaxPaidAmt")).Text = "<b>" + PaidTax.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotDueAmt")).Text = "<b>" + DueAmt.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotTaxDueAmt")).Text = "<b>" + TaxDueAmt.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotalBill")).Text = "<b>" + TotalBill.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotalPaid")).Text = "<b>" + TotalPaid.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotalDue")).Text = "<b>" + TotalDue.ToString("#0.00") + "</b>";
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void dgvBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                OpBal += decimal.Parse(((Label)e.Row.FindControl("lblOpBalAmt")).Text.Trim());
                BillAmt += decimal.Parse(((Label)e.Row.FindControl("lblBillAmt")).Text.Trim());
                BillTax += decimal.Parse(((Label)e.Row.FindControl("lblBillTaxAmt")).Text.Trim());
                PaidAmt += decimal.Parse(((Label)e.Row.FindControl("lblPaidAmt")).Text.Trim());
                PaidTax += decimal.Parse(((Label)e.Row.FindControl("lblTaxPaidAmt")).Text.Trim());
                DueAmt += decimal.Parse(((Label)e.Row.FindControl("lblDueAmt")).Text.Trim());
                TaxDueAmt += decimal.Parse(((Label)e.Row.FindControl("lblTaxDueAmt")).Text.Trim());
                TotalBill += decimal.Parse(((Label)e.Row.FindControl("lblTotalBill")).Text.Trim());
                TotalPaid += decimal.Parse(((Label)e.Row.FindControl("lblTotalPaid")).Text.Trim());
                TotalDue += decimal.Parse(((Label)e.Row.FindControl("lblTotalDue")).Text.Trim());
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[7];
            _header[0] = "Membership Category: " + ((ddlMembershipCategory.SelectedValue == "0") ? "All" : ddlMembershipCategory.SelectedItem.Text);
            _header[1] = "Block: " + ((ddlBlock.SelectedValue == "0") ? "All" : ddlBlock.SelectedItem.Text);
            _header[2] = "District: " + ((ddlDistrict.SelectedValue == "0") ? "All" : ddlDistrict.SelectedItem.Text);
            _header[3] = "State: " + ((ddlState.SelectedValue == "0") ? "All" : ddlState.SelectedItem.Text);
            _header[4] = "Fees Head: " + ((ddlFeesHead.SelectedValue == "0") ? "All" : ddlFeesHead.SelectedItem.Text);
            _header[5] = "From Date: " + txtFromDate.Text;
            _header[6] = "To Date: " + txtToDate.Text;

            string[] _footer = new string[0];
            string file = "CONSOLIDATED_MEMBER_OUTSTANDING_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvBill, _footer, file);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
        }
    
    }
}
