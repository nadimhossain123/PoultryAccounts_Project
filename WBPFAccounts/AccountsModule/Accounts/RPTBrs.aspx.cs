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

namespace CollegeERP.Accounts
{
    public partial class RPTBrs : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        DataSet ds;
        string strParams;
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                PopulateAllDropDowns();
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                btnExportToExcel.Visible = false;
                btnPrint.Visible = false;
            }
        }

        private void PopulateAllDropDowns()
        {
            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('BNK')";

            if (dv != null)
            {
                ddlBRLedgName.DataSource = dv;
                ddlBRLedgName.DataBind();
            }

            ddlBRLedgName.Items.Insert(0, li);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += ddlBRLedgName.SelectedValue.Trim() + chr.ToString();
            strParams += txtToDate.Text.Trim();
            ds = gf.ExecuteSelectSP("spRPTSelect_TrnBankReconsiliation", strParams);

            gvBnkReconsilition.DataSource = ds.Tables[0];
            gvBnkReconsilition.DataBind();

            if (gvBnkReconsilition.DataSource != null)
            {
                if (gvBnkReconsilition.Rows.Count > 0)
                {
                    btnExportToExcel.Visible = true;
                    btnPrint.Visible = true;
                }
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "BRS Statement for " + ddlBRLedgName.SelectedItem.Text.ToUpper() + " as on date " + txtToDate.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            string file = "BRS_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvBnkReconsilition, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Bank Reconsilation Report";
            string[] _header = new string[2];
            _header[0] = "BRS Statement for " + ddlBRLedgName.SelectedItem.Text.ToUpper() + " as on date " + txtToDate.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvBnkReconsilition, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}
