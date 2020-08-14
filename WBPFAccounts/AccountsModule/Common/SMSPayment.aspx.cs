using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLayer.Accounts;

namespace AccountsModule.Common
{
    public partial class SMSPayment : System.Web.UI.Page
    {
        public string FeesXml;
        string strParams;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        public int PaymentId
        {
            get { return Convert.ToInt32(ViewState["PaymentId"]); }
            set { ViewState["PaymentId"] = value; }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["SMSMemberId"] == null && Request.QueryString["PaymentId"] == null)
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Admin") && (Request.QueryString["SMSMemberId"] != null || Request.QueryString["PaymentId"] != null))
                this.MasterPageFile = "../EmptyMaster.master";
            else if (Session["UserType"].ToString().Equals("Agent") && (Request.QueryString["SMSMemberId"] != null || Request.QueryString["PaymentId"] != null))
                this.MasterPageFile = "../EmptyMaster.master";
            else if (Session["UserType"].ToString().Equals("Member"))
                this.MasterPageFile = "../MasterMember.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    LoadCashBankLedger();
                    LoadSMSMemberList();

                    if (Request.QueryString["PaymentId"] != null && Request.QueryString["PaymentId"].Trim().Length > 0)
                    {
                        PaymentId = Convert.ToInt32(Request.QueryString["PaymentId"].ToString());
                        LoadPaymentDetail();
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        ResetControl();
                        btnPrint.Visible = false;
                    }

                    LoadSMSCategoryWiseDetail();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ResetControl()
        {
            PaymentId = 0;
            txtPaymentNo.Text = "System Generate";

            if (Request.QueryString["SMSMemberId"] != null && Request.QueryString["SMSMemberId"].Trim().Length > 0)
            {
                ddlSMSMember.SelectedValue = Request.QueryString["SMSMemberId"].ToString();
                ddlSMSMember.Enabled = false;
                txtMobileNo.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                ddlSMSMember.SelectedIndex = 0;
                ddlSMSMember.Enabled = true;
                txtMobileNo.Enabled = true;
                btnSearch.Enabled = true;
            }

            txtPaymentAmount.Text = "0.00";
            txtPaymentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarExtenderPaymentDate.Enabled = true;
            
            txtSubscriptionEndDate.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");

            if (Session["UserType"].ToString().Equals("Admin"))
            {
                txtSubscriptionEndDate.Enabled = true;
            }
            else
            {
                txtSubscriptionEndDate.Enabled = false;
            }

            lblOpeningBalance.Text = string.Empty;
            txtNarration.Text = string.Empty;

            if (Session["UserType"].ToString().Equals("Agent"))
            {
                ddlPaymentMode.SelectedIndex = 0;
                ddlPaymentMode.Enabled = false;

                ddlCashBankLedger.SelectedValue = Session["AgentLedgerId"].ToString();
                ddlCashBankLedger.Enabled = false;
            }
            else if (Session["UserType"].ToString().Equals("Member"))
            {
                ddlPaymentMode.SelectedIndex = 4;
                ddlPaymentMode.Enabled = false;

                ddlCashBankLedger.SelectedValue = System.Configuration.ConfigurationManager.AppSettings["SBI_ONLINE_LEDGER_ID"].Trim();
                ddlCashBankLedger.Enabled = false;
            }
            else
            {
                ddlPaymentMode.Enabled = true;
                ddlCashBankLedger.Enabled = true;
            }

            Message.Show = false;
        }

