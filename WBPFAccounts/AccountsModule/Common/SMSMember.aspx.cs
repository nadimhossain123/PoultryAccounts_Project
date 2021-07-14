using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace AccountsModule.Common
{
    public partial class SMSMember : System.Web.UI.Page
    {
        public int SMSMemberId
        {
            get { return Convert.ToInt32(ViewState["SMSMemberId"]); }
            set { ViewState["SMSMemberId"] = value; }
        }

        public string RedirectUrl
        {
            get { return Convert.ToString(ViewState["RedirectUrl"]); }
            set { ViewState["RedirectUrl"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    LoadMemberList();
                   // LoadSMSMember();

                    if (Request.QueryString["SMSMemberId"] != null && Request.QueryString["SMSMemberId"].Trim().Length > 0
                        && Request.QueryString["RedirectUrl"] != null && Request.QueryString["RedirectUrl"].Length > 0)
                    {
                        SMSMemberId = Convert.ToInt32(Request.QueryString["SMSMemberId"]);
                        RedirectUrl = Convert.ToString(Request.QueryString["RedirectUrl"]);
                        PopulateSMSMember();
                        Message.Show = false;
                    }
                    else
                    {
                        ClearControl();
                    }
                }
            }
            
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            membermaster.BlockId = 0;
            membermaster.DistrictId = 0;
            membermaster.StateId = 0;
            membermaster.CategoryId = 0;
            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsNull(IsApproved,0) = 1 And IsNull(IsActive,0) = 1";

            ddlMember.DataSource = dv;
            ddlMember.DataBind();
            ddlMember.Items.Insert(0, new ListItem("--Select Member--", "0"));
        }

        private void ClearControl()
        {
            SMSMemberId = 0;
            txtAddress.Text = "";
            txtMemberName.Text = "";
            ddlDistrict.SelectedIndex = 0;
            ddlRegType.SelectedIndex = 0;
            txtMobileNo.Text = "";
            txtRemarks.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            chkIsActive.Checked = false;
            chkIsNECCMember.Checked = false;
            ddlMember.SelectedValue = "0";
            Message.Show = false;

            ddlMember.Enabled = true;
            txtMemberName.Enabled = true;
        }

        protected void PopulateDropDownLists()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable DT = objDistrict.GetAll(0);

            ddlDistrict.DataSource = DT;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlSearchDistrict.DataSource = DT;
            ddlSearchDistrict.DataBind();
            ddlSearchDistrict.Items.Insert(0, new ListItem("All District", "0"));
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateSMSMember()
        {
            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            Entity.Common.SMSMember sMSMember = new Entity.Common.SMSMember();
            sMSMember = objSMSMember.GetSMSMemberById(SMSMemberId);
            if (sMSMember != null)
            {
                SMSMemberId = sMSMember.SMSMemberId;
                ddlMember.SelectedValue = sMSMember.ParentMemberId.ToString();
                txtMemberName.Text = sMSMember.MemberName.ToString();
                txtMobileNo.Text = sMSMember.MobileNo.ToString();
                txtAddress.Text = sMSMember.Address.ToString();
                ddlRegType.Text = sMSMember.MemberType.ToString();
                chkIsActive.Checked = Convert.ToBoolean(sMSMember.IsActive);
                txtStartDate.Text = sMSMember.StartDate.ToString("dd/MM/yyyy");
                txtEndDate.Text = sMSMember.EndDate.ToString("dd/MM/yyyy");
                txtRemarks.Text = sMSMember.Remarks.ToString();
                chkIsNECCMember.Checked = Convert.ToBoolean(sMSMember.IsNECCMember);
                ddlDistrict.SelectedValue = sMSMember.DistrictId.ToString();

                //ddlMember.Enabled = false;
                txtMemberName.Enabled = false;

                btnSave.Text = "Update";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text.Split('/')[2] + "-" + txtStartDate.Text.Split('/')[1] + "-" + txtStartDate.Text.Split('/')[0]);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text.Split('/')[2] + "-" + txtEndDate.Text.Split('/')[1] + "-" + txtEndDate.Text.Split('/')[0]);

            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            Entity.Common.SMSMember sMSMember = new Entity.Common.SMSMember();
            sMSMember.SMSMemberId = SMSMemberId;
            sMSMember.ParentMemberId = Convert.ToInt32(ddlMember.SelectedValue);
            sMSMember.MemberName = txtMemberName.Text.Trim();
            sMSMember.MobileNo = txtMobileNo.Text.Trim();
            sMSMember.Address = txtAddress.Text.Trim();
            sMSMember.MemberType = Convert.ToInt32(ddlRegType.SelectedValue);
            sMSMember.IsActive = chkIsActive.Checked;
            sMSMember.IsNECCMember = chkIsNECCMember.Checked;
            sMSMember.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            sMSMember.CreatedBy = Convert.ToInt32(Session["UserId"]);
            int RowsUpdated = objSMSMember.Save(sMSMember, StartDate, EndDate, txtRemarks.Text.Trim());

            if (RowsUpdated > 0)
            {
                LoadSMSMember();
                Message.IsSuccess = true;
                Message.Text = "Data Saved Successfully.";
                ClearControl();

                if (RedirectUrl != null && RedirectUrl.Length > 0)
                {
                    Response.Redirect(RedirectUrl);
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save.";
            }
            Message.Show = true;
        }

        protected void LoadSMSMember()
        {
            string MemberName = txtSearchMemberName.Text.Trim();
            string MobileNo = txtSearchMobileNo.Text.Trim();
            int MemberType = Convert.ToInt32(ddlSearchRegType.SelectedValue);
            //int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue); 
            int DistrictId = Convert.ToInt32(ddlSearchDistrict.SelectedValue);  // Change on 25.07.19 _ Sayantani (Ref. Search by district was not working)
            int MemberCategoryId = Convert.ToInt32(ddlNECCOrNot.SelectedValue);

            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            DataTable dt = objSMSMember.GetAllMember(MemberName, MobileNo, MemberType, DistrictId, MemberCategoryId);
            if (dt != null)
            {
                dgvSMSMember.DataSource = dt;
                dgvSMSMember.DataBind();
            }
        }

        protected void dgvSMSMember_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SMSMemberId = Convert.ToInt32(dgvSMSMember.DataKeys[e.NewEditIndex].Value);
            PopulateSMSMember();
        }

        protected void dgvSMSMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSMSMember.PageIndex = e.NewPageIndex;
            LoadSMSMember();
        }

        protected void dgvSMSMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SMSMemberId = Convert.ToInt32(dgvSMSMember.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            objSMSMember.Delete(SMSMemberId);
            Message.IsSuccess = true;
            Message.Text = "Data Deleted Successfully.";

            Message.Show = true;
            LoadSMSMember();
        }

        protected void dgvSMSMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSMSMember();
            Message.Show = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadSMSMember();
            dgvSMSMember.AllowPaging = false;
            dgvSMSMember.DataBind();

            PrepareGridViewForExport(dgvSMSMember);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=SMSMember.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvSMSMember.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }

        private void PrepareGridViewForExport(Control gv)
        {
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Button))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
    }
}