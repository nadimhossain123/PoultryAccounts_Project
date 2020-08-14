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
using System.Drawing;
using System.Text;

namespace CollegeERP.Accounts
{
    public partial class RPTShowGrid : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //if (int.Parse(Session["CompanyId"].ToString()) == 2)
                    //{
                    //    imgHeader.ImageUrl = "~/Images/ReportHeader.png";
                    //}
                    //else if (int.Parse(Session["CompanyId"].ToString()) == 4)
                    //{
                    //    imgHeader.ImageUrl = "~/Images/DiplomaHeader.JPG";
                    //}
                    //else
                    //{
                    //    imgHeader.ImageUrl = "~/Images/Management.png";
                    //}
                    
                    
                    lblReportHeader.Text = Session[clsGlobalVariable.sesReportTitle].ToString();

                    if (Session[clsGlobalVariable.sesReportPageHeader] != null || Session[clsGlobalVariable.sesReportPageHeader].ToString() != "")
                        PlaceHolder2.Controls.Add(new LiteralControl(Session[clsGlobalVariable.sesReportPageHeader].ToString()));
                    if (Session[clsGlobalVariable.sesReportPageFooter] != null || Session[clsGlobalVariable.sesReportPageFooter].ToString() != "")
                        PlaceHolder3.Controls.Add(new LiteralControl(Session[clsGlobalVariable.sesReportPageFooter].ToString()));

                    if (Session[clsGlobalVariable.sesReportGrid] != null)
                    {
                        GridView gv = (GridView)Session[clsGlobalVariable.sesReportGrid];
                        if (gv != null)
                        {
                            PlaceHolder1.Controls.Add(gv);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //
                }
            }
            
        }
    }
}
