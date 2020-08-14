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
    public partial class RPTGeneralLedger : System.Web.UI.Page
    {
        public string strFilter;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        string strPrepareRPTHeader = "";
        string strPrepareRPTFooter = "";

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
                char chr = Convert.ToChar(130);
                ViewState["SortOrder"] = " ASC";
                string strValues = "";
                ViewState["ID"] = 0;
                PopulateGrid("LedgerName", SortDirection.Ascending, strFilter);
                // A/c group population in search Tab 
                ddlGroupVw.Items.Clear();
                strValues = Session["CompanyID"].ToString() + chr.ToString() + "" + chr.ToString() + "Main Group";
                genObj.BindAjaxDropDownColumnsBySP(ddlGroupVw, "spSelect_MstAccountsGroup", strValues);
                ddlGroupVw.Items.Insert(0, new ListItem("Select", "0"));
                ddlGroupVw.SelectedValue = "0";
            }
        }

        private void PopulateGrid(string strSortExpression, SortDirection sortDir, string strFilter)
        {
            char chr = Convert.ToChar(130);
            if (strFilter == "null" || strFilter == null)
                strFilter = "";
            string strValues = "";
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + "" + chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            genObj.BindGridViewSP(gdGenLedger, "spSelect_MstGeneralLedgerALL", strValues, strFilter);
            if (gdGenLedger.DataSource != null)
            {
                Session[clsGlobalVariable.sesReportPageHeader] = strPrepareRPTHeader;
                Session[clsGlobalVariable.sesReportPageFooter] = strPrepareRPTFooter;
                Session[clsGlobalVariable.sesReportGrid] = gdGenLedger;
                Session[clsGlobalVariable.sesReportTitle] = "General Ledger";

                btnPrint.Attributes["onclick"] = "window.open('RPTShowGrid.aspx');";
            }
        }

        protected void gdGenLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strMessage = "";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("btnEdit");
                ib.CommandArgument = e.Row.RowIndex.ToString();
            }

        }

        protected void gdGenLedger_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMessage = "";

            if (e.CommandName.ToString() == "btnEdit")
            {
                int rID = Convert.ToInt32(e.CommandArgument);
                HiddenField ib = (HiddenField)gdGenLedger.Rows[rID].FindControl("hdID");
                int intID = Convert.ToInt32(ib.Value);
                ViewState["ID"] = intID;
                //lblErrorMsg.Text = "";
            }

        }

        protected void ddlGroupVw_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMessage = "";
            //special character for string separation
            char chr = Convert.ToChar(130);
            string strValues = "";
            
                if (ddlGroupVw.SelectedValue != "0")
                {
                    ddlSubGroupVw.Items.Clear();
                    strValues = Session["CompanyID"].ToString();
                    strValues += chr.ToString() + ddlGroupVw.SelectedValue.ToString();
                    strValues += chr.ToString() + "Sub Group";
                    //genObj.BindAjaxDropDownColumnsBySP(ddlSubGroupVw, "spSelect_MstAccountsGroup", strValues);
                    DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
                    dv.RowFilter = "GroupID_FK=" + ddlGroupVw.SelectedValue.ToString();

                    ddlSubGroupVw.DataValueField = "GroupID";
                    ddlSubGroupVw.DataTextField = "GroupName";
                    ddlSubGroupVw.DataSource = dv;
                    ddlSubGroupVw.DataBind();
                    
                    ddlSubGroupVw.Items.Insert(0, new ListItem("Select", "0"));
                    ddlSubGroupVw.SelectedValue = "0";
                }
                else
                {
                    ddlSubGroupVw.Items.Clear();
                    ddlSubGroupVw.Items.Insert(0, new ListItem("Select", "0"));
                    ddlSubGroupVw.SelectedValue = "0";
                }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strFilter = "";
            if (txtLedgerNameVw.Text != "")
            {
                strFilter = "LedgerName like '" + txtLedgerNameVw.Text.Trim() + "%'";
            }
            if (ddlGroupVw.SelectedValue.ToString() != "0")
            {
                if (strFilter == "")
                    strFilter = "MAINGROUP = '" + ddlGroupVw.SelectedItem.Text.ToString() + "'";
                else
                    strFilter += " AND MAINGROUP = '" + ddlGroupVw.SelectedItem.Text.ToString() + "'";
            }
            if (ddlSubGroupVw.SelectedValue.ToString() != "0")
            {
                if (strFilter == "")
                    strFilter = "SUBGROUP = '" + ddlSubGroupVw.SelectedItem.Text.ToString() + "'";
                else
                    strFilter += " AND SUBGROUP = '" + ddlSubGroupVw.SelectedItem.Text.ToString() + "'";
            }
            gdGenLedger.PageIndex = 0;
            PopulateGrid("LedgerName", SortDirection.Ascending, strFilter);
        }


        protected void gdGenLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdGenLedger.PageIndex = e.NewPageIndex;
            PopulateGrid("LedgerName", SortDirection.Ascending, strFilter);
        }
    }
}
