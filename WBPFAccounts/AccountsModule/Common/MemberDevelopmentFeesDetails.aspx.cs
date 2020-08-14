using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class MemberDevelopmentFeesDetails : System.Web.UI.Page
    {
        public int MemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }
        public int FinYrId
        {
            get { return Convert.ToInt32(Session["FinYrId"]); }
            set { Session["FinYrId"] = value; }
        }
        public int FromMonth
        {
            get { return Convert.ToInt32(ViewState["FromMonth"]); }
            set { ViewState["FromMonth"] = value; }
        }
        public int ToMonth
        {
            get { return Convert.ToInt32(ViewState["ToMonth"]); }
            set { ViewState["ToMonth"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadMonth();
                    LoadMembers();
                    //btnSendEmail.Visible = false;
                    btnDownload.Visible = false;
                    Message.Show = false;
                    (ddlFromMonth.SelectedValue) = "4";
                    (ddlToMonth.SelectedValue) = "3";
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void LoadMembers()
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            DataTable dt = objMemberMaster.DevelopmentMemberGetAll(0, 0, 0, 0, "", "",0,DateTime.MinValue, DateTime.MinValue);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0)=1 And IsNull(IsActive,0)=1";
            dv.Sort = "MemberName ASC";
            if (dt != null)
            {
                ddlMember.DataSource = dv.ToTable();
                //ddlMember.DataTextField = "MemberName";
                //ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
            }
            ddlMember.Items.Insert(0, new ListItem("--SELECT MEMBER--", "0"));
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        private void LoadMonth()
        {
            BusinessLayer.Common.MonthMaster objMonth = new BusinessLayer.Common.MonthMaster();
            DataTable dt = objMonth.GetAll();

            ddlFromMonth.DataSource = dt;
            ddlFromMonth.DataBind();
            ddlToMonth.DataSource = dt;
            ddlToMonth.DataBind();
            //InsertFisrtItem(ddlMonth, "--SELECT MONTH--");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            FinYrId = Convert.ToInt32(Session["FinYrID"]);
            FromMonth = Convert.ToInt32(ddlFromMonth.SelectedValue);
            ToMonth = Convert.ToInt32(ddlToMonth.SelectedValue);
            LoadDevelopmentFeeList();
        }

        private void LoadDevelopmentFeeList()
        {
            BusinessLayer.Common.MemberFeesConfig objMember = new BusinessLayer.Common.MemberFeesConfig();
            DataSet ds = objMember.MemberDevelopmentFeeAllMonthGetAll(MemberId, FinYrId, FromMonth, ToMonth);
            DataTable dt = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                dgvMemberDevelopmentFee.DataSource = dt;
                dgvMemberDevelopmentFee.DataBind();
                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "javascript:alert('member_development_bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "')", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open('member_development_bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "','_blank','height=700px,width=1000px,scrollbars=1');", true);
            ClientScript.RegisterStartupScript(GetType(), "OnClick", "openpopup('member_development_bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "');", true);
        }

        protected void dgvMemberDevelopmentFee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label OpeningBalance = (Label)e.Row.FindControl("lblOpeningBalance");
                //Label Total = (Label)e.Row.FindControl("lblTotal");
            }
        }
    }
}