using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace AccountsModule.SMS
{
    public partial class DocSMSReport : System.Web.UI.Page
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
            int GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedValue);

            BusinessLayer.SMS.DoctorsSMSTrigger objDoc = new BusinessLayer.SMS.DoctorsSMSTrigger();
            DataTable dtdoc = new DataTable();
            dtdoc = objDoc.GetAll(GroupId, Month, Year);
            if (dtdoc != null)
            {
                dlReport.DataSource = dtdoc;
                dlReport.DataBind();
            }
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