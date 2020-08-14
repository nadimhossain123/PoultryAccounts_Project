using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessLayer.Accounts;
using System.Text;

namespace CollegeERP.Accounts
{
    public partial class RPTGridView : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strValues = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    int intID = Convert.ToInt32(Request.QueryString["id"].Trim());
                    strValues = Session["CompanyID"].ToString();
                    strValues += chr.ToString() + Session["FinYrID"].ToString();
                    strValues += chr.ToString() + Session["BranchID"].ToString();
                    strValues += chr.ToString() + Session["DataFlow"].ToString();
                    strValues += chr.ToString() + intID.ToString();
                    DataSet ds = gf.ExecuteSelectSP("spSelect_TrnCashBankVoucher", strValues);
                    LoadHeader(ds.Tables[0]);

                    DataSet ds1 = gf.ExecuteSelectSP("spSelect_TrnCashBankVoucherDetail", intID.ToString() + chr.ToString() + "");
                    LoadDetails(ds1.Tables[0]);
                    LoadFooterRow(ds.Tables[0]);

                    LoadFooter(ds.Tables[0]);
                    LoadSignature();
                }
            }
        }

        protected void LoadHeader(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            string TransactionType = DT.Rows[0]["TransactionType"].ToString();

            sb.Append(@"<table width='100%' align='left' cellpadding='0'>");
            sb.Append(@"<tr>");
            if (TransactionType == "RECEIVE")
                sb.Append(@"<td align='center' colspan='2'>Cash / Bank Receive Voucher</td>");
            else
                sb.Append(@"<td align='center' colspan='2'>Cash / Bank Payment Voucher</td>");

            sb.Append(@"</tr>");

            //line gap
            sb.Append(@"<tr><td colspan='2'><br /></td></tr>");
            //header
            sb.Append(@"<tr>");
            sb.Append(@"<td align='left' width='50%'>Voucher No: " + DT.Rows[0]["CBVoucherNo"].ToString() + "</td>");
            sb.Append(@"<td align='right' width='50%'>Date: " + Convert.ToDateTime(DT.Rows[0]["VoucherDate"].ToString()).ToString("dd/MM/yyyy") + "</td>");
            sb.Append(@"</tr>");

            //pay to
            if (TransactionType == "RECEIVE")
                sb.Append(@"<tr><td align='left' colspan='2'>Received By: " + DT.Rows[0]["PayTo"].ToString() + "</td></tr>");
            else
                sb.Append(@"<tr><td align='left' colspan='2'>Pay To: " + DT.Rows[0]["PayTo"].ToString() + "</td></tr>");

            sb.Append(@"</table>");
            ltrHeader.Text = sb.ToString();
        }


        protected void LoadDetails(DataTable DT)
        {
            dgvReceipt.DataSource = DT;
            dgvReceipt.DataBind();
        }

        protected void LoadFooterRow(DataTable DT)
        {
            string TransactionType = DT.Rows[0]["TransactionType"].ToString();
            ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBook")).Text = DT.Rows[0]["LedgerName"].ToString();
            if (TransactionType == "RECEIVE")
            {
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookDr")).Text = Convert.ToDecimal(DT.Rows[0]["TotalAmount"].ToString()).ToString("n");
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookCr")).Text = "0.00";
            }
            else
            {
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookCr")).Text = Convert.ToDecimal(DT.Rows[0]["TotalAmount"].ToString()).ToString("n");
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookDr")).Text = "0.00";
            }
            Session["CreatedByCBV"] = DT.Rows[0]["CreatedBy"].ToString();
        }

        protected void LoadFooter(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            string ModeOfPayment = DT.Rows[0]["ModeOfPayment"].ToString();

            sb.Append(DT.Rows[0]["AmountInWord"].ToString() + "<br />");
            if (ModeOfPayment == "CASH")
            {
                sb.Append(@"Cash<br />");
            }
            else
            {
                sb.Append(@"Cheque No: " + DT.Rows[0]["ChequeNo"].ToString());
                sb.Append(@"  Date: " + Convert.ToDateTime(DT.Rows[0]["ChequeDate"].ToString()).ToString("dd/MM/yyyy"));
                sb.Append(@"  Bank: " + DT.Rows[0]["DrawnOn"].ToString() + "<br />");
            }
            sb.Append(DT.Rows[0]["CBVNarration"].ToString());
            ltrFooter.Text = sb.ToString();
        }

        protected void LoadSignature()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(Convert.ToInt32(Session["CreatedByCBV"]));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table width='100%' align='center'>");
            sb.Append(@"<tr><td width='35%' align='center' style='border-top: dotted 1px #000'>");
            sb.Append(@"Prepared By  " + Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName);

            sb.Append(@"</td><td width='5%'></td><td width='20%' align='center' style='border-top: dotted 1px #000'>Approved By</td>");
            sb.Append(@"<td width='5%'></td><td width='35%' align='center' style='border-top: dotted 1px #000'>Received By</td></tr>");
            sb.Append(@"</table>");
            ltrSignature.Text = sb.ToString();
        }
    }
}
