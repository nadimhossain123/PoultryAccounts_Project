using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Accounts;

namespace AccountsModule.Accounts
{
    public partial class PopupMemberFeesCollection : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("---SELECT---", "0");
        ListItem liS = new ListItem(" ", "0");
        int rowCnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ClearControls();
                if (Request.QueryString["MemberId"] != null && Request.QueryString["MemberId"].ToString().Length > 0)
                {
                    ddlMember.SelectedValue = Request.QueryString["MemberId"].ToString();
                    btnSearch_Click(btnSearch, e);
                    lblMemberName.Visible = false;
                    ddlMember.Enabled = false;
                    btnSearch.Visible = false;
                    ddlCashBankLedger.Focus();
                }
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadMember();
        }

        protected void LoadMember()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            membermaster.BlockId = 0;
            membermaster.DistrictId = 0;
            membermaster.StateId = 0;
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.MemberMaster_GetAll_ForReport(membermaster);
            if (dt != null)
            {
                ddlMember.DataSource = dt;
                ddlMember.DataBind();
            }

            //ddlMember.Items.Insert(0, li);
        }

        protected void PopulateMembers(object sender, EventArgs e)
        {
            LoadMember();
        }

        protected void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            DataTable dt = gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerBNKandCASH", strParams);

            if (dt != null)
            {
                ddlCashBankLedger.DataSource = dt;
                ddlCashBankLedger.DataBind();
            }
            ddlCashBankLedger.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            Message.Show = false;
            txtReceiptNo.Text = "Auto Generated";
            txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtChequeNo.Text = "";
            txtDrawnOn.Text = "";
            txtChequeDate.Text = "";
            ltrLedgerBalance.Text = "";
            txtNarration.Text = "";
            ddlReceiptMode.SelectedIndex = 0;
            lblDropout.Visible = false;

            txtChequeNo.Enabled = false;
            txtDrawnOn.Enabled = false;
            txtChequeDate.Enabled = false;

            dgvFeesHead.DataSource = null;
            dgvFeesHead.DataBind();
            Session["State"] = 0;
            PopulateDropDownLists();
            LoadCashBankLedger();
            txtTotalAmt.Text = "0.00";
            txtFeesBookNo.Text = string.Empty;
            btnPrint.Attributes.Add("onclick", "javascript:alert('No Money Receipt To Print'); return false;");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["State"].ToString() == "0")
            {
                if (ddlMember.SelectedValue != "0" && ddlMember.Text != string.Empty)
                {
                    BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                    int MemberId = Convert.ToInt32(ddlMember.SelectedValue.Trim());

                    DataSet ds = ObjFees.GetStudentUnpaidTrans(MemberId, Convert.ToInt32(Session["FinYrID"].ToString()));
                    //if (ds.Tables[0].Rows.Count > 0)
                    //    ImgPhoto.ImageUrl = "../Student/" + ds.Tables[0].Rows[0]["Photo"].ToString();


                    dgvFeesHead.DataSource = ds.Tables[1];
                    dgvFeesHead.DataBind();

                    Message.Show = false;
                    rowCnt = dgvFeesHead.Rows.Count;
                    dgvFeesHead.Focus();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Select Member";
                    Message.Show = true;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                //{
                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                Entity.Accounts.StudentFeesCollection Fees = new Entity.Accounts.StudentFeesCollection();
                Fees.PaymentId = 0; //Always Save
                Fees.StudentId = Convert.ToInt32(ddlMember.SelectedValue.Trim());
                //Fees.SemNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
                Fees.Amount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                Fees.PaymentDate = Convert.ToDateTime(txtVoucherDate.Text.Trim() + " 00:00:00");
                Fees.CashBankLedgerID = Convert.ToInt32(ddlCashBankLedger.SelectedValue.Trim());
                Fees.TransactionType = "RECEIVE";
                Fees.ModeOfPayment = ddlReceiptMode.SelectedValue.Trim();
                Fees.ChequeNo = txtChequeNo.Text.Trim();

                if (txtChequeDate.Text.Trim().Length == 0)
                    Fees.ChequeDate = null;
                else
                    Fees.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim());

                Fees.DrawnOn = txtDrawnOn.Text.Trim();
                Fees.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                Fees.CompanyId = int.Parse(Session["CompanyId"].ToString());
                Fees.BranchId = int.Parse(Session["BranchId"].ToString());
                Fees.FinYrId = int.Parse(Session["FinYrID"].ToString());
                Fees.FeesBookNumber = txtFeesBookNo.Text.Trim();
                Fees.Narration = txtNarration.Text.Trim();

                DataTable DT = new DataTable();
                DT.Columns.Add("FeesHeadId", typeof(int));
                DT.Columns.Add("Amount", typeof(decimal));
                DataRow DR;

                foreach (GridViewRow DGV in dgvFeesHead.Rows)
                {
                    if (DGV.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                        decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                        if (Amount > 0)
                        {
                            DR = DT.NewRow();
                            DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[DGV.RowIndex].Values["id"].ToString());
                            DR["Amount"] = Amount;
                            DT.Rows.Add(DR);
                            DT.AcceptChanges();
                        }
                    }
                }

                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(DT);
                    Fees.PaymentDetailsXML = ds.GetXml().Replace("Table1>", "Table>");
                }

                Fees.XMLCashBankVoucherDetails = PrepareXMLString();
                Fees.IsRefund = false;

                ObjFees.Save(Fees);
                Fees = ObjFees.GetAllById(Fees.PaymentId);
                btnSearch_Click(sender, e);
                GetLedgerBalance();
                txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtTotalAmt.Text = "0.00";

                Message.IsSuccess = true;
                Message.Text = "Money Receipt No " + Fees.MoneyReceiptNo + " is generated. You can take print out now";
                txtReceiptNo.Text = Fees.MoneyReceiptNo;
                //-----------------------------------------------------------Add On 08-08-2013
                btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + Fees.PaymentId + "&refund=0'); return false;");
                //if (txtReceiptNo.Text.ToString().Substring(4, 4) == "ENGG")
                //{
                //    btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + Fees.PaymentId + "&refund=0'); return false;");
                //}
                //else if (txtReceiptNo.Text.Substring(4, 4) == "DEPL")
                //{
                //    btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceiptDiploma.aspx?id=" + Fees.PaymentId + "&refund=0'); return false;");
                //}
                //else
                //{
                //    btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceiptMgmnt.aspx?id=" + Fees.PaymentId + "&refund=0'); return false;");
                //}
                //------------------------------------------------------------
                //-------------------------CLEAR PAGE---------------------Add on 14-08-2013
                txtReceiptNo.Text = "Auto Generated";
                txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtChequeNo.Text = "";
                txtDrawnOn.Text = "";
                txtChequeDate.Text = "";
                ltrLedgerBalance.Text = "";
                txtNarration.Text = "";
                ddlReceiptMode.SelectedIndex = 0;

                txtChequeNo.Enabled = false;
                txtDrawnOn.Enabled = false;
                txtChequeDate.Enabled = false;

                dgvFeesHead.DataSource = null;
                dgvFeesHead.DataBind();
                Session["State"] = 0;
                //PopulateDropDownLists();
                LoadCashBankLedger();
                txtTotalAmt.Text = "0.00";
                txtFeesBookNo.Text = string.Empty;
                //--------------------------------------------------------

                //}
                //else
                //{
                //    Message.IsSuccess = false;
                //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
                //}
            }
            Message.Show = true;

        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            int SrlNo = 1;
            string ByTo = "PAYMENT";
            decimal DRAmount = 0;

            strXMLString = "<NewDataSet>";
            foreach (GridViewRow DGV in dgvFeesHead.Rows)
            {
                if (DGV.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                    decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                    if (Amount > 0)
                    {
                        strXMLString += "<TrnCashBankVoucherDetail";
                        strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
                        strXMLString += " ByTo = \"" + ByTo + "\"";
                        strXMLString += " LedgerID = \"" + dgvFeesHead.DataKeys[DGV.RowIndex].Values["AssestLedgerID_FK"].ToString() + "\"";
                        strXMLString += " DRAmount = \"" + DRAmount + "\"";
                        strXMLString += " CRAmount = \"" + Amount + "\"";
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
            bool result = false;
            string ErrorText = "";
            string strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlCashBankLedger.SelectedValue == "0" || ddlCashBankLedger.Text == string.Empty)
            {
                ErrorText = "Please Select Cash/Bank Ledger.";
                result = false;
            }
            else if (ddlMember.SelectedValue == "0" || ddlMember.Text == string.Empty)
            {
                ErrorText = "Please Select Student.";
                result = false;
            }
            else if (ddlReceiptMode.SelectedValue == "CHEQUE")
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
                ErrorText = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            //else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
            //{
            //    result = false;
            //    ErrorText = "Sorry! Future Voucher Date is Not Allowed.";
            //}
            else { result = true; }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void ddlReceiptMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceiptMode.SelectedValue == "CASH")
            {
                txtChequeNo.Text = "";
                txtDrawnOn.Text = "";
                txtChequeDate.Text = "";

                txtChequeNo.Enabled = false;
                txtDrawnOn.Enabled = false;
                txtChequeDate.Enabled = false;
                txtFeesBookNo.Focus();
            }
            else if (ddlReceiptMode.SelectedValue == "CHEQUE")
            {
                txtChequeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtChequeNo.Enabled = true;
                txtDrawnOn.Enabled = true;
                txtChequeDate.Enabled = true;
                txtChequeNo.Focus();
            }
        }

        protected void ddlCashBankLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLedgerBalance();
            dgvFeesHead.Focus();
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

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeydown", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
                Session["State"] = 1;
            }
        }
    }
}