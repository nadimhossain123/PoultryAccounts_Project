using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class renewal_bill : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PaymentNo"] != null && Request.QueryString["PaymentNo"].Trim().Length > 0)
                {
                    string PaymentNo = Request.QueryString["PaymentNo"].ToString();
                    LoadPaymentDetail(PaymentNo);
                }
            }
        }


        private void LoadPaymentDetail(string PaymentNo)
        {
            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dtPayment = objMemberPayment.GetFeesHeadWisePaymentReport(PaymentNo, 0, "All", DateTime.MinValue, DateTime.MinValue, 0,0);
            if (dtPayment != null)
            {
                lblSLNo.Text = dtPayment.Rows[0]["SrlNo"].ToString();
                lblMemberCode.Text = dtPayment.Rows[0]["MemberCode"].ToString();
                lblMemberName.Text = dtPayment.Rows[0]["MemberName"].ToString();
                lblPO.Text = dtPayment.Rows[0]["PO"].ToString();
                lblBlock.Text = dtPayment.Rows[0]["BlockName"].ToString();
                lblDistrict.Text = dtPayment.Rows[0]["DistrictName"].ToString();
                lblDate.Text = Convert.ToDateTime(dtPayment.Rows[0]["PaymentDate"].ToString()).ToString("dd/MM/yyyy");
                lblPAC.Text = "";
                lblSum.Text = dtPayment.Rows[0]["RenewalFeesPaymentAmount"].ToString();
                lblGST.Text = dtPayment.Rows[0]["RenewalFeesTaxPaymentAmount"].ToString();
                lblAdmissionFee.Text = dtPayment.Rows[0]["AdmissionFeesPaymentAmount"].ToString();
                lblAdmissionGST.Text = dtPayment.Rows[0]["AdmissionFeesTaxPaymentAmount"].ToString();
                lblDevelopmentFee.Text = dtPayment.Rows[0]["DevelopmentPaymentAmount"].ToString();
                lblTotal.Text = dtPayment.Rows[0]["PaymentAmount"].ToString();
                lblNarration.Text = dtPayment.Rows[0]["Narration"].ToString();
                lblChequeNo.Text = dtPayment.Rows[0]["PaymentMode"].ToString();
            }
        }
    }
}