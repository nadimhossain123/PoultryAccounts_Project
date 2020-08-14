using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

namespace AccountsModule.SMS
{
    public partial class DoctorsSMS : System.Web.UI.Page
    {
        string Mssg = "";
        string API_INDEX;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                txtCredit.Attributes.Add("readonly", "readonly");
            }
        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            SendSMS();
        }

        private void SendSMS()
        {
            BusinessLayer.SMS.ApiConfiguration ObjApi = new BusinessLayer.SMS.ApiConfiguration();
            DataTable dt = ObjApi.GetAll();
            DataView DV = new DataView(dt);
            DV.RowFilter = "IsActive = 1";
            string API = string.Empty;
            API_INDEX = Convert.ToString(DV[0]["SMSAPIId"]);

            try
            {
                string mobiles = "";
                string message = "";
                string strUrl;
                string dataString;

                message = txtMsg.Text.Trim();
                mobiles = txtMobNo.Text.Trim();

                int MobNoCount = 0;
                if (mobiles.Length > 0)
                {
                    string[] Arrmob = mobiles.Trim().Split(',');
                    MobNoCount = Arrmob.Length;//How many nos are sending for SMS
                    mobiles = "";
                    for (int index = 0; index < Arrmob.Length; index++)
                    {
                        if (Arrmob[index].Length == 10)
                        {
                            mobiles += "91" + Arrmob[index].Trim() + ";";
                        }
                        else if (Arrmob[index].Length == 12)
                        {
                            mobiles += Arrmob[index].Trim() + ";";
                        }
                    }
                    mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();

                }
                //------------------------------------

                if (message.Length > 0)
                {
                    if (mobiles.Length == 0)//Then fetch mobile numbers from Database
                    {
                        DataTable dtDB = getMobileNumbers(Convert.ToInt32(ddlGroup.SelectedValue));
                        MobNoCount = dtDB.Rows.Count;//How many nos are sending for SMS
                        int counter = 0;
                        foreach (DataRow dr in dtDB.Rows)
                        {
                            mobiles += "91" + dr["MobileNo"].ToString() + ";";
                            counter++;
                            if (counter == MobNoCount)
                            {
                                mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1);
                                counter = 0;
                            }
                        }
                    }
                    int Credit = Convert.ToInt32(txtCredit.Text);
                    int TotalCredit = Credit * MobNoCount;
                    strUrl = GetHTTPAPI(mobiles, message);

                    WebRequest request1 = HttpWebRequest.Create(strUrl);
                    HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                    Stream s1 = (Stream)response1.GetResponseStream();
                    StreamReader readStream1 = new StreamReader(s1);
                    dataString = readStream1.ReadToEnd();
                    response1.Close();
                    s1.Close();
                    readStream1.Close();

                    BusinessLayer.SMS.DoctorsSMSTrigger ObjDocSMSTrigger = new BusinessLayer.SMS.DoctorsSMSTrigger();
                    Entity.SMS.DoctorsSMSTrigger docEntity = new Entity.SMS.DoctorsSMSTrigger();
                    if (MobNoCount > 0)
                    {
                        docEntity.DoctorsSMSTriggerId = 0;
                        docEntity.Username = Session["UserId"].ToString();
                        docEntity.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                        docEntity.NoofTrigger = TotalCredit;
                        docEntity.MessageBody = txtMsg.Text.ToString();
                        ObjDocSMSTrigger.Save(docEntity);
                        lblMsg.Text = "<h2>Message send successfully</h2>";
                        Hidden1.Value = "1";
                        btnSendSMS.Style.Add("display", "none");

                    }
                }
            }

            catch (Exception ex)
            {
                lblMsg.Text = "<h2>Error: " + ex.Message + "</h2>";
            }
        }

        private string GetHTTPAPI(string mobiles, string message)
        {
            string API = "";

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

            return API;
        }

        private DataTable getMobileNumbers(int groupId)
        {
            BusinessLayer.SMS.DoctorMaster ObjDoc = new BusinessLayer.SMS.DoctorMaster();
            DataTable dtMobileNos = ObjDoc.GetDocNumbers(groupId);
            return dtMobileNos;
        }

        protected void ChckedChanged(object sender, EventArgs e)
        {
            //BusinessLayer.SMSTrigger objTrigger = new BusinessLayer.SMSTrigger();
            //objTrigger.Unlock();
            //{
            //    BusinessLayer.SMSTrigger ObjSMSTrigger = new BusinessLayer.SMSTrigger();
            //    Hidden1.Value = (ObjSMSTrigger.IsMessageSentToday() == true) ? "1" : "0";
            //    if (Hidden1.Value == "1")
            //        btnSendSMS.Style.Add("display", "none");
            //    else
            //        btnSendSMS.Style.Add("display", "block");
            //}
        }
    }
}