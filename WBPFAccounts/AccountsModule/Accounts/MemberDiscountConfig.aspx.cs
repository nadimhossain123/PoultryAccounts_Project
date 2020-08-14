using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class MemberDiscountConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Trim().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadMembers();
                    dgvFeesHead.DataSource = null;
                    dgvFeesHead.DataBind();
                    btnSave.Visible = false;
                    Message.Show = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void InsertFisrtItem(AjaxControlToolkit.ComboBox ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
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
            InsertFisrtItem(ddlMember, "--Select Member--");
        }

        protected void LoadFeesDetails()
        {
            BusinessLayer.Common.MemberDiscountConfig objDiscountConfig = new BusinessLayer.Common.MemberDiscountConfig();
            DataTable dt = objDiscountConfig.GetAll(Convert.ToInt32(ddlMember.SelectedValue));

            dgvFeesHead.DataSource = dt;
            dgvFeesHead.DataBind();

            if (dt.Rows.Count > 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberDiscountConfig objDiscountConfig = new BusinessLayer.Common.MemberDiscountConfig();
            Entity.Common.MemberDiscountConfig DiscountConfig = new Entity.Common.MemberDiscountConfig();
            DiscountConfig.MemberId = Convert.ToInt32(ddlMember.SelectedValue);

            int Error = 0;
            string strDiscountXml = "<NewDataSet>";
            string strDiscountAmount = string.Empty;

            foreach (GridViewRow gvr in dgvFeesHead.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlDiscountType = (DropDownList)gvr.FindControl("ddlDiscountType");
                    TextBox txtDiscountAmount = (TextBox)gvr.FindControl("txtDiscountAmount");
                    CheckBox ChkIsActive = (CheckBox)gvr.FindControl("ChkIsActive");
                    strDiscountAmount = (string.IsNullOrEmpty(txtDiscountAmount.Text.Trim()) ? "0" : txtDiscountAmount.Text.Trim());
                    TextBox txtNarration = (TextBox)gvr.FindControl("txtNarration");

                    strDiscountXml += "<Row";
                    strDiscountXml += " FeesHeadId = \"" + dgvFeesHead.DataKeys[gvr.RowIndex].Values["FeesHeadId"].ToString() + "\"";
                    strDiscountXml += " DiscountType = \"" + ddlDiscountType.SelectedValue + "\"";
                    strDiscountXml += " DiscountAmount = \"" + strDiscountAmount + "\"";
                    strDiscountXml += " IsActive = \"" + ChkIsActive.Checked.ToString() + "\"";
                    strDiscountXml += " Narration = \"" + txtNarration.Text.Trim().Replace("'", "''") + "\"";
                    strDiscountXml += " />";

                    if (ddlDiscountType.SelectedValue.Equals("P") && Convert.ToDecimal(strDiscountAmount) > 100)
                    {
                        Error = 1;
                    }
                }
            }

            strDiscountXml += "</NewDataSet>";

            DiscountConfig.DiscountConfigXml = strDiscountXml;

            if (Error == 0)
            {
                objDiscountConfig.Save(DiscountConfig);

                LoadFeesDetails();
                Message.IsSuccess = true;
                Message.Text = "Discount Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Invalid Discount Amount";
            }
            Message.Show = true;
        }

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((DropDownList)e.Row.FindControl("ddlDiscountType")).SelectedValue = dgvFeesHead.DataKeys[e.Row.RowIndex].Values["DiscountType"].ToString();
                ((CheckBox)e.Row.FindControl("ChkIsActive")).Checked = Convert.ToBoolean(dgvFeesHead.DataKeys[e.Row.RowIndex].Values["IsActive"].ToString());
            }
        }

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMember.SelectedIndex == 0)
            {
                dgvFeesHead.DataSource = null;
                dgvFeesHead.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                LoadFeesDetails();
            }
            Message.Show = false;
        }
    }
}