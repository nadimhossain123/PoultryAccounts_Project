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
    public partial class AgentSMSPaymentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" + DateTime.Now.Year.ToString();
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadAgentList();
                    LoadPaymentList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadAgentList()
        {
            BusinessLayer.Common.AgentMaster objAgentMaster = new BusinessLayer.Common.AgentMaster();
            DataTable DT = objAgentMaster.GetAll("");

            ddlAgent.DataSource = DT;
            ddlAgent.DataBind();
            ddlAgent.Items.Insert(0, new ListItem("All", "0"));
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPaymentList();
        }

        private void LoadPaymentList()
        {
            int AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);

            bool? IsApproved = null;

            if (ddlApprovalStatus.SelectedIndex == 1)
                IsApproved = true;
            else if (ddlApprovalStatus.SelectedIndex == 2)
                IsApproved = false;

            BusinessLayer.Common.SMSPayment objSMSPayment = new BusinessLayer.Common.SMSPayment();
            DataTable dt = objSMSPayment.GetAgentWisePaymentReport(AgentId, FromDate, ToDate, IsApproved);

            decimal TotalPaymentAmount = 0;

            dt.AsEnumerable().ToList().ForEach(row =>
            {
                TotalPaymentAmount += Convert.ToDecimal(row["PaymentAmount"].ToString());
            });

            dgvPaymentReport.DataSource = dt;
            dgvPaymentReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                dgvPaymentReport.FooterRow.Cells[6].Text = TotalPaymentAmount.ToString("F2");
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
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=AreaManagerSMSPaymentReport.xls");
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