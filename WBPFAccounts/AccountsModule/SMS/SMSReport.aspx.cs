using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace AccountsModule.SMS
{
    public partial class SMSReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadYear();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        private void LoadYear()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Year", typeof(int));

            for (int i = 2011; i <= DateTime.Now.Year + 1; i++)
            {
                DT.Rows.Add(i);
            }

            ddlYear.DataSource = DT;
            ddlYear.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            int RenewalSmsPerDay = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["RENEWAL_SMS_PER_DAY"]);
            int CreditPerSms = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CREDIT_PER_SMS"]);

            SqlConnection con = new SqlConnection(ConnectionString);
            string sql = string.Format(@"SELECT * FROM SMSTrigger WHERE MONTH(TriggerDate)={0} AND YEAR(TriggerDate)={1}", ddlMonth.SelectedValue, ddlYear.SelectedValue);
            SqlCommand command = new SqlCommand(sql);
            command.Connection = con;
            command.CommandType = System.Data.CommandType.Text;
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            con.Close();

            dt.AsEnumerable().ToList().ForEach(r => r["NoOfTrigger"] = (Convert.ToInt32(r["NoOfTrigger"]) * CreditPerSms) + (RenewalSmsPerDay * CreditPerSms));

            dlReport.DataSource = dt;
            dlReport.DataBind();
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Report.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //Change the Header Row back to white color
            dlReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < dlReport.HeaderRow.Cells.Count; i++)
            {
                dlReport.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            dlReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //return;
        }
    }
}