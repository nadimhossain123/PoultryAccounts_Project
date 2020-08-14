using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Accounts
{
    public partial class MemberFeesConfig : System.Web.UI.Page
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
            MemberDevelopmentDetails();

            BusinessLayer.Common.MemberFeesConfig objFeesConfig = new BusinessLayer.Common.MemberFeesConfig();
            DataTable dt = objFeesConfig.GetAll(Convert.ToInt32(ddlMember.SelectedValue));

            dgvFeesHead.DataSource = dt;
            dgvFeesHead.DataBind();

            if (dt.Rows.Count > 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberFeesConfig objFeesConfig = new BusinessLayer.Common.MemberFeesConfig();
            Entity.Common.MemberFeesConfig FeesConfig = new Entity.Common.MemberFeesConfig();
            FeesConfig.MemberId = Convert.ToInt32(ddlMember.SelectedValue);

            string strFeesXml = "<NewDataSet>";
            foreach (GridViewRow gvr in dgvFeesHead.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    strFeesXml += "<Row";
                    strFeesXml += " FeesHeadId = \"" + dgvFeesHead.DataKeys[gvr.RowIndex].Value.ToString() + "\"";
                    strFeesXml += " Amount = \"" + (string.IsNullOrEmpty(txtAmount.Text.Trim()) ? "0" : txtAmount.Text.Trim()) + "\"";
                    strFeesXml += " />";
                }
            }
            strFeesXml += "</NewDataSet>";
            FeesConfig.FeesXml = strFeesXml;

            string strParticularsXml = "<NewDataSet>";
            foreach (GridViewRow gvr in dgvDevelopmentFee.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtCapacity = (TextBox)gvr.FindControl("txtCapacity");
                    TextBox txtFee = (TextBox)gvr.FindControl("txtFee");
                    TextBox txtNarration = (TextBox)gvr.FindControl("txtNarration");

                    strParticularsXml += "<Row";
                    strParticularsXml += " ParticularsId = \"" + dgvDevelopmentFee.DataKeys[gvr.RowIndex].Values[1].ToString() + "\"";
                    strParticularsXml += " Capacity = \"" + (string.IsNullOrEmpty(txtCapacity.Text.Trim()) ? "0" : txtCapacity.Text.Trim()) + "\"";
                    strParticularsXml += " FeeAmount = \"" + (string.IsNullOrEmpty(txtFee.Text.Trim()) ? "0" : txtFee.Text.Trim()) + "\"";
                    strParticularsXml += " Narration = \"" + txtNarration.Text.Trim().Replace("'", "''") + "\"";
                    strParticularsXml += " />";
                }
            }
            strParticularsXml += "</NewDataSet>";
            FeesConfig.ParticularsXml = strParticularsXml;

            objFeesConfig.Save(FeesConfig);
            LoadFeesDetails();
            Message.IsSuccess = true;
            Message.Text = "Fees Saved Successfully";
            Message.Show = true;
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

        protected void MemberDevelopmentDetails()
        {
            BusinessLayer.Common.MemberFeesConfig objFeesConfig = new BusinessLayer.Common.MemberFeesConfig();
            DataTable dt = objFeesConfig.MemberDevelopmentFeeGetAll(Convert.ToInt32(ddlMember.SelectedValue));

            dgvDevelopmentFee.DataSource = dt;
            dgvDevelopmentFee.DataBind();
        }

        protected void dgvDevelopmentFee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int particularsId = Convert.ToInt32(((DataTable)dgvDevelopmentFee.DataSource).Rows[e.Row.RowIndex]["ParticularsId"]);
                int thresholdCapacity = Convert.ToInt32(((DataTable)dgvDevelopmentFee.DataSource).Rows[e.Row.RowIndex]["ThresholdCapacity"]);
                var txt = (TextBox)e.Row.FindControl("txtCapacity");
                var txtFee = (TextBox)e.Row.FindControl("txtFee");

                
                txt.Attributes.Add("onkeyup", "CalculateTotalAmount(this," + thresholdCapacity.ToString() + ")");
            }
        }
    }
}
