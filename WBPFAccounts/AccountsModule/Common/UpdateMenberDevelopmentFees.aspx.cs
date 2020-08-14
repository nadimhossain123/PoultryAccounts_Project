using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class UpdateMenberDevelopmentFees : System.Web.UI.Page
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

            ddlFromMonth.DataSource = dt;
            ddlFromMonth.DataBind();
            InsertFisrtItem(ddlFromMonth, "--SELECT FROM MONTH--");

            ddlToMonth.DataSource = dt;
            ddlToMonth.DataBind();
            InsertFisrtItem(ddlToMonth, "--SELECT TO MONTH--");
        }

        protected void LoadMembers()
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            DataTable dt = objMemberMaster.DevelopmentMemberGetAll(0, 0, 0, 0, "", "",0,DateTime.MinValue,DateTime.MinValue);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0)=1 And IsNull(IsActive,0)=1";

            if (dt != null)
            {
                ddlMember.DataSource = dv.ToTable();
                ddlMember.DataTextField = "MemberName";
                ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
            }
            ddlMember.Items.Insert(0, new ListItem("--SELECT MEMBER--", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberBill objBill = new BusinessLayer.Common.MemberBill();
            Entity.Common.MemberBill bill = new Entity.Common.MemberBill();
            bill.Month = Convert.ToInt32(ddlFromMonth.SelectedValue);
            bill.MonthTo = Convert.ToInt32(ddlToMonth.SelectedValue);
            bill.Year = Convert.ToInt32(ddlYear.SelectedValue);
            bill.MemberId = Convert.ToInt32(ddlMember.SelectedValue);
            bill.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            bill.Amount = Convert.ToInt32(txtAmount.Text);
            //objBill.DevelopmentFeeGenerate(bill);
            objBill.DevelopmentFeeUpdate(bill);
            Message.IsSuccess = true;
            Message.Text = "Bill Updated Successfully";
            Message.Show = true;
            LoadDevelopmentFeeDetails();
        }

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDevelopmentFeeDetails();
        }

        protected void LoadDevelopmentFeeDetails()
        {
            int memberId = Convert.ToInt32(ddlMember.SelectedValue);

            BusinessLayer.Common.MemberBill objBill = new BusinessLayer.Common.MemberBill();
            DataTable dt = new DataTable();
            dt = objBill.DevelopmentFeeBillGetAll(memberId);
            if (dt != null)
            {
                dgvBill.DataSource = dt;
                dgvBill.DataBind();
            }
        }
    }
}