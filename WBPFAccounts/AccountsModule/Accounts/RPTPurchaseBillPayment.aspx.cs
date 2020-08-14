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
    public partial class RPTPurchaseBillPayment : System.Web.UI.Page
    {
        char chr = Convert.ToChar(130);
        string strParams = "";
        clsGeneralFunctions gf = new clsGeneralFunctions();
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
                LoadSupplier();
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                //btnSearch_Click(sender, e);
            }
        }

        protected void LoadSupplier()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('SUP')";

            if (dv != null)
            {
                ddlSupplier.DataSource = dv;
                ddlSupplier.DataBind();
            }
            ddlSupplier.Items.Insert(0, li);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
            DateTime? Fromdate;
            DateTime? ToDate;
            if (txtFromDate.Text.Trim().Length == 0)
            {
                Fromdate = null;
            }
            else { Fromdate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00"); }

            if (txtToDate.Text.Trim().Length == 0)
            {
                ToDate = null;
            }
            else { ToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59"); }

            BusinessLayer.Accounts.PurchaseBillPayment ObjPayment = new BusinessLayer.Accounts.PurchaseBillPayment();
            //DataView dv = new DataView(ObjPayment.GetAll(SupplierId, Fromdate, ToDate));
            DataTable dt = ObjPayment.GetAll(SupplierId, Fromdate, ToDate);
            //dv.RowFilter = "CompanyID_FK=" + Convert.ToInt32(Session["CompanyId"].ToString());

            dgvPayment.DataSource = dt;
            dgvPayment.DataBind();
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Bill Payment Report";
            string[] _header = new string[2];
            _header[0] = "From: " + txtFromDate.Text;
            _header[1] = "To: " + txtToDate.Text;

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvPayment, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
       
    }
}
