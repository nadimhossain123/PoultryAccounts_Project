using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class SMSMemberSubscription : System.Web.UI.Page
    {
        public int SMSMemberId
        {
            get { return Convert.ToInt32(ViewState["SMSMemberId"]); }
            set { ViewState["SMSMemberId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["SMSMemberId"] != null && Request.QueryString["SMSMemberId"].ToString().Trim().Length > 0)
                {
                    SMSMemberId = Convert.ToInt32(Request.QueryString["SMSMemberId"].ToString().Trim());
                    LoadSMSMemberSubscriptionList();
                }
            }
        }

        protected void LoadSMSMemberSubscriptionList()
        {
            BusinessLayer.SMS.SMSTrigger objSMSTrigger = new BusinessLayer.SMS.SMSTrigger();
            DataTable dt = objSMSTrigger.MemberSetails_GetAll(SMSMemberId);
            if (dt != null)
            {
                dgvMemberMaster.DataSource = dt;
                dgvMemberMaster.DataBind();
            }
        }
    }
}