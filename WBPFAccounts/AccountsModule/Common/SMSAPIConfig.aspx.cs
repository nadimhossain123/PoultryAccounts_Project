using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class SMSAPIConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                Message.Show = false;
                LoadAPIList();
            }
        }

        private void LoadAPIList()
        {
            BusinessLayer.SMS.SMSAPIConfig objSMSAPIConfig = new BusinessLayer.SMS.SMSAPIConfig();
            DataTable dtAPI = objSMSAPIConfig.GetAll();
            string selectedAPI = dtAPI.Select("IsSelected = 1")[0]["APIId"].ToString();

            radioListAPI.DataSource = dtAPI;
            radioListAPI.DataBind();
            radioListAPI.SelectedValue = selectedAPI;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.SMSAPIConfig objSMSAPIConfig = new BusinessLayer.SMS.SMSAPIConfig();
            int APIId = Convert.ToInt32(radioListAPI.SelectedValue);
            objSMSAPIConfig.Update(APIId);

            LoadAPIList();
            Message.IsSuccess = true;
            Message.Text = "API Configuration Updated Successfully";
            Message.Show = true;
        }
    }
}