using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountsModule.Common
{
    public partial class NECCMember : System.Web.UI.Page
    {
        public int NECCMemberId
        {
            get { return Convert.ToInt32(ViewState["NECCMemberId"]); }
            set { ViewState["NECCMemberId"] = value; }
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
                    LoadNECCMember();
                    if (Request.QueryString["NECCMemberId"] != null && Request.QueryString["NECCMemberId"].Trim().Length > 0
                        && Request.QueryString["RedirectUrl"] != null && Request.QueryString["RedirectUrl"].Length > 0)
                    {
                        NECCMemberId = Convert.ToInt32(Request.QueryString["NECCMemberId"]);
                        RedirectUrl = Convert.ToString(Request.QueryString["RedirectUrl"]);
                        PopulateNECCMember();
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


        private void ClearControl()
        {
            txtAddress.Text = "";
            txtMemberName.Text = "";
            //ddlRegType.SelectedIndex = 0;
            txtMobileNo.Text = "";
            txtRemarks.Text = "";
            chkIsActive.Checked = false;
            ddlDistrict.SelectedValue = "0";
            Message.Show = false;
            txtMemberName.Enabled = true;
        }

        protected void PopulateDropDownLists()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable DT = objDistrict.GetAll(0);

            ddlDistrict.DataSource = DT;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All District", "0"));

            ddlDistrict2.DataSource = DT;
            ddlDistrict2.DataBind();
            ddlDistrict2.Items.Insert(0, new ListItem("All District", "0"));
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateNECCMember()
        {
            BusinessLayer.Common.NECCMemberMaster objNECCMember = new BusinessLayer.Common.NECCMemberMaster();
            Entity.Common.NECCMemberMaster sMSMember = new Entity.Common.NECCMemberMaster();
            sMSMember = objNECCMember.GetNECCMemberById(NECCMemberId);
            if (sMSMember != null)
            {
                NECCMemberId = sMSMember.NECCMemberId;
                txtMemberName.Text = sMSMember.MemberName.ToString();
                txtMobileNo.Text = sMSMember.MobileNo.ToString();
                txtAddress.Text = sMSMember.Address.ToString();
                chkIsActive.Checked = Convert.ToBoolean(sMSMember.IsActive);
                ddlDistrict2.SelectedValue = sMSMember.DistrictId.ToString();
                txtRemarks.Text = sMSMember.Remarks.ToString();
                txtMemberName.Enabled = false;

                btnSave.Text = "Update";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            BusinessLayer.Common.NECCMemberMaster objNECCMember = new BusinessLayer.Common.NECCMemberMaster();
            Entity.Common.NECCMemberMaster sMSMember = new Entity.Common.NECCMemberMaster();
            sMSMember.NECCMemberId = NECCMemberId;
            sMSMember.MemberName = txtMemberName.Text.Trim();
            sMSMember.MobileNo = txtMobileNo.Text.Trim();
            sMSMember.Address = txtAddress.Text.Trim();
            //sMSMember.MemberType = Convert.ToInt32(ddlRegType.SelectedValue);
            sMSMember.IsActive = chkIsActive.Checked;
            sMSMember.DistrictId = Convert.ToInt32(ddlDistrict2.SelectedValue);
            int RowsUpdated = objNECCMember.Save(sMSMember, txtRemarks.Text.Trim());

            if (RowsUpdated > 0)
            {
                LoadNECCMember();
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

        protected void LoadNECCMember()
        {
            string MemberName = txtSearchMemberName.Text.Trim();
            string MobileNo = txtSearchMobileNo.Text.Trim();
            //int MemberType = Convert.ToInt32(ddlSearchRegType.SelectedValue);
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);

            BusinessLayer.Common.NECCMemberMaster objNECCMember = new BusinessLayer.Common.NECCMemberMaster();
            DataTable dt = objNECCMember.GetAllMember(MemberName, MobileNo, DistrictId);
            if (dt != null)
            {
                dgvNECCMember.DataSource = dt;
                dgvNECCMember.DataBind();
            }
        }

        protected void dgvNECCMember_RowEditing(object sender, GridViewEditEventArgs e)
        {
            NECCMemberId = Convert.ToInt32(dgvNECCMember.DataKeys[e.NewEditIndex].Value);
            PopulateNECCMember();
        }

        protected void dgvNECCMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvNECCMember.PageIndex = e.NewPageIndex;
            LoadNECCMember();
        }

        protected void dgvNECCMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            NECCMemberId = Convert.ToInt32(dgvNECCMember.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.NECCMemberMaster objNECCMember = new BusinessLayer.Common.NECCMemberMaster();
            objNECCMember.Delete(NECCMemberId);
            Message.IsSuccess = true;
            Message.Text = "Data Deleted Successfully.";

            Message.Show = true;
            LoadNECCMember();
        }

       

        protected void dgvNECCMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string IsActive = ((HiddenField)e.Row.FindControl("HidIsActive")).Value;
                if (IsActive == "NO")
                {
                    e.Row.CssClass = "ExpiredRowStyle";
                  
                }
                else
                {
                    e.Row.CssClass = "ActiveRowStyle";
                   
                }

              
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadNECCMember();
            Message.Show = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadNECCMember();
            dgvNECCMember.AllowPaging = false;
            dgvNECCMember.DataBind();

            PrepareGridViewForExport(dgvNECCMember);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=NECCMember.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvNECCMember.RenderControl(hTextWriter);
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