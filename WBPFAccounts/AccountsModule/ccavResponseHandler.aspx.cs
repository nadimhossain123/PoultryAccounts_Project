using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.Linq;
using CCA.Util;

namespace AccountsModule
{
    public partial class ResponseHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //if (Request.Form["encResp"] != null && Request.Form["encResp"].Length > 0) { Response.Write("<script>alert('Payment Cancel '" + Request.Form["encResp"] + ");</script>"); }
            try
            {
                Message.Show = false;
                if (Request.Form.AllKeys.Contains("encResp"))
                {
                    if (Request.Form["encResp"] != null && Request.Form["encResp"].Length > 0)
                    {
                        string workingKey = "FF119789EA009958AC9FF44758A5A526";// "272ECCEC820303D21327C07B92A5A367";//"FF119789EA009958AC9FF44758A5A526";//put in the 32bit alpha numeric key in the quotes provided here
                        CCA.Util.CCACrypto ccaCrypto = new CCA.Util.CCACrypto();
                        string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                        NameValueCollection Params = new NameValueCollection();

                        string[] segments = encResponse.Split('&');
                        foreach (string seg in segments)
                        {
                            string[] parts = seg.Split('=');
                            if (parts.Length > 0)
                            {
                                string Key = parts[0].Trim();
                                string Value = parts[1].Trim();
                                Params.Add(Key, Value);
                            }
                        }
                        #region
                        //string jsMethodName = "";
                        //for (int i = 0; i < Params.Count; i++)
                        //{
                        //    StringBuilder sb = new StringBuilder();
                        //    jsMethodName+=("<tr><td class='label'>" + Params.Keys[i] + "</td><td>" + Params[i] + "</td></tr>");
                        //    //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
                        //}
                        //string[] values = null;
                        //foreach (string key in Params.Keys)
                        //{
                        //    values = Params.GetValues(key);
                        //    foreach (string value in values)
                        //    {
                        //        //MessageBox.Show(key + " - " + value);
                        //        //if (key.Equals("order_id"))
                        //        //{
                        //        //    lblOrderId.Text = Params["order_id"];
                        //        //}
                        //        Response.Write(key + " = " + value + "<br>");
                        //    }
                        //}
                        #endregion
                        order_id.Text = Params["order_id"];
                        tracking_id.Text = Params["tracking_id"];
                        bank_ref_no.Text = Params["bank_ref_no"];
                        order_status.Text = Params["order_status"];
                        mer_amount.Text = Params["mer_amount"];
                        trans_date.Text = Params["trans_date"];
                        failure_message.Text = Params["failure_message"];
                        payment_mode.Text = Params["payment_mode"];
                        billing_name.Text = Params["billing_name"];
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ALert", "alert('" + jsMethodName + "')", true);

                        Entity.Common.PaymentGateway payment = new Entity.Common.PaymentGateway();
                        payment.PaymentId = Convert.ToInt32(Params["merchant_param1"]);
                        payment.MemberId = Convert.ToInt32(Params["merchant_param2"]);
                        payment.MemberType = Params["merchant_param3"];
                        payment.OrderId = Params["order_id"];
                        payment.TrackingId = Params["tracking_id"];
                        payment.BankRefNo = Params["bank_ref_no"];
                        payment.OrderStatus = Params["order_status"];
                        payment.PaymentAmount = Convert.ToDecimal(Params["mer_amount"]);
                        payment.FailureMessage = Params["failure_message"];
                        payment.PaymentMode = Params["payment_mode"];
                        string transDate = Params["trans_date"];
                        if (transDate != null && transDate.Length > 0)
                        {
                            string[] tranDate = transDate.Split(' ');
                            string[] tranDate1 = tranDate[0].Split('/');
                            payment.PaymentDate = Convert.ToDateTime(tranDate1[2] + "-" + tranDate1[1] + "-" + tranDate1[0] + " " + tranDate[1]);// Params["trans_date"]);
                        }
                        else { payment.PaymentDate = DateTime.Now; }
                        payment.Currency = Params["currency"];
                        payment.CardName = Params["card_name"];
                        payment.StatusCode = Params["status_code"];
                        payment.StatusMessage = Params["status_message"];
                        payment.CardName = Params["card_name"];
                        payment.CreatedBy = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;
                        BusinessLayer.Common.MemberPayment ObjPayment = new BusinessLayer.Common.MemberPayment();
                        //DataTable DT = ObjPayment.GetAllById(payment.PaymentId);

                        //if (Convert.ToDecimal(DT.Rows[0]["PaymentAmount"]) == payment.PaymentAmount)
                        //{
                            ObjPayment.PaymentResponseSave(payment);
                        //}
                        //else
                        //{
                        //    order_status.Text = "Transaction Failed";
                        //    failure_message.Text = "Amount is not matched";
                        //}
                        if (payment.OrderStatus == "Success")
                        {
                            order_status.ForeColor = System.Drawing.Color.Green;
                            Message.IsSuccess = true;
                            Message.Text = "Payment detail saved successfully";
                        }
                        else
                        {
                            order_status.ForeColor = System.Drawing.Color.Red;
                            failure_message.ForeColor = System.Drawing.Color.Red;
                            Message.IsSuccess = false;
                            Message.Text = "Please try again later!";
                        }
                        Message.Show = true;
                    }
                    else { Response.Write("<script>alert('Payment Cancel ');</script>"); }
                }
                else { Response.Write("<script>alert('Payment Cancel ');</script>"); }
            }
            catch (Exception ex) { Message.Text = ex.Message; Message.Show = true; }
            //}
        }
    }
}