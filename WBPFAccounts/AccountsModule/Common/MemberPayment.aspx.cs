using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLayer.Accounts;
using CCA.Util;

namespace AccountsModule.Common
{
    public partial class MemberPayment : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "FF119789EA009958AC9FF44758A5A526";//"4CA4B59FE178432099D0D78DC2B727FE";//"272ECCEC820303D21327C07B92A5A367";//put in the 32bit alpha numeric key in the quotes provided here 	
        //string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "AVCR83GC95AY39RCYA";//"AVYG02GC61CH78GYHC";// put the access key in the quotes provided here.

        public string FeesXml;
        string strParams;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        public int PaymentId
        {
            get { return Convert.ToInt32(ViewState["PaymentId"]); }
            set { ViewState["PaymentId"] = value; }
        }
        public decimal PaymentAmount
        {
            get { return Convert.ToDecimal(ViewState["PaymentAmount"]); }
            set { ViewState["PaymentAmount"] = value; }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["MemberId"] == null && Request.QueryString["PaymentId"] == null)
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Admin") && (Request.QueryString["MemberId"] != null || Request.QueryString["PaymentId"] != null))
                this.MasterPageFile = "../EmptyMaster.master";
            else if (Session["UserType"].ToString().Equals("Member"))
                this.MasterPageFile = "../MasterMember.master";
            else if (Session["UserType"].ToString().Equals("Agent") && (Request.QueryString["MemberId"] != null || Request.QueryString["PaymentId"] != null))
                this.MasterPageFile = "../EmptyMaster.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    LoadCashBankLedger();
                    LoadMemberList();
                    btnPrint.Visible = false;
                    if (Request.QueryString["PaymentId"] != null && Request.QueryString["PaymentId"].Trim().Length > 0)
                    {
                        PaymentId = Convert.ToInt32(Request.QueryString["PaymentId"].ToString());
                        LoadPaymentDetail();
                    }
                    else
                    {
                        ResetControl();
                    }
                    LoadMemberOutstandingList();
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

            if (Session["UserType"].ToString().Equals("Member"))
            {
                ddlMember.SelectedValue = Session["UserId"].ToString();
                ddlMember.Enabled = false;
                txtMobileNo.Enabled = false;
                btnSearch.Enabled = false;
            }
            else if (Request.QueryString["MemberId"] != null && Request.QueryString["MemberId"].Trim().Length > 0)
            {
                ddlMember.SelectedValue = Request.QueryString["MemberId"].ToString();
                ddlMember.Enabled = false;
                txtMobileNo.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                ddlMember.Enabled = true;
                txtMobileNo.Enabled = true;
                btnSearch.Enabled = true;
            }

            if (Session["UserType"].ToString().Equals("Agent"))
            {
                ddlPaymentMode.SelectedIndex = 0;
                ddlPaymentMode.Enabled = true;

                ddlCashBankLedger.SelectedValue = Session["AgentLedgerId"].ToString();
                ddlCashBankLedger.Enabled = false;
            }
            else if (Session["UserType"].ToString().Equals("Member"))
            {
                ddlPaymentMode.SelectedIndex = 4;
                ddlPaymentMode.Enabled = false;

                ddlCashBankLedger.SelectedValue = System.Configuration.ConfigurationManager.AppSettings["SBI_ONLINE_LEDGER_ID"].ToString();
                ddlCashBankLedger.Enabled = false;
                btnSave.Text = "PROCEED";
            }
            else
            {
                ddlPaymentMode.Enabled = true;
                ddlCashBankLedger.Enabled = true;
            }

            txtPaymentAmount.Text = "0.00";
            //ddlPaymentMode.SelectedIndex = 0;
            txtPaymentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPaymentDate.Enabled = true;

