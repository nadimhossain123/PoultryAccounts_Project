using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class member_development_bill : System.Web.UI.Page
    {
        public int MemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }
        public int FinYrId
        {
            get { return Convert.ToInt32(Session["FinYrId"]); }
            set { Session["FinYrId"] = value; }
        }
        public int FromMonth
        {
            get { return Convert.ToInt32(ViewState["FromMonth"]); }
            set { ViewState["FromMonth"] = value; }
        }
        public int ToMonth
        {
            get { return Convert.ToInt32(ViewState["ToMonth"]); }
            set { ViewState["ToMonth"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["MemberId"] != null && Request.QueryString["FinYrId"].ToString().Trim().Length > 0)
                    && (Request.QueryString["FromMonth"] != null && Request.QueryString["ToMonth"].ToString().Trim().Length > 0))
                {
                    MemberId = Convert.ToInt32(Request.QueryString["MemberId"].ToString().Trim());
                    FromMonth = Convert.ToInt32(Request.QueryString["FromMonth"].ToString().Trim());
                    ToMonth = Convert.ToInt32(Request.QueryString["ToMonth"].ToString().Trim());
                    LoadBillDetails();
                }
            }
        }

        protected void LoadBillDetails()
        {
            BusinessLayer.Common.MemberFeesConfig objMemberBill = new BusinessLayer.Common.MemberFeesConfig();
            DataSet ds = objMemberBill.MemberDevelopmentFeeAllMonthGetAll(MemberId, FinYrId, FromMonth, ToMonth);
            DataTable dt = ds.Tables[0]; //Member Details
            DataTable dtBill = ds.Tables[1];//Bill Details
            decimal OpeningBalance = decimal.Parse(ds.Tables[2].Rows[0]["OpeningBalance"].ToString());
            lblOpeningBalance.Text = OpeningBalance.ToString();
            DataTable dtPaid = ds.Tables[3]; //Paid or Not

            // ** Populate Member Details
            lblMemberName.Text = (dt.Rows[0]["MemberName"] == DBNull.Value ? "" : dt.Rows[0]["MemberName"].ToString());
            lblAddress.Text = (dt.Rows[0]["VillageOrStreet"] == DBNull.Value ? "" : dt.Rows[0]["VillageOrStreet"].ToString().ToUpper()) + "</br>"
                + (dt.Rows[0]["DistrictName"] == DBNull.Value ? "" : dt.Rows[0]["DistrictName"].ToString().ToUpper()) + ", " + (dt.Rows[0]["PIN"] == DBNull.Value ? "" : dt.Rows[0]["PIN"].ToString().ToUpper());
            lblMemberCode.Text = dt.Rows[0]["MemberCode"] == DBNull.Value ? "" : dt.Rows[0]["MemberCode"].ToString().ToUpper();
            if (dtBill != null)
            {
                int count = dtBill.Rows.Count;
                lblBillNo.Text = ((dtBill.Rows[0]["BillNo"] == DBNull.Value) ? "" : dtBill.Rows[0]["BillNo"].ToString().ToUpper()) + " - " + ((dtBill.Rows[count - 1]["BillNo"] == DBNull.Value) ? "" : dtBill.Rows[count - 1]["BillNo"].ToString().ToUpper());
                lblBillDate.Text = DateTime.Parse(dtBill.Rows[0]["MonthlyRenewalDate"].ToString()).ToString("dd/MM/yyyy") + " - " + DateTime.Parse(dtBill.Rows[count - 1]["MonthlyRenewalDate"].ToString()).ToString("dd/MM/yyyy");

                //lblGSTNo.Text = dt.Rows[0]["GSTNo"] == DBNull.Value ? "" : dt.Rows[0]["GSTNo"].ToString().ToUpper();
                string MonthlyRate = "";
                //MonthlyRate=dt.Rows[0]["FinalAmount"].ToString().ToUpper();
                decimal total = 0;
                for (int k = 0; k < dtBill.Rows.Count; k++)
                {
                    lblMonth.Text += dtBill.Rows[k]["MonthName"].ToString().ToUpper() + ", " + dtBill.Rows[k]["YearNo"].ToString().ToUpper() + "</br></br>";
                    lblFinalAmount.Text += ((dtBill.Rows[k]["FinalAmount"] == DBNull.Value) ? "0.00" : dtBill.Rows[k]["FinalAmount"].ToString().ToUpper()) + "</br></br>";
                    total += (dtBill.Rows[k]["FinalAmount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dtBill.Rows[k]["FinalAmount"]);
                    MonthlyRate += "</br></br>";
                }
                decimal TotalAmount = (OpeningBalance + total);
                //if (dtPaid != null && dtPaid.Rows.Count > 0)  //from here
                //{
                //    MonthlyRate += "Development Fees Paid";
                //    for (int c = 0; c < dtPaid.Rows.Count; c++)
                //    {
                //        lblMonth.Text += "On " + DateTime.Parse(dtPaid.Rows[c]["PaymentDate"].ToString()).ToString("dd/MM/yyyy") + " (" + dtPaid.Rows[c]["PaymentNo"] + ")</br></br>";
                //        lblFinalAmount.Text += dtPaid.Rows[c]["PaymentAmount"].ToString() + "</br></br>";
                //        TotalAmount -= (dtPaid.Rows[c]["PaymentAmount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dtPaid.Rows[c]["PaymentAmount"]);
                //    }
                //}  //till here ,closed as per the request of Hironmoy
                lblMonthlyRate.Text += MonthlyRate; 
                lblTotal.Text = TotalAmount == 0 ? "0.00" : TotalAmount.ToString(".00");
                lblTotalInWords.Text = ConvertNumbertoWords(Convert.ToInt32(TotalAmount)) + " ONLY";
            }
        }

        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKH ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}