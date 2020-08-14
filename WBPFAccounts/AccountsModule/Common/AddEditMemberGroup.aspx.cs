using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditMemberGroup : System.Web.UI.Page
    {
        Entity.Common.MemberGroup memberGroup = new Entity.Common.MemberGroup();

        public int MemberGroupId
        {
            get { return Convert.ToInt32(ViewState["MemberGroupId"]); }
            set { ViewState["MemberGroupId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                PopulateDropDownLists();
                LoadMemberGroup();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    MemberGroupId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateMemberGroup();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtMemberGroupName.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void PopulateDropDownLists()
        {

        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateMemberGroup()
        {
            BusinessLayer.Common.MemberGroup objMemberGroup = new BusinessLayer.Common.MemberGroup();
            memberGroup = objMemberGroup.GetMemberGroupById(MemberGroupId);
            if (memberGroup != null)
            {
                MemberGroupId = memberGroup.MemberGroupId;
                txtMemberGroupName.Text = memberGroup.MemberGroupName.ToString();
                txtRemarks.Text = memberGroup.Remarks.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadMemberGroup()
        {
            BusinessLayer.Common.MemberGroup objMemberGroup = new BusinessLayer.Common.MemberGroup();
            DataTable dt = objMemberGroup.GetAll();
            if (dt != null)
            {
                dgvMemberGroup.DataSource = dt;
                dgvMemberGroup.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.MemberGroup objMemberGroup = new BusinessLayer.Common.MemberGroup();
                memberGroup.MemberGroupId = MemberGroupId;
                memberGroup.MemberGroupName = txtMemberGroupName.Text.Trim();
                if (txtRemarks.Text == string.Empty)
                    memberGroup.Remarks = string.Empty;
                else
                    memberGroup.Remarks = txtRemarks.Text.Trim();
                objMemberGroup.Save(memberGroup);
                Clear();
                LoadMemberGroup();
                Message.IsSuccess = true;
                Message.Text = "Member Group Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Member Group Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditMemberGroup.aspx");
        }

        protected void dgvMemberGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int MemberGroupId = Convert.ToInt32(dgvMemberGroup.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditMemberGroup.aspx?id=" + MemberGroupId);
        }
    }
}