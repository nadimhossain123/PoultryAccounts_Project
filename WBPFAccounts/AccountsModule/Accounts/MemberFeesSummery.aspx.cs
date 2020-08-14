using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CollegeERP.Accounts;

namespace AccountsModule.Accounts
{
    public partial class MemberFeesSummery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                PopulateDropDownLists();
                LoadMember();
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadStates();
            LoadDistricts();
            LoadBlock();
            LoadMembershipCategory();
            LoadMember();
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
            InsertFisrtItem(ddlState, "Select All");
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
            InsertFisrtItem(ddlDistrict, "Select All");
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
            InsertFisrtItem(ddlBlock, "Select All");
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
            InsertFisrtItem(ddlMembershipCategory, "Select All");
        }

        protected void LoadMember()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            if (ddlBlock.SelectedIndex == 0)
                membermaster.BlockId = 0;
            else
                membermaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());

            if (ddlDistrict.SelectedIndex == 0)
                membermaster.DistrictId = 0;
            else
                membermaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());

            if (ddlState.SelectedIndex == 0)
                membermaster.StateId = 0;
            else
                membermaster.StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());

            if (ddlMembershipCategory.SelectedIndex == 0)
                membermaster.CategoryId = 0;
            else
                membermaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());

            if (txtMemberName.Text == string.Empty)
                membermaster.MemberName = string.Empty;
            else
                membermaster.MemberName = txtMemberName.Text.Trim();


            DataTable dt = objMember.MemberFeesSummery_GetAll(membermaster);
            if (dt != null)
            {
                dgvMemberMaster.DataSource = dt;
                dgvMemberMaster.DataBind();

                DataView dv = new DataView(dt);
                dv.RowFilter = "IsMember='Yes'";
                lblTotalMember.Text = Convert.ToString(dv.ToTable().Rows.Count);
            }
        }
        
        protected void dgvMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMemberMaster.PageIndex = e.NewPageIndex;
            LoadMember();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMember();
        }
        
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
            LoadMember();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
            LoadMember();
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMember();
        }

        protected void ddlMembershipCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMember();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>WBPF</b>";
            _header[1] = "Members Fees Summery";
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];

            string file = "MEMBERS_FEES_SUMMERY_REPORT";

            dgvMemberMaster.AllowPaging = false;
            LoadMember();

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvMemberMaster, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Members Fees Summery Report";
            string[] _header = new string[3];
            _header[0] = "Members Fees Summery Report";
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[0];

            dgvMemberMaster.AllowPaging = false;
            LoadMember();

            Print.ReportPrint(Title, _header, dgvMemberMaster, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}