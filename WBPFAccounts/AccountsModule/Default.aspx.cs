using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserMode"] != null)
            {
                if (Session["UserMode"].ToString() == "Admin")
                {
                    this.MasterPageFile = "MasterAdmin.Master";
                }
                else
                {
                    this.MasterPageFile = "MasterMember.Master";
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null))
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
