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
    public partial class PrintContraVoucher : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strValues = "";
        public decimal TotalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    int CVHeaderID = Convert.ToInt32(Request.QueryString["id"].Trim());
                    strValues = Session["CompanyID"].ToString();
                    strValues += chr.ToString() + Session["FinYrID"].ToString();
                    strValues += chr.ToString() + Session["BranchID"].ToString();
                    strValues += chr.ToString() + CVHeaderID.ToString();
                    DataSet ds = gf.ExecuteSelectSP("spPrint_TrnContraVoucher", strValues);
                    LoadHeader(ds.Tables[0]);
                    LoadDetails(ds.Tables[1]);
                    LoadFooter(ds.Tables[0]);
                    LoadSignature();
                    string CompanyId = Session["CompanyId"].ToString();

                    if (CompanyId == "2")
                        img.ImageUrl = "../Images/ReportHeader.png";
                    else if (CompanyId == "4")
                        img.ImageUrl = "../Images/DiplomaHeader.jpg";
                    else if (CompanyId == "1")
                        img.ImageUrl = "../Images/Management.png";
                }
            }
        }

        protected void LoadHeader(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table width='100%' align='left' cellpadding='0'>");
            sb.Append(@"<tr><td align='center' colspan='2'>Contra Voucher</td></tr>");

            //line gap
            sb.Append(@"<tr><td colspan='2'><br /></td></tr>");
            //header
            sb.Append(@"<tr>");
            sb.Append(@"<td align='left' width='50%'>Voucher No: " + DT.Rows[0]["CVoucherNo"].ToString() + "</td>");
            sb.Append(@"<td align='right' width='50%'>Date: " + Convert.ToDateTime(DT.Rows[0]["CVoucherDate"].ToString()).ToString("dd/MM/yyyy") + "</td>");
            sb.Append(@"</tr>");

            sb.Append(@"</table>");
            ltrHeader.Text = sb.ToString();

            TotalAmount = Convert.ToDecimal(DT.Rows[0]["TotalAmount"].ToString());
        }

        protected void LoadDetails(DataTable DT)
        {
            dgvReceipt.DataSource = DT;
            dgvReceipt.DataBind();

            ((Literal)dgvReceipt.FooterRow.FindControl("ltrTotalDr")).Text = TotalAmount.ToString("n");
            ((Literal)dgvReceipt.FooterRow.FindControl("ltrTotalCr")).Text = TotalAmount.ToString("n");
        }

        protected void LoadFooter(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            string ModeOfTransaction = DT.Rows[0]["ModeOfTransaction"].ToString();

            sb.Append(DT.Rows[0]["AmountInWord"].ToString() + "<br />");
            if (ModeOfTransaction == "CASH")
            {
                sb.Append(@"Cash<br />");
            }
            else
            {
                sb.Append(@"Cheque No: " + DT.Rows[0]["ChequeNo"].ToString());
                sb.Append(@"  Date: " + Convert.ToDateTime(DT.Rows[0]["ChequeDate"].ToString()).ToString("dd/MM/yyyy"));
                sb.Append(@"  Bank: " + DT.Rows[0]["DrawnOn"].ToString() + "<br />");
            }
            sb.Append(DT.Rows[0]["CVNarration"].ToString());
            ltrFooter.Text = sb.ToString();
            Session["CreatedByCTV"] = DT.Rows[0]["CreatedBy"].ToString();
        }

        protected void LoadSignature()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(Convert.ToInt32(Session["CreatedByCTV"]));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table width='100%' align='center'>");
            sb.Append(@"<tr><td width='35%' align='center' style='border-top: dotted 1px #000'>");
            sb.Append(@"Prepared By  " + Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName);

            sb.Append(@"</td><td width='5%'></td><td width='20%'align='center' style='border-top: dotted 1px #000'>Approved By</td>");
            sb.Append(@"<td width='5%'></td><td width='35%' align='center' style='border-top: dotted 1px #000'>Received By</td></tr>");
            sb.Append(@"</table>");
            ltrSignature.Text = sb.ToString();
        }
    }
}
