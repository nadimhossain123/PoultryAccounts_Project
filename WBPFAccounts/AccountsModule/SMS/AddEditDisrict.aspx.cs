using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace AccountsModule.SMS
{
    public partial class AddEditDisrict : System.Web.UI.Page
    {
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
                loaddistrict();
            }
        }

        protected void loaddistrict()
        {
            BusinessLayer.SMS.district objdistrict = new BusinessLayer.SMS.district();
            DataTable dtdistrict = objdistrict.GetAll();
            if (dtdistrict != null)
            {
                dgvdistrict.DataSource = dtdistrict;
                dgvdistrict.DataBind();
            }
        }

        protected void clearcontrol()
        {
            DistrictId = 0;
            txtdistrictname.Text = "";
            txtShortName.Text = "";
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.district objdistrict = new BusinessLayer.SMS.district();
            Entity.SMS.district Entitydistrict = new Entity.SMS.district();
            Entitydistrict.DistrictId = DistrictId;
            Entitydistrict.DistrictName = txtdistrictname.Text.Trim();
            Entitydistrict.ShortName = txtShortName.Text.Trim();
            objdistrict.Save(Entitydistrict);
            loaddistrict();
            clearcontrol();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            clearcontrol();
            btnsubmit.Text = "submit";
        }


        protected void dgvdistrict_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DistrictId = Convert.ToInt32(dgvdistrict.DataKeys[e.NewEditIndex].Value);
            BusinessLayer.SMS.district objdistrict = new BusinessLayer.SMS.district();

            Entity.SMS.district district = new Entity.SMS.district();
            district = objdistrict.GetAllById(DistrictId);
            txtdistrictname.Text = district.DistrictName.ToString();
            txtShortName.Text = district.ShortName.ToString();
            btnsubmit.Text = "update";


        }
    }
}