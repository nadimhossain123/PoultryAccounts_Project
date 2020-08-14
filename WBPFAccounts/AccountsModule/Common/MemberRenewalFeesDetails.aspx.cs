using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class MemberRenewalFeesDetails : System.Web.UI.Page
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
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    LoadMemberList();
                    LoadMonth();
                    Message.Show = false;
                    btnDownload.Visible = false;
                    (ddlFromMonth.SelectedValue) = "4";
                    (ddlToMonth.SelectedValue) = "3";
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
            dv.Sort = "MemberName ASC";
            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        protected void LoadMonth()
        {
            BusinessLayer.Common.MonthMaster objMon = new BusinessLayer.Common.MonthMaster();
            DataTable DT = objMon.GetAll();
            ddlFromMonth.DataSource = DT;
            ddlFromMonth.DataBind();
            ddlToMonth.DataSource = DT;
            ddlToMonth.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRenewalFeeList();
            //btnDownload.Attributes.Add("OnClick", "openpopup('member-renewal-bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "'); return false;");
        }

        private void LoadRenewalFeeList()
        {
            MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            FinYrId = Convert.ToInt32(Session["FinYrID"]);
            FromMonth = Convert.ToInt32(ddlFromMonth.SelectedValue);
            ToMonth = Convert.ToInt32(ddlToMonth.SelectedValue);
            BusinessLayer.Common.MemberFeesConfig objMember = new BusinessLayer.Common.MemberFeesConfig();
            DataSet ds = objMember.MemberRenewalFeeGetAll(MemberId, FinYrId, FromMonth,ToMonth);
            DataTable dt = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                dgvMemberRenewalFee.DataSource = dt;
                dgvMemberRenewalFee.DataBind();
                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "openpopup('member-renewal-bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open('member-renewal-bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "','_blank','height=700px,width=1000px,scrollbars=1');", true);
            //Page.ClientScript.RegisterStartupScript(GetType(),"","");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "OnClick", "openpopup('member-renewal-bill.aspx?MemberId=" + MemberId + "&FinYrId=" + FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "');",true);
            //Page.RegisterStartupScript("OnClick","openpopup('member-renewal-bill.aspx?MemberId=" + MemberId+"&FinYrId="+FinYrId + "&FromMonth=" + FromMonth + "&ToMonth=" + ToMonth + "'); return false;");
        }

        protected void ExcelDownload()
        {
            if (dgvMemberRenewalFee.Rows.Count > 0)
            {
                string[] _header = new string[2];
                _header[0] = "MEMBER RENEWAL FEE DETAILS";
                _header[1] = "MEMBER NAME: " + ddlMember.SelectedItem.ToString();

                string[] _footer = new string[0];
                string file = "MEMBER_RENEWAL_FEE_DETAILS";

                BusinessLayer.Common.Excel.SaveExcel(_header, dgvMemberRenewalFee, _footer, file);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
    }
}