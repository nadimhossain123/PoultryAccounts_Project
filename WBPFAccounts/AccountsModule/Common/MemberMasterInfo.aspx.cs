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
    public partial class MemberMasterInfo : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin"))
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Agent"))
                this.MasterPageFile = "../MasterAgent.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null)
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    Message.Show = false;

                    if (!Session["UserType"].ToString().Equals("Admin"))
                    {
                        lnkBulkUpload.Visible = false;
                        //ddlDistrict.SelectedValue = Session["DistrictId"].ToString();
                        //ddlDistrict.Enabled = false;
                    }
                    LoadMemberList();
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
            LoadBusinessType();
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

        protected void LoadBusinessType()
        {
            BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
            DataTable dt = objBusinessType.GetAll();
            if (dt != null)
            {
                ddlBusinessType.DataSource = dt;
                ddlBusinessType.DataTextField = "BusinessTypeName";
                ddlBusinessType.DataValueField = "BusinessTypeId";
                ddlBusinessType.DataBind();
            }
            InsertFisrtItem(ddlBusinessType, "--Select Business Type--");
        }

        protected void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            membermaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            membermaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            membermaster.StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            membermaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            membermaster.MemberName = txtMemberName.Text.Trim();
            membermaster.MobileNo = txtMobile.Text.Trim();
            membermaster.BusinessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue.Trim());
            membermaster.MembershipMonth = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            membermaster.MembershipYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());

            DataTable dt = objMember.GetAll(membermaster);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "IsApproved Is Not Null";

                dgvMemberMaster.DataSource = dv;
                dgvMemberMaster.DataBind();
                lblTotalMemberCount.Text = Server.HtmlDecode("<b>Total Member Count: " + dv.ToTable().Rows.Count.ToString() + "</b>");
            }
        }

        protected void dgvMemberMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int MemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[e.NewEditIndex].Values["MemberId"]);
            Response.Redirect("AddEditMemberMaster.aspx?id=" + MemberId + "&Back=MemberMasterInfo.aspx");
        }

        protected void dgvMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMemberMaster.PageIndex = e.NewPageIndex;
            LoadMemberList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            Message.Show = false;
        }

        protected void dgvMemberMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool IsActive = Convert.ToBoolean(dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["IsActive"].ToString());
                bool IsPriority = Convert.ToBoolean(dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["IsPriority"].ToString());
                Button btnActivate = (Button)e.Row.FindControl("btnActivate");
                Button btnPriority = (Button)e.Row.FindControl("btnPriority");
                Button btnPayment = (Button)e.Row.FindControl("btnPayment");
                Button btnSMSPayment = (Button)e.Row.FindControl("btnSMSPayment");
                Button btnOutstanding = (Button)e.Row.FindControl("btnOutstanding");
                Button btnDevFeesOutstanding = (Button)e.Row.FindControl("btnDevFeesOutstanding");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");

                btnActivate.CommandArgument = e.Row.RowIndex.ToString();
                btnPriority.CommandArgument = e.Row.RowIndex.ToString();
                btnActivate.Text = (IsActive) ? "Active" : "In-Active";
                btnPriority.Text = (IsPriority) ? "High" : "Low";
                btnOutstanding.Attributes.Add("OnClick", "openpopup('MemberOutstandingReport.aspx?MemberId=" + dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["MemberId"].ToString() + "'); return false;");
                btnDevFeesOutstanding.Attributes.Add("OnClick", "openpopup('DevelopmentFeesOutstandingReport.aspx?MemberId=" + dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["MemberId"].ToString() + "'); return false;");
                btnPayment.Attributes.Add("OnClick", "openpopup('MemberPayment.aspx?MemberId=" + dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["MemberId"].ToString() + "'); return false;");
                btnSMSPayment.Attributes.Add("OnClick", "openpopup('SMSPayment.aspx?SMSMemberId=1'); return false;");

                if (!IsActive)
                {
                    btnEdit.Visible = false;
                    btnPriority.Visible = false;
                    btnOutstanding.Visible = false;
                    btnDevFeesOutstanding.Visible = false;
                    btnPayment.Visible = false;
                    btnSMSPayment.Visible = false;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Style.Add("background-color", "#DB2526");
                        e.Row.Cells[i].Style.Add("color", "#FFFFFF");
                    }
                }

                if (!Session["UserType"].ToString().Equals("Admin"))
                {
                    btnEdit.Visible = false;
                    btnPriority.Visible = false;
                    btnOutstanding.Visible = false;
                    btnDevFeesOutstanding.Visible = false;
                    btnActivate.Visible = false;
                    btnSMSPayment.Visible = false;
                }
            }
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

        protected void dgvMemberMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Activate")
            {
                int MemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Values["MemberId"].ToString());
                bool IsActive = Convert.ToBoolean(dgvMemberMaster.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Values["IsActive"].ToString());

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                objMemberMaster.MemberActivate(MemberId, !IsActive);
                LoadMemberList();
            }
            else if (e.CommandName == "ChangePriority")
            {
                int MemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Values["MemberId"].ToString());
                bool IsPriority = Convert.ToBoolean(dgvMemberMaster.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Values["IsPriority"].ToString());

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                objMemberMaster.MemberMaster_Priority_Update(MemberId, !IsPriority);
                LoadMemberList();
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            dgvMemberMaster.AllowPaging = false;
            dgvMemberMaster.DataBind();

            PrepareGridViewForExport(dgvMemberMaster);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberMaster.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            System.IO.StringWriter sWriter = new System.IO.StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvMemberMaster.RenderControl(hTextWriter);
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