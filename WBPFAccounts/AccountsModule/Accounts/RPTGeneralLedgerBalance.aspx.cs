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
    public partial class RPTGeneralLedgerBalance : System.Web.UI.Page
    {
        public string strFilter, strMessage;
        ListItem li = new ListItem("Select", "0");
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strPrepareRPTHeader = "";
        string strKarigarParams = "";
        string strParams = "";
        string strPrepareRPTFooter = "";

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
                txtFromDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                populateCombo();
            }
        }

        private void populateCombo()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";


            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += "";
            strKarigarParams = strParams + chr.ToString() + "LedgerType" + chr.ToString() + Session["DataFlow"].ToString();
            gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerALL", strKarigarParams);
            ddlLedger.Items.Insert(0, li);


        }

        protected void ddlBalanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlBalanceType.SelectedIndex == 0)
            //{
            //    string strValues = ddlFinancialYear.SelectedValue.ToString();
            //    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            //    txtToDate.Text = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
            //}
            //else
            //{
            //    string strValues = ddlFinancialYear.SelectedValue.ToString();
            //    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            //    txtToDate.Text = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
            //}

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            PopulateGrid("GROUPNAME", SortDirection.Ascending, strFilter);
        }
        private void PopulateGrid(string strSortExpression, SortDirection sortDir, string strFilter)
        {

            if (strFilter == "null" || strFilter == null)
                strFilter = "";
            string strValues = "";
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Convert.ToDateTime(txtFromDate.Text);
            strValues += chr.ToString() + Convert.ToDateTime(txtToDate.Text);
            strValues += chr.ToString() + ddlLedger.SelectedValue.ToString();
            strValues += chr.ToString() + Session["DataFlow"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_TrnLedgerTransactionDetails", strValues);
            //genObj.BindGridViewSP(gvGeneralLedger, "spSelect_TrnLedgerTransactionDetails", strValues);
            //txtNarration.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["Dr_Amount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Dr_Amount"]).ToString("0.00");
                ds.Tables[0].Rows[i]["Cr_Amount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Cr_Amount"]).ToString("0.00");
            }
            gvGeneralLedger.DataSource = ds.Tables[0].DefaultView;
            gvGeneralLedger.DataBind();

            strPrepareRPTHeader = @"<table border='0' width='100%' id='table1' cellspacing='1' cellpadding='2'>
                    <tr>         
	                    <td align='left' width='131'>From Date :</td>
	                    <td align='left'>" + txtFromDate.Text + @"</td>
                    </tr>
                     <tr>         
	                    <td align='left' width='131'>To Date :</td>
	                    <td align='left' >" + txtToDate.Text + @"</td>
                    </tr>
                 </table>
                ";
            strPrepareRPTFooter = "";
            if (gvGeneralLedger.DataSource != null)
            {
                Session[clsGlobalVariable.sesReportPageHeader] = strPrepareRPTHeader;
                Session[clsGlobalVariable.sesReportPageFooter] = strPrepareRPTFooter;
                Session[clsGlobalVariable.sesReportGrid] = gvGeneralLedger;
                Session[clsGlobalVariable.sesReportTitle] = "";
                btnPrint.Attributes["onclick"] = "window.open('RPTShowGrid.aspx');";
            }
        }
    }
}
