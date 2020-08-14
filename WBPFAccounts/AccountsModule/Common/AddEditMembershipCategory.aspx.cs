using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditMembershipCategory : System.Web.UI.Page
    {
        Entity.Common.MembershipCategory membershipCategory = new Entity.Common.MembershipCategory();

        public int MembershipCategoryId
        {
            get { return Convert.ToInt32(ViewState["MembershipCategoryId"]); }
            set { ViewState["MembershipCategoryId"] = value; }
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
                LoadMembershipCategory();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    MembershipCategoryId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateMembershipCategory();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtCategoryName.Text = string.Empty;
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

        protected void PopulateMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            membershipCategory = objMembershipCategory.GetMembershipCategoryById(MembershipCategoryId);
            if (membershipCategory != null)
            {
                MembershipCategoryId = membershipCategory.MembershipCategoryId;
                txtCategoryName.Text = membershipCategory.CategoryName.ToString();
                txtRemarks.Text = membershipCategory.CategoryRemarks.ToString();
                chkSMSApplicable.Checked = membershipCategory.SMSApplicable;

                chkSMSApplicable.Enabled = false;

                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            DataTable dt = objMembershipCategory.GetAll();
            if (dt != null)
            {
                dgvMembershipCategory.DataSource = dt;
                dgvMembershipCategory.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
                membershipCategory.MembershipCategoryId = MembershipCategoryId;
                membershipCategory.CategoryName = txtCategoryName.Text.Trim();
                if (txtRemarks.Text == string.Empty)
                    membershipCategory.CategoryRemarks = string.Empty;
                else
                    membershipCategory.CategoryRemarks = txtRemarks.Text.Trim();
                membershipCategory.SMSApplicable = Convert.ToBoolean(chkSMSApplicable.Checked);

                objMembershipCategory.Save(membershipCategory);
                Clear();
                LoadMembershipCategory();
                Message.IsSuccess = true;
                Message.Text = "Membership Category Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Membership Category Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditMembershipCategory.aspx");
        }

        protected void dgvMembershipCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int MembershipCategoryId = Convert.ToInt32(dgvMembershipCategory.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditMembershipCategory.aspx?id=" + MembershipCategoryId);
        }
    }
}