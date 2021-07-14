using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule
{
    public partial class TestSMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string SendSMS(string To, string Content)
        {
            string msg = "";

            // Message details
            string apiKey ="MGM5N2U4ZTcyOWJjNjE1NmFkMTUxMmI0ZDY0YzBlZjI=";
	        string mobiles = "919735747910";
            string sender = "WBPOLT";
            string message = "WBPF Broiler Suggested Rate for 09/04/2021%nFarmer,PTR,Retailer(Whole Bird/Dressed Bird)%nK.R.R.-148/156/230%nN24P-136/140/146/230%nS24P-138/142/148/230%nHWH-136/140/146/230%nEMDP-132/136/142/225%nWMDP-131/135/141/225%nBNK-129/133/139/220%nPURU-130/134/140/220%nHGLY-132/136/142/225%nBDN-131/135/141/225%nNAD-134/138/144/230%nSURI-128/132/138/220%nMUR-129/133/139/220%nMAL-119/122/128/205%nDDP-119/122/128/205%nUDP-119/122/128/205%nCHBR-116/120/126/200%nJPG-118/122/128/205%nSLG-118/125/133/215%nDARJ-118/128/138/220%nAPDR(FLK)-114/118/124/200%nAPDR-116/120/126/200%nBird weight below 1.2kg.= Dist.rate plus Rs.20.00.NECC WEST BENGAL Egg Rate of 09/04/2021 Rs. 4.70";



           // MyWebRequest myRequest = new MyWebRequest("https://api.textlocal.in/sendsms/", "POST", "apikey="+ apiKey+ "&numbers="+ numbers+ "&sender="+ sender+ "&message="+ message);


            string strUrl = string.Format("https://api.textlocal.in/send/?apiKey="+apiKey+"&sender="+sender+"&numbers="+mobiles+"&message="+message);

            string dataString;


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
            catch (Exception exp)
            {

            }

            return msg;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string To = txtMailTo.Text;
            string reply = SendSMS(To, "TEST SMS");
            lblMsg.Text = reply;

        }



    }
    

    }
