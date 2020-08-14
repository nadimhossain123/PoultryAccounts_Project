using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditBusinessType : System.Web.UI.Page
    {
        Entity.Common.BusinessType businessType = new Entity.Common.BusinessType();

        public int BusinessTypeId
        {
            get { return Convert.ToInt32(ViewState["BusinessTypeId"]); }
            set { ViewState["BusinessTypeId"] = value; }
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
                LoadBusinessType();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    BusinessTypeId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateBusinessType();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtBusinessTypeName.Text = string.Empty;
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

        protected void PopulateBusinessType()
        {
            BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
            businessType = objBusinessType.GetBusinessTypeById(BusinessTypeId);
            if (businessType != null)
            {
                BusinessTypeId = businessType.BusinessTypeId;
                txtBusinessTypeName.Text = businessType.BusinessTypeName.ToString();
                txtRemarks.Text = businessType.Remarks.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadBusinessType()
        {
            BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
            DataTable dt = objBusinessType.GetAll();
            if (dt != null)
            {
                dgvBusinessType.DataSource = dt;
                dgvBusinessType.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
                businessType.BusinessTypeId = BusinessTypeId;
                businessType.BusinessTypeName = txtBusinessTypeName.Text.Trim();
                if (txtRemarks.Text == string.Empty)
                    businessType.Remarks = string.Empty;
                else
                    businessType.Remarks = txtRemarks.Text.Trim();
                objBusinessType.Save(businessType);
                Clear();
                LoadBusinessType();
                Message.IsSuccess = true;
                Message.Text = "Business Type Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Business Type Name Is Not Allowed!!!";
            }
            Message.Show = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditBusinessType.aspx");
        }

        protected void dgvBusinessType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int BusinessTypeId = Convert.ToInt32(dgvBusinessType.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditBusinessType.aspx?id=" + BusinessTypeId);
        }
    }
}