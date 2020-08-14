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
using System.Globalization;

namespace CollegeERP.Accounts
{
    public partial class ContraVoucher : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        string strValues = "";
        string strPrepareRPTHeader = "";
        string strPrepareRPTFooter = "";
        public string strFilter;
        ListItem li = new ListItem("Select", "0");
        ListItem liSearch = new ListItem("Select", "0");

        public int CVHeaderID
        {
            get { return Convert.ToInt32(ViewState["CVHeaderID"]); }
            set { ViewState["CVHeaderID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDropdown();
                ResetControls();
                PopulateHeaderGrid();

            }
        }

        protected void LoadDropdown()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedger.Items.Insert(0, li);

            gf.BindAjaxDropDownColumnsBySP(ddlLedgerSearch, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedgerSearch.Items.Insert(0, liSearch);

            gf.BindAjaxDropDownColumnsBySP(ddlParentLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlParentLedger.Items.Insert(0, li);

            gf.BindAjaxDropDownColumnsBySP(ddlParentLedgerSearch, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlParentLedgerSearch.Items.Insert(0, liSearch);
        }

        protected void ResetControls()
        {
            CVHeaderID = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtVchNo.Text = "Auto Generated";
            txtVoucherDate.Text = DataAccess.Common.Common.GetCurrentDate(); //DateTime.Now.ToString("dd MMM yyyy");
            txtChequeNo.Text = "";
            txtChequeDate.Text = "";
            txtDrawnOn.Text = "";
            ddlParentLedger.SelectedValue = "0";
            ddlLedger.SelectedValue = "0";
            ddlDrCr.SelectedIndex = 0;
            ltrDrCr.Text = "<b>CR</b>";
            txtAmount.Text = "";
            txtAmountCopy.Text = "";
            txtNarration.Text = "Enter Voucher Narration...";
            ltrLedgerBalance.Text = "";
            ltrParentLedgerBalance.Text = "";
            btnPrint.Attributes["onClick"] = "alert('Please Create a Voucher Before Printing')";

            ddlParentLedgerSearch.SelectedValue = "0";
            ddlLedgerSearch.SelectedValue = "0";
        }

        protected void PopulatePage()
        {
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + Session["DataFlow"].ToString();
            strValues += chr.ToString() + CVHeaderID.ToString();
            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnContraVoucherHeader", strValues);

            txtVchNo.Text = ds.Tables[0].Rows[0]["CVoucherNo"].ToString();
            txtVoucherDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CVoucherDate"].ToString()).ToString("dd MMM yyyy");
            ddlLedger.SelectedValue = ds.Tables[0].Rows[0]["LedgerId_FK"].ToString();
            LoadLedgerBalance(ddlLedger, ltrLedgerBalance);
            ddlParentLedger.SelectedValue = ds.Tables[0].Rows[0]["ParentLedgerId_FK"].ToString();
            LoadLedgerBalance(ddlParentLedger, ltrParentLedgerBalance);

            txtChequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();
            if (ds.Tables[0].Rows[0]["ChequeDate"].ToString().Trim().Length > 0)
                txtChequeDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChequeDate"].ToString()).ToString("dd MMM yyyy");
            else
                txtChequeDate.Text = "";

            txtDrawnOn.Text = ds.Tables[0].Rows[0]["DrawnOn"].ToString();
            ddlDrCr.SelectedValue = ds.Tables[0].Rows[0]["TransactionType"].ToString();
            ltrDrCr.Text = (ddlDrCr.SelectedValue == "DR") ? "<b>CR</b>" : "</b>DR</b>";

            txtAmount.Text = txtAmountCopy.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
            txtNarration.Text = ds.Tables[0].Rows[0]["CVNarration"].ToString();

            Message.Show = false;
            btnSave.Text = "Update";

            btnPrint.Attributes.Add("onclick", "javascript:openPopup('PrintContraVoucher.aspx?id=" + CVHeaderID + "'); return false;");
        }

        protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLedgerBalance(ddlLedger, ltrLedgerBalance);
        }

        protected void LoadLedgerBalance(AjaxControlToolkit.ComboBox ddl, Literal ltr)
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += ddl.SelectedValue.ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString() + chr.ToString();

            ltr.Text = "";
            DataSet ds = gf.ExecuteSelectSP("spSelect_MstGeneralLedgerRefDetails", strParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal ClosingBalance = Convert.ToDecimal(ds.Tables[0].Rows[0]["ClosingBalance"].ToString());
                if (ClosingBalance < 0)
                    ltr.Text = "<i>Cur Bal.   </i><b style='color:Red;'>" + Math.Abs(ClosingBalance).ToString("n") + " Cr</b>";
                else
                    ltr.Text = "<i>Cur Bal.   </i><b style='color:#259D17;'>" + Math.Abs(ClosingBalance).ToString("n") + " Dr</b>";
            }
            txtAmountCopy.Text = txtAmount.Text;
        }

        protected void ddlParentLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLedgerBalance(ddlParentLedger, ltrParentLedgerBalance);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            strValues = DateTime.Now.ToString("dd MMM yyyy");
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            //{
                if (Validate())
                {
                    SaveVoucherDetails();
                    //--------------Clear Page---------------
                    txtVchNo.Text = "Auto Generated";
                    txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtChequeNo.Text = "";
                    txtChequeDate.Text = "";
                    txtDrawnOn.Text = "";
                    ddlParentLedger.SelectedValue = "0";
                    ddlLedger.SelectedValue = "0";
                    ddlDrCr.SelectedIndex = 0;
                    ltrDrCr.Text = "<b>CR</b>";
                    txtAmount.Text = "";
                    txtAmountCopy.Text = "";
                    txtNarration.Text = "Enter Voucher Narration...";
                    ltrLedgerBalance.Text = "";
                    ltrParentLedgerBalance.Text = "";
                    //---------------------------------------
                }
            //}
            //else
            //{
            //    Message.IsSuccess = false;
            //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            //}
            Message.Show = true;
        }

