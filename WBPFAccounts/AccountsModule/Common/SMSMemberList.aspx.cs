using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class SMSMemberList : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString().Equals("Admin"))
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Agent"))
                this.MasterPageFile = "../MasterAgent.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null)
            {
                Message.Visible = false;
                if (!IsPostBack)
                {
                    LoadDistrict();

                    if (!Session["UserType"].ToString().Equals("Admin"))
                    {
                        lnkBulkUpload.Visible = false;
                        ddlDistrict.SelectedValue = Session["DistrictId"].ToString();
                        //ddlDistrict.Enabled = false;
                    }
                    LoadSMSMemberList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable DT = objDistrict.GetAll(0);

            ddlDistrict.DataSource = DT;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All District", "0"));
        }

        protected void LoadSMSMemberList()
        {
            string MemberName = txtMemberName.Text.Trim();
            string MobileNo = txtMobile.Text.Trim();
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);

            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();

            DataTable dt = objSMSMember.GetAllMember(MemberName, MobileNo, 0, DistrictId,0);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsActive,0) = 1 And MemberType = 'PAID'";
            {
                dgvMemberMaster.DataSource = dv.ToTable();
                dgvMemberMaster.DataBind();
            }
        }

        protected void dgvMemberMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int SMSMemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[e.NewEditIndex].Values["SMSMemberId"]);
            Response.Redirect("SMSMember.aspx?SMSMemberId=" + SMSMemberId + "&RedirectUrl=SMSMemberList.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSMSMemberList();
            Message.Show = false;
        }

        protected void dgvMemberMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool IsActive = Convert.ToBoolean(dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["IsActive"].ToString());
                Button btnSMSPayment = (Button)e.Row.FindControl("btnSMSPayment");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");

                btnSMSPayment.Attributes.Add("OnClick", "openpopup('SMSPayment.aspx?SMSMemberId=" + dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["SMSMemberId"].ToString() + "'); return false;");

                if (!IsActive)
                {
                    btnEdit.Visible = false;
                    btnSMSPayment.Visible = false;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Style.Add("background-color", "#DB2526");
                        e.Row.Cells[i].Style.Add("color", "#FFFFFF");
                    }
                }

                if (!Session["UserType"].ToString().Equals("Admin"))
                {
                    btnEdit.Visible = false;
                }
            }
        }

        protected void dgvMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMemberMaster.PageIndex = e.NewPageIndex;
            LoadSMSMemberList();
            Message.Show = false;
        }
    }
}