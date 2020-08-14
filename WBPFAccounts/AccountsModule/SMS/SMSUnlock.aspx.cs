using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.SMS
{
    public partial class SMSUnlock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                BusinessLayer.SMS.SMSTrigger objTrigger = new BusinessLayer.SMS.SMSTrigger();
                btnUnlock.Enabled = (objTrigger.IsMessageSentToday() == true) ? true : false;
            }
        }
        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.SMSTrigger objTrigger = new BusinessLayer.SMS.SMSTrigger();
            objTrigger.Unlock();
            btnUnlock.Enabled = false;
        }
    }
}