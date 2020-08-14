using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using BusinessLayer.Accounts;

namespace AccountsModule.Common
{
    public partial class RenewalFeeAdjustmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    Message.Show = false;
                    btnDownload.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
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

        protected void LoadMemberList()
        {
            string MemberName = txtMemberName.Text.Trim();
            int StateId = Convert.ToInt32(ddlState.SelectedValue);
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue);
            int MembershipCategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue);

            BusinessLayer.Common.MemberBill objMember = new BusinessLayer.Common.MemberBill();
            DataTable dt = objMember.RenewalAdjustmentDetails(MemberName, StateId, DistrictId, BlockId, MembershipCategoryId);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dgvMemberMaster.DataSource = dv;
                dgvMemberMaster.DataBind();
                lblTotalMemberCount.Text = Server.HtmlDecode("<b>Total Member Count: " + dv.ToTable().Rows.Count.ToString() + "</b>");
            }
            if (dt.Rows.Count > 0)
            {
                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (dgvMemberMaster.Rows.Count > 0)
            {
                string[] _header = new string[5];
                _header[0] = "MEMBER LIST";
                _header[1] = "STATE: " + ddlState.SelectedItem.ToString();
                _header[2] = "DISTRICT: " + ddlDistrict.SelectedItem.ToString();
                _header[3] = "BLOCK: " + ddlBlock.SelectedItem.ToString();
                _header[4] = lblTotalMemberCount.Text;

                string[] _footer = new string[0];
                string file = "MEMBER_LIST";

                BusinessLayer.Common.Excel.SaveExcel(_header, dgvMemberMaster, _footer, file);
            }
        }
    }
}