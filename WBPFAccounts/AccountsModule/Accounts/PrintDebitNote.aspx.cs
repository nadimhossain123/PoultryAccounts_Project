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
using System.Text;
using BusinessLayer.Accounts;

namespace CollegeERP.Accounts
{
    public partial class PrintDebitNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsGeneralFunctions gf = new clsGeneralFunctions();
            char chr = Convert.ToChar(130);
            string strValues = "";

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    BusinessLayer.Accounts.TrnDebitNoteHeader ObjTrnDebitNoteHeader = new TrnDebitNoteHeader();
                    int intID = Convert.ToInt32(Request.QueryString["id"].Trim());
                    strValues = Session["CompanyID"].ToString();
                    strValues += chr.ToString() + intID.ToString();
                    DataTable DT = ObjTrnDebitNoteHeader.GetAll(intID);
                    dgvReceipt.DataSource = DT;
                    dgvReceipt.DataBind();
                    string CommanyId = Session["CompanyId"].ToString();
                    //LoadDetails(ds1.Tables[0]);
                    //LoadFooterRow(ds.Tables[0]);
                    LoadFooterRow(DT);
                    LoadHeader(DT);
                    LoadSignature();

                    if (CommanyId == "2")
                        img.ImageUrl = "../Images/ReportHeader.png";
                    else if (CommanyId == "4")
                        img.ImageUrl = "../Images/DiplomaHeader.jpg";
                    else if (CommanyId == "1")
                        img.ImageUrl = "../Images/Management.png";
                }
            }
        }

        protected void LoadFooterRow(DataTable DT)
        {
            //string TransactionType = DT.Rows[0]["TransactionType"].ToString();
            //((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBook")).Text = DT.Rows[0]["LedgerName"].ToString();
            //if (TransactionType == "RECEIVE")
            //{
            decimal crTotal = 0;
            decimal drTotal = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                drTotal += Convert.ToDecimal(DT.Rows[i]["DEBIT"].ToString());
                crTotal += Convert.ToDecimal(DT.Rows[i]["CREDIT"].ToString());
            }

            ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookDr")).Text = drTotal.ToString("n");
            ((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookCr")).Text = crTotal.ToString("n");
            //}
            //else
            //{
                //((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookCr")).Text = Convert.ToDecimal(DT.Rows[0]["CREDIT"].ToString()).ToString("n");
                //((Literal)dgvReceipt.FooterRow.FindControl("ltrCashBankBookDr")).Text = "0.00";
            //}

            Session["CreatedByCBV"] = DT.Rows[0]["CreatedBy"].ToString();
        }

        protected void LoadHeader(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            //string TransactionType = DT.Rows[0]["TransactionType"].ToString();

            sb.Append(@"<table width='100%' align='left' cellpadding='0'>");
            sb.Append(@"<tr>");
            //if (TransactionType == "RECEIVE")
            //    sb.Append(@"<td align='center' colspan='2'>Debit Note</td>");
            //else
            //    sb.Append(@"<td align='center' colspan='2'>Debit Note</td>");

            sb.Append(@"</tr>");

            //line gap
            sb.Append(@"<tr><td colspan='2'><br /></td></tr>");
            //header
            sb.Append(@"<tr>");
            sb.Append(@"<td align='left' width='50%'>Voucher No: " + DT.Rows[0]["DNVoucherNo"].ToString() + "</td>");
            sb.Append(@"<td align='right' width='50%'>Date: " + Convert.ToDateTime(DT.Rows[0]["DNVoucherDate"].ToString()).ToString("dd/MM/yyyy") + "</td>");
            sb.Append(@"</tr>");

            //pay to
            //if (TransactionType == "RECEIVE")
            //    sb.Append(@"<tr><td align='left' colspan='2'>Received By: " + DT.Rows[0]["PayTo"].ToString() + "</td></tr>");
            //else
            //    sb.Append(@"<tr><td align='left' colspan='2'>Pay To: " + DT.Rows[0]["PayTo"].ToString() + "</td></tr>");

            sb.Append(@"</table>");
            ltrHeader.Text = sb.ToString();
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

        protected void dgvReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
    }
}
