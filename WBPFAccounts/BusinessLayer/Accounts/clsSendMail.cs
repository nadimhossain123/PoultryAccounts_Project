using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using DataAccess.Accounts;


/// <summary>
/// Summary description for clsSendMail
/// </summary>
public class clsSendMail
{
	public clsSendMail()
	{
		
	}
    public void SendMail(string mailfrm, string mailto, string msgdesc, string msgsub, string name)
    {
        try
        {
            string body = "";
            string fromMail = mailfrm.Trim();
            string SenderName = "";
            //if (name == "")
                SenderName = "From Admin";
            //else
            //    SenderName = name;
            body += "Hi-" + "<br/>";
            body += msgdesc.ToString();
            body += "<br />" + SenderName.ToString();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail);
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);


            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = msgsub.ToString();
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);

            //==============================
        }
        catch (Exception ex)
        {

        }

    }

    public bool SendMail_SMTP(string gMailAccount, string password, string to, string subject, string message)
    {
        try
        {
            NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(gMailAccount);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com"); //smtp.mail.yahoo.com (port 25)
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public void SendMail1(string mailfrm, string mailto, string msgdesc, string msgsub, string name)
    {
        try
        {
            string body = "";
            string fromMail = mailfrm.Trim();
            string SenderName = "";
            //if (name == "")
                SenderName = "";
            //else
            //    SenderName = "From " + name;
            //body += "Hi-" + "<br/>";
            body += msgdesc.ToString();
            body += "<br />-" + SenderName.ToString();

            //MailAddress frm = new MailAddress(fromMail);
            //MailAddress frm = new MailAddress("<gopa@bitwavesolutions.com>");
            MailAddress frm = new MailAddress("<" + System.Configuration.ConfigurationSettings.AppSettings["FromMail"].ToString() + ">");
         
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = msgsub.ToString();
            mm.Body = body;
            mm.Priority = MailPriority.High;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    public void SendMail_General(string mailfrm, string mailto, string msgdesc, string msgsub)
    {
        try
        {
            string body = "";
            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = msgsub.ToString();
            mm.Body = msgdesc;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail_PaymentProgress(string mailfrm, string mailto, string name, string msgsub)
    {
        try
        {
            string body = "<table width='100%'><tr><td style='width: 80%'>";
            body += "<strong>Dear " + name + "</strong><br />";
            body += "</strong><br />Thank you for registration at www.seffta.com<br /><br />";
            body += "Your payment process is under progress. If you want to check the progress, login";
            body += "to seffta using your user is and password.<br /><br />your user id and password is send to you at your email id.<br />";
            body += "<br />Best Regards,<br /><br /><strong>Team Seffta<br /></strong></td>";
            body += "<td style='width: 100px'></td>";
            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Registration-under progress";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail_PaymentProgress1(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<div style='font-family:Tahoma, Verdana; font-size: 11px; color: Black;'>";
            body += "Dear <strong>" + name.ToString();
            body += "</strong>,<br /><br />We have received your <strong>Transaction No: " + tid.ToString();
            body += "</strong>. Your transaction will be<br />processed as soon as we receive your Cheque or DD and it is<br />";
            body += "cleared. Please refer to the payment instructions below so that we can<br />process your payment and order quickly:<br /><br />";
            body += "<strong><span style='color: darkred'>*** Paying by Cheque ***</span></strong><br /><br />";
            body += "1. Draw the cheque for the full amount in favour of<br />&nbsp; &nbsp;&nbsp;SEFFTA.COM. &nbsp;In case of outstation cheques (Non-Bokaro),<br />";
            body += "&nbsp; &nbsp;please include processing charges.<br />";
            body += "2. Cheques can be deposited into Seffta.com's account at your<br />&nbsp; &nbsp;nearest UNION BANK OF INDIA branch or ATM against the following<br />";
            body += "&nbsp; &nbsp;account numbers -<br />&nbsp; &nbsp;UNION BANK A/C no: &nbsp;452401010250825<br />";
            body += "&nbsp; &nbsp;(Alternately, you can send the cheque to our office address<br />&nbsp; &nbsp;mentioned below in this email though this may take longer to<br />";
            body += "&nbsp; &nbsp;process.<br />3. PLEASE CONTACT US VIA EMAIL OR PHONE AFTER SUBMITTING THE PAYMENT<br />";
            body += "&nbsp; &nbsp;so that we can track your cheque payment and begin processing your<br />&nbsp; &nbsp;order.<br /><br />";
            body += "<strong><span style='color: darkred'>*** Paying by Demand Draft ***</span></strong><br /><br />";
            body += "1. Draw the Demand Draft for the full order amount in favour of<br />&nbsp; &nbsp;&nbsp;SEFFTA.COM.<br />";
            body += "2. Send the DD to our office address mentioned below in this email.<br />3. PLEASE CONTACT US VIA EMAIL OR PHONE AFTER SUBMITTING THE PAYMENT<br />";
            body += "&nbsp; &nbsp;so that we can track your cheque payment and begin processing your<br />&nbsp; &nbsp;order.<br /><br />";
            body += "You will receive an email confirmation once your payment has been<br />processed.<br /><br />";
            body += "<strong><span style='color: darkred'>Your Transaction Summary:</span></strong><br /><br />";
            body += "<strong>Transaction No:</strong> &nbsp; &nbsp; &nbsp; &nbsp; " + tid.ToString() + " &nbsp; &nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <strong>Transaction Status:</strong> &nbsp; Awaiting Payment<br />";
            body += "<strong>Transaction Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<strong>PaymentMethod:</strong> Cheque/DD<br /><br /><br />";
            body += "*** Our Office Address ***<br />Seffta.com<br />Accounts Division,<br />BIT WAVE SOLUTIONS 2/2C,<br />Ballygung Place East, Jamini Roy Sarani,<br />Kolkata - 700019<br />";
            body += "Email: support@seffta.com<br />NOTE: PLEASE ENSURE THAT YOU E-MAIL US AT (support@seffta.com)<br />WITH THE FOLLOWING DETAILS TO ENSURE QUICK ORDER PROCESSING:<br /><br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp;1. ORDER NUMBER<br />&nbsp; &nbsp; &nbsp; &nbsp;2. CHEQUE/DD NUMBER<br />&nbsp; &nbsp; &nbsp; &nbsp;3. NAME OF THE BANK WHERE THE CHEQUE/DD HAS BEEN<br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; DEPOSITED UNION BANK OF INDIA<br />&nbsp; &nbsp; &nbsp; &nbsp;4. AMOUNT DEPOSITED<br />&nbsp; &nbsp; &nbsp; &nbsp;5. NAME OF BRANCH<br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp;6. YOUR BANK A/C NUMBER, IN CASE YOU HAVE DEPOSITED AN<br />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;UNION BANK OF INDIA CHEQUE<br /><br />";
            body += "Please call us once you have deposited the Cheque in the Drop Box.<br />After we receive the Cheque/DD, we will send it for processing to the<br />bank. Demand drafts are typically processed within 1 working day and<br />";
            body += "cheques payable at par in Bokaro within 5 working days. Outstation<br />cheques (not payable at par in Bokaro) may take up to 21 days to be cleared.<br /><br />Best Regards,<br />";
            body += "__________________________________________________________________________<br /><br /><strong>Seffta.com</strong><br />";
            body += "<br />You can contact us via phone or email. Please have your <strong>Transaction Number</strong><br />handy to help us assist you quickly and efficiently.<br />";
            body += "<br /><strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> +91-33-40010341,+91-33-40010342<br />&nbsp; &nbsp; &nbsp; &nbsp; We are available Mon-Sat from 10am to 6pm.<br /><br /></div>";
            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Registration Complete";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail_PaymentProgress2(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<span style='font-size: 10pt'><span style='font-family: Tahoma'><strong>";
            body += "Dear " + name.ToString() + "</strong></span><br />";
            body += "</span><br />";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>Thank you for registration at </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br />We have recieved the payment for your Transaction No: " + tid.ToString() + " and have<br />";
            body += "started processing the same.<br /><br />";
            body += "If you want to track the status of your Transaction, please log in to </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br /><strong><span style='color: darkred'>Your Transaction Summary:</span></strong><br /><br />";
            body += "<strong>Transaction No:</strong> &nbsp; &nbsp; &nbsp; &nbsp;" + tid.ToString() + "&nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; <strong>Transaction Status:</strong> &nbsp;Payment Under Process<br />";
            body += "<strong>Transaction Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp;";
            body += "&nbsp;<strong>Payment Method:</strong> Cheque/DD<br /></span><p>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>";
            body += "__________________________________________________________________<br />";
            body += "Best Regards,<br /><strong>Team Seffta</strong><br /><br />";
            body += "You can contact us via phone or email. Please have your Order Number<br />";
            body += "handy to help us assist you quickly and efficiently.<br /><br />";
            body += "<strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> &nbsp;<span style='font-family: Times New Roman'>+91-33-40010341,+91-33-40010342</span>&nbsp;</span></span><br />";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'></span></span>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>We are available Mon-Sat from 10am to 6pm.<br />";
            body += "</span><br /></span></p>";
            
            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Payment under progress";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail_PaymentProgress3(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<span style='font-size: 10pt'><span style='font-family: Tahoma'><strong>";
            body += "Dear " + name.ToString() + "</strong></span><br />";
            body += "</span><br />";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>Thank you for registration at </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br />We have recieved the payment for your Transaction No: " + tid.ToString() + " and have<br />";
            body += "started processing the same.<br /><br />";
            body += "If you want to track the status of your Transaction, please log in to </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br /><strong><span style='color: darkred'>Your Transaction Summary:</span></strong><br /><br />";
            body += "<strong>Transaction No:</strong> &nbsp; &nbsp; &nbsp; &nbsp;" + tid.ToString() + "&nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; <strong>Transaction Status:</strong> &nbsp; Payment Received<br />";
            body += "<strong>Transaction Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp;";
            body += "&nbsp;<strong>Payment Method:</strong> Cheque/DD<br /></span><p>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>";
            body += "__________________________________________________________________<br />";
            body += "Best Regards,<br /><strong>Team Seffta</strong><br /><br />";
            body += "You can contact us via phone or email. Please have your Order Number<br />";
            body += "handy to help us assist you quickly and efficiently.<br /><br />";
            body += "<strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> &nbsp;<span style='font-family: Times New Roman'>+91-33-40010341,+91-33-40010342</span>&nbsp;</span></span><br />";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'></span></span>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>We are available Mon-Sat from 10am to 6pm.<br />";
            body += "</span><br /></span></p>";

            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Payment Received";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }

    }
    public void SendMail_TipsAndTrics1(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<div style='font-family:Tahoma, Verdana; font-size: 11px; color: Black;'>";
            body += "Dear <strong>" + name.ToString();
            body += "</strong>,<br /><br />We have received your <strong>Order No: " + tid.ToString();
            body += "</strong>. Your Order will be<br />processed as soon as we receive your Cheque or DD and it is<br />";
            body += "cleared. Please refer to the payment instructions below so that we can<br />process your payment and order quickly:<br /><br />";
            body += "<strong><span style='color: darkred'>*** Paying by Cheque ***</span></strong><br /><br />";
            body += "1. Draw the cheque for the full amount in favour of<br />&nbsp; &nbsp;&nbsp;SEFFTA.COM. &nbsp;In case of outstation cheques (Non-Bokaro),<br />";
            body += "&nbsp; &nbsp;please include processing charges.<br />";
            body += "2. Cheques can be deposited into Seffta.com's account at your<br />&nbsp; &nbsp;nearest UNION BANK OF INDIA branch or ATM against the following<br />";
            body += "&nbsp; &nbsp;account numbers -<br />&nbsp; &nbsp;UNION BANK OF INDIA A/C no: &nbsp;000405035116<br />";
            body += "&nbsp; &nbsp;(Alternately, you can send the cheque to our office address<br />&nbsp; &nbsp;mentioned below in this email though this may take longer to<br />";
            body += "&nbsp; &nbsp;process.<br />3. PLEASE CONTACT US VIA EMAIL OR PHONE AFTER SUBMITTING THE PAYMENT<br />";
            body += "&nbsp; &nbsp;so that we can track your cheque payment and begin processing your<br />&nbsp; &nbsp;order.<br /><br />";
            body += "<strong><span style='color: darkred'>*** Paying by Demand Draft ***</span></strong><br /><br />";
            body += "1. Draw the Demand Draft for the full order amount in favour of<br />&nbsp; &nbsp;&nbsp;SEFFTA.COM.<br />";
            body += "2. Send the DD to our office address mentioned below in this email.<br />3. PLEASE CONTACT US VIA EMAIL OR PHONE AFTER SUBMITTING THE PAYMENT<br />";
            body += "&nbsp; &nbsp;so that we can track your cheque payment and begin processing your<br />&nbsp; &nbsp;order.<br /><br />";
            body += "You will receive an email confirmation once your payment has been<br />processed.<br /><br />";
            body += "<strong><span style='color: darkred'>Your Order Summary:</span></strong><br /><br />";
            body += "<strong>Order No:</strong> &nbsp; &nbsp; &nbsp; &nbsp; " + tid.ToString() + " &nbsp; &nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <strong>Order Status:</strong> &nbsp; Awaiting Payment<br />";
            body += "<strong>Order Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<strong>PaymentMethod:</strong> Cheque/DD<br /><br /><br />";
            body += "*** Our Office Address ***<br />Seffta.com<br />Accounts Division,<br />BIT WAVE SOLUTIONS 2/2C,<br />Ballygung Place East, Jamini Roy Sarani,<br />Kolkata - 700019<br />";
            body += "Email: support@seffta.com<br />NOTE: PLEASE ENSURE THAT YOU E-MAIL US AT (support@seffta.com)<br />WITH THE FOLLOWING DETAILS TO ENSURE QUICK ORDER PROCESSING:<br /><br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp;1. ORDER NUMBER<br />&nbsp; &nbsp; &nbsp; &nbsp;2. CHEQUE/DD NUMBER<br />&nbsp; &nbsp; &nbsp; &nbsp;3. NAME OF THE BANK WHERE THE CHEQUE/DD HAS BEEN<br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; DEPOSITED UNION BANK OF INDIA<br />&nbsp; &nbsp; &nbsp; &nbsp;4. AMOUNT DEPOSITED<br />&nbsp; &nbsp; &nbsp; &nbsp;5. NAME OF BRANCH<br />";
            body += "&nbsp; &nbsp; &nbsp; &nbsp;6. YOUR BANK A/C NUMBER, IN CASE YOU HAVE DEPOSITED AN<br />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;UNION BANK OF INDIA BANK CHEQUE<br /><br />";
            body += "Please call us once you have deposited the Cheque in the Drop Box.<br />After we receive the Cheque/DD, we will send it for processing to the<br />bank. Demand drafts are typically processed within 1 working day and<br />";
            body += "cheques payable at par in Bokaro within 5 working days. Outstation<br />cheques (not payable at par in Bokaro) may take up to 21 days to be cleared.<br /><br />Best Regards,<br />";
            body += "__________________________________________________________________________<br /><br /><strong>Seffta.com</strong><br />";
            body += "<br />You can contact us via phone or email. Please have your <strong>Order Number</strong><br />handy to help us assist you quickly and efficiently.<br />";
            body += "<br /><strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> +91-33-40010341,+91-33-40010342<br />&nbsp; &nbsp; &nbsp; &nbsp; We are available Mon-Sat from 10am to 6pm.<br /><br /></div>";
            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Order Complete";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }

    public void SendMail_TipsAndTrics2(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<span style='font-size: 10pt'><span style='font-family: Tahoma'><strong>";
            body += "Dear " + name.ToString() + "</strong></span><br />";
            body += "</span><br />";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>Thank you for Purchase at </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br />We have recieved the payment for your Order No: " + tid.ToString() + " and have<br />";
            body += "started processing the same.<br /><br />";
            body += "If you want to track the status of your Order, please log in to </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br /><strong><span style='color: darkred'>Your Order Summary:</span></strong><br /><br />";
            body += "<strong>Order No:</strong> &nbsp; &nbsp; &nbsp; &nbsp;" + tid.ToString() + "&nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; <strong>Order Status:</strong> &nbsp;Payment Under Process<br />";
            body += "<strong>Order Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp;";
            body += "&nbsp;<strong>Payment Method:</strong> Cheque/DD<br /></span><p>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>";
            body += "__________________________________________________________________<br />";
            body += "Best Regards,<br /><strong>Team Seffta</strong><br /><br />";
            body += "You can contact us via phone or email. Please have your Order Number<br />";
            body += "handy to help us assist you quickly and efficiently.<br /><br />";
            body += "<strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> &nbsp;<span style='font-family: Times New Roman'>+91-33-40010341,+91-33-40010342</span>&nbsp;</span></span><br />";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'></span></span>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>We are available Mon-Sat from 10am to 6pm.<br />";
            body += "</span><br /></span></p>";

            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Payment under progress";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail_TipsAndTrics3(string mailfrm, string mailto, string name, string msgsub, string tid, string date)
    {
        try
        {
            string body = "<span style='font-size: 10pt'><span style='font-family: Tahoma'><strong>";
            body += "Dear " + name.ToString() + "</strong></span><br />";
            body += "</span><br />";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>Thank you for registration at </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br />We have recieved the payment for your Order No: " + tid.ToString() + " and have<br />";
            body += "started processing the same.<br /><br />";
            body += "If you want to track the status of your Order, please log in to </span>";
            body += "<a href='http://www.seffta.com' title='http://www.seffta.com'><strong title='http://www.seffta.com'>";
            body += "<span style='font-size: 10pt; font-family: Tahoma'>seffta.com</span></strong></a><span style='font-size: 10pt; font-family: Tahoma'>.<br />";
            body += "<br /><strong><span style='color: darkred'>Your Order Summary:</span></strong><br /><br />";
            body += "<strong>Order No:</strong> &nbsp; &nbsp; &nbsp; &nbsp;" + tid.ToString() + "&nbsp; &nbsp;";
            body += "&nbsp; &nbsp; &nbsp; <strong>Order Status:</strong> &nbsp; Payment Received<br />";
            body += "<strong>Order Placed on:</strong>" + date.ToString() + "&nbsp; &nbsp; &nbsp; &nbsp;";
            body += "&nbsp;<strong>Payment Method:</strong> Cheque/DD<br /></span><p>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>";
            body += "__________________________________________________________________<br />";
            body += "Best Regards,<br /><strong>Team Seffta</strong><br /><br />";
            body += "You can contact us via phone or email. Please have your Order Number<br />";
            body += "handy to help us assist you quickly and efficiently.<br /><br />";
            body += "<strong>Email:</strong> &nbsp;support@seffta.com<br />&nbsp; &nbsp; &nbsp; &nbsp; We will respond to your query within 2 working days.<br /><br />";
            body += "<strong>Phone:</strong> &nbsp;<span style='font-family: Times New Roman'>+91-33-40010341,+91-33-40010342</span>&nbsp;</span></span><br />";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'></span></span>";
            body += "<span style='font-size: 10pt'><span style='font-family: Tahoma'>We are available Mon-Sat from 10am to 6pm.<br />";
            body += "</span><br /></span></p>";

            string fromMail = mailfrm.Trim();

            //MailAddress frm = new MailAddress(fromMail);
            MailAddress frm = new MailAddress(fromMail, "Seffta");
            MailAddress frmto = new MailAddress(mailto);
            MailMessage mm = new MailMessage(frm, frmto);

            //mm.To.Add(mailto.Trim());
            //mm.From = frm;
            mm.Subject = "Payment Received";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient mailclient = new SmtpClient();
            //mailclient.Host = "127.0.0.1";
            mailclient.Host = WebConfig.SMTPEmailIP;
            mailclient.Send(mm);
        }
        catch (Exception ex)
        {

        }

    }
}
