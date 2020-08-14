using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinessLayer.Accounts;
using DataAccess.Accounts;

namespace CollegeERP.Accounts
{
    public partial class CreatefinYr : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsConnection objConn = new clsConnection();
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["ID"] = 0;
                genObj.BindGridViewSP(gdFinancialYr, "spSelect_MstFinancialYear_New", "");
                Message.Show = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strSpParams = "";
            string rtMsg = "";
            string strMessage = "";
            
            string strStartYr = txtStartYr.Text.Trim();
            string strEndYr = txtEndYr.Text.Trim();
            string strActive = "";
            if (chkActive.Checked)
                strActive = "true";
            else
                strActive = "false";
            strSpParams = strStartYr + chr.ToString() + strEndYr + chr.ToString() + strActive;
            strSpParams += chr.ToString() + Session["UserId"].ToString() + chr.ToString() + "";
            try
            {
                rtMsg = genObj.ExecuteAnySPOutput("spInsert_MstFinancialYear", strSpParams);
                if (rtMsg == "True")
                {
                    Message.IsSuccess = true;
                    Message.Text = "Your request has been processed successfully!";
                }
                else if (rtMsg == "Duplicate")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Financial Year already exist!";
                }

                genObj.BindGridViewSP(gdFinancialYr, "spSelect_MstFinancialYear_New", "");
            }
            catch (Exception exp)
            {
                objConn.closeConnection();
                Message.IsSuccess = false;
                Message.Text = "An Unhandled Exception Occured!";
            }
            Message.Show = true;
        }
    }
}
