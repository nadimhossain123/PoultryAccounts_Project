using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.SMS
{
    public partial class DoctorMaster : System.Web.UI.Page
    {
        public int DoctorId
        {
            get { return Convert.ToInt32(ViewState["DoctorId"]); }
            set { ViewState["DoctorId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                InsertFisrtItem(ddlBlock, "--SELECT--");
                LoadDistrict();
                LoadDoctors();
            }
        }

        protected void ClearControls()
        {
            DoctorId = 0;
            ddlGroup.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            LoadBlock(Convert.ToInt32(ddlDistrict.SelectedValue));
            ddlBlock.SelectedValue = "0";
            txtName.Text = "";
            txtMobileNo.Text = "";
        }

        protected void Populate()
        {
            BusinessLayer.SMS.DoctorMaster objdoc = new BusinessLayer.SMS.DoctorMaster();
            Entity.SMS.DoctorMaster docEntity = new Entity.SMS.DoctorMaster();
            docEntity = objdoc.GetById(DoctorId);
            if (docEntity != null)
            {
                ddlGroup.SelectedValue = docEntity.GroupId.ToString();
                ddlDistrict.SelectedValue = docEntity.DistrictId.ToString();
                LoadBlock(Convert.ToInt32(ddlDistrict.SelectedValue));
                ddlBlock.SelectedValue = docEntity.BlockId.ToString();
                txtName.Text = docEntity.FullName.ToString();
                txtMobileNo.Text = docEntity.MobileNo.ToString();
            }
        }

        protected void LoadDoctors()
        {
            BusinessLayer.SMS.DoctorMaster objDoc = new BusinessLayer.SMS.DoctorMaster();
            DataTable dtDoc = new DataTable();
            dtDoc = objDoc.GetAll();
            if (dtDoc != null)
            {
                dgvDoctors.DataSource = dtDoc;
                dgvDoctors.DataBind();
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void LoadDistrict()
        {
            BusinessLayer.SMS.district objDistrict = new BusinessLayer.SMS.district();
            DataTable dt = new DataTable();
            dt = objDistrict.GetAllDistrictMaster();
            if (dt != null)
            {
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataBind();
            }
            InsertFisrtItem(ddlDistrict, "--SELECT--");
        }

        protected void LoadBlock(int districtId)
        {
            BusinessLayer.SMS.district objBlock = new BusinessLayer.SMS.district();
            DataTable dtBlock = new DataTable();
            dtBlock = objBlock.GetAllBlock(districtId);
            if (dtBlock != null)
            {
                ddlBlock.DataSource = dtBlock;
                ddlBlock.DataBind();
            }
            InsertFisrtItem(ddlBlock, "--SELECT--");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.DoctorMaster objdoc = new BusinessLayer.SMS.DoctorMaster();
            Entity.SMS.DoctorMaster docEntity = new Entity.SMS.DoctorMaster();
            docEntity.DoctorId = DoctorId;
            docEntity.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
            docEntity.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            docEntity.BlockId = Convert.ToInt32(ddlBlock.SelectedValue);
            docEntity.FullName = txtName.Text.ToString();
            docEntity.MobileNo = txtMobileNo.Text.ToString();
            int RowAffected = objdoc.Save(docEntity);
            if (RowAffected > 0)
            {
                ltrMsg.Text = "DATA SAVED SUCCESSFULLY !!";
                ClearControls();
                LoadDoctors();
            }
            else
            {
                ltrMsg.Text = "DUPLICATE DATA !!";
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            int districtId = Convert.ToInt32(ddlDistrict.SelectedValue);
            LoadBlock(districtId);
        }

        protected void dgvDoctors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDoctors.PageIndex = e.NewPageIndex;
            LoadDoctors();
        }

        protected void dgvDoctors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.SMS.DoctorMaster objDoc = new BusinessLayer.SMS.DoctorMaster();
            objDoc.Delete(Convert.ToInt32(dgvDoctors.DataKeys[e.RowIndex].Value));
            LoadDoctors();
        }

        protected void dgvDoctors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DoctorId = Convert.ToInt32(dgvDoctors.DataKeys[e.NewEditIndex].Value);
            Populate();
        }
    }
}