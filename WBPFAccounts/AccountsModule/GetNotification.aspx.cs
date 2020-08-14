using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule
{
    public partial class GetNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["MAC"] != null && Request.QueryString["MAC"].Trim().Length > 0)
            {
                Response.Write("1st Text Message @ " + Request.QueryString["MAC"].Trim());
                Response.Write("|2nd Text Message @ " + Request.QueryString["MAC"].Trim());
                Response.Write("|3rd Text Message @ " + Request.QueryString["MAC"].Trim());
                Response.End();
            }
        }
    }
}