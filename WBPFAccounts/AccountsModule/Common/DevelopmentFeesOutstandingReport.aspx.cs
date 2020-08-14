using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLayer.Accounts;
using System.Text;

namespace AccountsModule.Common
{
    public partial class DevelopmentFeesOutstandingReport : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        public DateTime FromDate;
        public DateTime ToDate;
       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["MemberId"] == null)
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["MemberId"] != null)
                this.MasterPageFile = "../EmptyMaster.master";
            else if (Session["UserType"].ToString().Equals("Member"))
                this.MasterPageFile = "../MasterMember.master";
            else if (Session["UserType"].ToString().Equals("Agent"))
                this.MasterPageFile = "../MasterAgent.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    string strValues = Session["FinYrID"].ToString();
                    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartYear"].ToString() + "-04-01");
                        ToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndYear"].ToString() + "-03-31");
                    }
                    else
                    {
                        if (DateTime.Now.Month >= 4 && DateTime.Now.Month <= 12)
                        {
                            FromDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-04-01");
                            ToDate = Convert.ToDateTime((DateTime.Now.Year + 1).ToString() + "-03-31");
                        }
                        else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
                        {
                            FromDate = Convert.ToDateTime((DateTime.Now.Year - 1).ToString() + "-04-01");
                            ToDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-03-31");
                        }
                    }

                    LoadMemberOutstandingList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadMemberOutstandingList()
        {
            int MemberId = Convert.ToInt32(Request.QueryString["MemberId"].ToString());
            
            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataSet ds = objMemberPayment.GetDevelopmentFeesOutstandingReport(MemberId, FromDate, ToDate);

            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("To,<br/>");
                sb.Append(ds.Tables[0].Rows[0]["MemberName"].ToString().ToUpper() + "<br/>");
                sb.Append(ds.Tables[0].Rows[0]["BlockName"].ToString().ToUpper()+", " + ds.Tables[0].Rows[0]["DistrictName"].ToString().ToUpper() + "<br/>");
                sb.Append(ds.Tables[0].Rows[0]["StateName"].ToString().ToUpper() + "<br/>");
                sb.Append("Mobile No. " + ds.Tables[0].Rows[0]["MobileNo"].ToString().ToUpper());

                lblMemberDetail.Text = Server.HtmlDecode(sb.ToString());

                sb = new StringBuilder();
                sb.Append("Date: " + DateTime.Now.ToString("dd.MM.yyyy") + "<br/>");
                sb.Append("<b>Membership No. </b>" + ds.Tables[0].Rows[0]["MemberCode"].ToString().ToUpper() + "<br/>");
                sb.Append("<b>Membership Date </b>" + Convert.ToDateTime(ds.Tables[0].Rows[0]["MembershipDate"]).ToString("dd/MM/yyyy"));

                lblDate.Text = Server.HtmlDecode(sb.ToString());
            }

            dgvReport.DataSource = ds.Tables[1];
            dgvReport.DataBind();
        }      

        protected void dgvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dgvReport.DataKeys[e.Row.RowIndex].Values["TOTAL_FIELD"].ToString().Equals("1"))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                    }
                }
            }
        }
    }
}