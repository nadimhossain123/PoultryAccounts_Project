using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;
using DataAccess.Accounts;

namespace AccountsModule.Common
{
    public partial class MemberMasterLedgerDefaultConfiguration : System.Web.UI.Page
    {
        public string strFilter;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsConnection objConn = new clsConnection();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDropdown();
                LoadSubGroup();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void Clear()
        {
            //ddlAccountsGroup.SelectedIndex = 0;
            //ddlLedgerType.SelectedIndex = 0;
            //ddlAccountsSubGroup.SelectedIndex = 0;
            //chkCostCenter.Checked = false;
        }

        protected void LoadDropdown()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";
            // A/c group population
            strValues = "" + chr.ToString() + "Main Group";
            genObj.BindAjaxDropDownColumnsBySP(ddlAccountsGroup, "spSelect_MstAccountsGroup", strValues);
            ddlAccountsGroup.Items.Insert(0, li);
        }

        protected void LoadSubGroup()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";

            strValues = "" + chr.ToString() + "Sub Group";
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
            dv.RowFilter = "GroupID_FK=" + ddlAccountsGroup.SelectedValue.ToString();

            ddlAccountsSubGroup.DataSource = dv;
            ddlAccountsSubGroup.DataBind();

            ddlAccountsSubGroup.Items.Insert(0, li);
        }

        protected void Populate()
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            membermaster = objMemberMaster.MemberMasterLedgerDefaultConfiguration_Get();
            ddlLedgerType.SelectedValue = membermaster.LedgerTypeId;
            ddlAccountsGroup.SelectedValue = membermaster.AccountGroupId.ToString();
            ddlAccountsSubGroup.SelectedValue = membermaster.AccountSubGroupId.ToString();
            chkCostCenter.Checked = Convert.ToBoolean(membermaster.IsCostCenterApplicable);
            Message.Show = false;
        }

        protected bool Validate()
        {
            bool result = true;
            string ErrorText = "";
            if (ddlAccountsGroup.SelectedValue == "0" || ddlAccountsGroup.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Group";
            }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void ddlAccountsGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubGroup();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate())
                {
                    Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
                    BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();

                    membermaster.LedgerTypeId = ddlLedgerType.SelectedValue;
                    membermaster.AccountGroupId = Convert.ToInt32(ddlAccountsGroup.SelectedValue);
                    if (ddlAccountsSubGroup.SelectedIndex == 0)
                        membermaster.AccountSubGroupId = 0;
                    else
                        membermaster.AccountSubGroupId = Convert.ToInt32(ddlAccountsSubGroup.SelectedValue);
                    membermaster.IsCostCenterApplicable = (chkCostCenter.Checked) ? true : false;
                    objMemberMaster.MemberMasterLedgerDefaultConfiguration_Update(membermaster);
                    Message.IsSuccess = true;
                    Message.Text = "Updated Successfully.";
                }
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Update!!!";
            }
            Message.Show = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberMasterLedgerDefaultConfiguration.aspx");
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Populate();
        }
    }
}