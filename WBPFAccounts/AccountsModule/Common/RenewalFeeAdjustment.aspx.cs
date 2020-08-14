using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLayer.Accounts;

namespace AccountsModule.Common
{
    public partial class RenewalFeeAdjustment : System.Web.UI.Page
    {
        public string FeesXml;
        string strParams;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        public int PaymentId
        {
            get { return Convert.ToInt32(ViewState["PaymentId"]); }
            set { ViewState["PaymentId"] = value; }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["MemberId"] == null && Request.QueryString["PaymentId"] == null)
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Admin") && (Request.QueryString["MemberId"] != null || Request.QueryString["PaymentId"] != null))
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
                    ResetControl();
                    LoadMemberList();
                    LoadMemberOutstandingList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ResetControl()
        {
            Message.Show = false;
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

            DataTable dt = objMember.MemberMaster_GetAll_ForReport(membermaster);
            DataView dv = new DataView(dt);
            //dv.RowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        private void LoadMemberOutstandingList()
        {
            int FinYrId = Convert.ToInt32(Session["FinYrID"].ToString());
            int MemberId = Convert.ToInt32(ddlMember.SelectedValue);

            if (MemberId != 0)
            {
                BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
                DataView DV = new DataView(objMemberPayment.GetOutstanding(MemberId, PaymentId, FinYrId));
                DV.RowFilter = "FeesHeadId IN (1,2)";
                DataTable dt = new DataTable();
                dt = DV.ToTable();
                if (dt != null)
                {
                    dgvMemberOutstanding.DataSource = dt;
                    dgvMemberOutstanding.DataBind();
                }
            }
            else
            {
                dgvMemberOutstanding.DataSource = null;
                dgvMemberOutstanding.DataBind();
            }
        }

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMemberOutstandingList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            FeesXml = "<NewDataSet>";
            foreach (GridViewRow gvr in dgvMemberOutstanding.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    FeesXml += "<Row";
                    FeesXml += "  FeesHeadId = \"" + dgvMemberOutstanding.DataKeys[gvr.RowIndex].Values["FeesHeadId"].ToString() + "\"";
                    FeesXml += "  FeesPaymentAmount = \"" + (string.IsNullOrEmpty(((TextBox)gvr.FindControl("txtFeesPaymentAmount")).Text.Trim()) ? "0" : ((TextBox)gvr.FindControl("txtFeesPaymentAmount")).Text.Trim()) + "\"";
                    FeesXml += "  TaxPaymentAmount = \"" + (string.IsNullOrEmpty(((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) ? "0" : ((TextBox)gvr.FindControl("txtTaxPaymentAmount")).Text.Trim()) + "\"";
                    FeesXml += " />";
                }
            }
            FeesXml += "</NewDataSet>";
            BusinessLayer.Common.MemberBill objMemberBillAdjustment = new BusinessLayer.Common.MemberBill();
            objMemberBillAdjustment.RenewalBillAdjustment(Convert.ToInt32(ddlMember.SelectedValue), Convert.ToInt32(Session["UserId"].ToString()), FeesXml);
            LoadMemberOutstandingList();
            Message.IsSuccess = true;
            Message.Text = "Renewal fee is adjusted successfully";
            Message.Show = true;
        }
    }
}