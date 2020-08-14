using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditAgent : System.Web.UI.Page
    {
        public int AgentId
        {
            get { return Convert.ToInt32(ViewState["AgentId"].ToString()); }
            set { ViewState["AgentId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Trim().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    LoadState();
                    LoadDistrict();
                    LoadBlock();
                    ClearControls();
                    LoadAgentList();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ClearControls()
        {
            AgentId = 0;
            Message.Show = false;
            btnSave.Text = "Save";

            txtAgentCode.Text = "SYSTEM GENERATED";
            txtAgentName.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            ddlState.SelectedIndex = 0;
            ddlState.Enabled = true;

            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.Enabled = true;

            ddlBlock.SelectedIndex = 0;
            ddlBlock.Enabled = true;

            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtBranchAddress.Text = "";
            txtIFSCCode.Text = "";
            ChkIsActive.Checked = true;

            txtAgentNameSearch.Text = "";
        }

        private void LoadAgentList()
        {
            BusinessLayer.Common.AgentMaster objAgentMaster = new BusinessLayer.Common.AgentMaster();
            DataTable dt = objAgentMaster.GetAll(txtAgentNameSearch.Text.Trim());

            dgvAgentMaster.DataSource = dt;
            dgvAgentMaster.DataBind();
        }

        private void LoadState()
        {
            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            DataTable dt = objState.GetAll();

            ddlState.DataSource = dt;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }

        private void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dt = objDistrict.GetAll(Convert.ToInt32(ddlState.SelectedValue));

            ddlDistrict.DataSource = dt;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
        }

        private void LoadBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            DataTable dt = objBlock.GetAll(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlState.SelectedValue));

            ddlBlock.DataSource = dt;
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--Select Block--", "0"));
        }
        
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrict();
            LoadBlock();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.AgentMaster objAgentMaster = new BusinessLayer.Common.AgentMaster();
            Entity.Common.AgentMaster agent = new Entity.Common.AgentMaster();
            agent.AgentId = AgentId;
            agent.AgentCode = txtAgentCode.Text.Trim();
            agent.AgentName = txtAgentName.Text.Trim();
            agent.Address = txtAddress.Text.Trim();
            agent.PhoneNo = txtPhoneNo.Text.Trim();
            agent.StateId = Convert.ToInt32(ddlState.SelectedValue);
            agent.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            agent.BlockId = Convert.ToInt32(ddlBlock.SelectedValue);
            agent.BankName = txtBankName.Text.Trim();
            agent.BranchName = txtBranchName.Text.Trim();
            agent.BranchAddress = txtBranchAddress.Text.Trim();
            agent.IFSCCode = txtIFSCCode.Text.Trim();
            agent.IsActive = ChkIsActive.Checked;
            agent.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            objAgentMaster.Save(agent);
            ClearControls();
            LoadAgentList();
            txtAgentCode.Text = agent.AgentCode;

            Message.IsSuccess = true;
            Message.Text = "Area Manager Detail Saved Successfully";
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            LoadAgentList();
        }

        private void PopulateAgentDetail()
        {
            BusinessLayer.Common.AgentMaster objAgentMaster = new BusinessLayer.Common.AgentMaster();
            DataTable dt = objAgentMaster.GetAllById(AgentId);

            txtAgentCode.Text = dt.Rows[0]["AgentCode"].ToString();
            txtAgentName.Text = dt.Rows[0]["AgentName"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();

            ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();
            //ddlState.Enabled = false;
            LoadDistrict();

            ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            //ddlDistrict.Enabled = false;
            LoadBlock();

            ddlBlock.SelectedValue = dt.Rows[0]["BlockId"].ToString();
            //ddlBlock.Enabled = false;

            txtBankName.Text = dt.Rows[0]["BankName"].ToString();
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
            txtBranchAddress.Text = dt.Rows[0]["BranchAddress"].ToString();
            txtIFSCCode.Text = dt.Rows[0]["IFSCCode"].ToString();
            ChkIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());

            btnSave.Text = "Update";
            Message.Show = false;
        }

        protected void dgvAgentMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            AgentId = Convert.ToInt32(dgvAgentMaster.DataKeys[e.NewEditIndex].Values["AgentId"].ToString());
            PopulateAgentDetail();
        }

        protected void dgvAgentMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool IsActive = Convert.ToBoolean(dgvAgentMaster.DataKeys[e.Row.RowIndex].Values["IsActive"].ToString());

                if (!IsActive)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Style.Add("background-color", "#EC5D4E");
                        e.Row.Cells[i].Style.Add("color","#FFFFFF");
                    }
                }
            }
        }

        protected void dgvAgentMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Message.Show = false;
            dgvAgentMaster.PageIndex = e.NewPageIndex;
            LoadAgentList();
        }
    }
}