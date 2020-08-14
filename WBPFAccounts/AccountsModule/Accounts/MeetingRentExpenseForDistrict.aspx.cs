using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using BusinessLayer.Accounts;
using System.Web.UI.WebControls;
using System.Text;

namespace AccountsModule.Accounts
{
    public partial class MeetingRentExpenseReport : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("Select", "0");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ResetControls();
                //txtFromDate.Text = Convert.ToDateTime(Session["SesFromDate"].ToString()).ToString("dd/MM/yyyy");
                //txtToDate.Text = Convert.ToDateTime(Session["SesToDate"].ToString()).ToString("dd/MM/yyyy");
            }
        }

        protected void ResetControls()
        {
            LoadLedger();
            LoadDistrict();
            txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtToDate.Attributes.Add("readonly", "readonly");
            //this.btnSave.Attributes["onclick"] = "return Confirmationmessage();";
            btnDownload.Visible = false;
        }

        protected void LoadLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType NOT IN ('BNK', 'CASH')";
            if (dv != null)
            {
                ddlLedgerS.DataSource = dv;
                ddlLedgerS.DataBind();
            }
            InsertFisrtItem(ddlLedgerS, "--SELECT--");
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }
        protected void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            int stateid = 0;
            DataView dtDistrict = new DataView(objDistrict.GetAll(stateid));
            dtDistrict.RowFilter = "DistrictId NOT IN (3,5,9,10,13,15,18,27,28,33)";
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataBind();
                InsertFisrtItem(ddlDistrict, "--SELECT--");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }
        private void BindReport()
        {
            Entity.Accounts.MeetingRentExpense EntMeet = new Entity.Accounts.MeetingRentExpense();
            EntMeet.LedgerId = Convert.ToInt32(ddlLedgerS.SelectedValue);
            EntMeet.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            string FromDate1 = (txtFromDate.Text.ToString());
            string ToDate1 = (txtToDate.Text.ToString());

            if (FromDate1 != "" && ToDate1 != "")
            {
                EntMeet.FromDate = Convert.ToDateTime(FromDate1);// Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
                EntMeet.ToDate = Convert.ToDateTime(ToDate1);// Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            }
            else { EntMeet.FromDate = DateTime.MinValue; EntMeet.ToDate = DateTime.MinValue; }
            BusinessLayer.Accounts.MeetingRentExpense objmeeting = new MeetingRentExpense();
            DataTable DT = objmeeting.GetAll(EntMeet);
            if (DT != null && DT.Rows.Count > 0)
            {
                grdReport.DataSource = DT;
                grdReport.DataBind();
                btnDownload.Visible = true;
            }
        }

        protected void grdReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable DT = (DataTable)grdReport.DataSource;
                //Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                //int LedgerId = int.Parse(grdReport.DataKeys[e.Row.RowIndex].Values["LedgerId"].ToString());
                if (((DataTable)(grdReport.DataSource)).Rows[e.Row.RowIndex]["DistrictId"].ToString() == "0")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightBlue;
                    }
                }
                if (((DataTable)(grdReport.DataSource)).Rows[e.Row.RowIndex]["DistrictId"].ToString() == "")
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                    }
                }
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[3];
            _header[0] = "<b>WEST BENGAL POULTRY FEDERATION</b>";
            _header[1] = "From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "";

            string[] _footer = new string[1];
            _footer[0] = "";

            string file = "MEETING_RENT_EXPENSE_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, grdReport, _footer, file);
        }

        protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReport.PageIndex = e.NewPageIndex;
            BindReport();
        }
    }
}