        private void SaveVoucherDetails()
        {
            string rtMsg = "";
            string Msg = "";
            string strSPName = "";
            strValues = "";

            if (CVHeaderID == 0)
            {
                strSPName = "spInsert_TrnContraVoucher";
            }
            else
            {
                strValues = CVHeaderID.ToString();
                strSPName = "spUpdate_TrnCONTRAVoucher";
            }

            if (strValues == "")
                strValues = txtVchNo.Text.Trim();
            else
                strValues += chr.ToString() + txtVchNo.Text.Trim();

            strValues += chr.ToString() + Session["CompanyId"].ToString().Trim();
            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();
            strValues += chr.ToString() + txtVoucherDate.Text.ToString().Trim();
            strValues += chr.ToString() + "1";
            strValues += chr.ToString() + ddlLedger.SelectedValue.Trim();
            strValues += chr.ToString() + ddlDrCr.SelectedValue.Trim();

            if (txtChequeNo.Text.Trim() == "")
                strValues += chr.ToString() + "CASH";
            else
                strValues += chr.ToString() + "CHEQUE";

            strValues += chr.ToString() + txtChequeNo.Text.Trim();
            strValues += chr.ToString() + txtChequeDate.Text.Trim();
            strValues += chr.ToString() + txtDrawnOn.Text.Trim();
            strValues += chr.ToString() + ddlParentLedger.SelectedValue.Trim();
            strValues += chr.ToString() + txtAmount.Text.Trim();
            strValues += chr.ToString() + txtNarration.Text.ToString().Trim();
            strValues += chr.ToString() + Session["UserId"].ToString().Trim();

            rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);
            Msg = rtMsg.Substring(0, 4);

