using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.UserControl
{
    public partial class Message : System.Web.UI.UserControl
    {

        public bool IsSuccess { get; set; }
        public string Text
        {
            set
            {
                if (IsSuccess == true)
                {
                    SpanSuccessText.InnerHtml = "<b>Success Message</b><br />" + value;
                }
                else
                {
                    SpanErrorText.InnerHtml = "<b>Warning Message</b><br />" + value;
                }
            }
        }

        public bool Show
        {
            set
            {
                if (value == true)
                {
                    if (IsSuccess == true)
                    {
                        divsuccess.Visible = true;
                        diverror.Visible = false;
                    }
                    else
                    {
                        divsuccess.Visible = false;
                        diverror.Visible = true;
                    }
                }
                else
                {
                    divsuccess.Visible = false;
                    diverror.Visible = false;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}