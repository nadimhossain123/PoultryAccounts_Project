using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace AccountsModule.Common
{
    public partial class website_committee_report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownLists();
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dgvMemberOutstanding.DataSource = null;
                dgvMemberOutstanding.DataBind();
                btnDownload.Visible = false;
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

        private void LoadConsolidatedOutstandingList()
        {
            int StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            int CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            DateTime FromDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            string MemberName = txtMemberName.Text.Trim();
            string MemberCode = txtMemberCode.Text.Trim();
            string MobileNo = txtMobileNo.Text.Trim();

            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dt = objMemberPayment.GetExecutiveCommitteeReport(StateId, DistrictId, BlockId, CategoryId, ToDate, FromDate, MemberName, MemberCode, MobileNo);

            dgvMemberOutstanding.DataSource = dt;
            dgvMemberOutstanding.DataBind();

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
            LoadConsolidatedOutstandingList();
            //Message.Show = false;
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
            PrepareGridViewForExport(dgvMemberOutstanding);
            dgvMemberOutstanding.Columns[0].Visible = false;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberConsolidatedOutstandingReport.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvMemberOutstanding.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }

        private void PrepareGridViewForExport(Control gv)
        {
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Button))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
    }
}