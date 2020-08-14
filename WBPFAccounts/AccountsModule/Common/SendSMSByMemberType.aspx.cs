using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class SendSMSByMemberType : System.Web.UI.Page
    {
        string Mssg = "";
        string API_INDEX;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                Message.Show = false;
                BusinessLayer.Common.SMSTrigger objTrigger = new BusinessLayer.Common.SMSTrigger();
                Hidden1.Value = (objTrigger.IsMessageSentToday() == true) ? "1" : "0";
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
                API = string.Format("http://sms.fourfusiontechnologies.com/new/api/api_http.php?username=FOURFTECH&password=FourFM@2020&senderid=SAHYOG&to={0}&text={1}&route=Informative&type=text", mobiles, message);
            else if (API_INDEX == "6")
                API = string.Format("http://sms.afraconnect.com/api/mt/SendSMS?user=BISWA2K6@GMAIL.COM&password=9836634433&senderid=WBPOLT&channel=Trans&DCS=0&flashsms=0&number={0}&text={1}&route=20", mobiles, message);


           // else if (API_INDEX == "5")
           //     API = string.Format("http://sms.fourfusiontechnologies.com/new/api/api_http.php?username=WBPOLT&password=wbpolt123&senderid=WBPOLT&to={0}&text={1}&route=Informative&type=text", mobiles, message);




            return API;
        }

        private DataTable getMobileNumbers(int type)
        {
            BusinessLayer.Common.SMSMemberMaster objMember = new BusinessLayer.Common.SMSMemberMaster();
            DataView DV = new DataView(objMember.MemberTypeWiseMobileNumbersForSMS(type));

            //if (ChkIncludeGovtMembers.Checked == false)
            //    DV.RowFilter = "RegistrationType <> 4";

            return DV.ToTable();
            //BusinessLayer.Common.MemberMaster ObjMember = new BusinessLayer.Common.MemberMaster();
            //Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            //DataView DV = new DataView(ObjMember.MemberMaster_GetAll_ForSMS(int.Parse(Session["FinYrID"].ToString()), chkIsGovtMember.Checked, (ddlMemberSMSCategory.SelectedIndex == 0) ? 0 : int.Parse(ddlMemberSMSCategory.SelectedValue)));

            ////if (ChkIncludeGovtMembers.Checked == false)
            ////    DV.RowFilter = "RegistrationType <> 4";

            //return DV.ToTable();
        }

        protected void btnSentSMS_Click(object sender, EventArgs e)
        {
            string mobiles = "";
            string message = (txtMessageBody.Text.Trim());
            int smsPerTrans = 50;
            int memberType = int.Parse(ddlRegType.SelectedValue); //1=All, 2=Only Paid,3=Only UnPaid
            BusinessLayer.Common.SMSTrigger objTrigger = new BusinessLayer.Common.SMSTrigger();

            string API = string.Empty;
            //API_INDEX = "1";//Convert.ToString(DV[0]["SMSAPIId"]);
            BusinessLayer.SMS.SMSAPIConfig objSMSAPIConfig = new BusinessLayer.SMS.SMSAPIConfig();
            API_INDEX = objSMSAPIConfig.GetAll().Select("IsSelected=1")[0]["APIId"].ToString();

            string strUrl;
            string dataString;
            int MobNoCount = 0;

            try
            {
                if (txtMobiles.Text.Trim().Length > 0)
                {
                    string[] Arrmob = txtMobiles.Text.Trim().Split(',');
                    for (int i = 0; i < Arrmob.Length; i++)
                    {
                        if (Arrmob[i].Length == 10)
                        {
                            if (API_INDEX == "1")
                                mobiles += "91" + Arrmob[i].Trim() + ",";
                            else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                mobiles += Arrmob[i].Trim() + ",";
                            else if (API_INDEX == "5" || API_INDEX == "6")
                                mobiles += "91" + Arrmob[i].Trim() + ",";
                        }
                        else if (Arrmob[i].Length == 12)
                        {
                            if (API_INDEX == "1")
                                mobiles += Arrmob[i].Trim() + ",";
                            else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                mobiles += Arrmob[i].Trim().Substring(2) + ",";
                            else if (API_INDEX == "5" || API_INDEX == "6")
                                mobiles += Arrmob[i].Trim() + ",";
                        }
                    }

                    if (mobiles.Trim().Length > 0)
                    {
                        mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();
                        strUrl = GetHTTPAPI(mobiles, message);

                        WebRequest request1 = HttpWebRequest.Create(strUrl);
                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                        Stream s1 = (Stream)response1.GetResponseStream();
                        StreamReader readStream1 = new StreamReader(s1);
                        dataString = readStream1.ReadToEnd();
                        response1.Close();
                        s1.Close();
                        readStream1.Close();
                    }
                }
                else
                {
                    DataSet Ds = new DataSet();
                    DataTable DTMobNos = getMobileNumbers(memberType);//Fetch mobile numbers from Member tables based on MemberType(pritam)
                    DataView Dv;
                    Dv = new DataView(DTMobNos);
                    Ds.Tables.Add(Dv.ToTable("TBL_PRIORITY_0"));

                    foreach (DataTable DT in Ds.Tables)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            int counter = 0;
                            mobiles = "";

                            foreach (DataRow DR in DT.Rows)
                            {
                                if (API_INDEX == "1")
                                    mobiles += "91" + DR["MobileNo"].ToString() + ",";
                                else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                    mobiles += DR["MobileNo"].ToString() + ",";
                                else if (API_INDEX == "5" || API_INDEX == "6")
                                    mobiles += "91" + DR["MobileNo"].ToString() + ",";

                                MobNoCount++;
                                counter++;

                                if (counter == smsPerTrans)
                                {
                                    mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();
                                    strUrl = GetHTTPAPI(mobiles, message);

                                    WebRequest request = HttpWebRequest.Create(strUrl);
                                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                    Stream s = (Stream)response.GetResponseStream();
                                    StreamReader readStream = new StreamReader(s);
                                    dataString = readStream.ReadToEnd();
                                    response.Close();
                                    s.Close();
                                    readStream.Close();

                                    counter = 0;
                                    mobiles = "";
                                }
                            }

                            if (mobiles.Trim().Length > 0)
                            {
                                mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();
                                strUrl = GetHTTPAPI(mobiles, message);

                                WebRequest request1 = HttpWebRequest.Create(strUrl);
                                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                Stream s1 = (Stream)response1.GetResponseStream();
                                StreamReader readStream1 = new StreamReader(s1);
                                dataString = readStream1.ReadToEnd();
                                response1.Close();
                                s1.Close();
                                readStream1.Close();
                            }
                        }
                    }
                }
                ShowMsg("Message Send Successfully");

            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
            finally
            {
                if (txtMobiles.Text.Trim().Length == 0)
                {
                    if (MobNoCount > 0)
                    {
                        objTrigger.Save(MobNoCount);
                        Hidden1.Value = "1";
                        btnSentSMS.Style.Add("display", "none");
                    }
                }
            }
        }

        protected void ShowMsg(string message)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: alert('" + message + "'); ", true);
        }
    }
}