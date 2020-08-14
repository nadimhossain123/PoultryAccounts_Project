using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class FeeCollectionReport : System.Web.UI.Page
    {
        public decimal TotalAdmissionFee, TotalAdmissionFeeTax, TotalRenewalFee, TotalRenewalFeeTax, TotalDevelopmentFee;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadDistrict();
                    txtFromDate.Text = Convert.ToDateTime(Session["SesFromDate"].ToString()).ToString("dd/MM/yyyy");
                    txtToDate.Text = Convert.ToDateTime(Session["SesToDate"].ToString()).ToString("dd/MM/yyyy");
                    Message.Show = false;
                    //LoadFeeCollectionReport();
                    btnExportExcel.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }
        protected void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dtDistrict = new DataTable();
            int stateid = 0;

            dtDistrict = objDistrict.GetAll(stateid);
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
            InsertFisrtItem(ddlDistrict, "--Select District--");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFeeCollectionReport();
        }

        protected void LoadFeeCollectionReport()
        {
            int DistrictId = int.Parse(ddlDistrict.SelectedValue);
            string FromDate1 = (txtFromDate.Text.ToString());
            string ToDate1 = (txtToDate.Text.ToString());
            DateTime FromDate, ToDate;
            if (FromDate1 != "" && ToDate1 != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
                ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);
            }
            else { FromDate = DateTime.MinValue; ToDate = DateTime.MinValue; }
            BusinessLayer.Common.MemberPayment ObjFee = new BusinessLayer.Common.MemberPayment();
            DataTable dt = ObjFee.GetFeeCollectionBusinessTypeReport(DistrictId, FromDate, ToDate);
            if (dt.Rows.Count > 0)
            {
                dgvFeeCollectionReport.DataSource = dt;
                dgvFeeCollectionReport.DataBind();
                btnExportExcel.Visible = true;
            }
            else btnExportExcel.Visible = false;
            //Total Footer Added

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>WEST BENGAL POULTRY FEDERATION</b>";
            _header[1] = "For " + ddlDistrict.SelectedItem.Text + " From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString("dd/MM/yyyy");
            _header[3] = "";

            string[] _footer = new string[0];
            //_footer[0] = "";

            string file = "FEE_COLLECTION_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvFeeCollectionReport, _footer, file);
        }

        protected void dgvFeeCollectionReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAdmissionFee = (Label)e.Row.FindControl("lblAdmissionFee");
                Label lblAdmissionFeeTax = (Label)e.Row.FindControl("lblAdmissionFeeTax");
                Label lblRenewalFee = (Label)e.Row.FindControl("lblRenewalFee");
                Label lblRenewalFeeTax = (Label)e.Row.FindControl("lblRenewalFeeTax");
                Label lblDevelopmentFee = (Label)e.Row.FindControl("lblDevelopmentFee");
                decimal AdmissionFee = lblAdmissionFee.Text == "" ? 0 : decimal.Parse(lblAdmissionFee.Text.Trim());
                decimal AdmissionFeeTax = lblAdmissionFeeTax.Text == "" ? 0 : decimal.Parse(lblAdmissionFeeTax.Text.Trim());
                decimal RenewalFee = lblRenewalFee.Text == "" ? 0 : decimal.Parse(lblRenewalFee.Text.Trim());
                decimal RenewalFeeTax = lblRenewalFeeTax.Text == "" ? 0 : decimal.Parse(lblRenewalFeeTax.Text.Trim());
                decimal DevelopmentFee = lblDevelopmentFee.Text == "" ? 0 : decimal.Parse(lblDevelopmentFee.Text.Trim());
                TotalAdmissionFee += AdmissionFee;
                TotalAdmissionFeeTax += AdmissionFeeTax;
                TotalRenewalFee += RenewalFee;
                TotalRenewalFeeTax += RenewalFeeTax;
                TotalDevelopmentFee += DevelopmentFee;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdmissionFee = (Label)e.Row.FindControl("lblTotalAdmissionFee");
                Label lblTotalAdmissionFeeTax = (Label)e.Row.FindControl("lblTotalAdmissionFeeTax");
                Label lblTotalRenewalFee = (Label)e.Row.FindControl("lblTotalRenewalFee");
                Label lblTotalRenewalFeeTax = (Label)e.Row.FindControl("lblTotalRenewalFeeTax");
                Label lblTotalDevelopmentFee = (Label)e.Row.FindControl("lblTotalDevelopmentFee");
                lblTotalAdmissionFee.Text = TotalAdmissionFee == 0 ? "0.00" : TotalAdmissionFee.ToString(".00");
                lblTotalAdmissionFeeTax.Text = TotalAdmissionFeeTax == 0 ? "0.00" : TotalAdmissionFeeTax.ToString(".00");
                lblTotalRenewalFee.Text = TotalRenewalFee == 0 ? "0.00" : TotalRenewalFee.ToString(".00");
                lblTotalRenewalFeeTax.Text = TotalRenewalFeeTax == 0 ? "0.00" : TotalRenewalFeeTax.ToString(".00");
                lblTotalDevelopmentFee.Text = TotalDevelopmentFee == 0 ? "0.00" : TotalDevelopmentFee.ToString(".00");
            }
        }
    }
}