            if (Msg == "True")
            {
                CVHeaderID = Convert.ToInt32(rtMsg.Substring(5, rtMsg.Length - 5));
                PopulateHeaderGrid();
                PopulatePage();

                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";

            }
            else
            {
                if (rtMsg == "Duplicate:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Voucher No. is already exists!";

                }
            }
            Message.Show = true;
        }

        protected bool Validate()
        {
            bool result = true;
            string error = "";
            strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlLedger.SelectedValue == "0" || ddlLedger.Text == string.Empty)
            {
                result = false;
                error = "Please Select Cash/Bank Book";
            }
            else if (ddlParentLedger.SelectedValue == "0" || ddlParentLedger.Text == string.Empty)
            {
                result = false;
                error = "Please Select Cash/Bank Book";
            }
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                error = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()).Date > DateTime.Now.Date)
            {
                result = false;
                error = "Sorry! Future Voucher Date is Not Allowed.";
            }

            if (result == false)
            {
                Message.IsSuccess = false;
                Message.Text = error;
            }
            return result;

        }

        private void PopulateHeaderGrid()
        {
            strParams = Session["CompanyId"].ToString().Trim();
            strParams += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strParams += chr.ToString() + Session["BranchId"].ToString().Trim();
            strParams += chr.ToString() + Session["DataFlow"].ToString().Trim();
            string strHParams = strParams + chr.ToString() + "";
            gf.BindGridViewSP(grdvwtrnsctnsearch, "spSelect_TrnContraVoucherHeader", strHParams, PrepareSearchFilter());

        }

        private string PrepareSearchFilter()
        {
            string strFilterString = "";
            if (txtVchNoSearch.Text.ToString() != "")
            {
                strFilterString = "CVoucherNo = '" + txtVchNoSearch.Text.ToString() + "'";
            }
            if (txtVchDateSearch.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "CVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
                else
                    strFilterString += " AND CVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
            }
            if (txtVchDateSearchTo.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "CVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
                else
                    strFilterString += " AND CVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
            }
            if (ddlLedgerSearch.SelectedValue != "0")
            {
                if (strFilterString == "")
                    strFilterString = "LedgerName = '" + ddlLedgerSearch.SelectedItem.ToString() + "'";
                else
                    strFilterString += " AND LedgerName = '" + ddlLedgerSearch.SelectedItem.ToString() + "'";
            }
            if (ddlParentLedgerSearch.SelectedValue != "0")
            {
                if (strFilterString == "")
                    strFilterString = "ParentLedgerName = '" + ddlParentLedgerSearch.SelectedItem.ToString() + "'";
                else
                    strFilterString += " AND ParentLedgerName = '" + ddlParentLedgerSearch.SelectedItem.ToString() + "'";
            }

            return strFilterString;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid();
        }

        protected void grdvwtrnsctnsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("imgbtnedit");
                ib.Attributes["onClick"] = "changeTab();";
                //((ImageButton)e.Row.FindControl("imgbtnDelete")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);
                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('PrintContraVoucher.aspx?id=" + grdvwtrnsctnsearch.DataKeys[e.Row.RowIndex].Value.ToString() + "'); return false;");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void grdvwtrnsctnsearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CVHeaderID = Convert.ToInt32(grdvwtrnsctnsearch.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }

        protected void grdvwtrnsctnsearch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grdvwtrnsctnsearch.DataKeys[e.RowIndex].Value);
            bool Success = Convert.ToBoolean(gf.ExecuteAnySPOutput("spDelete_TrnContraVoucher", id.ToString()));
            if (Success)
               PopulateHeaderGrid();
        }

        protected void ddlDrCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDrCr.SelectedValue == "DR")
                ltrDrCr.Text = "<b>CR</b>";
            else if (ddlDrCr.SelectedValue == "CR")
                ltrDrCr.Text = "<b>DR</b>";
        }

        //protected void btnPrintReport_Click(object sender, EventArgs e)
        //{
        //    string Title = "Contra Voucher Report";
        //    string[] _header = new string[3];
        //    //_header[0] = "Trial Balance From " + txtFromDate.Text + " To " + txtToDate.Text;
        //    _header[0] = "Printed on " + DateTime.Now.ToString();
        //    _header[1] = "";

        //    string[] _footer = new string[0];

        //    Print.ReportPrint(Title, _header, grdvwtrnsctnsearch, _footer);
        //    Response.Redirect("RPTShowGrid.aspx");
        //}

        //protected void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    string[] _header = new string[4];
        //    _header[0] = "<b>West Bengal Poultry Federation</b>";
        //    //_header[1] = "Trial Balance From " + txtFromDate.Text + " To " + txtToDate.Text;
        //    _header[1] = "Printed on " + DateTime.Now.ToString();
        //    _header[2] = "";

        //    string[] _footer = new string[0];

        //    string file = "CONTRA_VOUCHER_REPORT";

        //    BusinessLayer.Common.Excel.SaveExcel(_header, grdvwtrnsctnsearch, _footer, file);
        //}
    }
}
