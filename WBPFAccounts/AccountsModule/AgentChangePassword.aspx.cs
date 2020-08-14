using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule
{
    public partial class AgentChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Agent"))
            {
                if (!IsPostBack)
                {
                    Message.Show = false;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.AgentMaster objAgentMaster = new BusinessLayer.Common.AgentMaster();
            objAgentMaster.ChangePassword(Convert.ToInt32(Session["UserId"].ToString()), txtPassword.Text.Trim());

            Message.IsSuccess = true;
            Message.Text = "Your Password is Changed Successfully";
            Message.Show = true;
            txtPassword.Text = "";
        }
    }
}