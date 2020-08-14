using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class Tax : System.Web.UI.Page
    {
        Entity.Accounts.Tax tax = new Entity.Accounts.Tax();

        public int TaxId
        {
            get { return Convert.ToInt32(ViewState["TaxId"]); }
            set { ViewState["TaxId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadTax();
                    
                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                    {
                        TaxId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                        PopulateTax();
                    }
                    else
                    {
                        Clear();
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void Clear()
        {
            TaxId = 0;
            txtTaxHead.Text = string.Empty;
            txtTaxPercent.Text = string.Empty;
            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void PopulateTax()
        {
            BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
            tax = objTax.GetTaxById(TaxId);

            if (tax != null)
            {
                TaxId = tax.TaxId;
                txtTaxHead.Text = tax.TaxHead.ToString();
                txtTaxPercent.Text = tax.TaxPercent.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadTax()
        {
            BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
            DataTable dt = objTax.GetAll();

            if (dt != null)
            {
                dgvTax.DataSource = dt;
                dgvTax.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
                tax.TaxId = TaxId;
                tax.TaxHead = txtTaxHead.Text.Trim();
                tax.TaxPercent = (txtTaxPercent.Text.Trim().Length > 0) ? Convert.ToDecimal(txtTaxPercent.Text.Trim()) : 0;
                int messagecode = objTax.Save(tax);

                if (messagecode == 0)
                {
                    Clear();
                    LoadTax();
                    Message.IsSuccess = true;
                    Message.Text = "Tax Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Duplicate Tax Head not allowed";
                }
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Tax Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void dgvTax_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int TaxId = Convert.ToInt32(dgvTax.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("Tax.aspx?id=" + TaxId);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}