using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class SMSSubscription : System.Web.UI.Page
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
                Message.Show = false;
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


            DataTable dt = objMember.GetAll(membermaster);
            if (dt != null)
            {
                dgvMemberMaster.DataSource = dt;
                dgvMemberMaster.DataBind();
            }

            DataTable dtSMS = objMember.SMSSubscription_GetAll(int.Parse(Session["FinYrID"].ToString()));
            if (dtSMS != null)
            {
                foreach (DataRow dr in dtSMS.Rows)
                {
                    foreach (GridViewRow gvr in dgvMemberMaster.Rows)
                    {
                        CheckBox chkSubscribe = (CheckBox)gvr.FindControl("chkSubscribe");

                        if (dr["MemberId"].ToString() == dgvMemberMaster.DataKeys[gvr.RowIndex].Values[0].ToString())
                        {
                            chkSubscribe.Checked = true;
                            chkSubscribe.Enabled = false;

                            if (dr["IsBlocked"].ToString() == "True")
                            {
                                Button btnBlock = (Button)gvr.FindControl("btnBlock");

                                btnBlock.Text = "Unblock";
                                for (int i = 0; i < 10; i++)
                                {
                                    gvr.Cells[i].Style.Add("background-color", "#E5D254");
                                }
                            }
                        }
                    }
                }
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
            Message.Show = false;
        }

        protected void dgvMemberMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool active = Convert.ToBoolean(dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["active"]);
                bool IsPriority = Convert.ToBoolean(dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["IsPriority"]);
                Label lblPriority = (Label)e.Row.FindControl("lblPriority");

                if (IsPriority == true)
                    lblPriority.Text = "H.P.";
                else
                    lblPriority.Text = "N.P.";

                if (active == false)
                {
                    CheckBox chkSubscribe = (CheckBox)e.Row.FindControl("chkSubscribe");
                    Button btnBlock = (Button)e.Row.FindControl("btnBlock");

                    chkSubscribe.Checked = false;
                    chkSubscribe.Enabled = false;
                    btnBlock.Enabled = false;

                    for (int i = 0; i < 10; i++)
                    {
                        e.Row.Cells[i].Style.Add("background-color", "#f93b3f");
                    }
                }
            }
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

        protected void dgvMemberMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Outstanding")
            {
                int memberid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("../Accounts/MemberOutstandingReport.aspx?MemberId=" + memberid);
            }
            else if (e.CommandName == "FeesCollection")
            {
                int memberid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("../Accounts/MemberFeesCollection.aspx?MemberId=" + memberid);
            }
        }

        protected void chkSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSubscribe = (CheckBox)sender;
                GridViewRow gvr = (GridViewRow)chkSubscribe.NamingContainer;
                int MemberId = int.Parse(dgvMemberMaster.DataKeys[gvr.RowIndex].Values[0].ToString());

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

                DataTable dt = new DataTable();
                dt.Columns.Add("MemberId");
                dt.Columns.Add("FinYearID");

                dt.Rows.Add();
                dt.Rows[0]["MemberId"] = MemberId;
                dt.Rows[0]["FinYearID"] = Session["FinYrID"];
                dt.AcceptChanges();

                membermaster.SubscriptionDetails = dt;

                int i = objMemberMaster.SMSSubscription_Save(membermaster);

                Message.IsSuccess = true;
                Message.Text = "Member SMS Subscription Updated.";

                LoadMember();

            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Member SMS Subscription Can Not Update!!!";
            }
            Message.Show = true;
        }

        protected void chkSubscribeAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MemberId");
                dt.Columns.Add("FinYearID");

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

                foreach (GridViewRow gvr in dgvMemberMaster.Rows)
                {
                    CheckBox chkSubscribe = (CheckBox)gvr.FindControl("chkSubscribe");
                    chkSubscribe.Checked = true;

                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["MemberId"] = dgvMemberMaster.DataKeys[gvr.RowIndex].Values[0].ToString();
                    dt.Rows[dt.Rows.Count - 1]["FinYearID"] = Session["FinYrID"];
                    dt.AcceptChanges();
                }

                membermaster.SubscriptionDetails = dt;

                int i = objMemberMaster.SMSSubscription_Save(membermaster);

                Message.IsSuccess = true;
                Message.Text = "Member SMS Subscription Updated.";

                LoadMember();

            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Member SMS Subscription Can Not Update!!!";
            }
            Message.Show = true;
        }

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                CheckBox chkSubscribe = (CheckBox)gvr.FindControl("chkSubscribe");

                if (!chkSubscribe.Checked)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Member can not update!!! Please check, member may not SMS subscribed...";
                    Message.Show = true;

                    return;
                }

                int MemberId = int.Parse(dgvMemberMaster.DataKeys[gvr.RowIndex].Values[0].ToString());
                bool IsBlock = (btn.Text == "Block") ? true : false;

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();

                int i = objMemberMaster.SMSSubscription_Block(MemberId, IsBlock);

                if (i > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Member updated.";

                    LoadMember();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Member can not update!!! Please check, member may not SMS subscribed...";
                }
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Member can not update!!!";
            }
            Message.Show = true;
        }
    }
}