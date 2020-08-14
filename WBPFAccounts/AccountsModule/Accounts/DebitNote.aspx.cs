using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Accounts;
using System.Data;

namespace CollegeERP.Accounts
{
    public partial class DebitNote : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        string strValues = "";
        public string strFilter;
        ListItem li = new ListItem("---SELECT---", "0");

        public int DNHeaderID
        {
            get { return Convert.ToInt32(ViewState["DNHeaderID"]); }
            set { ViewState["DNHeaderID"] = value; }
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

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('SUP')";

            if (dv != null)
            {
                ddlSupplierLedger.DataSource = dv;
                ddlSupplierLedger.DataBind();
            }
            ddlSupplierLedger.Items.Insert(0, li);
        }

        protected void ResetControls()
        {
            DNHeaderID = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtVchNo.Text = "Auto Generated";
            txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlSupplierLedger.SelectedIndex = 0;
            dgvBill.DataSource = null;
            dgvBill.DataBind();
            txtTotalAmt.Text = "0.00";
            txtNarration.Text = "";
            btnPrint.Attributes["onClick"] = "alert('Please Save a Voucher Before Printing')";
        }

        private void PopulateHeaderGrid()
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
            int FinYrID = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
            int BranchId = Convert.ToInt32(Session["BranchId"].ToString().Trim());
            string DNVoucherNo = txtVchNoSearch.Text.Trim();
            string FromDate = txtVchDateSearch.Text.Trim();
            string ToDate = txtVchDateSearchTo.Text.Trim();

            BusinessLayer.Accounts.TrnDebitNoteHeader objDebitNote = new TrnDebitNoteHeader();
            DataTable DT = objDebitNote.GetAll(CompanyId, FinYrID, BranchId, DNVoucherNo, FromDate, ToDate);

            if (DT != null)
            {
                dgvDebitNote.DataSource = DT;
                dgvDebitNote.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid();
        }

        protected void dgvDebitNote_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((ImageButton)e.Row.FindControl("imgbtnDelete")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);
                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('PrintDebitNote.aspx?id=" + dgvDebitNote.DataKeys[e.Row.RowIndex].Value.ToString() + "'); return false;");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void dgvDebitNote_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvDebitNote.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Accounts.TrnDebitNoteHeader objDebitNote = new TrnDebitNoteHeader();
            objDebitNote.Delete(id);
            PopulateHeaderGrid();
        }

        protected void ddlSupplierLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDueBills();
        }

        protected void LoadDueBills()
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            strValues = txtVoucherDate.Text;
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            {
                if (Validate())
                {
                    SaveVoucherDetails();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            }
            Message.Show = true;
        }

        private void SaveVoucherDetails()
        {
            BusinessLayer.Accounts.TrnDebitNoteHeader objDebitNote = new TrnDebitNoteHeader();
            Entity.Accounts.TrnDebitNoteHeader DebitNote = new Entity.Accounts.TrnDebitNoteHeader();
            DebitNote.DNHeaderID = DNHeaderID;
            DebitNote.CompanyID_FK=Convert.ToInt32(Session["CompanyId"].ToString().Trim());
            DebitNote.FinYearID_FK=Convert.ToInt32(Session["FinYrID"].ToString().Trim());
            DebitNote.BranchID_FK=Convert.ToInt32(Session["BranchId"].ToString().Trim());
            DebitNote.DNVoucherDate=Convert.ToDateTime(txtVoucherDate.Text.Trim() + " 00:00:00");
            DebitNote.SupplierLedgerID_FK=Convert.ToInt32(ddlSupplierLedger.SelectedValue.Trim());
            DebitNote.TotalAmount=Convert.ToDecimal(txtTotalAmt.Text.Trim());
            DebitNote.DNNarration=txtNarration.Text.Trim();
            DebitNote.OperationBy=Convert.ToInt32(Session["UserId"].ToString().Trim());
            DebitNote.XMLDebitNoteDetails= PrepareXMLString();

            int RowsAffected = objDebitNote.Save(DebitNote);
            if (RowsAffected != -1)
            {
                PopulateHeaderGrid();
                LoadDueBills();
                txtNarration.Text = "";
                DataTable DT = objDebitNote.GetAllById(DebitNote.DNHeaderID);
                txtVchNo.Text = DT.Rows[0]["DNVoucherNo"].ToString();

                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";
                btnPrint.Attributes.Add("onclick", "javascript:openPopup('PrintDebitNote.aspx?id=" + DebitNote.DNHeaderID + "'); return false;");
            }
            
            Message.Show = true;
        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            int SrlNo = 1;
            decimal Amount = 0;

            strXMLString = "<NewDataSet>";
            foreach (GridViewRow DGV in dgvBill.Rows)
            {
                if (DGV.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                    Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                    if (Amount > 0)
                    {
                        strXMLString += "<TrnDebitNoteDetail";
                        strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
                        strXMLString += " PurchaseBillId_FK = \"" + dgvBill.DataKeys[DGV.RowIndex].Value.ToString() + "\"";
                        strXMLString += " Amount = \"" + Amount + "\"";
                        strXMLString += " />";
                        SrlNo += 1;
                    }
                }
            }

            strXMLString += "</NewDataSet>";
            return strXMLString;
        }

        protected bool Validate()
        {
            bool result = true;
            string error = "";
            strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                error = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
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
    }
}