            lblOpeningBalance.Text = string.Empty;
            txtNarration.Text = string.Empty;
            Message.Show = false;
        }

        private void LoadPaymentDetail()
        {
            BusinessLayer.Common.MemberPayment ObjMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dtPayment = ObjMemberPayment.GetAllById(PaymentId);

            txtPaymentNo.Text = dtPayment.Rows[0]["PaymentNo"].ToString();
            ddlMember.SelectedValue = dtPayment.Rows[0]["MemberId"].ToString();
            ddlMember.Enabled = false;
            txtMobileNo.Enabled = false;
            btnSearch.Enabled = false;

            txtPaymentAmount.Text = dtPayment.Rows[0]["PaymentAmount"].ToString();
            ddlPaymentMode.SelectedValue = dtPayment.Rows[0]["PaymentMode"].ToString();
            hdnAmount.Value = dtPayment.Rows[0]["PaymentAmount"].ToString();

            if (Session["UserType"].ToString().Equals("Member"))//Session["UserType"].ToString().Equals("Agent") || 
            {
                ddlPaymentMode.Enabled = false;
            }
            else
            {
                ddlPaymentMode.Enabled = true;
            }

            txtPaymentDate.Text = Convert.ToDateTime(dtPayment.Rows[0]["PaymentDate"].ToString()).ToString("dd/MM/yyyy");
            txtPaymentDate.Enabled = false;

            ddlCashBankLedger.SelectedValue = dtPayment.Rows[0]["CashBankLedgerId"].ToString();

            if (Session["UserType"].ToString().Equals("Agent") || Session["UserType"].ToString().Equals("Member"))
            {
                ddlCashBankLedger.Enabled = false;
            }
            else
            {
                ddlCashBankLedger.Enabled = true;
            }
            LoadLedgerOpeningBalance();

            txtNarration.Text = dtPayment.Rows[0]["Narration"].ToString();
            Message.Show = false;
        }

        private void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindDropDownColumnsBySP(ddlCashBankLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);

            //if (!Session["UserType"].ToString().Equals("Admin"))
            //{
            //    string strPaymentLedgerIds = System.Configuration.ConfigurationManager.AppSettings["PAYMENT_LEDGER_ID"].Trim();
            //    List<ListItem> lstItem = new List<ListItem>();

            //    foreach (ListItem li in ddlCashBankLedger.Items)
            //    {
            //        if (!strPaymentLedgerIds.Split(',').Contains(li.Value))
            //        {
            //            lstItem.Add(li);
            //        }
            //    }

            //    foreach (ListItem li in lstItem)
            //    {
            //        ddlCashBankLedger.Items.Remove(li);
            //    }
            //}
            ddlCashBankLedger.Items.Insert(0, new ListItem("Select Ledger", "0"));
        }

        private void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            membermaster.BlockId = 0;
            membermaster.DistrictId = 0;
            membermaster.StateId = 0;
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            if (dt != null && dt.Rows.Count > 0)
                dt.AsEnumerable().ToList().ForEach(r => r["Name"] = r["Name"].ToString() + " [" + r["MemberCode"].ToString() + "] [" + r["MobileNo"].ToString() + "]");

            DataView dv = new DataView(dt);
            string strRowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            if (txtMobileNo.Text.Trim().Length > 0)
                strRowFilter = strRowFilter + " And MobileNo = '" + txtMobileNo.Text.Trim() + "'";

            dv.RowFilter = strRowFilter;
            dv.Sort = "MemberName ASC";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        private void LoadMemberOutstandingList()
        {
            int FinYrId = Convert.ToInt32(Session["FinYrID"].ToString());
            int MemberId = Convert.ToInt32(ddlMember.SelectedValue);

            if (MemberId != 0)
            {
                BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
                DataTable dt = objMemberPayment.GetOutstanding(MemberId, PaymentId, FinYrId);
                dgvMemberOutstanding.DataSource = dt;
                dgvMemberOutstanding.DataBind();
                PaymentAmount = 1;//Convert.ToDecimal(dt.Compute("Sum(FeesOutstandingAmount)", string.Empty)) +
                //Convert.ToDecimal(dt.Compute("Sum(TaxOutstandingAmount)", string.Empty));
            }
            else
            {
                dgvMemberOutstanding.DataSource = null;
                dgvMemberOutstanding.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            FeesXml = "<NewDataSet>";

            foreach (GridViewRow gvr in dgvMemberOutstanding.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    FeesXml += "<Row";
                    FeesXml += "  FeesHeadId = \"" + dgvMemberOutstanding.DataKeys[gvr.RowIndex].Values["FeesHeadId"].ToString() + "\"";
                    FeesXml += "  FeesPaymentAmount = \"" + (string.IsNullOrEmpty(((TextBox)gvr.FindControl("txtFeesPaymentAmount")).Text.Trim()) ? "0" : ((TextBox)gvr.FindControl("txtFeesPaymentAmount")).Text.Trim()) + "\"";
                    FeesXml += "  TaxPaymentAmount = \"" + (string.IsNullOrEmpty(((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) ? "0" : ((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) + "\"";
                    FeesXml += " />";
                }
            }

            FeesXml += "</NewDataSet>";

            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            Entity.Common.MemberPayment payment = new Entity.Common.MemberPayment();
            payment.PaymentId = PaymentId;
            payment.MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            payment.PaymentMode = ddlPaymentMode.SelectedValue;
            payment.PaymentDate = Convert.ToDateTime(txtPaymentDate.Text.Split('/')[2] + "-" + txtPaymentDate.Text.Split('/')[1] + "-" + txtPaymentDate.Text.Split('/')[0]);
            //if (Session["UserType"].ToString().Equals("Member")) { payment.PaymentAmount = PaymentAmount; }//PaymentAmount; }
            //else
            //{
                payment.PaymentAmount = Convert.ToDecimal(hdnAmount.Value.Trim());
            //}
            payment.Narration = txtNarration.Text.Trim();
            payment.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            payment.CreatedByUserType = Session["UserType"].ToString();
            payment.CashBankLedgerId = Convert.ToInt32(ddlCashBankLedger.SelectedValue);
            payment.FeesXml = FeesXml;
            payment.IsExcelUpload = false;

            if (Session["UserType"].ToString().Equals("Admin") || Session["UserType"].ToString().Equals("Agent"))
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

            string strPaymentNo = objMemberPayment.Save(payment);

            PaymentId = 0;
            txtPaymentNo.Text = strPaymentNo;

            LoadMemberOutstandingList();

            txtPaymentAmount.Text = "0.00";
            txtPaymentDate.Enabled = true;
            txtNarration.Text = "";
            LoadLedgerOpeningBalance();
            btnPrint.Visible = true;
            Message.IsSuccess = true;
            Message.Text = "Payment detail saved successfully";
            Message.Show = true;

            if (Session["UserType"].ToString().Equals("Member") && ddlPaymentMode.SelectedItem.Text.ToUpper().Equals("ONLINE PAYMENT"))
            {
                //Response.Redirect(@"https://www.onlinesbi.com/prelogin/icollecthome.htm?corpid=649959");
                BusinessLayer.Common.MemberPayment ObjPayment = new BusinessLayer.Common.MemberPayment();
                Entity.Common.PaymentGateway paymentGate = new Entity.Common.PaymentGateway();
                paymentGate.PaymentId = payment.PaymentId;
                paymentGate.MemberId = payment.MemberId;
                paymentGate.MemberType = "Member";
                paymentGate.OrderId = GetAutoTransactionId();
                paymentGate.PaymentAmount = payment.PaymentAmount;
                paymentGate.Currency = "INR";
                paymentGate.CreatedBy = payment.CreatedBy;
                ObjPayment.PaymentResponseSave(paymentGate);//&tid=76023071
                string ccaRequest = "merchant_id=211354&order_id=" + paymentGate.OrderId + "&amount=" + payment.PaymentAmount + "&currency=INR&"
                        + "redirect_url=http://accounts.wbpoultryfederation.org/ccavResponseHandler.aspx&cancel_url=http://accounts.wbpoultryfederation.org/MemberDefault.aspx&";
                //+ "redirect_url=http://localhost:1044/ccavResponseHandler.aspx&cancel_url=http://localhost:1044/ccavResponseHandler.aspx&";
                ccaRequest += "billing_name=" + ddlMember.SelectedItem.Text +
                    "&billing_address=46C, Chowringhee Road, 11th Floor, Room No - C&billing_city=Kolkata&billing_state=West Bengal&billing_zip=700071&billing_country=India&billing_tel=03365229085&billing_email=wbpoultryfederation@yahoo.in&"
                    + "delivery_name=" + ddlMember.SelectedItem.Text + "&delivery_address=46C, Chowringhee Road, 11th Floor, Room No - C&delivery_city=Kolkata&delivery_state=West Bengal&delivery_zip=700071&delivery_country=India&delivery_tel=03365229085"
                    + "&merchant_param1=" + payment.PaymentId + "&merchant_param2=" + payment.MemberId + "&merchant_param3=Member";
                string strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
                Response.Redirect("../ccavRequestHandler.aspx?DATA=" + strEncRequest);
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "window.open('renewal-bill.aspx?PaymentNo=" + strPaymentNo + "','','height=600,width=1000')", true);
        }

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMemberOutstandingList();
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
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string strPaymentNo = txtPaymentNo.Text.Trim();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "window.open('renewal-bill.aspx?PaymentNo=" + strPaymentNo + "','','height=600,width=1000')", true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }
        private string GetAutoTransactionId()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}