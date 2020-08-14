using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace AccountsModule.SMS
{
    public partial class SendSMS : System.Web.UI.Page
    {
        string Mssg = "";
        string API_INDEX;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                BusinessLayer.SMS.SMSTrigger objTrigger = new BusinessLayer.SMS.SMSTrigger();
                Hidden1.Value = (objTrigger.IsMessageSentToday() == true) ? "1" : "0";

                if (Hidden1.Value == "1")
                    btnSend.Style.Add("display", "none");
                else
                    btnSend.Style.Add("display", "block");

                ddlDays.SelectedValue = DateTime.Now.Day.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                BStrMssg();
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

        private DataTable getMobileNumbers(int type)
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            DataView DV = new DataView(ObjMember.getMobileNumbers(type));

            if (ChkIncludeGovtMembers.Checked == false)
                DV.RowFilter = "RegistrationType <> 4";

            return DV.ToTable();
        }

        protected String BStrMssg()
        {
            int Day = int.Parse(ddlDays.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            string districtDetails = "";
            //string SDate = (Month + "/" + Day + "/" + Year).ToString();
            string SDate = (Year + "-" + Month + "-" + Day).ToString();
            DateTime DDate = Convert.ToDateTime(SDate);

            StringBuilder StrMssg = new StringBuilder();
            string Str1 = "WBPF Broiler suggested rate for " + DDate.ToString("dd/MM/yyyy") + "\n";
            StrMssg.Append(Str1);

            BusinessLayer.SMS.BirdPrice ObjBirdPrice = new BusinessLayer.SMS.BirdPrice();
            DataTable dt = ObjBirdPrice.GetAll(DDate);

            if (Mssg == "")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    districtDetails = dr["DistrictName"].ToString() + "-" + dr["FarmRate"].ToString() + "/" + dr["RetailerRate"].ToString() + "/" + dr["BroilerRate"].ToString() + "/" + dr["DressedRate"].ToString() + "\n";
                    StrMssg.Append(districtDetails);
                }
                BusinessLayer.SMS.EggPrice ObjEggPrice = new BusinessLayer.SMS.EggPrice();
                Entity.SMS.EggPrice Eggprice = new Entity.SMS.EggPrice();
                Eggprice = ObjEggPrice.GetAllById(DDate);


                //string Str2 = "Bird weight below" + Eggprice.belowWt.ToString() + "kg.=Dist.rate plus Rs." + Eggprice.belowAddRate.ToString() + ".NECC egg rate-" + Eggprice.NECCPrice.ToString();
                string Str2 = "Bird weight below " + Eggprice.belowWt.ToString() + "kg.=Dist.rate plus Rs." + Eggprice.belowAddRate.ToString() + ". NECC egg rate " + Eggprice.NECCPrice.ToString();
                StrMssg.Append(Str2);

                txtMssg.Text = StrMssg.ToString();
            }
            else
                Mssg = txtMssg.Text;

            return (Mssg);

        }

        private string FitMessage(string MessageStr)
        {
            string FitString = "";
            string[] MessageQueue = MessageStr.Trim().Split('\n');
            for (int i = 0; i < MessageQueue.Length; i++)
            {
                if (i != MessageQueue.Length - 1)
                {
                    FitString += MessageQueue[i].Trim() + "\n";
                }
                else
                {
                    FitString += MessageQueue[i].Trim();
                }
            }

            return FitString.Trim();
        }

        protected void GetPriceBtn_Click(object sender, EventArgs e)
        {
            Mssg = "";
            BStrMssg();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.ApiConfiguration ObjApi = new BusinessLayer.SMS.ApiConfiguration();
            DataTable dt = ObjApi.GetAll();
            DataView DV = new DataView(dt);
            DV.RowFilter = "IsActive = 1";
            string mobiles = "";
            string message = FitMessage(txtMssg.Text.Trim());
            int smsPerTrans = 20;
            int memberType = 1; //1=All, 2=Only Paid,3=Only UnPaid
            BusinessLayer.SMS.SMSTrigger objTrigger = new BusinessLayer.SMS.SMSTrigger();
            //string ROUTE_1 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_1"];
            //string ROUTE_2 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_2"];
            //API_INDEX = ROUTE_1;

            string API = string.Empty;
            API_INDEX = Convert.ToString(DV[0]["SMSAPIId"]);

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
                            else if (API_INDEX == "5")
                                mobiles += "91" + Arrmob[i].Trim() + ";";
                        }
                        else if (Arrmob[i].Length == 12)
                        {
                            if (API_INDEX == "1")
                                mobiles += Arrmob[i].Trim() + ",";
                            else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                mobiles += Arrmob[i].Trim().Substring(2) + ",";
                            else if (API_INDEX == "5")
                                mobiles += Arrmob[i].Trim() + ";";
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
                    Dv.RowFilter = "Priority = 0"; //Route 1
                    Ds.Tables.Add(Dv.ToTable("TBL_PRIORITY_0"));

                    Dv = new DataView(DTMobNos);
                    Dv.RowFilter = "Priority = 1"; //Route 2
                    Ds.Tables.Add(Dv.ToTable("TBL_PRIORITY_1"));

                    foreach (DataTable DT in Ds.Tables)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            //if (DT.Rows[0]["Priority"].ToString().Equals("0"))
                            //    API_INDEX = ROUTE_1;
                            //else if (DT.Rows[0]["Priority"].ToString().Equals("1"))


                            //API_INDEX = ROUTE_2;

                            int counter = 0;
                            mobiles = "";

                            foreach (DataRow DR in DT.Rows)
                            {
                                if (API_INDEX == "1")
                                    mobiles += "91" + DR["MobileNo"].ToString() + ",";
                                else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                    mobiles += DR["MobileNo"].ToString() + ",";
                                else if (API_INDEX == "5")
                                    mobiles += "91" + DR["MobileNo"].ToString() + ";";

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
                        btnSend.Style.Add("display", "none");
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