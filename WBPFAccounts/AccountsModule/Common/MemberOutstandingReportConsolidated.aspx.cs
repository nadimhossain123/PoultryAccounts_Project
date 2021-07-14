using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace AccountsModule.Common
{
    public partial class MemberOutstandingReportConsolidated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    dgvMemberOutstanding.DataSource = null;
                    dgvMemberOutstanding.DataBind();
                    btnSendEmail.Visible = false;
                    btnDownload.Visible = false;
                    Message.Show = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadStates();
            LoadDistricts();
            LoadBlock();
            LoadMembershipCategory();
            LoadBusinessType();
        }

        protected void LoadStates()
        {
            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            DataTable dtState = new DataTable();
            dtState = objState.GetAll();
            if (dtState != null)
            {
                ddlState.DataSource = dtState;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
            }
            InsertFisrtItem(ddlState, "Any State");
        }

        protected void LoadDistricts()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dtDistrict = new DataTable();

            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            dtDistrict = objDistrict.GetAll(stateid);
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
            InsertFisrtItem(ddlDistrict, "Any District");
        }

        protected void LoadBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();

            int districtid = (ddlDistrict.SelectedIndex == 0) ? 0 : int.Parse(ddlDistrict.SelectedValue);
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            DataTable dt = objBlock.GetAll(districtid, stateid);
            if (dt != null)
            {
                ddlBlock.DataSource = dt;
                ddlBlock.DataTextField = "BlockName";
                ddlBlock.DataValueField = "BlockId";
                ddlBlock.DataBind();
            }
            InsertFisrtItem(ddlBlock, "Any Block");
        }

        protected void LoadMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            DataTable dt = objMembershipCategory.GetAll();
            if (dt != null)
            {
                ddlMembershipCategory.DataSource = dt;
                ddlMembershipCategory.DataTextField = "CategoryName";
                ddlMembershipCategory.DataValueField = "MembershipCategoryId";
                ddlMembershipCategory.DataBind();
            }
            InsertFisrtItem(ddlMembershipCategory, "Any Category");
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


        private void LoadConsolidatedOutstandingList()
        {
            int StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            int CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            //string MemberName = txtMemberName.Text.Trim();
            string MemberName = "";
            int BusinessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue); //14/10/19-business type fieldfor search
            int ReportType= Convert.ToInt32(ddlReportType.SelectedValue.Trim()); 
            
            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataSet ds = objMemberPayment.GetOutstandingReportConsolidated(StateId, DistrictId, BlockId, CategoryId, ToDate, FromDate, MemberName,"","", BusinessTypeId, ReportType);

            dgvMemberOutstanding.DataSource = ds.Tables[0];
            dgvMemberOutstanding.DataBind();

            Label1.Text = ds.Tables[1].Rows[0]["GrandTotal"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnDownload.Visible = true;
                btnSendEmail.Visible = true;
                Label1.Text = "Grand Total : ";
                Label2.Text = ds.Tables[1].Rows[0]["GrandTotal"].ToString();
            }
            else
            {
                btnDownload.Visible = false;
                btnSendEmail.Visible = false;
                Label1.Text = "Grand Total : ";
                Label2.Text = "0.00";
            }
            


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadConsolidatedOutstandingList();
            Message.Show = false;
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvMemberOutstanding.Rows.Count; i++)
            {
                if (((CheckBox)dgvMemberOutstanding.Rows[i].FindControl("ChkSelect")).Checked)
                {
                    string ToAddress = dgvMemberOutstanding.Rows[i].Cells[8].Text.Trim();
                    if (!string.IsNullOrEmpty(ToAddress))
                    {
                        string MessageContent = GetMessageContent(dgvMemberOutstanding.Rows[i]);
                        SendEmail(ToAddress, MessageContent);
                    }
                }
            }

            Message.IsSuccess = true;
            Message.Text = "Mail Sent Successfully";
            Message.Show = true;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadConsolidatedOutstandingList();
            dgvMemberOutstanding.AllowPaging = false;

            dgvMemberOutstanding.DataBind();
            PrepareGridViewForExport(dgvMemberOutstanding);
            //dgvMemberOutstanding.Columns[0].Visible = false;
            
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberConsolidatedOutstandingReport.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvMemberOutstanding.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
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

        

        private string GetMessageContent(GridViewRow GVR)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='font-family:calibri; font-size:14px;'>");
            sb.Append("Dear Member,<br/><br/>Your outstanding summary as on " + txtToDate.Text.Trim() + "<br/><br/>");
            sb.Append("<table cellspacing='0' cellpadding='0' border='1' width='75%'>");
            sb.Append("<tr><td><b>Member Code</b></td><td><b>Member Name</b></td><td><b>Admission Fees</b></td><td><b>Admission Fees Tax</b></td><td><b>Renewal Fees</b></td><td><b>Renewal Fees Tax</b></td><td><b>Total</b></td></tr>");
            sb.Append("<tr><td>" + GVR.Cells[1].Text + "</td><td>" + GVR.Cells[2].Text + "</td><td>" + GVR.Cells[9].Text + "</td><td>" + GVR.Cells[10].Text + "</td><td>" + GVR.Cells[11].Text + "</td><td>" + GVR.Cells[12].Text + "</td><td>" + GVR.Cells[13].Text + "</td></tr>");
            sb.Append("</table>");
            sb.Append("<br/><br/>Regards,<br/>WBPF");
            sb.Append("</div>");

            return sb.ToString();
        }

        private void SendEmail(string To, string Content)
        {
            try
            {
                string MailFromAddress = System.Configuration.ConfigurationManager.AppSettings["MAIL_FROM_ADDRESS"].Trim();
                string MailFromPassword = System.Configuration.ConfigurationManager.AppSettings["MAIL_FROM_PASSWORD"].Trim();

                MailMessage email = new MailMessage();
                email.To.Add(To);
                //email.CC.Add(txtcemail.Text);
                email.From = new MailAddress(MailFromAddress);
                email.IsBodyHtml = true;
                email.Subject = "Outstanding Summary";
                email.Body = Content;
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential(MailFromAddress, MailFromPassword);
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }

        protected void dgvMemberOutstanding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMemberOutstanding.PageIndex = e.NewPageIndex;
            LoadConsolidatedOutstandingList();
        }
    }
}