        private void LoadPaymentDetail()
        {
            BusinessLayer.Common.SMSPayment ObjSMSPayment = new BusinessLayer.Common.SMSPayment();
            DataTable dtPayment = ObjSMSPayment.GetAllById(PaymentId);

            txtPaymentNo.Text = dtPayment.Rows[0]["PaymentNo"].ToString();
            ddlSMSMember.SelectedValue = dtPayment.Rows[0]["SMSMemberId"].ToString();
            ddlSMSMember.Enabled = false;
            txtMobileNo.Enabled = false;
            btnSearch.Enabled = false;

            txtPaymentAmount.Text = dtPayment.Rows[0]["PaymentAmount"].ToString();
            ddlPaymentMode.SelectedValue = dtPayment.Rows[0]["PaymentMode"].ToString();
            txtPaymentDate.Text = Convert.ToDateTime(dtPayment.Rows[0]["PaymentDate"].ToString()).ToString("dd/MM/yyyy");
            CalendarExtenderPaymentDate.Enabled = false;
            txtSubscriptionEndDate.Text= Convert.ToDateTime(dtPayment.Rows[0]["SMSSubscriptionEndDate"].ToString()).ToString("dd/MM/yyyy");

            ddlCashBankLedger.SelectedValue = dtPayment.Rows[0]["CashBankLedgerId"].ToString();
            LoadLedgerOpeningBalance();

            txtNarration.Text = dtPayment.Rows[0]["Narration"].ToString();

            if (Session["UserType"].ToString().Equals("Admin"))
            {
                txtSubscriptionEndDate.Enabled = true;
            }
            else
            {
                txtSubscriptionEndDate.Enabled = false;
            }

            if (Session["UserType"].ToString().Equals("Agent") || Session["UserType"].ToString().Equals("Member"))
            {
                ddlPaymentMode.Enabled = false;
                ddlCashBankLedger.Enabled = false;
            }
            else
            {
                ddlPaymentMode.Enabled = true;
                ddlCashBankLedger.Enabled = true;
            }

            Message.Show = false;
        }

        private void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindDropDownColumnsBySP(ddlCashBankLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlCashBankLedger.Items.Insert(0, new ListItem("Select Ledger","0"));
        }

        private void LoadSMSMemberList()
        {
            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();

            DataTable dt = objSMSMember.GetAllMember("", "", 0, 0,0);
            if (dt != null && dt.Rows.Count > 0)
                dt.AsEnumerable().ToList().ForEach(r => r["MemberName"] = r["MemberName"].ToString() + " [" + r["MemberCode"].ToString() + "] [" + r["MobileNo"].ToString() + "]");

            DataView dv = new DataView(dt);
            string DataViewFilter = "IsNull(IsActive,0) = 1 And MemberType = 'PAID'";

            if (txtMobileNo.Text.Trim().Length > 0)
                DataViewFilter = DataViewFilter + " And MobileNo = '" + txtMobileNo.Text.Trim() + "'";

            if (Session["UserType"].ToString().Equals("Member"))
            {
                DataViewFilter += " AND ParentMemberId = " + Session["UserId"].ToString();
            }

            dv.RowFilter = DataViewFilter;

            ddlSMSMember.DataSource = dv;
            ddlSMSMember.DataBind();
            ddlSMSMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        private void LoadSMSCategoryWiseDetail()
        {            
            int SMSMemberId = Convert.ToInt32(ddlSMSMember.SelectedValue);

            if (SMSMemberId != 0)
            {
                BusinessLayer.Common.SMSPayment objSMSPayment = new BusinessLayer.Common.SMSPayment();
                DataTable dt = objSMSPayment.GetCategoryWiseDetail(SMSMemberId, PaymentId);
                dgvSMS.DataSource = dt;
                dgvSMS.DataBind();
            }
            else
            {
                dgvSMS.DataSource = null;
                dgvSMS.DataBind();
            }
        }
                        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin") || Session["UserType"].ToString().Equals("Agent"))
                Save(true);
            else
                Save(null);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VR", "window.open('sms-payment-receipt.aspx?No=" + txtPaymentNo.Text + "','','height=600,width=1000')", true);
        }

        protected void ddlSMSMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSMSCategoryWiseDetail();
        }

