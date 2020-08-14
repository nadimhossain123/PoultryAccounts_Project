using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class MemberLogDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    LoadSMSMemberLogDetails();
                    txtToDate.Text= DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void LoadSMSMemberLogDetails()
        {
            string MemberName = txtSearchMemberName.Text.Trim();
            string MobileNo = txtSearchMobileNo.Text.Trim();
            string FromDate1 = (txtFromDate.Text.ToString());
            string ToDate1 = (txtToDate.Text.ToString());
            DateTime FromDate, ToDate;
            if (FromDate1 != "" && ToDate1 != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
                ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            }
            else { FromDate = DateTime.MinValue; ToDate = DateTime.MinValue; }
            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            DataTable dt = objSMSMember.GetAllMemberLogDetails(MemberName, MobileNo, FromDate, ToDate);
            if (dt.Rows.Count > 0)
            {
                dgvSMSMemberLogDetails.DataSource = dt;
                dgvSMSMemberLogDetails.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSMSMemberLogDetails();
        }
        protected void dgvSMSMemberLogDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSMSMemberLogDetails.PageIndex = e.NewPageIndex;
            LoadSMSMemberLogDetails();
        }
    }
}