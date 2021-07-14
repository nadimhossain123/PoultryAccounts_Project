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
    public partial class DevelopmentFeePaymentReport : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        decimal TotalDevelopmentFee;

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
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Any Member--", "0"));
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

            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dt = objMemberPayment.GetDevelopmentFeesPaymentReport(PaymentNo, CashBankLedgerId, PaymentMode, FromDate, ToDate, MemberId);
            
            TotalDevelopmentFee = 0;

            dt.AsEnumerable().ToList().ForEach(row =>
            {
                TotalDevelopmentFee += Convert.ToDecimal(row["DevelopmentFeesPaymentAmount"].ToString());
            });

            dgvPaymentReport.DataSource = dt;
            dgvPaymentReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                dgvPaymentReport.FooterRow.Cells[6].Text = TotalDevelopmentFee.ToString("F2");
                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadPaymentList();
            dgvPaymentReport.AllowPaging = false;
            dgvPaymentReport.DataBind();
            dgvPaymentReport.FooterRow.Cells[6].Text = TotalDevelopmentFee.ToString("F2");

            PrepareGridViewForExport(dgvPaymentReport);
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=DevelopmentFeesPaymentReport.xls");
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

        protected void dgvPaymentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaymentReport.PageIndex = e.NewPageIndex;
            LoadPaymentList();
        }
    }
}