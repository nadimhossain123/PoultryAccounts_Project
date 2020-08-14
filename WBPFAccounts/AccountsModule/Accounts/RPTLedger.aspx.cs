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
    public partial class RPTLedger : System.Web.UI.Page
    {
        ListItem li = new ListItem("Select", "0");
        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strValues = "";
        char chr = Convert.ToChar(130);

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
                populateCombo();
                LoadCashBankLedger();
                ResetControls();
            }
        }

        protected void ResetControls()
        {
            Message.Show = false;
            btnExportExcel.Visible = false;
            btnPrint.Visible = false;
            txtFromDate.Text = Session["SesFromDate"].ToString();
            txtToDate.Text = Session["SesToDate"].ToString();

        }

        private void populateCombo()
        {
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["DataFlow"].ToString();
            gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerFull", strValues);
            ddlLedger.Items.Insert(0, li);
        }

        protected void LoadCashBankLedger()
        {
            string strParams = "";
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            DataTable dt = gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerBNKandCASH", strParams);

            if (dt != null)
            {
                ddlCashBankLedger.DataSource = dt;
                ddlCashBankLedger.DataBind();
            }
            ddlCashBankLedger.Items.Insert(0, li);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlLedger.SelectedValue == "0" || ddlLedger.Text == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Ledger";
                Message.Show = true;
            }
            else if (!DateRangeValidation())
            {
                Message.Show = true;
            }
            else
            {
                PopulateGrid();
                if (gvGeneralLedger.Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                    btnPrint.Visible = true;
                }
                Message.Show = false;
            }
        }

        protected bool DateRangeValidation()
        {
            bool result = true;
            DateTime minValue = Convert.ToDateTime(Session["SesFromDate"].ToString());
            DateTime maxValue = Convert.ToDateTime(Session["SesToDate"].ToString());

            if (Convert.ToDateTime(txtFromDate.Text) < minValue || Convert.ToDateTime(txtFromDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select From Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            else if (Convert.ToDateTime(txtToDate.Text) < minValue || Convert.ToDateTime(txtToDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select To Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            return result;
        }

        private void PopulateGrid()
        {

            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += ddlLedger.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim() + chr.ToString();
            strValues += (ddlCashBankLedger.SelectedIndex == 0) ? "0" : ddlCashBankLedger.SelectedValue;

            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnLedger", strValues);
            if (ds.Tables[0] != null)
            {
                gvGeneralLedger.DataSource = ds.Tables[0];
                gvGeneralLedger.DataBind();
            }

            lblOpeningBal.Text = "Opening balance  :  " + ds.Tables[1].Rows[0][0].ToString();
            lblTotalDr.Text = "Total Debit between Dates  :  " + ds.Tables[2].Rows[0][0].ToString();
            lblTotalCr.Text = "Total Credit between Dates  :  " + ds.Tables[3].Rows[0][0].ToString();
            lblClosingBal.Text = "Closing balance  :  " + ds.Tables[4].Rows[0][0].ToString();


        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>WBPF</b>";
            _header[1] = "For " + ddlLedger.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = lblOpeningBal.Text;
            _footer[2] = lblTotalDr.Text;
            _footer[3] = lblTotalCr.Text;
            _footer[4] = lblClosingBal.Text;

            string file = "LEDGER_BALANCE_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvGeneralLedger, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Ledger Balance Report";
            string[] _header = new string[3];
            _header[0] = "For " + ddlLedger.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = lblOpeningBal.Text;
            _footer[2] = lblTotalDr.Text;
            _footer[3] = lblTotalCr.Text;
            _footer[4] = lblClosingBal.Text;

            Print.ReportPrint(Title, _header, gvGeneralLedger, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}
