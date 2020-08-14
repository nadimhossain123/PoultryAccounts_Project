using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace AccountsModule
{
    public partial class MasterMember : System.Web.UI.MasterPage
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrTitle.Text = Session["UserName"].ToString();
            }
        }
    }
}