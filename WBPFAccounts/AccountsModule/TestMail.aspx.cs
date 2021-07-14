using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AccountsModule
{
    public partial class TestMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string To = txtMailTo.Text;
            string reply=SendEmail(To,"TEST MAIL");
            lblMsg.Text = reply;

        }
    }
}