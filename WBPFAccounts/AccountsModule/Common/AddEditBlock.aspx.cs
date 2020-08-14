using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class AddEditBlock : System.Web.UI.Page
    {
        Entity.Common.Block block = new Entity.Common.Block();

        public int BlockId
        {
            get { return Convert.ToInt32(ViewState["BlockId"]); }
            set { ViewState["BlockId"] = value; }
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
                LoadBlock();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    BlockId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    PopulateBlock();
                }
                else
                {
                    Clear();
                }
            }
        }

        protected void Clear()
        {
            txtCode.Text = string.Empty;
            txtBlockName.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void PopulateDropDownLists()
        {
            PopulateDistricts();
        }

        protected void PopulateDistricts()
        {

            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dtDistrict = new DataTable();
            int stateid = 0;

            dtDistrict = objDistrict.GetAll(stateid);
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            block = objBlock.GetBlockById(BlockId);
            if (block != null)
            {
                BlockId = block.BlockId;
                txtBlockName.Text = block.BlockName.ToString();
                ddlDistrict.SelectedValue = block.DistrictId.ToString();
                txtCode.Text = block.Code.ToString();
                txtRemarks.Text = block.Remarks.ToString();
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void LoadBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            int districtid = 0;
            int stateid = 0;

            DataTable dt = objBlock.GetAll(districtid, stateid);
            if (dt != null)
            {
                dgvBlock.DataSource = dt;
                dgvBlock.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
                block.BlockId = BlockId;
                block.BlockName = txtBlockName.Text.Trim();
                block.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                block.Code = txtCode.Text.Trim();
                if (txtRemarks.Text == string.Empty)
                    block.Remarks = string.Empty;
                else
                    block.Remarks = txtRemarks.Text.Trim();
                objBlock.Save(block);
                Clear();
                LoadBlock();
                Message.IsSuccess = true;
                Message.Text = "Block Saved Successfully.";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Block Name Is Not Allowed!!!";
            }
            Message.Show = true;
        }

        protected void dgvBlock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int BlockId = Convert.ToInt32(dgvBlock.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditBlock.aspx?id=" + BlockId);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditBlock.aspx");
        }
    }
}