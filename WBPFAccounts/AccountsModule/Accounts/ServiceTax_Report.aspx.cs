using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;
using CollegeERP.Accounts;

namespace AccountsModule.Accounts
{
    public partial class ServiceTax_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFeesHead();
                LoadLedger();
                LoadCashBankLedger();
            }
        }

        protected void LoadFeesHead()
        {
            BusinessLayer.Accounts.StreamGroup ObjFees = new BusinessLayer.Accounts.StreamGroup();
            DataTable dt = ObjFees.GetAllFeesHead();

            ddlFeesHead.DataSource = dt;
            ddlFeesHead.DataTextField = "fees";
            ddlFeesHead.DataValueField = "id";
            ddlFeesHead.DataBind();
        }

        protected void LoadLedger()
        {
            clsGeneralFunctions gf = new clsGeneralFunctions();
            string strValues = "";
            char chr = Convert.ToChar(130);
            ListItem li = new ListItem("Select", "0");

            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["DataFlow"].ToString();
            gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerFull", strValues);
            ddlLedger.Items.Insert(0, li);
        }

        protected void LoadCashBankLedger()
        {
            clsGeneralFunctions gf = new clsGeneralFunctions();
            char chr = Convert.ToChar(130);
            ListItem li = new ListItem("Select", "0");

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
            BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
            Entity.Accounts.Tax tax = new Entity.Accounts.Tax();
            tax.FeesHeadId = int.Parse(ddlFeesHead.SelectedValue);
            tax.FromDate = DateTime.Parse(txtFromDate.Text);
            tax.ToDate = DateTime.Parse(txtToDate.Text);
            tax.CompanyId = int.Parse(Session["CompanyId"].ToString());

            DataTable dt = new DataTable();
            dt = objTax.Service_Tax_ByFeesHead_Report(tax);
            gvServiceTax.DataSource = dt;
            gvServiceTax.DataBind();
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            BusinessLayer.Accounts.Tax objTax = new BusinessLayer.Accounts.Tax();
            Entity.Accounts.Tax tax = new Entity.Accounts.Tax();
            tax.LedgerId = int.Parse(ddlLedger.SelectedValue);
            tax.ParentLedgerId = int.Parse(ddlCashBankLedger.SelectedValue);
            tax.FromDate = DateTime.Parse(txtFromDate2.Text);
            tax.ToDate = DateTime.Parse(txtToDate2.Text);
            tax.BranchId = int.Parse(Session["BranchID"].ToString());
            tax.CompanyId = int.Parse(Session["CompanyId"].ToString());

            DataTable dt = new DataTable();
            dt = objTax.Service_Tax_ByLedgerId_Report(tax);
            gvServiceTax2.DataSource = dt;
            gvServiceTax2.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Service Tax Report";
            string[] _header = new string[3];
            _header[0] = "For " + ddlFeesHead.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = "";
            _footer[2] = "";
            _footer[3] = "";
            _footer[4] = "";

            Print.ReportPrint(Title, _header, gvServiceTax, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>West Bengal Poultry Federation</b>";
            _header[1] = "For " + ddlFeesHead.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = "";
            _footer[2] = "";
            _footer[3] = "";
            _footer[4] = "";

            string file = "SERVICE_TAX_REPORT_"+DateTime.Today.ToString("dd-MM-yyyy");

            BusinessLayer.Common.Excel.SaveExcel(_header, gvServiceTax, _footer, file);
        }

        protected void btnPrint2_Click(object sender, EventArgs e)
        {
            string Title = "Service Tax Report";
            string[] _header = new string[3];
            _header[0] = "For " + ddlLedger.SelectedItem.Text + " From " + txtFromDate2.Text + " To " + txtToDate2.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = "";
            _footer[2] = "";
            _footer[3] = "";
            _footer[4] = "";

            Print.ReportPrint(Title, _header, gvServiceTax2, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnExport2_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>West Bengal Poultry Federation</b>";
            _header[1] = "For " + ddlLedger.SelectedItem.Text + " From " + txtFromDate2.Text + " To " + txtToDate2.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = "";
            _footer[2] = "";
            _footer[3] = "";
            _footer[4] = "";

            string file = "SERVICE_TAX_REPORT_" + DateTime.Today.ToString("dd-MM-yyyy");

            BusinessLayer.Common.Excel.SaveExcel(_header, gvServiceTax2, _footer, file);
        }
    }
}