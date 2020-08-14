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
using System.Net;

namespace AccountsModule.Common
{
    public partial class MonthlyDevelopmentFeeBill : System.Web.UI.Page
    {
        string API_INDEX;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    //btnSendEmail.Visible = false;
                    //btnDownload.Visible = false;
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
            LoadMonth();
            LoadStates();
            LoadDistricts();
            LoadBlock();
            LoadMembershipCategory();
        }

        private void LoadMonth()
        {
            BusinessLayer.Common.MonthMaster objMonth = new BusinessLayer.Common.MonthMaster();
            DataTable dt = objMonth.GetAll();

            ddlMonth.DataSource = dt;
            ddlMonth.DataBind();
            InsertFisrtItem(ddlMonth, "--SELECT MONTH--");
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

        private void LoadMonthlyDevelopmentBill()
        {
            int StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            int CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            string MemberName = txtMemberName.Text.Trim();

            //DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            //DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);

            BusinessLayer.Common.MemberBill objMemberBill = new BusinessLayer.Common.MemberBill();
            DataTable dt = objMemberBill.MemberMonthlyDevelopmentBill(StateId, DistrictId, BlockId, CategoryId, Month, year, MemberName, 0);
            if (dt != null)
            {
                dgvMemberMonthlyBill.DataSource = dt;
                dgvMemberMonthlyBill.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMonthlyDevelopmentBill();
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

        protected void dgvMemberMonthlyBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnPayment = (Button)e.Row.FindControl("btnPayment");
                btnPayment.Attributes.Add("OnClick", "openpopup('development-bill.aspx?BillId=" + dgvMemberMonthlyBill.DataKeys[e.Row.RowIndex].Values["BillId"].ToString() + "'); return false;");
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvMemberMonthlyBill.Rows.Count; i++)
            {
                if (((CheckBox)dgvMemberMonthlyBill.Rows[i].FindControl("ChkSelect")).Checked)
                {
                    string ToAddress = dgvMemberMonthlyBill.Rows[i].Cells[9].Text.Trim();
                    string MobileNo = dgvMemberMonthlyBill.Rows[i].Cells[11].Text.Trim();

                    if (!string.IsNullOrEmpty(ToAddress))
                    {
                        string MessageContent = GetMessageContent(dgvMemberMonthlyBill.Rows[i]);
                        SendEmail(ToAddress, MessageContent);
                    }
                    if (!string.IsNullOrEmpty(MobileNo))
                    {
                        string MessageContent = GetMobileMessageContent(dgvMemberMonthlyBill.Rows[i]);
                        TriggerSMS(MobileNo, MessageContent);
                    }
                }
            }

            Message.IsSuccess = true;
            Message.Text = "Mail Sent Successfully";
            Message.Show = true;
        }

        private string SendEmail(string To, string Content)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress("accounts@wbpoultryfederation.org", "West Bengal Poultry Federation");
                message.From = fromAddress;
                message.To.Add(To);

                message.Subject = "MONTHLY DEVELOPMENT FEE BILL";
                message.IsBodyHtml = true;
                message.Body = Content;

                System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient("relay-hosting.secureserver.net", 25);
                Client.Credentials = CredentialCache.DefaultNetworkCredentials;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.Send(message);
                message = null; // free up resources
                // client.Send(message);
                msg = "Successful";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        private string GetMobileMessageContent(GridViewRow GVR)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Dear " + GVR.Cells[3].Text.ToString() + ", " + "%0a");
            sb.Append(@"Your monthly development fee bill has been generated for " + GVR.Cells[5].Text + " " + GVR.Cells[6].Text + " and sent to your registered email." + "%0a");
            sb.Append(@"Bill Amount - Rs." + GVR.Cells[10].Text.ToString() + "%0a");
            sb.Append(@"Bill Date - 01," + GVR.Cells[5].Text + " " + GVR.Cells[6].Text + "%0a");
            sb.Append(@"Thank You" + "%0a");
            sb.Append(@"West Bengal Poultry Federation");
            string message = sb.ToString();

            return sb.ToString();
        }

