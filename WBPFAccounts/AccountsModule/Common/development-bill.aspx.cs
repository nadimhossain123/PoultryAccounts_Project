using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class development_bill : System.Web.UI.Page
    {
        public int BillId
        {
            get { return Convert.ToInt32(ViewState["BillId"]); }
            set { ViewState["BillId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BillId"] != null && Request.QueryString["BillId"].ToString().Trim().Length > 0)
                {
                    BillId = Convert.ToInt32(Request.QueryString["BillId"].ToString().Trim());
                    LoadBillDetails();
                }
            }

        }

        protected void LoadBillDetails()
        {
            BusinessLayer.Common.MemberBill objMemberBill = new BusinessLayer.Common.MemberBill();
            DataTable dt = objMemberBill.MemberMonthlyDevelopmentBill(0, 0, 0, 0, 0, 0, "", BillId);
            if (dt != null)
            {
                lblBillNo.Text = dt.Rows[0]["BillNo"].ToString().ToUpper();
                lblBillDate.Text = "01/" + dt.Rows[0]["MonthNo"].ToString().ToUpper() + "/" + dt.Rows[0]["Year"].ToString().ToUpper();//DateTime.Now.Date.ToString("dd/MM/yyyy");
                lblMemberName.Text = dt.Rows[0]["MemberName"].ToString();
                lblAddress.Text = dt.Rows[0]["VillageOrStreet"].ToString().ToUpper() + "</br>" + dt.Rows[0]["DistrictName"].ToString().ToUpper() + ", " + dt.Rows[0]["PIN"].ToString().ToUpper();
                lblMemberCode.Text = dt.Rows[0]["MemberCode"].ToString().ToUpper();
                lblFinalAmount.Text = dt.Rows[0]["FinalAmount"].ToString().ToUpper();
                lblMonthlyRate.Text = dt.Rows[0]["FinalAmount"].ToString().ToUpper();
                //lblServiceTax.Text = "N/A";//(Convert.ToDecimal(dt.Rows[0]["FinalAmount"]) * 18 / 100).ToString(".00");
                lblTotal.Text = dt.Rows[0]["FinalAmount"].ToString().ToUpper();
                lblMonth.Text = dt.Rows[0]["Month"].ToString().ToUpper() + ", " + dt.Rows[0]["Year"].ToString().ToUpper();
                lblTotalInWords.Text = dt.Rows[0]["FinalAmountInWords"].ToString().ToUpper();
            }
        }
    }
}