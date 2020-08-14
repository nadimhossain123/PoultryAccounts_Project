using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace BusinessLayer.Common
{
    public class MailFunctionality
    {
        public static void SendEmail(string To, string Subject, string Content)
        {
            try
            {
                string MailFromAddress = "erpopjitandjipt@gmail.com";
                string MailFromPassword = "GUESSING";

                MailMessage email = new MailMessage();
                email.To.Add(To);
                //email.CC.Add(txtcemail.Text);
                email.From = new MailAddress(MailFromAddress);
                email.IsBodyHtml = true;
                email.Subject = Subject;
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
    }
}
