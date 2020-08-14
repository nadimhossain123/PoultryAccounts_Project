using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Common
{
    public partial class CollegeMaster : System.Web.UI.Page
    {
        public int CompanyId
        {
            get { return Convert.ToInt32(ViewState["CompanyId"]); }
            set { ViewState["CompanyId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {

                LoadCompanyList();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    CompanyId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadCompanyDetails();
                }
                else
                {
                    ClearControls();
                }
            }
        }

        protected void LoadCompanyDetails()
        {
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            Entity.Common.Company Company = new Entity.Common.Company();
            Company = ObjCompany.GetAllById(CompanyId);
            if (Company != null)
            {
                txtCompanyName.Text = Company.CompanyName;
                txtCompanyName.Enabled = true;
                txtAlias.Text = Company.CompanyAliasName;
                txtAddress.Text = Company.CompanyAddress;
                txtPhone.Text = Company.CompanyPhoneNo;
                txtFax.Text = Company.CompanyPhoneFax;
                txtEmail.Text = Company.CompanyEmailId;
                txtPTaxNo.Text = Company.CompanyPTax;
                txtTanNo.Text = Company.CompanyTanNo;
                txtPFNo.Text = Company.CompanyPFNo;
                txtESINo.Text = Company.CompanyESINo;
                txtCINNo.Text = Company.CompanyCIN;
                txtVatRegnNo.Text = Company.CompanyVATRegistrationNo;
                txtCSTRegnNo.Text = Company.CompanyCSTRegistrationNo;
                txtSTRegnNo.Text = Company.CompanySTRegistrationNo;
                txtWebsite.Text = Company.CompanyWebsite;
                txtContactPerson.Text = Company.CompanyContactPersonName;
                txtContactPersonNo.Text = Company.CompanyContactPersonNo;
                ddlCompanyType.SelectedValue = Company.CompanyType;
                txtPANNo.Text = Company.CompanyPANNo;
                Message.Show = false;
                btnSave.Text = "Update";
                
            }
        }

        protected void LoadCompanyList()
        {
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            DataTable dt = ObjCompany.GetAll();
            if (dt != null)
            {
                dgvCompany.DataSource = dt;
                dgvCompany.DataBind();
            }
        }

        protected void ClearControls()
        {
            CompanyId = 0;
            txtCompanyName.Text = "";
            txtCompanyName.Enabled = false;
            txtAlias.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtPTaxNo.Text = "";
            txtTanNo.Text = "";
            txtPFNo.Text = "";
            txtESINo.Text = "";
            txtCINNo.Text = "";
            txtVatRegnNo.Text = "";
            txtCSTRegnNo.Text ="";
            txtSTRegnNo.Text = "";
            txtWebsite.Text = "";
            txtContactPerson.Text ="";
            txtContactPersonNo.Text = "";
            ddlCompanyType.SelectedIndex = 0;
            txtPANNo.Text = "";
            btnSave.Text = "Update";
            Message.Show = false;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            Entity.Common.Company Company = new Entity.Common.Company();
            Company.CompanyId = CompanyId;
            Company.CompanyName = txtCompanyName.Text.Trim();
            Company.CompanyAliasName = txtAlias.Text.Trim();
            Company.CompanyAddress = txtAddress.Text.Trim();
            Company.CompanyPhoneNo = txtPhone.Text.Trim();
            Company.CompanyPhoneFax = txtFax.Text.Trim();
            Company.CompanyEmailId = txtEmail.Text.Trim();
            Company.CompanyPTax = txtPTaxNo.Text.Trim();
            Company.CompanyTanNo = txtTanNo.Text.Trim();
            Company.CompanyPFNo = txtPFNo.Text.Trim();
            Company.CompanyESINo = txtESINo.Text.Trim();
            Company.CompanyCIN = txtCINNo.Text.Trim();
            Company.CompanyVATRegistrationNo = txtVatRegnNo.Text.Trim();
            Company.CompanyCSTRegistrationNo = txtCSTRegnNo.Text.Trim();
            Company.CompanySTRegistrationNo = txtSTRegnNo.Text.Trim();
            Company.CompanyWebsite = txtWebsite.Text.Trim();
            Company.CompanyContactPersonName = txtContactPerson.Text.Trim();
            Company.CompanyContactPersonNo = txtContactPersonNo.Text.Trim();
            Company.CompanyType = ddlCompanyType.SelectedValue.Trim();
            Company.CompanyPANNo = txtPANNo.Text.Trim();

            Company.CompanyCUser_UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            int RowsAffected= ObjCompany.Save(Company);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadCompanyList();
                Message.IsSuccess = true;
                Message.Text = "Company Configuration Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not save. Duplicate Company Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvCompany_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvCompany.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("CollegeMaster.aspx?id=" + Id);
        }

        
    }
}