        protected void ddlCashBankLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLedgerOpeningBalance();
        }

        private void LoadLedgerOpeningBalance()
        {
            if (ddlCashBankLedger.SelectedIndex > 0 && Session["UserType"].ToString().Equals("Admin"))
            {
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
                        lblOpeningBalance.Text = "<i>Cur Bal.   </i><b style='color:Red;'>" + Math.Abs(ClosingBalance).ToString("n") + " Cr</b>";
                    else
                        lblOpeningBalance.Text = "<i>Cur Bal.   </i><b style='color:#259D17;'>" + Math.Abs(ClosingBalance).ToString("n") + " Dr</b>";
                }
            }
            else
            {
                lblOpeningBalance.Text = string.Empty;
            }
        }

        protected void dgvSMS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int FeesPaymentId = Convert.ToInt32(dgvSMS.DataKeys[e.Row.RowIndex].Values["FeesPaymentId"].ToString());
                CheckBox ChkSelect = (CheckBox)e.Row.FindControl("ChkSelect");
                TextBox txtTaxPaymentAmount = (TextBox)e.Row.FindControl("txtTaxPaymentAmount");

                ChkSelect.Checked = (FeesPaymentId != 0) ? true : false;
                
                if (PaymentId != 0) //Edit Mode
                {
                    ChkSelect.Enabled = false;
                    txtTaxPaymentAmount.Enabled = false;
                }
                else
                {
                    ChkSelect.Enabled = true;
                    txtTaxPaymentAmount.Enabled = true;
                }
            }
        }

        private void Save(bool? IsApproved)
        {
            Message.Show = false;
            FeesXml = "<NewDataSet>";

            foreach (GridViewRow gvr in dgvSMS.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)gvr.FindControl("ChkSelect")).Checked)
                    {
                        FeesXml += "<Row";
                        FeesXml += "  SMSCategoryId = \"" + dgvSMS.DataKeys[gvr.RowIndex].Values["SMSCategoryId"].ToString() + "\"";
                        FeesXml += "  SMSAmount = \"" + gvr.Cells[2].Text + "\"";
                        //FeesXml += "  TaxAmount = \"" + gvr.Cells[5].Text + "\"";
                        FeesXml += "  TaxAmount = \"" + (string.IsNullOrEmpty(((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) ? "0" : ((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) + "\"";
                        FeesXml += " />";
                    }
                }
            }

            FeesXml += "</NewDataSet>";

            BusinessLayer.Common.SMSPayment objSMSPayment = new BusinessLayer.Common.SMSPayment();
            Entity.Common.SMSPayment payment = new Entity.Common.SMSPayment();
            payment.PaymentId = PaymentId;
            payment.SMSMemberId = Convert.ToInt32(ddlSMSMember.SelectedValue);
            payment.PaymentMode = ddlPaymentMode.SelectedValue;
            payment.PaymentDate = Convert.ToDateTime(txtPaymentDate.Text.Split('/')[2] + "-" + txtPaymentDate.Text.Split('/')[1] + "-" + txtPaymentDate.Text.Split('/')[0]);
            payment.SMSEndDate = Convert.ToDateTime(txtSubscriptionEndDate.Text.Split('/')[2] + "-" + txtSubscriptionEndDate.Text.Split('/')[1] + "-" + txtSubscriptionEndDate.Text.Split('/')[0]);
            payment.PaymentAmount = Convert.ToDecimal(txtPaymentAmount.Text.Trim());
            payment.Narration = txtNarration.Text.Trim();
            payment.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            payment.CreatedByUserType = Session["UserType"].ToString();
            payment.CashBankLedgerId = Convert.ToInt32(ddlCashBankLedger.SelectedValue);
            payment.FeesXml = FeesXml;
            payment.IsExcelUpload = false;

            if (IsApproved != null)
            {
                payment.IsApproved = true;
                payment.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                payment.ApprovedDate = DateTime.Now;
            }
            else
            {
                payment.IsApproved = null;
                payment.ApprovedBy = null;
                payment.ApprovedDate = null;
            }

            PaymentId = 0;

            string strPaymentNo = objSMSPayment.Save(payment);
            txtPaymentNo.Text = strPaymentNo;

            LoadSMSCategoryWiseDetail();

            txtPaymentAmount.Text = "0.00";
            CalendarExtenderPaymentDate.Enabled = true;
            txtNarration.Text = "";
            LoadLedgerOpeningBalance();

            Message.IsSuccess = true;
            Message.Text = "Payment detail saved successfully";
            Message.Show = true;
            btnPrint.Visible = true;

            if (Session["UserType"].ToString().Equals("Member") && ddlPaymentMode.SelectedItem.Text.ToUpper().Equals("ONLINE PAYMENT"))
            {
                Response.Redirect(@"https://www.onlinesbi.com/prelogin/icollecthome.htm?corpid=649959");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSMSMemberList();
        }
    }
}