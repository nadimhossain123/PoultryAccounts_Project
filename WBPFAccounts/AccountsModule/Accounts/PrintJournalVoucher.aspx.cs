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
    public partial class PrintJournalVoucher : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strValues = "";
        public decimal TotalDr = 0;
        public decimal TotalCr = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    int JVHeaderID = Convert.ToInt32(Request.QueryString["id"].Trim());
                    string CommanyId = Session["CompanyId"].ToString();

                    if (CommanyId == "2")
                        img.ImageUrl = "../Images/ReportHeader.png";
                    else if (CommanyId == "4")
                        img.ImageUrl = "../Images/DiplomaHeader.jpg";
                    else if (CommanyId == "1")
                        img.ImageUrl = "../Images/Management.png";

                    strValues = Session["CompanyID"].ToString();
                    strValues += chr.ToString() + Session["FinYrID"].ToString();
                    strValues += chr.ToString() + Session["BranchID"].ToString();
                    strValues += chr.ToString() + JVHeaderID.ToString();
                    DataSet ds = gf.ExecuteSelectSP("spPrint_TrnJournalVoucher", strValues);
                    LoadHeader(ds.Tables[0]);
                    LoadDetails(ds.Tables[1]);
                    LoadFooter(ds.Tables[0]);
                    //LoadStudent(ds.Tables[2]);
                    LoadSignature();
                }
            }
        }

        protected void LoadHeader(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table width='100%' align='left' cellpadding='0'>");
            sb.Append(@"<tr><td align='center' colspan='2'>Journal Voucher</td></tr>");

            //line gap
            sb.Append(@"<tr><td colspan='2'><br /></td></tr>");
            //header
            sb.Append(@"<tr>");
            sb.Append(@"<td align='left' width='50%'>Voucher No: " + DT.Rows[0]["JVoucherNo"].ToString() + "</td>");
            sb.Append(@"<td align='right' width='50%'>Date: " + Convert.ToDateTime(DT.Rows[0]["JVoucherDate"].ToString()).ToString("dd/MM/yyyy") + "</td>");
            sb.Append(@"</tr>");

            sb.Append(@"</table>");
            ltrHeader.Text = sb.ToString();

        }
        protected void LoadStudent(DataTable DT)
        {
            if (DT.Rows.Count>0)
            {
                ltrStudentName.Text = "Student Name: " + DT.Rows[0]["Student"].ToString();
            }
        }

        protected void LoadDetails(DataTable DT)
        {
            dgvReceipt.DataSource = DT;
            dgvReceipt.DataBind();

            if (DT.Rows.Count > 0)
            {
                TotalDr = 0;
                TotalCr = 0;

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    TotalDr += Convert.ToDecimal(DT.Rows[i]["DRAmount"].ToString());
                    TotalCr += Convert.ToDecimal(DT.Rows[i]["CRAmount"].ToString());
                }
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrTotalDr")).Text = TotalDr.ToString("n");
                ((Literal)dgvReceipt.FooterRow.FindControl("ltrTotalCr")).Text = TotalCr.ToString("n");
            }
        }

        protected void LoadFooter(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DT.Rows[0]["JVNarration"].ToString());
            ltrFooter.Text = sb.ToString();
            Session["CreatedBy"] = DT.Rows[0]["CreatedBy"].ToString();
        }

        protected void LoadSignature()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(Convert.ToInt32(Session["CreatedBy"]));

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
