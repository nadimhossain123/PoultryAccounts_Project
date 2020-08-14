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
    public partial class SMSMemberView : System.Web.UI.Page
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
                    LoadSMSMember();

                    if (Request.QueryString["SMSMemberId"] != null && Request.QueryString["SMSMemberId"].Trim().Length > 0
                        && Request.QueryString["RedirectUrl"] != null && Request.QueryString["RedirectUrl"].Length > 0)
                    {
                        SMSMemberId = Convert.ToInt32(Request.QueryString["SMSMemberId"]);
                        RedirectUrl = Convert.ToString(Request.QueryString["RedirectUrl"]);
                        
                        
                    }
                    else
                    {
                        
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

           
        }


        protected void PopulateDropDownLists()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable DT = objDistrict.GetAll(0);

            ddlDistrict.DataSource = DT;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All District", "0"));
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }



        protected void LoadSMSMember()
        {
            string MemberName = txtSearchMemberName.Text.Trim();
            string MobileNo = txtSearchMobileNo.Text.Trim();
            int MemberType = Convert.ToInt32(ddlSearchRegType.SelectedValue);
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);

            BusinessLayer.Common.SMSMemberMaster objSMSMember = new BusinessLayer.Common.SMSMemberMaster();
            DataTable dt = objSMSMember.GetAllMember(MemberName, MobileNo, MemberType, DistrictId,0);
            if (dt != null)
            {
                dgvSMSMember.DataSource = dt;
                dgvSMSMember.DataBind();
            }
        }


        protected void dgvSMSMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSMSMember.PageIndex = e.NewPageIndex;
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
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LoadSMSMember();
            dgvSMSMember.AllowPaging = false;
            dgvSMSMember.DataBind();

            PrepareGridViewForExport(dgvSMSMember);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=SMSMember.xls");
            Response.ContentType = "application/excel";
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

    }
}