        private string GetMessageContent(GridViewRow GVR)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table width='100%'>");
            sb.Append(@"<tr>");
            sb.Append(@"<td align='center'><b><span style='font-family: Calibri; font-size: 44px;'><u>WEST BENGAL POULTRY FEDERATION</u></span></b>");
            sb.Append(@"</td>");
            sb.Append(@"</tr>");
            sb.Append(@"<tr>");
            sb.Append(@"<td align='center'>46C, Chowringhee Road, 11th Floor, Room No - C, Kolkata - 700071");
            sb.Append(@"</td>");
            sb.Append(@"</tr>");
            sb.Append(@"<tr>");
            sb.Append(@"<td align='center'>Phone No. 033 - 40515700 / 22885525");
            sb.Append(@"</td>");
            sb.Append(@"</tr>");
            sb.Append(@" <tr>");
            sb.Append(@"<td>&nbsp;");
            sb.Append(@"</td>");
            sb.Append(@" </tr>");
            sb.Append(@"</table>");


            sb.Append(@"<table width='100%' cellpadding='3' style='border: 1px solid black;'>");
            sb.Append(@"<tr>");
            sb.Append(@"   <td colspan='2' align='center'> <h2> <b><u>Monthly Developement Fees</u> </b> </h2>");
            sb.Append(@" </td>");
            sb.Append(@"</tr>");
            sb.Append(@"<tr>");
            sb.Append(@"  <td colspan='2'>");
            sb.Append(@"  </td>");
            sb.Append(@" </tr>");
            sb.Append(@" <tr>");
            sb.Append(@"    <td style='width: 50%'>                Bill No :" + GVR.Cells[1].Text);
            sb.Append(@"   </td>");
            sb.Append(@"  <td align='right'>                Date: 01, " + GVR.Cells[5].Text + " " + GVR.Cells[6].Text);
            sb.Append(@"  </td>");
            sb.Append(@" </tr>");
            sb.Append(@"<tr>");
            sb.Append(@" <td>");
            sb.Append(@"  </td>");
            sb.Append(@"  <td>");
            sb.Append(@"  </td>");
            sb.Append(@"  </tr>");
            sb.Append(@" <tr>");
            sb.Append(@"   <td>                To");
            sb.Append(@"</td>");
            sb.Append(@"  <td>");
            sb.Append(@"  </td>");
            sb.Append(@"</tr>");
            sb.Append(@" <tr>");
            sb.Append(@"     <td valign='top'>              <b> " + GVR.Cells[3].Text + "</b><br/>" + GVR.Cells[4].Text + "<br/>" + GVR.Cells[7].Text);
            sb.Append(@"    </td>");
            sb.Append(@"    <td align='right' valign='top'>                <b><u>Membership No. " + GVR.Cells[2].Text + "</u></b>");
            sb.Append(@"    </td>");
            sb.Append(@"  </tr>");
            sb.Append(@" </table>");

            sb.Append(@" <table border='1' cellpadding='5' cellspacing='0' width='100%' style='border-collapse: collapse;'>");
            sb.Append(@" <tr>");
            sb.Append(@"  <td style='width: 33%' align='center'>                <b><u>Bill Period</u></b>");
            sb.Append(@" </td>");
            sb.Append(@" <td style='width: 53%' align='center'>                <b><u>Description</u></b>");
            sb.Append(@" </td>");
            sb.Append(@" <td style='width: 13%' align='center'>                <b><u>Amount</u></b>");
            sb.Append(@" </td>");
            sb.Append(@"  </tr>");
            sb.Append(@"  <tr>");
            sb.Append(@"     <td>                " + GVR.Cells[5].Text + ", " + GVR.Cells[6].Text);
            sb.Append(@"    </td>");
            sb.Append(@"   <td>                MONTHLY DONATION FEES FOR DEVELOPMENT OF VARIOUS ACTIVITIES");
            sb.Append(@" </td>");
            sb.Append(@"   <td align='right'>              " + GVR.Cells[10].Text);
            sb.Append(@"   </td>");
            sb.Append(@" </tr>");
            sb.Append(@" <tr>");
            sb.Append(@"    <td>                &nbsp;");
            sb.Append(@"   </td>");
            sb.Append(@"   <td align='right'>                ");
            sb.Append(@"   </td>");
            sb.Append(@"   <td align='right'>                ");
            sb.Append(@"   </td>");
            sb.Append(@" </tr>");
            sb.Append(@" <tr>");
            sb.Append(@"   <td colspan='2' align='right'>                Total :");
            sb.Append(@"  </td>");
            sb.Append(@"  <td align='right'>                " + GVR.Cells[10].Text);
            sb.Append(@"  </td>");
            sb.Append(@"  </tr>");
            sb.Append(@" </table>");



