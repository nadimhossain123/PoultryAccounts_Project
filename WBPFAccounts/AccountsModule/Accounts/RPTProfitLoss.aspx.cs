using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using BusinessLayer.Accounts;

namespace CollegeERP.Accounts
{
    public partial class RPTProfitLoss : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strValues = "";
        string strValues2 = "";

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
                ltrPLHeading.Text = @"<b>Income & Expenditure Statement</b><br /><br />" + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
                ltrBalanceSheetHeading.Text = @"<b>Balance Sheet</b><br /><br />" + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
                txtfrmdate.Text = Session["SesFromDate"].ToString();
                txtToDate.Text = Session["SesToDate"].ToString();
                PopulateGrid();
            }

        }

        private void PopulateGrid()
        {
            if (Session["CompanyID"].ToString() == "3")
                strValues = "" + chr.ToString();
            else
                strValues = Session["CompanyID"].ToString() + chr.ToString();

            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();

            strValues += txtfrmdate.Text.Trim() + " 00:00:00" + chr.ToString();
            strValues += txtToDate.Text.Trim() + " 23:59:59";


            //if (Session["CompanyID"].ToString() == "3")
            //    strValues2 = "" + chr.ToString();
            //else
            //    strValues2 = Session["CompanyID"].ToString() + chr.ToString();

            //strValues2 += Session["BranchID"].ToString() + chr.ToString();
            //strValues2 += Session["FinYrID"].ToString() + chr.ToString();

            //if (txtfrmdate.Text.Length > 0)
            //    strValues2 += txtfrmdate.Text.Trim() + " 00:00:00" + chr.ToString();
            //else
            //    strValues2 += txtfrmdate.Text.Trim() + chr.ToString();


            //if (txtToDate.Text.Length > 0)
            //    strValues2 += txtToDate.Text.Trim() + " 23:59:59";

            //else
            //    strValues2 += txtToDate.Text.Trim();

            genObj.BindGridViewSP(gvPL, "sp_ProfitLoss", strValues);
            genObj.BindGridViewSP(gvBalanceSheet, "sp_BalanceSheet", strValues);

            //ltrPLHeading.Text = @"<b>Income & Expenditure Statement</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
            //ltrBalanceSheetHeading.Text = @"<b>Balance Sheet</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
        }

        protected void btnPLExport_Click(object sender, EventArgs e)
        {

            string[] _header = new string[7];
            _header[0] = "<b>WEST BENGAL POULTRY FEDERATION</b>";
            _header[1] = "46/C Chowringee Road,Everest Building,";
            _header[2] = "11th Floor,Room No: C, Kol-71";
            _header[3] = "";
            _header[4] = "<b>Income & Expenditure Statement</b>";
            if (txtfrmdate.Text.Trim().Length > 0)
            {
                Session["SesFromDate"] = txtfrmdate.Text.Trim();
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                Session["SesToDate"] = txtToDate.Text.Trim();
            }
            _header[5] = Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            _header[6] = "";

            string[] _footer = new string[0];

            string file = "INCOME_EXPENDITURE_STATEMENT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvPL, _footer, file);
        }

        protected void btnPLPrint_Click(object sender, EventArgs e)
        {
            string Title = "Income & Expenditure Statement";
            string[] _header = new string[1];

            if (txtfrmdate.Text.Trim().Length > 0)
            {
                Session["SesFromDate"] = txtfrmdate.Text.Trim();
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                Session["SesToDate"] = txtToDate.Text.Trim();
            }
            _header[0] = "<center>" + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString() + "</center>";
            
            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvPL, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnBalanceSheetExport_Click(object sender, EventArgs e)
        {

            string[] _header = new string[7];
            _header[0] = "<b>WEST BENGAL POULTRY FEDERATION</b>";
            _header[1] = "46/C Chowringee Road,Everest Building,";
            _header[2] = "11th Floor,Room No: C, Kol-71";
            _header[3] = "";
            _header[4] = "<b>Balance Sheet</b>";

            if (txtfrmdate.Text.Trim().Length > 0)
            {
                Session["SesFromDate"] = txtfrmdate.Text.Trim();
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                Session["SesToDate"] = txtToDate.Text.Trim();
            }


            _header[5] = Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            _header[6] = "";

            string[] _footer = new string[0];

            string file = "BALANCE_SHEET_STATEMENT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvBalanceSheet, _footer, file);
        }

        protected void btnBalanceSheetPrint_Click(object sender, EventArgs e)
        {
            string Title = "Balance Sheet";
            string[] _header = new string[1];

            if (txtfrmdate.Text.Trim().Length > 0)
            {
                Session["SesFromDate"] = txtfrmdate.Text.Trim();
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                Session["SesToDate"] = txtToDate.Text.Trim();
            }
           

            _header[0] = "<center>" + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString() + "</center>";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvBalanceSheet, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateGrid();

            ltrPLHeading.Text = @"<b>Income & Expenditure Statement</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
            //ltrBalanceSheetHeading.Text = @"<b>Balance Sheet</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
        }
        protected void btnsearchblnc_Click(object sender, EventArgs e)
        {
            PopulateGrid();
            //ltrPLHeading.Text = @"<b>Income & Expenditure Statement</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
            ltrBalanceSheetHeading.Text = @"<b>Balance Sheet</b><br /><br />" + txtfrmdate.Text.Trim() + " to " + txtToDate.Text.Trim();
        }

        protected void gvBalanceSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow GVR in gvBalanceSheet.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    Label lblamount1 = (Label)GVR.FindControl("lblamount1");
                    if (lblamount1.Text == "0.00")
                    {
                        lblamount1.Text = "";
                    }

                    Label lblamnt2 = (Label)GVR.FindControl("lblamnt2");
                    if (lblamnt2.Text == "0.00")
                    {
                        lblamnt2.Text = "";
                    }
                }
            }
        }
        
    }
}
