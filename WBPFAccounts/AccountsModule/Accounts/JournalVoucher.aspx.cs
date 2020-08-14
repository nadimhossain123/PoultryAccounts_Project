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
    public partial class JournalVoucher : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        DataSet dsD;
        string strPrepareRPTHeader = "";
        string strPrepareRPTFooter = "";
        ListItem li = new ListItem("Select", "0");
        public int RowIndex
        {
            get { return Convert.ToInt32(ViewState["RowIndex"]); }
            set { ViewState["RowIndex"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ResetControls();
                ViewState["sortColumn"] = "JVoucherDate";
                ViewState["sortDirection"] = gf.ConvertSortDirectionToSql(SortDirection.Descending);
                PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
                ViewState["sortColumntransaction"] = "Ledger Name";
                ViewState["sortDirectiontransaction"] = gf.ConvertSortDirectionToSql(SortDirection.Descending);
            }
        }

        protected void ResetControls()
        {
            SetCombo();
            hdnJournalID.Value = "0";
            RowIndex = -1;
            Message.Show = false;

            EmptyControlPopulate();

            BlankDataSet();
            GetCostCenterDetails();

            txtVchNo.Text = "Auto Generated";
            txtVchDate.Text = DataAccess.Common.Common.GetCurrentDate(); //DateTime.Now.ToString("dd MMM yyyy");
            txtblncdr.Text = "0.00";
            txtblnccr.Text = "0.00";
            txtNarration.Text = "";

            btnSave.Text = "Save";
            AdjustButtons(true, true, false, false);

            txtVchDateSearch.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
            txtVchDateSearch.Attributes.Add("readonly", "readonly");
            txtVchDateSearchTo.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtVchDateSearchTo.Attributes.Add("readonly", "readonly");

            //-------------------------------------//
            txtamnrdr.Enabled = true;
            txtblncdr.Enabled = true;
            txtamnrcr.Enabled = true;
            //------------------------------------//
            this.btnSave.Attributes["onclick"] = "return Confirmationmessage();";
        }

        protected void SetCombo()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            
            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType NOT IN ('BNK', 'CASH')";
            if (dv != null)
            {
                ddlledgernm.DataSource = dv;
                ddlledgernm.DataBind();
            }

            ddlledgernm.Items.Insert(0, li);
        }


        private void InitializeDataSet()
        {
            dsD = new DataSet();
            dsD.Tables.Add("TrnVoucherDetails");//TrnSalesOrderDetail
            dsD.Tables[0].Columns.Add("JVHeaderID_FK");
            dsD.Tables[0].Columns.Add("JVDetailID");
            dsD.Tables[0].Columns.Add("Serial No");
            dsD.Tables[0].Columns.Add("BY/To");
            dsD.Tables[0].Columns.Add("Ledger Name");
            //dsD.Tables[0].Columns.Add("Closing Balance");
            //dsD.Tables[0].Columns.Add("Type");
            dsD.Tables[0].Columns.Add("Cost Center");
            dsD.Tables[0].Columns.Add("Reference No");
            dsD.Tables[0].Columns.Add("Amount DR");
            dsD.Tables[0].Columns.Add("Amount CR");
            dsD.Tables[0].Columns.Add("Ledger ID");
            dsD.Tables[0].Columns.Add("CostCenterId");
            Session["SOD"] = dsD;

        }


        private void BlankDataSet()
        {
            InitializeDataSet();
            dsD.Tables[0].Rows.Add();
            dsD.Tables[0].Rows[0]["JVHeaderID_FK"] = "0";
            dsD.Tables[0].Rows[0]["JVDetailID"] = "0";
            dsD.Tables[0].Rows[0]["Serial No"] = "";
            dsD.Tables[0].Rows[0]["BY/To"] = "";
            dsD.Tables[0].Rows[0]["Ledger Name"] = "";
            //dsD.Tables[0].Rows[0]["Closing Balance"] = "";
            // dsD.Tables[0].Rows[0]["Type"] = "";
            dsD.Tables[0].Rows[0]["Cost Center"] = "";
            dsD.Tables[0].Rows[0]["Reference No"] = "";
            dsD.Tables[0].Rows[0]["Amount DR"] = "";
            dsD.Tables[0].Rows[0]["Amount CR"] = "";
            dsD.Tables[0].Rows[0]["Ledger ID"] = "";
            dsD.Tables[0].Rows[0]["CostCenterId"] = "0";
            grdvwtrnsctn.DataSource = dsD.Tables[0].DefaultView;
            grdvwtrnsctn.DataBind();
            InitializeDataSet();

        }


        protected void GetLedgerDetails()
        {

            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += ddlledgernm.SelectedValue.ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString() + chr.ToString();

            DataSet ds = gf.ExecuteSelectSP("spSelect_MstGeneralLedgerRefDetails", strParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hdnCCApp.Value = ds.Tables[0].Rows[0]["CostCenterApplied"].ToString();
                decimal ClosingBalance = Convert.ToDecimal(ds.Tables[0].Rows[0]["ClosingBalance"].ToString());
                if (ClosingBalance < 0)
                    txtbalance.Text = Math.Abs(ClosingBalance).ToString("n") + " Cr";
                else
                    txtbalance.Text = Math.Abs(ClosingBalance).ToString("n") + " Dr";
            }
            else
            {
                hdnCCApp.Value = "";
                txtbalance.Text = "";
            }

        }
        
        protected void GetCostCenterDetails()
        {
            strParams = Session["CompanyId"].ToString().Trim() + chr.ToString();
            strParams += "";
            gf.BindDropDownColumnsBySP(ddlcostcntr, "spSelect_MstCostCenter", strParams);
            ddlcostcntr.Items.Insert(0, li);
        }

        protected void ddlledgernm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlledgernm.SelectedValue != "0")
            {
                GetLedgerDetails();
                GetCostCenterDetails();
            }
        }


        protected void btntsctnadd_Click(object sender, EventArgs e)
        {
            if (ddlledgernm.SelectedValue == "0" || ddlledgernm.Text == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Ledger is mandatory field!";
                Message.Show = true;
            }
            else if (Convert.ToDouble(txtamnrcr.Text) == 0 && Convert.ToDouble(txtamnrdr.Text) == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "DR or CR Amount is mandatory field!";
                Message.Show = true;
            }
            else
            {
                if (!DuplicateLedger(ddlledgernm.SelectedValue.ToString(), ddlbyto.SelectedItem.ToString()))
                {
                    AddGridDetails();
                    SetCRDRTextBox(ddlbyto.SelectedValue.ToString());
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Ledger has already been selected! Please select a different Ledger.";
                    Message.Show = true;
                }

            }
        }


        protected bool DuplicateLedger(string strLedgerID, string strLedgerType)
        {

            bool bLedger = false;
            dsD = new DataSet();
            dsD = (DataSet)Session["SOD"];
            for (int i = 0; i < dsD.Tables[0].Rows.Count; i++)
            {
                if (dsD.Tables[0].Rows[i]["Ledger ID"].ToString() == strLedgerID && dsD.Tables[0].Rows[i]["BY/To"].ToString() == strLedgerType && i != RowIndex)
                {
                    bLedger = true;
                    break;
                }
            }
            return bLedger;
        }


        protected void AddGridDetails()
        {

            dsD = new DataSet();
            dsD = (DataSet)Session["SOD"];
            if (RowIndex == -1)
            {
                int i = dsD.Tables[0].Rows.Count;
                dsD.Tables[0].Rows.Add();
                dsD.Tables[0].Rows[i]["JVHeaderID_FK"] = hdnJournalID.Value.ToString().Trim();
                dsD.Tables[0].Rows[i]["JVDetailID"] = "0";
                dsD.Tables[0].Rows[i]["Serial No"] = (i + 1).ToString();
                dsD.Tables[0].Rows[i]["BY/To"] = ddlbyto.SelectedItem.ToString();
                dsD.Tables[0].Rows[i]["Ledger Name"] = ddlledgernm.SelectedItem.ToString();
                
                if (ddlcostcntr.SelectedValue == "0")
                {
                    dsD.Tables[0].Rows[i]["Cost Center"] = "";
                }
                else
                {
                    dsD.Tables[0].Rows[i]["Cost Center"] = ddlcostcntr.SelectedItem.ToString();
                }
                dsD.Tables[0].Rows[i]["Reference No"] = txtrfno.Text.ToString();
                dsD.Tables[0].Rows[i]["Amount DR"] = Convert.ToDouble(txtamnrdr.Text.ToString()).ToString("0.00");
                dsD.Tables[0].Rows[i]["Amount CR"] = Convert.ToDouble(txtamnrcr.Text.ToString()).ToString("0.00");
                dsD.Tables[0].Rows[i]["Ledger ID"] = ddlledgernm.SelectedValue.ToString();
                dsD.Tables[0].Rows[i]["CostCenterId"] = ddlcostcntr.SelectedValue.ToString();
                dsD.Tables[0].AcceptChanges();

            }
            else
            {
                dsD.Tables[0].Rows[RowIndex]["JVHeaderID_FK"] = hdnJournalID.Value.ToString().Trim();
                //dsD.Tables[0].Rows[RowIndex]["JVDetailID"] = hdndtlid.Value.ToString().Trim();
                //dsD.Tables[0].Rows[RowIndex]["Serial No"] = (j + 1).ToString();
                dsD.Tables[0].Rows[RowIndex]["BY/To"] = ddlbyto.SelectedItem.ToString();
                dsD.Tables[0].Rows[RowIndex]["Ledger Name"] = ddlledgernm.SelectedItem.ToString();
                
                if (ddlcostcntr.SelectedValue == "0")
                {
                    dsD.Tables[0].Rows[RowIndex]["Cost Center"] = "";
                }
                else
                {
                    dsD.Tables[0].Rows[RowIndex]["Cost Center"] = ddlcostcntr.SelectedItem.ToString();
                }
                dsD.Tables[0].Rows[RowIndex]["Reference No"] = txtrfno.Text.ToString();
                dsD.Tables[0].Rows[RowIndex]["Amount DR"] = Convert.ToDouble(txtamnrdr.Text.ToString()).ToString("0.00");
                dsD.Tables[0].Rows[RowIndex]["Amount CR"] = Convert.ToDouble(txtamnrcr.Text.ToString()).ToString("0.00");
                dsD.Tables[0].Rows[RowIndex]["Ledger ID"] = ddlledgernm.SelectedValue.ToString();
                dsD.Tables[0].Rows[RowIndex]["CostCenterId"] = ddlcostcntr.SelectedValue.ToString();
                dsD.Tables[0].AcceptChanges();
            }


            if (dsD.Tables[0].Rows.Count > 0)
            {
                Session["SOD"] = dsD;
                grdvwtrnsctn.DataSource = dsD.Tables[0].DefaultView;
                grdvwtrnsctn.DataBind();
                CalculateCRDRTotal(dsD);
                EmptyControlPopulate();
            }

            RowIndex = -1;

        }

        private void EmptyControlPopulate()
        {

            ddlbyto.SelectedIndex = 0;
            ddlledgernm.SelectedIndex = 0;
            txtbalance.Text = "";
            txtrfno.Text = "";
            txtamnrdr.Text = "0.00";
            txtamnrcr.Text = "0.00";
            SetCRDRTextBox(ddlbyto.SelectedItem.ToString());
            //txtblncdr.Text="";
            //txtblnccr.Text="";
            //txtNarration.Text="";
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strValues = DateTime.Now.ToString("dd MMM yyyy");
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);
            strValues = "";
            //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            //{
                if (Validate())
                {
                    if (Convert.ToDouble(txtblnccr.Text) != Convert.ToDouble(txtblncdr.Text))
                    {
                        Message.IsSuccess = false;
                        Message.Text = "DR Amount & CR Amount should be equal!";
                        Message.Show = true;
                    }
                    else
                    {
                        SaveVoucherDetails();
                        //----CLEAR PAGE---
                        SetCombo();
                        EmptyControlPopulate();
                        BlankDataSet();
                        GetCostCenterDetails();
                        txtVchNo.Text = "Auto Generated";
                        txtVchDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                        txtblncdr.Text = "0.00";
                        txtblnccr.Text = "0.00";
                        txtNarration.Text = "";
                        //-----------------
                    }
                }
            //}
            //else
            //{
            //    Message.IsSuccess = false;
            //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            //}
            Message.Show = true;
        }

        private bool Validate()
        {
            bool result = true;
            string error = "";
            string strValues = txtVchDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                error = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVchDate.Text.Trim()) > DateTime.Now)
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

        private void SetCRDRTextBox(string strType)
        {
            if (strType.ToUpper() == "BY")
            {
                txtamnrdr.Text = "0.00";
                txtamnrcr.Text = "0.00";
                txtamnrcr.Attributes.Add("readonly", "readonly");
                txtamnrdr.Attributes.Remove("readonly");
            }
            else if (strType.ToUpper() == "TO")
            {
                txtamnrdr.Text = "0.00";
                txtamnrcr.Text = "0.00";
                txtamnrdr.Attributes.Add("readonly", "readonly");
                txtamnrcr.Attributes.Remove("readonly");
            }
        }


        private void SaveVoucherDetails()
        {
            string rtMsg = "";
            string Msg = "";

            string strSPName = "";
            string strValues = "";

            dsD = ((DataSet)Session["SOD"]);

            if (dsD.Tables[0].Rows.Count == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select any Item for Order!";
                Message.Show = true;

                return;
            }
            if (hdnJournalID.Value == "0")
            {
                strSPName = "spInsert_TrnJournalVoucher";
            }
            else
            {
                strSPName = "spUpdate_TrnJournalVoucher";
            }
            //if()
            strValues = hdnJournalID.Value.ToString().Trim();
            strValues += chr.ToString() + txtVchNo.Text.ToString().Trim();
            strValues += chr.ToString() + Session["CompanyId"].ToString().Trim();

            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();

            strValues += chr.ToString() + txtVchDate.Text.ToString().Trim();
            strValues += chr.ToString() + "1";
            strValues += chr.ToString() + txtNarration.Text.ToString().Trim();
            strValues += chr.ToString() + Session["UserId"].ToString().Trim();

            strValues += chr.ToString() + PrepareXMLString();
            rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);
            Msg = rtMsg.Substring(0, 4);
            //strvchnoshow=rtMsg[1].ToString();
            if (Msg == "True")
            {
                hdnJournalID.Value = rtMsg.Substring(5, rtMsg.Length - 5);
                ViewState["ID"] = hdnJournalID.Value;
                BlankDataSet();
                PopulatePageControl(Convert.ToInt32(hdnJournalID.Value));
                
                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";
                
                AdjustButtons(true, true, true, false);
                PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
                
            }
            else
            {
                if (rtMsg == "Duplicate:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Voucher No. is already exists!";

                }
                else if (rtMsg == "Blank:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "Voucher No. can not be blank, please set the AutoCode master!";

                }
                else
                {

                }
                AdjustButtons(true, true, false, false);
            }
            Message.Show = true;
        }


        private void AdjustButtons(bool bSave, bool bCancel, bool bPrint, bool bDelete)
        {
            btnSave.Enabled = bSave;
            btnReset.Enabled = bCancel;
            btnPrint.Enabled = bPrint;
        }


        private string PrepareXMLString()
        {
            dsD = (DataSet)Session["SOD"];
            string strXMLString = "<NewDataSet>";
            for (int i = 0; i < dsD.Tables[0].Rows.Count; i++)
            {
                strXMLString += " <TrnJournalVoucherDetail";
                strXMLString += " JVHeaderID_FK = \"" + dsD.Tables[0].Rows[i]["JVHeaderID_FK"].ToString() + "\"";
                strXMLString += " JVDetailID = \"" + dsD.Tables[0].Rows[i]["JVDetailID"].ToString() + "\"";
                strXMLString += " SrlNo = \"" + dsD.Tables[0].Rows[i]["Serial No"].ToString() + "\"";
                strXMLString += " LedgerID = \"" + dsD.Tables[0].Rows[i]["Ledger ID"].ToString() + "\"";
                strXMLString += " ReferenceNo = \"" + dsD.Tables[0].Rows[i]["Reference No"].ToString() + "\"";
                strXMLString += " DRAmount = \"" + dsD.Tables[0].Rows[i]["Amount Dr"].ToString() + "\"";
                strXMLString += " CRAmount = \"" + dsD.Tables[0].Rows[i]["Amount Cr"].ToString() + "\"";
                strXMLString += " LedgerType = \"" + dsD.Tables[0].Rows[i]["BY/To"].ToString() + "\"";
                strXMLString += " CostCenterId = \"" + dsD.Tables[0].Rows[i]["CostCenterId"].ToString() + "\"";
                strXMLString += " />";
            }
            strXMLString += " </NewDataSet>";
            return strXMLString;
        }

        private void PopulateHeaderGrid(string strSortExpression, string sortDir)
        {
            //For Search
            strParams = Session["CompanyId"].ToString().Trim();
            strParams += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strParams += chr.ToString() + Session["BranchId"].ToString().Trim();
            strParams += chr.ToString() + Session["DataFlow"].ToString().Trim();
            string strHParams = strParams + chr.ToString() + "";
            //strHParams += "";
            gf.BindGridViewSP(grdvwtrnsctnsearch, "spSelect_TrnVoucherHeaderDetails", strHParams, PrepareSearchFilter(), strSortExpression, sortDir);
            
        }

        private string PrepareSearchFilter()
        {
            string strFilterString = "";
            if (txtVchNoSearch.Text.ToString() != "")
            {
                strFilterString = "JVoucherNo = '" + txtVchNoSearch.Text.ToString() + "'";
            }
            if (txtVchDateSearch.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "JVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
                //strFilterString = "JVoucherDate >= '" + txtVchDateSearch.Text.ToString(),System.Configuration.ConfigurationManager.AppSettings["DateFormat"] + "'";
                else
                    strFilterString += " AND JVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
            }
            if (txtVchDateSearchTo.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "JVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
                else
                    strFilterString += " AND JVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
            }

            return strFilterString;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
        }

        protected void grdvwtrnsctnsearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMessage = "";
           
                if (e.CommandName.ToString() == "imgbtneditsearch")
                {
                    hdnJournalID.Value = e.CommandArgument.ToString();
                    ViewState["ID"] = hdnJournalID.Value;
                    BlankDataSet();
                    PopulatePageControl(Convert.ToInt32(hdnJournalID.Value));
                    btnSave.Enabled = true;
                }
                else if (e.CommandName.ToString() == "imgbtndeletesearch")
                {
                    gf.ExecuteAnySPOutput("spDelete_TrnJournalVoucher", e.CommandArgument.ToString());
                    PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
                }
        }


        protected void grdvwtrnsctnsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {      
               if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton ib = new ImageButton();
                    ib = (ImageButton)e.Row.FindControl("imgbtneditsearch");
                    ib.Attributes["onClick"] = "changeTab();";
                    //((ImageButton)e.Row.FindControl("imgbtndeletesearch")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);
                    ImageButton ibPrint = new ImageButton();
                    ibPrint = (ImageButton)e.Row.FindControl("btnPrint");
                    ibPrint.CommandArgument = e.Row.RowIndex.ToString();
                    ibPrint.Attributes.Add("onclick", "javascript:openPopup('PrintJournalVoucher.aspx?id=" + ((DataRowView)e.Row.DataItem)["JVHeaderID"].ToString() + "'); return false;"); 
                }
            
        }


        private void PopulatePageControl(int intID)
        {
            //Populate Controls For Edit
            string strValues = "";
            RowIndex = -1;
            txtblncdr.Text = "";
            txtblnccr.Text = "";
            strValues = Session["CompanyId"].ToString().Trim();
            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();
            strValues += chr.ToString() + intID;
            DataSet dsOH = gf.ExecuteSelectSP("spSelect_TrnVoucherHeaderPopulate", strValues);

            if (dsOH.Tables[0].Rows.Count > 0)
            {
                txtVchNo.Text = dsOH.Tables[0].Rows[0]["JVoucherNo"].ToString();
                txtVchDate.Text = ((DateTime)dsOH.Tables[0].Rows[0]["JVoucherDate"]).ToString("dd MMM yyyy");
                txtNarration.Text = dsOH.Tables[0].Rows[0]["JVNarration"].ToString();

                dsD = new DataSet();
                dsD = (DataSet)Session["SOD"];
               
                DataSet dsDT = gf.ExecuteSelectSP("spSelect_TrnVoucherDetailsPopulate", strValues);

                //Double dblamntdr=Double.Parse(dsOH.Tables[0].Rows[0]["DRAmount"].ToString());
                //Double dblamntcr = Double.Parse(dsOH.Tables[0].Rows[0]["CRAmount"].ToString());
                if (dsDT.Tables[0].Rows.Count > 0)
                {
                    AdjustButtons(true, true, true, true);
                    for (int i = 0; i <= dsDT.Tables[0].Rows.Count - 1; i++)
                    {
                        dsD.Tables[0].Rows.Add();
                        dsD.Tables[0].Rows[i]["JVHeaderID_FK"] = dsDT.Tables[0].Rows[i]["JVHeaderID_FK"];
                        dsD.Tables[0].Rows[i]["JVDetailID"] = dsDT.Tables[0].Rows[i]["JVDetailID"];
                        dsD.Tables[0].Rows[i]["Serial No"] = dsDT.Tables[0].Rows[i]["SrlNo"];
                        if (Convert.ToDouble(dsDT.Tables[0].Rows[i]["DRAmount"]) == 0)
                        {
                            dsD.Tables[0].Rows[i]["BY/To"] = "To";
                        }
                        else
                        {
                            dsD.Tables[0].Rows[i]["BY/To"] = "By";
                        }
                        dsD.Tables[0].Rows[i]["Ledger Name"] = dsDT.Tables[0].Rows[i]["LedgerName"];
                        //dsD.Tables[0].Rows[i]["Closing Balance"] = dsDT.Tables[0].Rows[i]["ClosingBalance"];
                        //dsD.Tables[0].Rows[i]["Type"] = dsDT.Tables[0].Rows[i]["LedgerType"];
                        dsD.Tables[0].Rows[i]["Cost Center"] = dsDT.Tables[0].Rows[i]["CostCenterName"];
                        dsD.Tables[0].Rows[i]["Reference No"] = dsDT.Tables[0].Rows[i]["ReferenceNo"];
                        dsD.Tables[0].Rows[i]["Amount DR"] = dsDT.Tables[0].Rows[i]["DRAmount"];
                        dsD.Tables[0].Rows[i]["Amount CR"] = dsDT.Tables[0].Rows[i]["CRAmount"];
                        dsD.Tables[0].Rows[i]["Ledger ID"] = dsDT.Tables[0].Rows[i]["LedgerID"];
                        dsD.Tables[0].Rows[i]["CostCenterId"] = dsDT.Tables[0].Rows[i]["CostCenterId"];
                    }
                    CalculateCRDRTotal(dsD);
                }
                grdvwtrnsctn.DataSource = dsD.Tables[0].DefaultView;
                grdvwtrnsctn.DataBind();
                Session["SOD"] = dsD;
                btnSave.Text = "Update";
                btnPrint.Attributes.Add("onclick", "javascript:openPopup('PrintJournalVoucher.aspx?id=" + intID + "'); return false;"); 
            }
            else
            {
                //btnSave.Text = "Save";
                //AdjustButtons(true, true, false, false);
            }
            Message.Show = false;

        }



        protected void CalculateCRDRTotal(DataSet ds)
        {
            double dblTotalCR = 0.00;
            double dblTotalDR = 0.00;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                dblTotalCR += Convert.ToDouble(ds.Tables[0].Rows[i]["Amount CR"]);
                dblTotalDR += Convert.ToDouble(ds.Tables[0].Rows[i]["Amount DR"]);
            }
            txtblnccr.Text = dblTotalCR.ToString("0.00");
            txtblncdr.Text = dblTotalDR.ToString("0.00");
        }


        private void PopulateControlUpdate()
        {

            dsD = new DataSet();
            dsD = (DataSet)Session["SOD"];
           
            if (dsD.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDouble(dsD.Tables[0].Rows[RowIndex]["Amount Dr"].ToString()) == 0)
                    ddlbyto.SelectedValue = "To";
                else
                    ddlbyto.SelectedValue = "By";
                SetCRDRTextBox(ddlbyto.SelectedValue);
                ddlledgernm.SelectedValue = dsD.Tables[0].Rows[RowIndex]["Ledger ID"].ToString();
                GetLedgerDetails();
                //txtbalance.Text = dsD.Tables[0].Rows[i]["Closing Balance"].ToString();
                //txttype.Text = dsD.Tables[0].Rows[i]["Type"].ToString();
               // hdnCCApp.Value = dsD.Tables[0].Rows[0]["Cost Center"].ToString();
                hdndtlid.Value = dsD.Tables[0].Rows[RowIndex]["JVDetailID"].ToString();
                //if (hdnCCApp.Value == "True")
                    GetCostCenterDetails();
                txtrfno.Text = dsD.Tables[0].Rows[RowIndex]["Reference No"].ToString();
                txtamnrdr.Text = dsD.Tables[0].Rows[RowIndex]["Amount Dr"].ToString();
                txtamnrcr.Text = dsD.Tables[0].Rows[RowIndex]["Amount Cr"].ToString();
                ddlcostcntr.SelectedValue = dsD.Tables[0].Rows[RowIndex]["CostCenterId"].ToString();
                //Print();
                AdjustButtons(true, true, true, true);
            }
            else
            {

                AdjustButtons(true, true, false, false);
            }
            Message.Show = false;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }
     
        protected void ddlbyto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCRDRTextBox(ddlbyto.SelectedItem.ToString());
        }

        protected void grdvwtrnsctn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RowIndex = e.NewEditIndex;
            PopulateControlUpdate();

        }




    }
}
