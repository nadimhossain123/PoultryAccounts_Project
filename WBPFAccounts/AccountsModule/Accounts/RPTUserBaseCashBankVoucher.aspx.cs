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
    public partial class RPTUserBaseCashBankVoucher : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strValues = "";
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");
        decimal DrTotalAmt = 0;
        decimal CrTotalAmt = 0;
        decimal DrTotalAmtBnk = 0;
        decimal CrTotalAmtBnk = 0;

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
                LoadUser();
                txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");// Session["SesFromDate"].ToString();
                txtToDate.Text =  DateTime.Now.ToString("dd MMM yyyy");//Session["SesToDate"].ToString();
                Message.Show = false;
                //CheckRole();
            }
           
        }

        protected void CheckRole()
        {
            DataSet dsCnt = gf.ExecuteSelectSP("usp_getUserRole", HttpContext.Current.User.Identity.Name);
            if (dsCnt.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.Enabled = true;
            }
            else 
            {
                ddlEmployee.SelectedValue = HttpContext.Current.User.Identity.Name;
                ddlEmployee.Enabled = false;
                
            }

        }

        protected void LoadUser()
        {
            BusinessLayer.Common.Employee objEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(objEmployee.GetAll("", ""));
            dv.RowFilter = "CompanyId=" + Session["CompanyID"].ToString() ;
            dv.Sort = "FullName";

            if (dv != null)
            {
                ddlEmployee.DataSource = dv;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, li);
        }

        protected void PopulateGrid()
        {
            strValues = ddlEmployee.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();

            DataSet ds = gf.ExecuteSelectSP("spRPT_UserBaseCashBankVoucherDetails", strValues);
            if (ds.Tables[0] != null)
            {
                gvCBVDetails.DataSource = ds.Tables[0];
                gvCBVDetails.DataBind();
            }
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
            _header[1] = "Receipt/Payment Voucher From " + txtFromDate.Text + " To " + txtToDate.Text + " by " + ddlEmployee.SelectedItem.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];

            string file = "CBV_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvCBVDetails, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "User Base Receipt/Payment Report";
            string[] _header = new string[2];
            _header[0] = "Receipt/Payment Voucher From " + txtFromDate.Text + " To " + txtToDate.Text + " by " + ddlEmployee.SelectedItem.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvCBVDetails, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void gvCBVDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCBVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DrTotalAmt += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DrAmount"));
                CrTotalAmt += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CrAmount"));
                DrTotalAmtBnk += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DRAmountBnk"));
                CrTotalAmtBnk += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CRAmountBnk"));
                 
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDrCashtotal = (Label)e.Row.FindControl("lblDrCashtotal");
                lblDrCashtotal.Text = DrTotalAmt.ToString();
                Label lblCrCashtotal = (Label)e.Row.FindControl("lblCrCashtotal");
                lblCrCashtotal.Text = CrTotalAmt.ToString();
                Label lblDrbanktotal = (Label)e.Row.FindControl("lblDrbanktotal");
                lblDrbanktotal.Text = DrTotalAmtBnk.ToString();
                Label lblCrbanktotal = (Label)e.Row.FindControl("lblCrbanktotal");
                lblCrbanktotal.Text = CrTotalAmtBnk.ToString();
            }
        }
    }
}
