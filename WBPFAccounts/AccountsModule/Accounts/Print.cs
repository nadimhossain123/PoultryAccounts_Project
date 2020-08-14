using System;
using System.Data;
using System.Configuration;
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
    public static class Print
    {
        public static void ReportPrint(string Title, string[] header, System.Web.UI.WebControls.GridView DataGridView, string[] footer)
        {
            StringBuilder sb = new StringBuilder();
            if (header.Length > 0)
            {
                sb.Append(@"<table>");
                for (int i = 0; i < header.Length; i++)
                {
                    sb.Append(@"<tr><td>" + header[i].Trim() + "</td></tr>");
                }
                sb.Append(@"</table>");
            }
            System.Web.HttpContext.Current.Session[clsGlobalVariable.sesReportPageHeader] = sb.ToString();
            System.Web.HttpContext.Current.Session[clsGlobalVariable.sesReportGrid] = DataGridView;

            sb = new StringBuilder();
            if (footer.Length > 0)
            {
                sb.Append(@"<table>");
                for (int i = 0; i < footer.Length; i++)
                {
                    sb.Append(@"<tr><td>" + footer[i].Trim() + "</td></tr>");
                }
                sb.Append(@"</table>");
            }
            System.Web.HttpContext.Current.Session[clsGlobalVariable.sesReportPageFooter] = sb.ToString();
            System.Web.HttpContext.Current.Session[clsGlobalVariable.sesReportTitle] = Title;
            //HttpContext.Current.Response.Redirect("RPTShowGrid.aspx");

        }
    }
}
