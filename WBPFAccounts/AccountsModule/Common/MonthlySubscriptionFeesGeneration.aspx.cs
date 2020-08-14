using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace AccountsModule.Common
{
    public partial class MonthlySubscriptionFeesGeneration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadMonth();
                    LoadMembers();
                    Message.Show = false;
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

        private void LoadMonth()
        {
            BusinessLayer.Common.MonthMaster objMonth = new BusinessLayer.Common.MonthMaster();
            DataTable dt = objMonth.GetAll();

            ddlMonth.DataSource = dt;
            ddlMonth.DataBind();
            InsertFisrtItem(ddlMonth, "--Select Month--");
        }

        protected void LoadMembers()
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster member = new Entity.Common.MemberMaster();
            member.CategoryId = 0;
            member.BlockId = 0;
            member.DistrictId = 0;
            member.StateId = 0;
            member.MemberName = string.Empty;

            DataTable dt = objMemberMaster.GetAll(member);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0)=1 And IsNull(IsActive,0)=1";

            if (dt != null)
            {
                ddlMember.DataSource = dv.ToTable();
                ddlMember.DataTextField = "MemberName";
                ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
            }
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberBill objBill = new BusinessLayer.Common.MemberBill();
            Entity.Common.MemberBill bill = new Entity.Common.MemberBill();
            bill.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            bill.Year = Convert.ToInt32(ddlYear.SelectedValue);
            bill.MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            bill.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            objBill.Generate(bill);
            Message.IsSuccess = true;
            Message.Text = "Bill Generated Successfully";
            Message.Show = true;
        }
    }
}