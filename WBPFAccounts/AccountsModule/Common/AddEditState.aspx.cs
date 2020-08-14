using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditState : System.Web.UI.Page
    {
        Entity.Common.State state = new Entity.Common.State();

        public int StateId
        {
            get { return Convert.ToInt32(ViewState["StateId"]); }
            set { ViewState["StateId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                PopulateDropDownLists();
                LoadState();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    StateId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateState();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtStateName.Text = string.Empty;
            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void PopulateDropDownLists()
        {

        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateState()
        {
            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            state = objState.GetStateById(StateId);
            if (state != null)
            {
                StateId = state.StateId;
                txtStateName.Text = state.StateName.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadState()
        {
            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            DataTable dt = objState.GetAll();
            if (dt != null)
            {
                dgvState.DataSource = dt;
                dgvState.DataBind();
            }
        }

        protected void dgvState_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int StateId = Convert.ToInt32(dgvState.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditState.aspx?id=" + StateId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
                state.StateId = StateId;
                state.StateName = txtStateName.Text.Trim();
                objState.Save(state);
                Clear();
                LoadState();
                Message.IsSuccess = true;
                Message.Text = "State Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate State Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditState.aspx");
        }
    }
}