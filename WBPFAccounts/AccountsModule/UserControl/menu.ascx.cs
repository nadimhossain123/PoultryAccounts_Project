using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeERP.UserControl
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Module"].ToString().Equals("AC"))
                {
                    liMasters.Visible = false;
                    liMemberApproval.Visible = false;
                    liMemberDetail.Visible = false;
                    liAgentDetail.Visible = false;
                    liAgentPaymentReport.Visible = false;
                    liAgentPaymentReportSMS.Visible = false;
                    liFeeSettings.Visible = false;
                    liSMS.Visible = false;
                    liMemberPaymentEntry.Visible = true;
                    liDevelopmentFeesReportConsolidated.Visible = false;
                    liDevelopmentFeePaymentReport.Visible = false;
                }
                else if (Session["Module"].ToString().Equals("M"))
                {
                    liSettings.Visible = false;
                    liAccounts.Visible = false;
                    liPO.Visible = false;
                    liMemberPaymentEntry.Visible = false;
                    liDevelopmentFeesReportConsolidated.Visible = true;
                    liDevelopmentFeePaymentReport.Visible = true;
                }
                else if (Session["Module"].ToString().Equals("S"))
                {
                    liMasters.Visible = false;
                    liMemberApproval.Visible = false;
                    liMemberDetail.Visible = false;
                    liAgentDetail.Visible = false;
                    liAgentPaymentReport.Visible = false;
                    liAgentPaymentReportSMS.Visible = false;
                    liFeeSettings.Visible = false;
                    liSMS.Visible = true;
                    liMemberPaymentEntry.Visible = false;

                    liSettings.Visible = false;
                    liAccounts.Visible = false;
                    liPO.Visible = false;

                    liDevelopmentFee.Visible = false;
                    liEntries.Visible = false;
                }
            }
        }
    }
}