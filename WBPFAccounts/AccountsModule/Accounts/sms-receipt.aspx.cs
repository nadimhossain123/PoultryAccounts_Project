using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinessLayer.Accounts;
using System.IO;

namespace AccountsModule.Accounts
{
    public partial class sms_receipt : System.Web.UI.Page
    {
        DataSet ds;
        string strParams;
        char chr = Convert.ToChar(130);
        public string strFilter;
        clsGeneralFunctions gf = new clsGeneralFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["No"] != null && Request.QueryString["No"].Trim().Length > 0)
                {
                    string VoucherNo = Request.QueryString["No"].ToString();
                    LoadReceipt(VoucherNo);
                }
            }
        }

        protected void LoadReceipt(string VoucherNo)
        {
            BusinessLayer.Common.SemFeesGeneration objBill = new BusinessLayer.Common.SemFeesGeneration();
            DataView DV = new DataView(objBill.SMSFeeReceipt(VoucherNo));
            
            DataTable dt = new DataTable();
            dt = DV.ToTable();
            if (dt.Rows.Count > 0)
            {
                gvCshBnk.DataSource = dt;
                gvCshBnk.DataBind();

                lblSLNo.Text = VoucherNo.ToString().Split('/').Last();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["VoucherDate"]).ToString("dd/MM/yyyy");
                lblMemberName.Text = dt.Rows[0]["CBVNarration"].ToString();

                gvCshBnk.FooterRow.Cells[2].Text = dt.Rows[0]["TotalAmount"].ToString();
            }
        }
    }
}