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
    public partial class RPTCostCenterWiseReport : System.Web.UI.Page
    {
        decimal grdDrTotal = 0;
        decimal grdCrTotal = 0;
        ListItem li = new ListItem("--Select--", "0");
        clsGeneralFunctions gf = new clsGeneralFunctions();
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
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                LoadCostCenter();
                dgvReport.DataSource = null;
                dgvReport.DataBind();
                btnPrint.Visible = false;
                btnExportExcel.Visible = false;
            }
        }

        private void LoadCostCenter()
        {
            string strParams = Session["CompanyId"].ToString().Trim() + chr.ToString();
            strParams += "";
            gf.BindDropDownColumnsBySP(ddlCostCenter, "spSelect_MstCostCenter", strParams);
            ddlCostCenter.Items.Insert(0, li);
        }

        private void LoadReport()
        {

            BusinessLayer.Accounts.CostCentreWiseReport ObjCostCentreWiseReport=new CostCentreWiseReport();
            DataTable DT = ObjCostCentreWiseReport.GetAll(Convert.ToInt32(ddlCostCenter.SelectedValue),Convert.ToInt32(Session["CompanyId"]),txtFromDate.Text.Trim(),txtToDate.Text.Trim());

            dgvReport.DataSource = DT;
            dgvReport.DataBind();

            if (DT.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExportExcel.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnExportExcel.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Cost Center Wise Report";
            string[] _header = new string[2];
            _header[0] = "Cost Center Name: " + ddlCostCenter.SelectedItem.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvReport, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "Cost Center Name: " + ddlCostCenter.SelectedItem.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            string file = "COST_CENTER_WISE_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

        protected void dgvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowDrTotal = Convert.ToDecimal
                    (DataBinder.Eval(e.Row.DataItem, "DRAmount"));
                grdDrTotal = grdDrTotal + rowDrTotal;

                decimal rowCrTotal = Convert.ToDecimal
                    (DataBinder.Eval(e.Row.DataItem, "CRAmount"));
                grdCrTotal = grdCrTotal + rowCrTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDr = (Label)e.Row.FindControl("lblDrTotal");
                lblDr.Text = grdDrTotal.ToString();

                Label lblCr = (Label)e.Row.FindControl("lblCrTotal");
                lblCr.Text = grdCrTotal.ToString();
            }
        }
    }
}
