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
    public partial class MemberOutstandingReport : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
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
                    LoadMemberList();
                    //======================= Code Added to display outstansing for selected financial year=============
                    string strValues = Session["FinYrID"].ToString();
                    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtFromDate.Text = "01/04/" + ds.Tables[0].Rows[0]["StartYear"].ToString();
                        txtToDate.Text = "31/03/" + ds.Tables[0].Rows[0]["EndYear"].ToString();
                    }
                    else
                    {
                        if (DateTime.Now.Month >= 4 && DateTime.Now.Month <= 12)
                        {
                            txtFromDate.Text = "01/04/" + DateTime.Now.Year.ToString();
                            txtToDate.Text = "31/03/" + (DateTime.Now.Year + 1).ToString();
                        }
                        else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
                        {
                            txtFromDate.Text = "01/04/" + (DateTime.Now.Year - 1).ToString();
                            txtToDate.Text = "31/03/" + DateTime.Now.Year.ToString();
                        }
                    }
                    //====================================
                    //if (DateTime.Now.Month >= 4 && DateTime.Now.Month <= 12)
                    //{
                    //    txtFromDate.Text = "01/04/" + DateTime.Now.Year.ToString();
                    //    txtToDate.Text = "31/03/" + (DateTime.Now.Year + 1).ToString();
                    //}
                    //else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
                    //{
                    //    txtFromDate.Text = "01/04/" + (DateTime.Now.Year - 1).ToString();
                    //    txtToDate.Text = "31/03/" + DateTime.Now.Year.ToString();
                    //}

                    if (!ddlMember.SelectedValue.Equals("0"))
                    {
                        LoadMemberOutstandingList();
                    }
                    else
                    {
                        dgvMemberOutstanding.DataSource = null;
                        dgvMemberOutstanding.DataBind();
                        btnDownload.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            membermaster.BlockId = 0;
            membermaster.DistrictId = 0;
            membermaster.StateId = 0;
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));

            if (Session["UserType"].ToString().Equals("Member"))
            {
                ddlMember.SelectedValue = Session["UserId"].ToString();
                ddlMember.Enabled = false;
            }
            else if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["MemberId"] != null && Request.QueryString["MemberId"].Trim().Length > 0)
            {
                ddlMember.SelectedValue = Request.QueryString["MemberId"].ToString();
                ddlMember.Enabled = false;
            }
            else
            {
                ddlMember.Enabled = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberOutstandingList();
        }

        private void LoadMemberOutstandingList()
        {
            int MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Split('/')[2] + "-" + txtFromDate.Text.Split('/')[1] + "-" + txtFromDate.Text.Split('/')[0]);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Split('/')[2] + "-" + txtToDate.Text.Split('/')[1] + "-" + txtToDate.Text.Split('/')[0]);

            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataSet ds = objMemberPayment.GetOutstandingReport(MemberId, FromDate, ToDate);

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

            //dgvMemberOutstanding.DataSource = dt;
            //dgvMemberOutstanding.DataBind();

            //if (dt.Rows.Count > 0)
            //{
               // btnDownload.Visible = true;
           // }
            //else
            //{
                //btnDownload.Visible = false;
           // }
        }

        protected void dgvMemberOutstanding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SlNo = Convert.ToInt32(dgvMemberOutstanding.DataKeys[e.Row.RowIndex].Value.ToString());

                if (SlNo == 2)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Style.Add("background-color", "rgb(181,181,181)");
                        e.Row.Cells[i].Style.Add("font-weight", "bold");
                    }
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            PrepareGridViewForExport(dgvMemberOutstanding);
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberOutstandingReport.xls");
            Response.ContentType = "application/excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvMemberOutstanding.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }

        private void PrepareGridViewForExport(Control gv)
        {
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
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