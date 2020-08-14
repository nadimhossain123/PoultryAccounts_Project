using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;

namespace BusinessLayer
{
    public class SendNotificationSMS
    {
        string MobileNo = "";
        string EndDate = "";
        int MemberId = 0;
        string API_INDEX;

        public SendNotificationSMS()
        {
        }

        public void SendSMS()
        {
            DataTable DTMob = BusinessLayer.MemberNotification.GetNotificationMobNos();
            if (DTMob != null)
            {
                if (DTMob.Rows.Count > 0)
                {
                    for (int i = 0; i < DTMob.Rows.Count; i++)
                    {
                        MobileNo = DTMob.Rows[i]["MobileNo"].ToString().Trim();
                        EndDate = Convert.ToDateTime(DTMob.Rows[i]["EndDate"].ToString().Trim()).ToString("dd/MM/yy");
                        MemberId = Convert.ToInt32(DTMob.Rows[i]["MemberId"].ToString().Trim());
                        Step1(MobileNo, EndDate, MemberId);
                    }
                }
            }
        }

        private void Step1(string Mob, string EndDt, int id)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Your renewal of rate message has fallen due on " + EndDt + ". Please renew for continution. Message from WBPF");
                string msg = sb.ToString();
                string strUrl = GetHTTPAPI(Mob, msg);

                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();

                BusinessLayer.MemberNotification.UpdateNotificationDate(id);
            }
            catch (Exception ex)
            {
                //Exception
            }
        }

        private string GetHTTPAPI(string mobiles, string message)
        {
            string API = string.Empty;

            BusinessLayer.SMS.SMSAPIConfig objSMSAPIConfig = new BusinessLayer.SMS.SMSAPIConfig();
            API_INDEX = objSMSAPIConfig.GetAll().Select("IsSelected=1")[0]["APIId"].ToString();
            //string API = "";

            if (API_INDEX == "1")
                API = string.Format("http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=admin@fourfusionsolutions.com:solution2012&senderID=WBPOLT&receipientno={0}&msgtxt={1}&state=1", mobiles, message);
            else if (API_INDEX == "2")
                API = string.Format("http://www.krishsms.com/PostSms.aspx?userid=WBPOLT&pass=WBPOLT12345&phone={0}&msg={1}&title=WBPOLT", mobiles, message);
            else if (API_INDEX == "3")
                API = string.Format("http://login.tbulksms.com/API/WebSMS/Http/v1.0a/index.php?userid=124294&password=al4145IS&sender=WBPOLT&to={0}&message={1}&reqid=1&format=text&route_id=11&unique=0&msgtype=Unicode", mobiles, message);
            else if (API_INDEX == "4")
                API = string.Format("http://login.hivemsg.com/api/send_transactional_sms.php?username=u1348&msg_token=3PEV69&sender_id=WBPOLT&message={0}&mobile={1}", message, mobiles);
            else if (API_INDEX == "5")
                API = string.Format("http://sms.fourfusiontechnologies.com/new/api/api_http.php?username=WBPOLT&password=wbpolt123&senderid=WBPOLT&to={0}&text={1}&route=Informative&type=text", mobiles, message);
            else if (API_INDEX == "6")
                API = string.Format("http://sms.afraconnect.com/api/mt/SendSMS?user=BISWA2K6@GMAIL.COM&password=9836634433&senderid=WBPOLT&channel=Trans&DCS=0&flashsms=0&number={0}&text={1}&route=20", mobiles, message);

            //return API;
            //if (CurrentProvider.Trim() == "Mvayoo")
            //{
            //API = (string.Format("http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=admin@fourfusionsolutions.com:solution2012&senderID=WBPOLT&receipientno={0}&msgtxt={1}&state=1", mobiles, message));

            //}
            //else if (CurrentProvider.Trim() == "ACL")
            //{
            //    API = string.Format("http://203.122.58.168/prepaidgetbroadcast/PrepaidGetBroadcast?userid=fourfs&pwd=fourfs12&sender=WBPOLT&pno={0}&msgtxt={1}&msgtype=S", mobiles, message);
            //}
            return API;
        }
    }
}
