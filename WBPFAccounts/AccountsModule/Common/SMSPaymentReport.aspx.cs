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
    public partial class SMSPaymentReport : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin"))
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Member"))
                this.MasterPageFile = "../MasterMember.master";
            else if (Session["UserType"].ToString().Equals("Agent"))
                this.MasterPageFile = "../MasterAgent.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadCashBankLedger();
                    LoadSMSMemberList();

                    Message.Show = false;
                    dgvPaymentReport.DataSource = null;
                    dgvPaymentReport.DataBind();
                    btnDownload.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            DataTable dt = gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerBNKandCASH", strParams);

            ddlCashBankLedger.DataSource = dt;
            ddlCashBankLedger.DataBind();
            ddlCashBankLedger.Items.Insert(0, new ListItem("--Any Ledger--", "0"));
        }
        
        private void LoadSMSMemberList()
        {
            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();

            DataTable dt = objSMSMember.GetAllMember("", "", 0, 0,0);
            DataView dv = new DataView(dt);
            string DataViewFilter = "IsNull(IsActive,0) = 1 And MemberType = 'PAID'";

            if (Session["UserType"].ToString().Equals("Member"))
            {
                DataViewFilter += " AND ParentMemberId = " + Session["UserId"].ToString();
            }

            dv.RowFilter = DataViewFilter;

            ddlSMSMember.DataSource = dv;
            ddlSMSMember.DataBind();
            ddlSMSMember.Items.Insert(0, new ListItem("--Any Member--", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPaymentList();
        }

        private void LoadPaymentList()
        {
            string PaymentNo = txtPaymentNo.Text.Trim();
            string PaymentMode = ddlPaymentMode.SelectedValue;
            int CashBankLedgerId = Convert.ToInt32(ddlCashBankLedger.SelectedValue);
            int SMSMemberId = Convert.ToInt32(ddlSMSMember.SelectedValue);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);

            BusinessLayer.Common.SMSPayment objSMSPayment = new BusinessLayer.Common.SMSPayment();
            DataTable dt = objSMSPayment.GetAll(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, SMSMemberId);
            DataView dv = new DataView(dt);

            if (Session["UserType"].ToString().Equals("Agent"))
                dv.RowFilter = "CreatedBy = " + Session["UserId"].ToString() + " AND CreatedByUserType = 'Agent'";
            else if (Session["UserType"].ToString().Equals("Member"))
                dv.RowFilter = "CreatedBy = " + Session["UserId"].ToString() + " AND CreatedByUserType = 'Member'";

            dt = dv.ToTable();

            decimal TotalAmount = 0, TotalFeesAmount = 0, TotalTaxAmount = 0;

            dt.AsEnumerable().ToList().ForEach(row =>
            {
                TotalAmount += Convert.ToDecimal(row["PaymentAmount"].ToString());
                TotalFeesAmount += Convert.ToDecimal(row["ReadyBirdPriceSMSAmount"].ToString());// +Convert.ToDecimal(row["NECCRateSMSAmount"].ToString());
                TotalTaxAmount += Convert.ToDecimal(row["ReadyBirdPriceSMSTaxAmount"].ToString());// +Convert.ToDecimal(row["NECCRateSMSTaxAmount"].ToString());
            });


            dgvPaymentReport.DataSource = dt;
            dgvPaymentReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                dgvPaymentReport.FooterRow.Cells[7].Text = TotalFeesAmount.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[8].Text = TotalTaxAmount.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[9].Text = TotalAmount.ToString("F2");

                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            PrepareGridViewForExport(dgvPaymentReport);
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=SMSPaymentReport.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvPaymentReport.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }

        private void PrepareGridViewForExport(Control gv)
        {
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Button))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void dgvPaymentReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PaymentId = Convert.ToInt32(dgvPaymentReport.DataKeys[e.Row.RowIndex].Values["PaymentId"].ToString());
                string PaymentNo = dgvPaymentReport.DataKeys[e.Row.RowIndex].Values["PaymentNo"].ToString();
                string strIsApproved = dgvPaymentReport.DataKeys[e.Row.RowIndex].Values["IsApproved"].ToString();

                ((ImageButton)e.Row.FindControl("ImgEdit")).Attributes.Add("OnClick", "openpopup('SMSPayment.aspx?PaymentId=" + PaymentId + "'); return false;");
                ((Button)e.Row.FindControl("btnApprove")).CommandArgument = PaymentId.ToString();

                if (!string.IsNullOrEmpty(strIsApproved.Trim()))
                {
                    e.Row.Cells[10].Text = "Approved";

                    if (!Session["UserType"].ToString().Equals("Admin"))
                    {
                        ((ImageButton)e.Row.FindControl("ImgEdit")).Visible = false;
                        ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = false;
                    }
                }

                if (((Button)e.Row.FindControl("btnApprove")) != null)
                {
                    if (string.IsNullOrEmpty(strIsApproved.Trim()) && Session["UserType"].ToString().Equals("Admin"))
                        ((Button)e.Row.FindControl("btnApprove")).Visible = true;
                    else
                        ((Button)e.Row.FindControl("btnApprove")).Visible = false;
                }

                Button btnPayment = (Button)e.Row.FindControl("btnPrint");
                btnPayment.Attributes.Add("OnClick", "openpopup('sms-payment-receipt.aspx?No=" + PaymentNo + "'); return false;");
            }
        }

        protected void dgvPaymentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaymentReport.PageIndex = e.NewPageIndex;
            LoadPaymentList();
        }

        protected void dgvPaymentReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.Common.SMSPayment ObjSMSPayment = new BusinessLayer.Common.SMSPayment();
            int Id = Convert.ToInt32(dgvPaymentReport.DataKeys[e.RowIndex].Values["PaymentId"].ToString());
            ObjSMSPayment.Delete(Id);
            LoadPaymentList();

            Message.IsSuccess = true;
            Message.Text = "Payment Deleted";
            Message.Show = true;
        }

        protected void dgvPaymentReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Approve"))
            {
                BusinessLayer.Common.SMSPayment ObjSMSPayment = new BusinessLayer.Common.SMSPayment();
                ObjSMSPayment.Approve(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["UserId"].ToString()));
                LoadPaymentList();

                Message.IsSuccess = true;
                Message.Text = "Payment Approved";
                Message.Show = true;
            }
        }
    }
}