using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class MemberSMSCategory : System.Web.UI.Page
    {
        public int MemberSMSCategoryId
        {
            get { return Convert.ToInt32(ViewState["MemberSMSCategoryId"]); }
            set { ViewState["MemberSMSCategoryId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadMemberSMSCategory();
                Message.Show = false;
            }
        }

        protected void LoadMemberSMSCategory()
        {
            BusinessLayer.Common.MemberSMSCategory objMemberSMSCategory = new BusinessLayer.Common.MemberSMSCategory();
            DataTable dt = new DataTable();

            dt = objMemberSMSCategory.MemberSMSCategory_GetAll();
            if (dt != null)
            {
                dgvMemberSMSCategory.DataSource = dt;
                dgvMemberSMSCategory.DataBind();
            }
        }

        protected void PopulateMemberSMSCategory()
        {
            BusinessLayer.Common.MemberSMSCategory objMemberSMSCategory = new BusinessLayer.Common.MemberSMSCategory();
            Entity.Common.MemberSMSCategory memberSMSCategory = new Entity.Common.MemberSMSCategory();

            memberSMSCategory = objMemberSMSCategory.MemberSMSCategory_GetById(MemberSMSCategoryId);
            txtCategoryName.Text = memberSMSCategory.MemberSMSCategoryName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberSMSCategory objMemberSMSCategory = new BusinessLayer.Common.MemberSMSCategory();
            Entity.Common.MemberSMSCategory memberSMSCategory = new Entity.Common.MemberSMSCategory();

            memberSMSCategory.MemberSMSCategoryId = MemberSMSCategoryId;
            memberSMSCategory.MemberSMSCategoryName = txtCategoryName.Text.Trim();

            int i = objMemberSMSCategory.MemberSMSCategory_Save(memberSMSCategory);
            if (i > 0)
            {
                LoadMemberSMSCategory();
                txtCategoryName.Text = "";

                Message.IsSuccess = true;
                Message.Text = "Member SMS Category successfully saved...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Member SMS Category can not save!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            MemberSMSCategoryId = 0;
            Message.Show = false;
            txtCategoryName.Text = "";
        }

        protected void dgvMemberSMSCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                MemberSMSCategoryId = int.Parse(e.CommandArgument.ToString());
                PopulateMemberSMSCategory();
            }
        }
    }
}