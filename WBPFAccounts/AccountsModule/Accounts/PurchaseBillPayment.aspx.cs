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
    public partial class PurchaseBillPayment : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("Select", "0");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                LoadCashBankLedger();
                LoadSupplierLedger();
                LoadDeductionLedger();
                ClearControls();
            }
        }

        protected void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            DataTable DT = gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerBNKandCASH", strParams);

            if (DT != null)
            {
                ddlCashBankLedger.DataSource = DT;
                ddlCashBankLedger.DataBind();
            }
            ddlCashBankLedger.Items.Insert(0, li);
        }

        protected void LoadSupplierLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('SUP')";

            if (dv != null)
            {
                ddlSupplierLedger.DataSource = dv;
                ddlSupplierLedger.DataBind();
            }
            ddlSupplierLedger.Items.Insert(0, li);
        }

        protected void LoadDeductionLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('TAX','NONCASH')";

            if (dv != null)
            {
                ddlDeductionLedger.DataSource = dv;
                ddlDeductionLedger.DataBind();
            }
            ddlDeductionLedger.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            Message.Show = false;
            ddlCashBankLedger.SelectedIndex = 0;
            ddlPaymentMode.SelectedIndex = 0;
            txtChequeNo.Text = "";
            ltrLedgerBalance.Text = "";
            ddlSupplierLedger.SelectedIndex = 0;
            txtDrawnOn.Text = "";
            txtChequeDate.Text = "";
            txtChequeNo.Enabled = false;
            txtDrawnOn.Enabled = false;
            txtChequeDate.Enabled = false;
            txtPaymentDate.Text = DateTime.Now.ToString("dd MMM yyyy");

            dgvBill.DataSource = null;
            dgvBill.DataBind();

            txtTotalAmt.Text = "0.00";

            ddlDeductionLedger.SelectedIndex = 0;
            txtDeductionAmount.Text = "";
            
            ViewState["DeductionDetails"] = null;
            dgvDeduction.DataSource = null;
            dgvDeduction.DataBind();
            txtTotalDeductionAmt.Text = "0.00";

            txtNarration.Text = "";
            btnPrint.Attributes.Add("onclick", "javascript:alert('No Voucher To Print'); return false;");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedValue == "CASH")
            {
                txtChequeNo.Text = "";
                txtDrawnOn.Text = "";
                txtChequeDate.Text = "";

                txtChequeNo.Enabled = false;
                txtDrawnOn.Enabled = false;
                txtChequeDate.Enabled = false;
            }
            else if (ddlPaymentMode.SelectedValue == "CHEQUE")
            {
                txtChequeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtChequeNo.Enabled = true;
                txtDrawnOn.Enabled = true;
                txtChequeDate.Enabled = true;
            }
        }

        protected void ddlCashBankLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLedgerBalance();
        }

        protected void ddlSupplierLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SupplierLedgerId = Convert.ToInt32(ddlSupplierLedger.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString().Trim());

            BusinessLayer.Accounts.PurchaseBillPayment objPayment = new BusinessLayer.Accounts.PurchaseBillPayment();
            DataTable DT = objPayment.GetDueBills(SupplierLedgerId, CompanyId);

            if (DT != null)
            {
                dgvBill.DataSource = DT;
                dgvBill.DataBind();
                txtTotalAmt.Text = "0.00";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable DT;
            if (ViewState["DeductionDetails"] != null)
            {
                DT = (DataTable)ViewState["DeductionDetails"];
            }
            else
            {
                DT = new DataTable();
                DT.Columns.Add("DeductionLedgerId", typeof(int));
                DT.Columns.Add("DeductionHead", typeof(string));
                DT.Columns.Add("Amount", typeof(decimal));
            }

            DataView dv = new DataView(DT);
            dv.RowFilter = "DeductionLedgerId=" + ddlDeductionLedger.SelectedValue.Trim();

            if (dv.ToTable().Rows.Count == 0)
            {
                DataRow DR = DT.NewRow();
                DR["DeductionLedgerId"] = Convert.ToInt32(ddlDeductionLedger.SelectedValue.Trim());
                DR["DeductionHead"] = ddlDeductionLedger.SelectedItem.Text;
                DR["Amount"] = Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                DT.Rows.Add(DR);
                DT.AcceptChanges();

                ViewState["DeductionDetails"] = DT;
                dgvDeduction.DataSource = DT;
                dgvDeduction.DataBind();

                txtTotalDeductionAmt.Text = (Convert.ToDecimal(txtTotalDeductionAmt.Text) + Convert.ToDecimal(txtDeductionAmount.Text.Trim())).ToString();
                Message.Show = false;
            }
            else
            {
                Message.Show = false;
                Message.Text = "This Ledger Already Exists in The Deduction List. Please Select Other.";
                Message.Show = true;
            }
        }

        protected void dgvDeduction_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable DT = (DataTable)ViewState["DeductionDetails"];
            txtTotalDeductionAmt.Text = (Convert.ToDecimal(txtTotalDeductionAmt.Text) - Convert.ToDecimal(DT.Rows[e.RowIndex]["Amount"].ToString())).ToString();
            DT.Rows[e.RowIndex].Delete();
            DT.AcceptChanges();
            ViewState["DeductionDetails"] = DT;
            dgvDeduction.DataSource = DT;
            dgvDeduction.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string strValues = txtPaymentDate.Text;
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                {
                    BusinessLayer.Accounts.PurchaseBillPayment ObjPayment = new BusinessLayer.Accounts.PurchaseBillPayment();
                    Entity.Accounts.PurchaseBillPayment Payment = new Entity.Accounts.PurchaseBillPayment();
                    Payment.PurchaseBillPaymentId = 0;
                    Payment.SupplierLedgerId_FK = Convert.ToInt32(ddlSupplierLedger.SelectedValue.Trim());
                    Payment.CashBankLedgerID = Convert.ToInt32(ddlCashBankLedger.SelectedValue.Trim());
                    Payment.TransactionType = "PAYMENT";
                    Payment.AmountPaid = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                    Payment.AmountDeducted = Convert.ToDecimal(txtTotalDeductionAmt.Text.Trim());
                    Payment.PaymentDate = Convert.ToDateTime(txtPaymentDate.Text.Trim() + " 00:00:00");
                    Payment.ModeOfPayment = ddlPaymentMode.SelectedValue.Trim();
                    Payment.ChequeNo = txtChequeNo.Text.Trim();

                    if (txtChequeDate.Text.Trim().Length == 0)
                        Payment.ChequeDate = null;
                    else
                        Payment.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim() + " 00:00:00");

                    Payment.DrawnOn = txtDrawnOn.Text.Trim();
                    Payment.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Payment.CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
                    Payment.BranchId = Convert.ToInt32(Session["BranchId"].ToString());
                    Payment.FinYrId = Convert.ToInt32(Session["FinYrID"].ToString());
                    Payment.Narration = txtNarration.Text.Trim();
                    
                    //-------------------------------------------------------------------------------------//
                    DataTable DT = new DataTable();
                    DT.Columns.Add("PurchaseBillId", typeof(int));
                    DT.Columns.Add("Amount", typeof(decimal));
                    DataRow DR;

                    foreach (GridViewRow GVR in dgvBill.Rows)
                    {
                        if (GVR.RowType == DataControlRowType.DataRow)
                        {
                            TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                            decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                            if (Amount > 0)
                            {
                                DR = DT.NewRow();
                                DR["PurchaseBillId"] = Convert.ToInt32(dgvBill.DataKeys[GVR.RowIndex].Value.ToString());
                                DR["Amount"] = Amount;
                                DT.Rows.Add(DR);
                                DT.AcceptChanges();
                            }
                        }
                    }

                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(DT);
                        Payment.PaymentDetailsXML = ds.GetXml().Replace("Table1>", "Table>");
                    }


                    //---------------------------------------------------------------------------//
                    string DeductionDetails = "";

                    if (ViewState["DeductionDetails"] != null)
                    {
                        DT = (DataTable)ViewState["DeductionDetails"];
                        DT.Columns.Remove("DeductionHead");
                        DT.AcceptChanges();

                        using (DataSet ds = new DataSet())
                        {
                            ds.Tables.Add(DT);
                            DeductionDetails = ds.GetXml().Replace("Table1>", "Table>");
                        }
                    }

                    Payment.DeductionDetailsXML = DeductionDetails;
                    Payment.XMLCashBankVoucherDetails = PrepareXMLString();
                    ObjPayment.Save(Payment);
                    ClearControls();

                    Message.IsSuccess = true;
                    Message.Text = "Payment Saved Successfully";
                    btnPrint.Attributes.Add("onclick", "javascript:window.open('RPTGridView.aspx?ID=" + Payment.CBVHeaderID + "'); return false;");
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
                }
            }
            Message.Show = true;
        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            int SrlNo = 1;
            
            strXMLString = "<NewDataSet>";
            strXMLString += "<TrnCashBankVoucherDetail";
            strXMLString += " SrlNo = \"" + 1 + "\"";
            strXMLString += " ByTo = \"" + "RECEIVE" + "\"";
            strXMLString += " LedgerID = \"" + ddlSupplierLedger.SelectedValue.Trim() + "\"";
            strXMLString += " DRAmount = \"" + txtTotalAmt.Text.Trim() + "\"";
            strXMLString += " CRAmount = \"" + 0 + "\"";
            strXMLString += " />";

            foreach (GridViewRow GVR in dgvDeduction.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    decimal Amount = Convert.ToDecimal(GVR.Cells[2].Text.Trim());
                    SrlNo = SrlNo + 1;
                    strXMLString += "<TrnCashBankVoucherDetail";
                    strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
                    strXMLString += " ByTo = \"" + "PAYMENT" + "\"";
                    strXMLString += " LedgerID = \"" + dgvDeduction.DataKeys[GVR.RowIndex].Value.ToString() + "\"";
                    strXMLString += " DRAmount = \"" + 0 + "\"";
                    strXMLString += " CRAmount = \"" + Amount + "\"";
                    strXMLString += " />";
                }
            }

            strXMLString += "</NewDataSet>";
            return strXMLString;
        }

        protected bool Validate()
        {
            bool result;
            string ErrorText = "";
            string strValues = txtPaymentDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlCashBankLedger.SelectedValue == "0" || ddlCashBankLedger.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Cash/Bank Ledger";
            }
            else if (ddlSupplierLedger.SelectedValue == "0" || ddlSupplierLedger.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Supplier Ledger";
            }
            else if (ddlPaymentMode.SelectedValue == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim().Length == 0 || txtChequeDate.Text.Trim().Length == 0)
                {
                    result = false;
                    ErrorText = "You Must Provide Cheque No and Cheque Date When Payment Mode Is Cheque.";
                }
                else { result = true; }
            }
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                ErrorText = "Sorry! Payment Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtPaymentDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                ErrorText = "Sorry! Future Payment Date is Not Allowed.";
            }
            else { result = true; }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void GetLedgerBalance()
        {
            ltrLedgerBalance.Text = "";

            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += ddlCashBankLedger.SelectedValue.ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString() + chr.ToString();

            DataSet ds = gf.ExecuteSelectSP("spSelect_MstGeneralLedgerRefDetails", strParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal ClosingBalance = Convert.ToDecimal(ds.Tables[0].Rows[0]["ClosingBalance"].ToString());
                if (ClosingBalance < 0)
                    ltrLedgerBalance.Text = "<i>Cur Bal.   </i><b style='color:Red;'>" + Math.Abs(ClosingBalance).ToString("n") + " Cr</b>";
                else
                    ltrLedgerBalance.Text = "<i>Cur Bal.   </i><b style='color:#259D17;'>" + Math.Abs(ClosingBalance).ToString("n") + " Dr</b>";
            }
        }
    }
}
