using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AccountsModule
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();  
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            Session["UserType"] = null;
            Session["UserName"] = null;
            Session["CompanyId"] = null;
            Session["SuperAdmin"] = null;
            Session["Module"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}
