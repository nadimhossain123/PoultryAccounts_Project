using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class ServiceTaxReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadFeesHead();
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    btnExport.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadFeesHead()
        {
            BusinessLayer.Common.FeesHeadMaster ObjFeesHeadMaster = new BusinessLayer.Common.FeesHeadMaster();
            DataTable dt = ObjFeesHeadMaster.GetAll();

            ddlFeesHead.DataSource = dt;
            ddlFeesHead.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int FeesHeadId = Convert.ToInt32(ddlFeesHead.SelectedValue);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);

            BusinessLayer.Common.MemberPayment ObjMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dt = ObjMemberPayment.GetServiceTaxReport(FeesHeadId, FromDate, ToDate);

            dgvServiceTax.DataSource = dt;
            dgvServiceTax.DataBind();

            if (dt.Rows.Count > 0)
                btnExport.Visible = true;
            else
                btnExport.Visible = false;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>West Bengal Poultry Federation</b>";
            _header[1] = "For " + ddlFeesHead.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[5];
            _footer[0] = "";
            _footer[1] = "";
            _footer[2] = "";
            _footer[3] = "";
            _footer[4] = "";

            string file = "SERVICE_TAX_REPORT_" + DateTime.Today.ToString("dd-MM-yyyy");

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvServiceTax, _footer, file);
        }
    }
}