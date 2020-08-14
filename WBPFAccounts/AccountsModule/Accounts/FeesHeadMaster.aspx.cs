using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinessLayer.Accounts;

namespace AccountsModule.Accounts
{
    public partial class FeesHeadMaster : System.Web.UI.Page
    {
        public int FeesHeadId
        {
            get { return Convert.ToInt32(ViewState["FeesHeadId"]); }
            set { ViewState["FeesHeadId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Trim().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadTaxMaster();
                    ClearControls();
                    LoadFeesHeadList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void LoadTaxMaster()
        {
            BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
            CheckBoxListTax.DataSource = objTax.GetAllDistinct();
            CheckBoxListTax.DataBind();
        }

        protected void ClearControls()
        {
            FeesHeadId = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtFeesHeadName.Text = "";
            ddlFrequency.SelectedIndex = 1;
            ddlFrequency.Enabled = true;

            ddlFeesType.SelectedIndex = 0;
            ddlFeesType.Enabled = true;

            ChkIsMandatory.Checked = true;
            ChkIsActive.Checked = true;

            foreach (ListItem li in CheckBoxListTax.Items)
            {
                li.Selected = false;
            }
        }

        protected void LoadFeesHeadList()
        {
            BusinessLayer.Common.FeesHeadMaster objFeesHeadMaster = new BusinessLayer.Common.FeesHeadMaster();
            DataTable dt = objFeesHeadMaster.GetAll();

            if (dt != null)
            {
                dgvFeesHead.DataSource = dt;
                dgvFeesHead.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void dgvFeesHead_RowEditing(object sender, GridViewEditEventArgs e)
        {
            FeesHeadId = Convert.ToInt32(dgvFeesHead.DataKeys[e.NewEditIndex].Value);
            BusinessLayer.Common.FeesHeadMaster objFeesHeadMaster = new BusinessLayer.Common.FeesHeadMaster();
            DataSet ds = objFeesHeadMaster.GetAllById(FeesHeadId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFeesHeadName.Text = ds.Tables[0].Rows[0]["FeesHeadName"].ToString();
                ddlFrequency.SelectedValue = ds.Tables[0].Rows[0]["Frequency"].ToString();
                ddlFrequency.Enabled = false;

                ddlFeesType.SelectedValue = ds.Tables[0].Rows[0]["FeesType"].ToString();
                ddlFeesType.Enabled = false;

                ChkIsMandatory.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsMandatory"].ToString());
                ChkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
            }

            foreach (ListItem li in CheckBoxListTax.Items)
            {
                li.Selected = false;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    CheckBoxListTax.Items.FindByValue(dr["TaxId"].ToString()).Selected = true;
                }
            }

            btnSave.Text = "Update";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.FeesHeadMaster objFeesHeadMaster = new BusinessLayer.Common.FeesHeadMaster();
            Entity.Common.FeesHeadMaster FeesHead = new Entity.Common.FeesHeadMaster();

            FeesHead.FeesHeadId = FeesHeadId;
            FeesHead.FeesHeadName = txtFeesHeadName.Text.Trim();
            FeesHead.Frequency = ddlFrequency.SelectedValue;
            FeesHead.FeesType = Convert.ToInt32(ddlFeesType.SelectedValue);
            FeesHead.IsMandatory = ChkIsMandatory.Checked;
            FeesHead.IsActive = ChkIsActive.Checked;

            string FeesHeadTaxMapXml = "<NewDataSet>";

            foreach (ListItem li in CheckBoxListTax.Items)
            {
                if (li.Selected)
                {
                    FeesHeadTaxMapXml += "<Row TaxId = \"" + li.Value + "\" />";
                }
            }

            FeesHeadTaxMapXml += "</NewDataSet>";

            FeesHead.FeesHeadTaxMapXml = FeesHeadTaxMapXml;
            int messageCode = objFeesHeadMaster.Save(FeesHead);
            if (messageCode == 0)
            {
                ClearControls();
                LoadFeesHeadList();
                Message.IsSuccess = true;
                Message.Text = "Fees Head Saved/Updated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Fees Head Name Is Not Allowed";
            }
            Message.Show = true;
        }

    }
}
