using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.SMS
{
    public partial class AddEditGeneralMember : System.Web.UI.Page
    {
        public int MemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadCategory();
                ClearControls();
                LoadMemberList();
            }
        }

        private void LoadCategory()
        {
            BusinessLayer.SMS.MemberCategory objMemberCategory = new BusinessLayer.SMS.MemberCategory();
            DataTable DT = objMemberCategory.GetAll();
            DataRow DR = DT.NewRow();
            DR["CategoryId"] = 0;
            DR["CategoryName"] = "Select";
            DT.Rows.InsertAt(DR, 0);

            ddlCategory.DataSource = DT;
            ddlCategory.DataBind();
        }

        private void ClearControls()
        {
            MemberId = 0;
            lblMsg.Text = "";
            btnSave.Text = "Save";

            txtName.Text = "";
            txtCompanyName.Text = "";
            txtVillage.Text = "";
            txtPostOffice.Text = "";
            txtPoliceStation.Text = "";
            txtPinCode.Text = "";
            txtBlockName.Text = "";
            txtDistrictName.Text = "";
            txtStateName.Text = "";
            txtCode.Text = "";
            txtMobileNo.Text = "";
            txtPhoneNo.Text = "";
            ddlCategory.SelectedIndex = 0;
        }

        private void LoadMemberList()
        {
            string Name = txtSearchName.Text;
            string Mobile = txtSearchMob.Text;
            BusinessLayer.SMS.GeneralMember objGeneralMember = new BusinessLayer.SMS.GeneralMember();
            DataTable DT = objGeneralMember.GetAll(Name, Mobile);

            dgvGeneralMember.DataSource = DT;
            dgvGeneralMember.DataBind();
        }

        private void LoadMemberDetails()
        {
            BusinessLayer.SMS.GeneralMember objGeneralMember = new BusinessLayer.SMS.GeneralMember();
            DataTable DT = objGeneralMember.GetAllById(MemberId);

            if (DT.Rows.Count > 0)
            {
                txtName.Text = DT.Rows[0]["Name"].ToString();
                txtCompanyName.Text = DT.Rows[0]["CompanyName"].ToString();
                txtVillage.Text = DT.Rows[0]["Village"].ToString();
                txtPostOffice.Text = DT.Rows[0]["PostOffice"].ToString();
                txtPoliceStation.Text = DT.Rows[0]["PoliceStation"].ToString();
                txtPinCode.Text = DT.Rows[0]["PinCode"].ToString();
                txtBlockName.Text = DT.Rows[0]["BlockName"].ToString();
                txtDistrictName.Text = DT.Rows[0]["DistrictName"].ToString();
                txtStateName.Text = DT.Rows[0]["StateName"].ToString();
                txtCode.Text = DT.Rows[0]["Code"].ToString();
                txtMobileNo.Text = DT.Rows[0]["MobileNo"].ToString();
                txtPhoneNo.Text = DT.Rows[0]["PhoneNo"].ToString();
                ddlCategory.SelectedValue = DT.Rows[0]["CategoryId"].ToString();

                lblMsg.Text = "";
                btnSave.Text = "Edit";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.GeneralMember objGeneralMember = new BusinessLayer.SMS.GeneralMember();
            Entity.SMS.GeneralMember generalMember = new Entity.SMS.GeneralMember();
            generalMember.MemberId = MemberId;
            generalMember.Name = txtName.Text.Trim();
            generalMember.CompanyName = txtCompanyName.Text.Trim();
            generalMember.Village = txtVillage.Text.Trim();
            generalMember.PostOffice = txtPostOffice.Text.Trim();
            generalMember.PoliceStation = txtPoliceStation.Text.Trim();
            generalMember.PinCode = txtPinCode.Text.Trim();
            generalMember.BlockName = txtBlockName.Text.Trim();
            generalMember.DistrictName = txtDistrictName.Text.Trim();
            generalMember.StateName = txtStateName.Text.Trim();
            generalMember.Code = txtCode.Text.Trim();
            generalMember.MobileNo = txtMobileNo.Text.Trim();
            generalMember.PhoneNo = txtPhoneNo.Text.Trim();
            generalMember.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);

            int RowsAffected = objGeneralMember.Save(generalMember);
            if (RowsAffected > 0)
            {
                ClearControls();
                LoadMemberList();
                lblMsg.Text = "Saved Successfully";
            }
            else
            {
                lblMsg.Text = "Can Not Save. Mobile No Already Exists";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            lblMsg.Text = "";
        }
        protected void dgvGeneralMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvGeneralMember.PageIndex = e.NewPageIndex;
            LoadMemberList();
        }
        protected void dgvGeneralMember_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MemberId = Convert.ToInt32(dgvGeneralMember.DataKeys[e.NewEditIndex].Value);
            LoadMemberDetails();
        }
        protected void dgvGeneralMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.SMS.GeneralMember objGeneralMember = new BusinessLayer.SMS.GeneralMember();
            objGeneralMember.Delete(Convert.ToInt32(dgvGeneralMember.DataKeys[e.RowIndex].Value));
            LoadMemberList();
        }
    }
}