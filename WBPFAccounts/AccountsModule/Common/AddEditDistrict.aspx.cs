using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditDistrict : System.Web.UI.Page
    {
        Entity.Common.District district = new Entity.Common.District();

        public int DistrictId
        {
            get { return Convert.ToInt32(ViewState["DistrictId"]); }
            set { ViewState["DistrictId"] = value; }
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
                LoadDistrict();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    DistrictId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateDistrict();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtCode.Text = string.Empty;
            txtDistrictName.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void PopulateDropDownLists()
        {
            PopulateStates();
        }

        protected void PopulateStates()
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
        }

        protected void PopulateDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            district = objDistrict.GetDistrictById(DistrictId);
            if (district != null)
            {
                DistrictId = district.DistrictId;
                txtDistrictName.Text = district.DistrictName.ToString();
                ddlState.SelectedValue = district.StateId.ToString();
                txtCode.Text = district.Code.ToString();
                if (txtRemarks.Text == string.Empty)
                    district.Remarks = string.Empty;
                else
                    txtRemarks.Text = district.Remarks.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            DataTable dt = objDistrict.GetAll(stateid);
            if (dt != null)
            {
                dgvDistrict.DataSource = dt;
                dgvDistrict.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
                district.DistrictId = DistrictId;
                district.DistrictName = txtDistrictName.Text.Trim();
                district.StateId = Convert.ToInt32(ddlState.SelectedValue);
                district.Code = txtCode.Text.Trim();
                district.Remarks = txtRemarks.Text.Trim();
                objDistrict.Save(district);
                Clear();
                LoadDistrict();
                Message.IsSuccess = true;
                Message.Text = "District Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate District Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditDistrict.aspx");
        }

        protected void dgvDistrict_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int DistrictId = Convert.ToInt32(dgvDistrict.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditDistrict.aspx?id=" + DistrictId);
        }
    }
}