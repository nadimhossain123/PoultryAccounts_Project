using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;

namespace BusinessLayer.Common
{
    public class FeeNotification
    {
        string API_INDEX;
        public void MailToMember()
        {
            DateTime date = DateTime.Now.AddHours(12).AddMinutes(30);
            int Month = date.Month;
            int Year = date.Year;
            BusinessLayer.Common.MemberBill objMemberBill = new BusinessLayer.Common.MemberBill();
            DataTable dt = objMemberBill.MemberMonthlyDevelopmentBill(0, 0, 0, 0, Month, Year, "", 0);

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ToAddress = dt.Rows[i]["Email"].ToString();
                    string MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    if (!string.IsNullOrEmpty(ToAddress))
                    {
                        string MessageContent = GetMessageContent(dt.Rows[i]);
                        SendEmail(ToAddress, MessageContent);
                    }
                    if (!string.IsNullOrEmpty(MobileNo))
                    {
                        string MessageContent = GetMobileMessageContent(dt.Rows[i]);
                        TriggerSMS(MobileNo, MessageContent);
                    }
                }
            }
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

        private string GetMobileMessageContent(DataRow DR)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Dear " + DR["MemberName"].ToString() + ", " + "%0a");
            sb.Append(@"Your monthly development fee bill has been generated for " + DR["Month"] + " " + DR["Year"] + " and sent to your registered email." + "%0a");
            sb.Append(@"Bill Amount - Rs." + DR["FinalAmount"].ToString() + "%0a");
            sb.Append(@"Bill Date - 01," + DR["Month"] + " " + DR["Year"] + "%0a");
            sb.Append(@"Thank You" + "%0a");
            sb.Append(@"West Bengal Poultry Federation");
            string message = sb.ToString();

            return sb.ToString();
        }

        private string GetMessageContent(DataRow DR)
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
            sb.Append(@"    <td style='width: 50%'>                Bill No :" + DR["BillNo"].ToString());
            sb.Append(@"   </td>");
            sb.Append(@"  <td align='right'>                Date: 01, " + DR["Month"] + " " + DR["Year"]);
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
            sb.Append(@"     <td valign='top'>              <b> " + DR["MemberName"] + " </b><br/>" + DR["VillageOrStreet"] + " < br/>" + DR["BlockName"]);
            sb.Append(@"    </td>");
            sb.Append(@"    <td align='right' valign='top'>                <b><u>Membership No. " + DR["MemberCode"] + "</u></b>");
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
            sb.Append(@"     <td>                " + DR["Month"] + ", " + DR["Year"]);
            sb.Append(@"    </td>");
            sb.Append(@"   <td>                MONTHLY DONATION FEES FOR DEVELOPMENT OF VARIOUS ACTIVITIES");
            sb.Append(@" </td>");
            sb.Append(@"   <td align='right'>              " + DR["FinalAmount"]);
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
            sb.Append(@"  <td align='right'>                " + DR["FinalAmount"]);
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
            sb.Append(@"      <td>                1. Payments can be made by A/C payee cheque/NEFT/RTGS/Online Payment.");
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
