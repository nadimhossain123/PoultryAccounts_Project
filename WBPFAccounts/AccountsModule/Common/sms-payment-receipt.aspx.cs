using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class sms_payment_receipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["No"] != null && Request.QueryString["No"].Trim().Length > 0)
                {
                    string VoucherNo = Request.QueryString["No"].ToString();
                    LoadReceipt(VoucherNo);
                }
            }
        }

        protected void LoadReceipt(string VoucherNo)
        {
            BusinessLayer.Common.SMSPayment objSMSPayment = new BusinessLayer.Common.SMSPayment();
            DataTable dt = objSMSPayment.GetAll(VoucherNo, 0, "All", DateTime.MinValue, DateTime.MinValue, 0);

            if (dt != null)
            {
                lblSLNo.Text = VoucherNo.ToString().Split('/').Last();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]).ToString("dd/MM/yyyy");
                lblMemberName.Text = dt.Rows[0]["MemberName"].ToString() + " / " + dt.Rows[0]["MobileNo"].ToString();
                lblMessageRate.Text = dt.Rows[0]["ReadyBirdPriceSMSAmount"].ToString();
                lblMessageTax.Text = dt.Rows[0]["ReadyBirdPriceSMSTaxAmount"].ToString();
                lblMessageTotal.Text = Convert.ToDecimal(dt.Rows[0]["PaymentAmount"]).ToString();
                lblPaymentMode.Text = dt.Rows[0]["PaymentMode"].ToString();
            }
        }
    }
}