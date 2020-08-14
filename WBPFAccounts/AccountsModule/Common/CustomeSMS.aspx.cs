using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using BusinessLayer.Accounts;
using System.Text;

namespace AccountsModule.Common
{
    public partial class CustomeSMS : System.Web.UI.Page
    {
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
                LoadMemberSMSCategory();

                BusinessLayer.Common.SMSTrigger objTrigger = new BusinessLayer.Common.SMSTrigger();
                Hidden1.Value = (objTrigger.IsMessageSentToday() == true) ? "1" : "0";

                if (Hidden1.Value == "1")
                    btnSentSMS.Style.Add("display", "none");
                else
                    btnSentSMS.Style.Add("display", "block");

            }
        }

        protected void LoadMemberSMSCategory()
        {
            BusinessLayer.Common.MemberSMSCategory objMemberSMSCategory = new BusinessLayer.Common.MemberSMSCategory();
            DataTable dt = objMemberSMSCategory.MemberSMSCategory_GetAll();
            if (dt != null)
            {
                ddlMemberSMSCategory.DataSource = dt;
                ddlMemberSMSCategory.DataTextField = "MemberSMSCategoryName";
                ddlMemberSMSCategory.DataValueField = "MemberSMSCategoryId";
                ddlMemberSMSCategory.DataBind();
            }
            ddlMemberSMSCategory.Items.Insert(0, "--SELECT--");
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

            return API;
        }

        private DataTable getMobileNumbers()
        {
            BusinessLayer.Common.MemberMaster ObjMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            DataView DV = new DataView(ObjMember.MemberMaster_GetAll_ForSMS(int.Parse(Session["FinYrID"].ToString()), chkIsGovtMember.Checked, (ddlMemberSMSCategory.SelectedIndex == 0) ? 0 : int.Parse(ddlMemberSMSCategory.SelectedValue)));

            //if (ChkIncludeGovtMembers.Checked == false)
            //    DV.RowFilter = "RegistrationType <> 4";

            return DV.ToTable();
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

        protected void ShowMsg(string message)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: alert('" + message + "'); ", true);
        }

        protected void btnSentSMS_Click(object sender, EventArgs e)
        {
            string strValues = Session["FinYrID"].ToString();
            clsGeneralFunctions genObj = new clsGeneralFunctions();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            string startdate = "";
            string enddate = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                startdate = ds.Tables[0].Rows[0]["StartYear"].ToString();
                startdate += "-04-01";
                enddate = ds.Tables[0].Rows[0]["EndYear"].ToString();
                enddate += "-03-31";
            }

            if (!(System.DateTime.Now >= Convert.ToDateTime(startdate) && System.DateTime.Now <= Convert.ToDateTime(enddate)))
            {
                Message.IsSuccess = false;
                Message.Text = "Please select current financial year to send SMS";
                Message.Show = true;
                return;
            }

            string mobiles = "";
            string message = FitMessage(txtMessageBody.Text.Trim());
            BusinessLayer.Common.SMSTrigger objTrigger = new BusinessLayer.Common.SMSTrigger();
            string ROUTE_1 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_1"];
            string ROUTE_2 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_2"];
            API_INDEX = ROUTE_1;

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
                        }
                        else if (Arrmob[i].Length == 12)
                        {
                            if (API_INDEX == "1")
                                mobiles += Arrmob[i].Trim() + ",";
                            else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                                mobiles += Arrmob[i].Trim().Substring(2) + ",";
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
                    DataTable DTMobNos = getMobileNumbers();//Fetch mobile numbers from Member tables based on MemberType(pritam)
                    DataView Dv;
                    Dv = new DataView(DTMobNos);
                    Dv.RowFilter = "IsPriority = 0"; //Route 1
                    Ds.Tables.Add(Dv.ToTable("TBL_PRIORITY_0"));

                    Dv = new DataView(DTMobNos);
                    Dv.RowFilter = "IsPriority = 1"; //Route 2
                    Ds.Tables.Add(Dv.ToTable("TBL_PRIORITY_1"));

                    //    foreach (DataTable DT in Ds.Tables)
                    //    {
                    //        if (DT.Rows.Count > 0)
                    //        {
                    //            if (DT.Rows[0]["IsPriority"].ToString().Equals("0"))
                    //                API_INDEX = ROUTE_1;
                    //            else if (DT.Rows[0]["IsPriority"].ToString().Equals("1"))
                    //                API_INDEX = ROUTE_2;

                    //            int counter = 0;
                    //            mobiles = "";

                    //            foreach (DataRow DR in DT.Rows)
                    //            {
                    //                if (API_INDEX == "1")
                    //                    mobiles += "91" + DR["MobileNo"].ToString() + ",";
                    //                else if (API_INDEX == "2" || API_INDEX == "3" || API_INDEX == "4")
                    //                    mobiles += DR["MobileNo"].ToString() + ",";

                    //                MobNoCount++;
                    //                counter++;

                    //                if (counter == smsPerTrans)
                    //                {
                    //                    mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();
                    //                    strUrl = GetHTTPAPI(mobiles, message);

                    //                    WebRequest request = HttpWebRequest.Create(strUrl);
                    //                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //                    Stream s = (Stream)response.GetResponseStream();
                    //                    StreamReader readStream = new StreamReader(s);
                    //                    dataString = readStream.ReadToEnd();
                    //                    response.Close();
                    //                    s.Close();
                    //                    readStream.Close();

                    //                    counter = 0;
                    //                    mobiles = "";
                    //                }
                    //            }

                    //            if (mobiles.Trim().Length > 0)
                    //            {
                    //                mobiles = mobiles.Trim().Substring(0, mobiles.Length - 1).Trim();
                    //                strUrl = GetHTTPAPI(mobiles, message);

                    //                WebRequest request1 = HttpWebRequest.Create(strUrl);
                    //                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                    //                Stream s1 = (Stream)response1.GetResponseStream();
                    //                StreamReader readStream1 = new StreamReader(s1);
                    //                dataString = readStream1.ReadToEnd();
                    //                response1.Close();
                    //                s1.Close();
                    //                readStream1.Close();
                    //            }
                    //        }
                    //    }
                }
                Message.IsSuccess = true;
                Message.Text = "Message Send Successfully";
            }
            catch (Exception ex)
            {
                Message.IsSuccess = false;
                Message.Text = ex.Message;
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
                Message.Show = true;
            }
        }
    }
}