            sb.Append(@"  <table width='100%' cellpadding='3' style='border: 1px solid black;'>");
            sb.Append(@"  <tr>");
            sb.Append(@"   <td style='width: 50%'>                &nbsp;");
            sb.Append(@" </td>");
            sb.Append(@" <td align='right'>                E. &amp; O.E.");
            sb.Append(@"   </td>");
            sb.Append(@"  </tr>");
            sb.Append(@"  <tr>");
            sb.Append(@"   <td>                <u><b>Conditions:</u> </b>");
            sb.Append(@"    </td>");
            sb.Append(@"   <td align='right'>                For West Bengal Poultry Federation");
            sb.Append(@"   </td>");
            sb.Append(@"  </tr>");
            sb.Append(@"  <tr>");
            sb.Append(@"      <td>                1. Payments should be made by A/C payee cheque only.");
            sb.Append(@"     </td>");
            sb.Append(@"     <td>");
            sb.Append(@"     </td>");
            sb.Append(@"  </tr>");
            sb.Append(@"  <tr>");
            sb.Append(@"      <td>                2. If the bill is paid within 1st week of the month.");
            sb.Append(@"     </td>");
            sb.Append(@"     <td>");
            sb.Append(@"     </td>");
            sb.Append(@"  </tr>");
            sb.Append(@" <tr>");
            sb.Append(@"    <td style='height: 100px;'>");
            sb.Append(@"    </td>");
            sb.Append(@"    <td>");
            sb.Append(@"   </td>");
            sb.Append(@" </tr>");
            sb.Append(@"  <tr>");
            sb.Append(@"     <td>");
            sb.Append(@"     </td>");
            sb.Append(@"     <td align='right'>                Authorised signatory");
            sb.Append(@"      </td>");
            sb.Append(@"  </tr>");
            sb.Append(@" </table>");

            return sb.ToString();
        }

        private string GetHTTPAPI(string mobiles, string message)
        {
            string API = "1";

            if (API_INDEX == "1")
                API = string.Format("http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=admin@fourfusionsolutions.com:solution2012&senderID=WBPOLT&receipientno={0}&msgtxt={1}&state=1", mobiles, message);
            else if (API_INDEX == "2")
                API = string.Format("http://www.krishsms.com/PostSms.aspx?userid=WBPOLT&pass=WBPOLT12345&phone={0}&msg={1}&title=WBPOLT", mobiles, message);
            else if (API_INDEX == "3")
                API = string.Format("http://login.tbulksms.com/API/WebSMS/Http/v1.0a/index.php?userid=124294&password=al4145IS&sender=WBPOLT&to={0}&message={1}&reqid=1&format=text&route_id=11&unique=0&msgtype=Unicode", mobiles, message);
            else if (API_INDEX == "4")
                API = string.Format("http://login.hivemsg.com/api/send_transactional_sms.php?username=u1348&msg_token=3PEV69&sender_id=WBPOLT&message={0}&mobile={1}", message, mobiles);

            return API;
        }

        protected void TriggerSMS(string MobileNo, string Message)
        {
            string ROUTE_1 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_1"];
            API_INDEX = ROUTE_1;
            string dataString;
            string strUrl = GetHTTPAPI(MobileNo, Message);
            try
            {
                WebRequest request1 = HttpWebRequest.Create(strUrl);
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                Stream s1 = (Stream)response1.GetResponseStream();
                StreamReader readStream1 = new StreamReader(s1);
                dataString = readStream1.ReadToEnd();
                response1.Close();
                s1.Close();
                readStream1.Close();
            }
            catch (Exception e) { }
        }
    }
}