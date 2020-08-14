using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class DevelopmentFeeAdjustment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    Message.Show = false;
                    btnSave.Visible = false;
                    LoadMonth();
                    LoadStates();
                    LoadDistricts();
                    LoadBlock();
                    LoadMembershipCategory();
                }
            }
            else
                Response.Redirect("../Login.aspx");
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        private void LoadMonth()
        {
            BusinessLayer.Common.MonthMaster objMonth = new BusinessLayer.Common.MonthMaster();
            DataTable dt = objMonth.GetAll();

            ddlMonth.DataSource = dt;
            ddlMonth.DataBind();
            InsertFisrtItem(ddlMonth, "--MONTH--");
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
            InsertFisrtItem(ddlState, "Any State");
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
            InsertFisrtItem(ddlDistrict, "Any District");
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
            InsertFisrtItem(ddlBlock, "Any Block");
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
            InsertFisrtItem(ddlMembershipCategory, "Any Category");
        }

        private void LoadMonthlyDevelopmentBill()
        {
            int StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            int CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            string MemberName = "";

            BusinessLayer.Common.MemberBill objMemberBill = new BusinessLayer.Common.MemberBill();
            DataTable dt = objMemberBill.MemberMonthlyDevelopmentBill(StateId, DistrictId, BlockId, CategoryId, Month, year, MemberName, 0);
            if (dt != null)
            {
                dgvDevelopmentFee.DataSource = dt;
                dgvDevelopmentFee.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                lblTotalMemberCount.Text = "Total Records: " + dt.Rows.Count.ToString();
                btnSave.Visible = true;
            }
            else
                btnSave.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMonthlyDevelopmentBill();
            Message.Show = false;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDevelopmentFee.Rows.Count; i++)
            {
                GridViewRow row = dgvDevelopmentFee.Rows[i];
                if (((CheckBox)row.FindControl("ChkSelect")).Checked)
                {
                    int billId = Convert.ToInt32(dgvDevelopmentFee.DataKeys[i].Values[0]);
                    TextBox txtAmount = (TextBox)dgvDevelopmentFee.Rows[i].Cells[8].FindControl("txtBillAmount");
                    decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                    BusinessLayer.Common.MemberBill objDevelopmentBill = new BusinessLayer.Common.MemberBill();
                    objDevelopmentBill.MonthlyDevelopmentBillUpdate(billId, Amount);
                }
            }
            Message.IsSuccess = true;
            Message.Text = "Development fee bill updated !!";            
            Message.Show = true;
            LoadMonthlyDevelopmentBill();
        }
    }
}