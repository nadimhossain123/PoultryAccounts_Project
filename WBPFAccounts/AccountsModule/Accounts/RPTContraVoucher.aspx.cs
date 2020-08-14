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
    public partial class RPTContraVoucher : System.Web.UI.Page
    {
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
                txtFromDate.Text = Session["SesFromDate"].ToString();
                txtToDate.Text = Session["SesToDate"].ToString();
                Message.Show = false;
                btnPrint.Visible = false;
                btnExportExcel.Visible = false;
                PopulateGrid();
            }
        }

        private void PopulateGrid()
        {
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();

            DataSet ds = gf.ExecuteSelectSP("spRPT_ContraVoucherDetails", strValues);
            if (ds.Tables[0] != null)
            {
                gvCVDetails.DataSource = ds.Tables[0];
                gvCVDetails.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (DateRangeValidation())
            {
                PopulateGrid();
                Message.Show = false;
            }
            else
                Message.Show = true;
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>West Bengal Poultry Federation</b>";
            _header[1] = "Contra Voucher From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];

            string file = "CV_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvCVDetails, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Contra Voucher Report";
            string[] _header = new string[3];
            _header[0] = "Contra Voucher From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvCVDetails, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}
