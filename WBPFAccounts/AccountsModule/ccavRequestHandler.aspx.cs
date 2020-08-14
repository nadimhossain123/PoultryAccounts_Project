using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;

namespace AccountsModule
{
    public partial class SubmitData : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "FF119789EA009958AC9FF44758A5A526";//"272ECCEC820303D21327C07B92A5A367";//"FF119789EA009958AC9FF44758A5A526";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "AVCR83GC95AY39RCYA";//"AVCR83GC95AY39RCYA";// put the access key in the quotes provided here.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["DATA"] != null && Request.QueryString["DATA"].Length > 0)
                {
                    ccaRequest = ccaCrypto.Decrypt(Request.QueryString["DATA"], workingKey);
                }
                else
                {
                    foreach (string name in Request.Form)
                    {
                        if (name != null)
                        {
                            if (!name.StartsWith("_"))
                            {
                                ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                                /* Response.Write(name + "=" + Request.Form[name]);
                                  Response.Write("</br>");*/
                            }
                        }
                    }
                }
                strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }
        }

        #region Another Way
        //Another Way
        //public string CCAvenueItemList
        //{
        //    get
        //    {
        //        System.Text.StringBuilder CCAvenueItems = new System.Text.StringBuilder();
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        System.Data.DataTable dtClientInfo = new System.Data.DataTable();
        //        dt = (System.Data.DataTable)Session["CheckedItems"];
        //        dtClientInfo = (System.Data.DataTable)Session["ClientInfo"];
        //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        {

        //            string amountTemplate = "<input type=\"hidden\" name=\"Amount\" value=\"$Amount$\" />\n";
        //            string orderTemplate = "<input type=\"hidden\" name=\"Order_Id\" value=\"$Order_Id$\" />\n";

        //            // BILLING INFO
        //            string billingNameTemplate = "<input type=\"hidden\" name=\"billing_cust_name\" value=\"$billing_cust_name$\" />\n";
        //            string billingCustAddressTemplate = "<input type=\"hidden\" name=\"billing_cust_address\" value=\"$billing_cust_address$\" />\n";
        //            string billingCountryTemplate = "<input type=\"hidden\" name=\"billing_cust_country\" value=\"$billing_cust_country$\" />\n";
        //            string billingEmailTemplate = "<input type=\"hidden\" name=\"billing_cust_email\" value=\"$billing_cust_email$\" />\n";
        //            string billingTelTemplate = "<input type=\"hidden\" name=\"billing_cust_tel\" value=\"$billing_cust_tel$\" />\n";
        //            string billingStateTemplate = "<input type=\"hidden\" name=\"billing_cust_state\" value=\"$billing_cust_state$\" />\n";
        //            string billingCityTemplate = "<input type=\"hidden\" name=\"billing_cust_city\" value=\"$billing_cust_city$\" />\n";
        //            string billingZipTemplate = "<input type=\"hidden\" name=\"billing_zip_code\" value=\"$billing_zip_code$\" />\n";

        //            billingCustAddressTemplate = billingCustAddressTemplate.Replace("$billing_cust_address$", dtClientInfo.Rows[0]["Address"].ToString());
        //            billingCountryTemplate = billingCountryTemplate.Replace("$billing_cust_country$", dtClientInfo.Rows[0]["Country"].ToString());
        //            billingEmailTemplate = billingEmailTemplate.Replace("$billing_cust_email$", dtClientInfo.Rows[0]["Email_ID"].ToString());
        //            billingTelTemplate = billingTelTemplate.Replace("$billing_cust_tel$", dtClientInfo.Rows[0]["Phone_no"].ToString());
        //            billingStateTemplate = billingStateTemplate.Replace("$billing_cust_state$", dtClientInfo.Rows[0]["State"].ToString());
        //            billingCityTemplate = billingCityTemplate.Replace("$billing_cust_city$", dtClientInfo.Rows[0]["City"].ToString());
        //            billingZipTemplate = billingZipTemplate.Replace("$billing_zip_code$", dtClientInfo.Rows[0]["ZipCode"].ToString());

        //            strAmount = dt.Rows[i]["INR"].ToString();
        //            amountTemplate = amountTemplate.Replace("$Amount$", dt.Rows[i]["INR"].ToString());
        //            orderTemplate = orderTemplate.Replace("$Order_Id$", dt.Rows[i]["ClientID"].ToString());
        //            billingNameTemplate = billingNameTemplate.Replace("$billing_cust_name$", dtClientInfo.Rows[0]["Name"].ToString());

        //            CCAvenueItems.Append(amountTemplate)
        //                .Append(orderTemplate)
        //                .Append(billingNameTemplate)
        //                .Append(billingCustAddressTemplate)
        //                .Append(billingCountryTemplate)
        //                .Append(billingEmailTemplate)
        //                .Append(billingTelTemplate)
        //                .Append(billingStateTemplate)
        //                .Append(billingCityTemplate)
        //                .Append(billingZipTemplate)
        //                .Append(deliveryNameTemplate)
        //                .Append(deliveryCustAddressTemplate)
        //                .Append(deliveryCountryTemplate)
        //          }
        //        return CCAvenueItems.ToString();
        //    }
        //}
        //public string propcheckSum
        //{
        //    get
        //    {
        //        libfuncs objLib = new libfuncs();
        //        string strCheckSum = objLib.getchecksum("YourMerchantID", Session["ClientID"].ToString(), strAmount, "UrReturnUrl", "your working key");
        //        return strCheckSum;
        //    }
        //}
        #endregion
    }
}