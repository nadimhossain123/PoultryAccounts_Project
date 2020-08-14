using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class MembershipCategoryFeesConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Trim().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadMembershipCategory();
                    dgvFeesHead.DataSource = null;
                    dgvFeesHead.DataBind();
                    btnSave.Visible = false;
                    Message.Show = false;
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
            InsertFisrtItem(ddlMembershipCategory, "--Select Membership Category--");
        }

        protected void LoadFeesDetails()
        {
            BusinessLayer.Common.MembershipCategoryFeesConfig objFeesConfig = new BusinessLayer.Common.MembershipCategoryFeesConfig();
            DataTable dt = objFeesConfig.GetAll(Convert.ToInt32(ddlMembershipCategory.SelectedValue));

            dgvFeesHead.DataSource = dt;
            dgvFeesHead.DataBind();

            if (dt.Rows.Count > 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MembershipCategoryFeesConfig objFeesConfig = new BusinessLayer.Common.MembershipCategoryFeesConfig();
            Entity.Common.MembershipCategoryFeesConfig FeesConfig = new Entity.Common.MembershipCategoryFeesConfig();
            FeesConfig.MembershipCategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue);

            string strFeesXml = "<NewDataSet>";

            foreach (GridViewRow gvr in dgvFeesHead.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    strFeesXml += "<Row";
                    strFeesXml += " FeesHeadId = \"" + dgvFeesHead.DataKeys[gvr.RowIndex].Value.ToString() + "\"";
                    strFeesXml += " Amount = \"" + (string.IsNullOrEmpty(txtAmount.Text.Trim()) ? "0" : txtAmount.Text.Trim()) + "\"";
                    strFeesXml += " />";
                }
            }

            strFeesXml += "</NewDataSet>";

            FeesConfig.FeesXml = strFeesXml;
            objFeesConfig.Save(FeesConfig);

            LoadFeesDetails();
            Message.IsSuccess = true;
            Message.Text = "Fees Saved Successfully";
            Message.Show = true;
        }

        protected void ddlMembershipCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMembershipCategory.SelectedIndex == 0)
            {
                dgvFeesHead.DataSource = null;
                dgvFeesHead.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                LoadFeesDetails();
            }
            Message.Show = false;
        }
    }
}
