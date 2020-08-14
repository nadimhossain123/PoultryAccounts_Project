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
    public partial class MemberPaymentReport : System.Web.UI.Page
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
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" + DateTime.Now.Year.ToString();
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadCashBankLedger();
                    LoadMemberList();
                    LoadBusinessType();
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
        
        private void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            membermaster.BlockId = 0;
            membermaster.DistrictId = 0;
            membermaster.StateId = 0;

            //membermaster.BlockId = Session["UserType"].ToString().Equals("Agent") ? Convert.ToInt32(Session["BlockId"]) : 0;
            //membermaster.DistrictId = Session["UserType"].ToString().Equals("Agent") ? Convert.ToInt32(Session["DistrictId"]) : 0;
            //membermaster.StateId = Session["UserType"].ToString().Equals("Agent") ? Convert.ToInt32(Session["StateId"]) : 0;
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Any Member--", "0"));

            if (Session["UserType"].ToString().Equals("Member"))
            {
                ddlMember.SelectedValue = Session["UserId"].ToString();
                ddlMember.Enabled = false;
            }
            else
            {
                ddlMember.Enabled = true;
            }
        }

        protected void LoadBusinessType()
        {
            BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
            DataTable dt = objBusinessType.GetAll();
            if (dt != null)
            {
                ddlBusinessType.DataSource = dt;
                ddlBusinessType.DataTextField = "BusinessTypeName";
                ddlBusinessType.DataValueField = "BusinessTypeId";
                ddlBusinessType.DataBind();
            }
            InsertFisrtItem(ddlBusinessType, "--Select Business Type--");
        }
        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
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
            int MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            int BusinessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue);
            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dt = objMemberPayment.GetFeesHeadWisePaymentReport(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, MemberId, BusinessTypeId);
            DataView dv = new DataView(dt);

            if (Session["UserType"].ToString().Equals("Agent"))
                dv.RowFilter = "CreatedBy = " + Session["UserId"].ToString() + " AND CreatedByUserType = 'Agent'";

            dt = dv.ToTable();

            decimal TotalAdmissionFees = 0, TotalAdmissionFeesTax = 0, TotalRenewalFees = 0, TotalRenewalFeesTax = 0, Total = 0, TotalDevelopmentFee = 0;

            dt.AsEnumerable().ToList().ForEach(row =>
            {
                Total += Convert.ToDecimal(row["PaymentAmount"].ToString());
                TotalAdmissionFees += Convert.ToDecimal(row["AdmissionFeesPaymentAmount"].ToString());
                TotalRenewalFees += Convert.ToDecimal(row["RenewalFeesPaymentAmount"].ToString());
                TotalAdmissionFeesTax += Convert.ToDecimal(row["AdmissionFeesTaxPaymentAmount"].ToString());
                TotalRenewalFeesTax += Convert.ToDecimal(row["RenewalFeesTaxPaymentAmount"].ToString());
                TotalDevelopmentFee += Convert.ToDecimal(row["DevelopmentPaymentAmount"].ToString());
            });


            dgvPaymentReport.DataSource = dt;
            dgvPaymentReport.DataBind();
            if (dt.Rows.Count > 0)
            {
                dgvPaymentReport.FooterRow.Cells[6].Text = TotalAdmissionFees.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[7].Text = TotalAdmissionFeesTax.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[8].Text = TotalRenewalFees.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[9].Text = TotalRenewalFeesTax.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[10].Text = TotalDevelopmentFee.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[11].Text = Total.ToString("F2");

                btnDownload.Visible = true;
            }
            else
                btnDownload.Visible = false;
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMember.SelectedIndex = 0;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            PrepareGridViewForExport(dgvPaymentReport);
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 2].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 3].Visible = false;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberPaymentReport.xls");
            Response.ContentType = "application/excel";
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
                //new for online payment status
                string OrderStatus = ((DataTable)dgvPaymentReport.DataSource).Rows[e.Row.RowIndex]["OrderStatus"].ToString();
                ((ImageButton)e.Row.FindControl("ImgEdit")).Attributes.Add("OnClick", "openpopup('MemberPayment.aspx?PaymentId=" + PaymentId + "'); return false;");
                ((Button)e.Row.FindControl("btnApprove")).CommandArgument = PaymentId.ToString();

                if (!string.IsNullOrEmpty(strIsApproved.Trim()))
                {
                    e.Row.Cells[13].Text = "Approved";

                    //if (!Session["UserType"].ToString().Equals("Admin"))
                    //{
                        ((ImageButton)e.Row.FindControl("ImgEdit")).Visible = false;
                        ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = false;
                    //}
                    if (Session["UserType"].ToString().Equals("Admin"))
                    {
                        ((ImageButton)e.Row.FindControl("ImgEdit")).Visible = true;
                        ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = true;
                    }
                }

                if (((Button)e.Row.FindControl("btnApprove")) != null)
                {
                    if (string.IsNullOrEmpty(strIsApproved.Trim()) && Session["UserType"].ToString().Equals("Admin"))
                        ((Button)e.Row.FindControl("btnApprove")).Visible = true;
                    else
                        ((Button)e.Row.FindControl("btnApprove")).Visible = false;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {                    
                    Button btnPayment = (Button)e.Row.FindControl("btnPrint");
                    btnPayment.Attributes.Add("OnClick", "openpopup('renewal-bill.aspx?PaymentNo=" + PaymentNo + "'); return false;");
                }

                //Newly added for delete & edit for Payment Status 
                if (OrderStatus!=null && OrderStatus.ToUpper().Equals("SUCCESS"))
                {
                    ((ImageButton)e.Row.FindControl("ImgEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("ImgDelete")).Visible = false;
                }
            }
        }

        protected void dgvPaymentReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.Common.MemberPayment ObjMemberPayment = new BusinessLayer.Common.MemberPayment();
            int Id = Convert.ToInt32(dgvPaymentReport.DataKeys[e.RowIndex].Values["PaymentId"].ToString());
            ObjMemberPayment.Delete(Id);
            LoadPaymentList();
        }

        protected void dgvPaymentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaymentReport.PageIndex = e.NewPageIndex;
            LoadPaymentList();
        }

        protected void dgvPaymentReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Approve"))
            {
                BusinessLayer.Common.MemberPayment ObjMemberPayment = new BusinessLayer.Common.MemberPayment();
                ObjMemberPayment.Approve(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["UserId"].ToString()));
                LoadPaymentList();

                Message.IsSuccess = true;
                Message.Text = "Payment Approved";
                Message.Show = true;
            }
        }
    }
}