using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class MembersOutstandingReport : System.Web.UI.Page
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
                //LoadMember();
                //Message.Show = false;
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
            //LoadMember();
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


            DataTable dt = objMember.GetAllOutstandingReport(membermaster);
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
            //Message.Show = false;
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

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (dgvMemberMaster.Rows.Count > 0)
                ExportGrid(dgvMemberMaster, "Members Outstanding Report-" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".xls");
        }

        public static void ExportGrid(GridView oGrid, string exportFile)
        {
            //Clear the response, and set the content type and mark as attachment
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + exportFile + "\"");

            //Clear the character set
            HttpContext.Current.Response.Charset = "";

            //Create a string and Html writer needed for output
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            //Clear the controls from the pased grid
            //ClearControls(oGrid);

            //Show grid lines
            oGrid.GridLines = GridLines.Both;

            //Color header
            oGrid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

            //Render the grid to the writer
            oGrid.RenderControl(oHtmlTextWriter);
            //Write out the response (file), then end the response
            HttpContext.Current.Response.Write(oStringWriter.ToString());
            HttpContext.Current.Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}