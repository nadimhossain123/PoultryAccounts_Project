using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace BusinessLayer.Common
{
    public class Common
    {
        public static string GetNewCode(char custOrStk, bool increment)
        {
            return DataAccess.Common.Common.GetNewCode(custOrStk, increment);
        }


        public static string GetScalarValue(string columnName,  string tableName, string id, string value)
        {
            return DataAccess.Common.Common.GetScalarValue(columnName, tableName, id, value);
        }

        public static string StingToArray(string [] arrVal, string delem)
        {
            string strReturn = "";
            foreach (string strVal in arrVal)
            {
                strReturn += strVal + delem;
            }
            return strReturn.Substring(0,strReturn.Length -1);

        }
        public static string GetCurrentDate()
        {
            return DataAccess.Common.Common.GetCurrentDate();
        }
        public static void ErrorLog(string sErrMsg)
        {
            string sLogFormat;
            string sErrorTime;

            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;

            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/TextLog/");
            StreamWriter sw = new StreamWriter(mappedPath + sErrorTime, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }


        public DataSet GetStockCustomerMobEmail()
        {
            return DataAccess.Common.Common.GetStockCustomerMobEmail();
        }
        public int SaveStockCustomerMobEmail(string Type, string Email, string MobileNo, int Id)
        {
            return DataAccess.Common.Common.SaveStockCustomerMobEmail(Type,Email,MobileNo,Id);
        }
        public void AuditTrailLogSave(int UserId,int ActionType, string ActionDesc)
        {
            DataAccess.Common.Common.AuditTrailLogSave(UserId,ActionType, ActionDesc);
        }

        //MAIL AND SMS METHOD
        public static int SendEmail(string To, string Subject, string Content, string[] Attachment)
        {
            try
            {
                string MailFromAddress = "support@transventor.com";
                string MailFromPassword = "@#Support123";

                MailMessage email = new MailMessage();
                email.To.Add(To);
                //email.CC.Add(txtcemail.Text);
                email.From = new MailAddress(MailFromAddress);
                email.IsBodyHtml = true;
                email.Subject = Subject;
                email.Body = Content;

                //NEW CODE//
                if (Attachment != null && Attachment.Length > 0)
                {
                    foreach (var item in Attachment)
                    {
                        if (item.Length > 0)
                        {
                            email.Attachments.Add(new Attachment(item));
                        }
                    }
                }
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential(MailFromAddress, MailFromPassword);
                smtp.Send(email);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void SendSms(string To, string Text)
        {
            try
            {
                string API = string.Format("http://priority.muzztech.in/sms_api/sendsms.php?username=transventorcrm&password=muzztech@123&mobile={0}&sendername=WEBTEL&message={1}",To,Text);
                    
                
                WebRequest request = HttpWebRequest.Create(API);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static DataTable GetMailSMSContent(string Code,string Type)
        {
            return DataAccess.Common.Common.GetMailSMSContent(Code, Type);
        }
        public static void DeleteMailSMSContent(int ContentId)
        {
            DataAccess.Common.Common.DeleteMailSMSContent(ContentId);
        }
    }
   
}
