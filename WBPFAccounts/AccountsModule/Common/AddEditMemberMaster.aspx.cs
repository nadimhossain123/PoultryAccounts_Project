using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;
using System.Text;

namespace AccountsModule.Common
{
    public partial class AddEditMemberMaster : System.Web.UI.Page
    {
        Entity.Common.MemberMaster memberMaster = new Entity.Common.MemberMaster();

        public int MemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }

        public string BackPage
        {
            get { return ViewState["BackPage"].ToString(); }
            set { ViewState["BackPage"] = value; }
        }
        public string Mode { get; set; }
        public string Photo { get; set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserType"] == null)
                this.MasterPageFile = "../Home.master";
            else if (Session["UserType"].ToString().Equals("Admin"))
                this.MasterPageFile = "../MasterAdmin.master";
            else if (Session["UserType"].ToString().Equals("Member"))
                this.MasterPageFile = "../MasterMember.master";
            else if (Session["UserType"].ToString().Equals("Agent"))
                this.MasterPageFile = "../MasterAgent.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownLists();
                BackPage = Request.QueryString["Back"].ToString();

                if (Session["UserType"] != null)
                {
                    if (Session["UserType"].ToString().Equals("Admin") && Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                    {
                        MemberId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                        PopulateMemberMaster();
                        PopulateMemberDocument();
                    }
                    else if (Session["UserType"].ToString().Equals("Agent") && Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                    {
                        MemberId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                        PopulateMemberMaster();
                        PopulateMemberDocument();
                    }
                    else if (Session["UserType"].ToString().Equals("Member"))
                    {
                        MemberId = Convert.ToInt32(Session["UserId"].ToString());
                        PopulateMemberMaster();
                        PopulateMemberDocument();
                    }
                    else
                    {
                        Response.Redirect("../Login.aspx");
                    }
                }
                else
                {
                    InitializeForm();                   
                }
            }
        }

        private void InitializeForm()
        {
            MemberId = 0;
            Message.Show = false;
            txtMemberCode.Text = "SYSTEM GENERATED AFTER APPROVAL";
            chkMember.Checked = true;
            ImgPhoto.ImageUrl = "MemberPhoto/Male.jpg";
            txtMembershipDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtMembershipDate.Enabled = false;
            txtEffectiveDate.Enabled = false;
            txtDocName_1.Text = string.Empty;
            txtDocName_2.Text = string.Empty;
            txtDocName_3.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lblDocFile_1.Text = string.Empty;
            lblDocFile_2.Text = string.Empty;
            lblDocFile_3.Text = string.Empty;
            txtMembershipCategoryEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Text = "Submit";
        }

        protected void LoadState()
        {
            BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            DataTable dtState = new DataTable();
            dtState = objState.GetAll();
            if (dtState != null)
            {
                ddlState.DataSource = dtState;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
            }
            InsertFisrtItem(ddlState, "--Select State--");
        }

        protected void LoadDistrict()
        {
            BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            DataTable dtDistrict = new DataTable();
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            dtDistrict = objDistrict.GetAll(stateid);
            if (dtDistrict != null)
            {
                ddlDistrict.DataSource = dtDistrict;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
            InsertFisrtItem(ddlDistrict, "--Select District--");
        }

        protected void LoadBlock()
        {
            BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            int districtid = (ddlDistrict.SelectedIndex == 0) ? 0 : int.Parse(ddlDistrict.SelectedValue);
            int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            DataTable dt = objBlock.GetAll(districtid, stateid);
            if (dt != null)
            {
                ddlBlock.DataSource = dt;
                ddlBlock.DataTextField = "BlockName";
                ddlBlock.DataValueField = "BlockId";
                ddlBlock.DataBind();
            }
            InsertFisrtItem(ddlBlock, "--Select Block--");
        }

        protected void LoadMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            DataTable dt = objMembershipCategory.GetAll();
            if (dt != null)
            {
                ddlMembershipCategory.DataSource = dt;
                ddlMembershipCategory.DataTextField = "CategoryName";
                ddlMembershipCategory.DataValueField = "MembershipCategoryId";
                ddlMembershipCategory.DataBind();
            }
            InsertFisrtItem(ddlMembershipCategory, "--Select Category--");
        }

        protected void LoadMemberGroup()
        {
            BusinessLayer.Common.MemberGroup objMemberGroup = new BusinessLayer.Common.MemberGroup();
            DataTable dt = objMemberGroup.GetAll();
            if (dt != null)
            {
                ddlMemberGroup.DataSource = dt;
                ddlMemberGroup.DataTextField = "MemberGroupName";
                ddlMemberGroup.DataValueField = "MemberGroupId";
                ddlMemberGroup.DataBind();
            }
            InsertFisrtItem(ddlMemberGroup, "--Select Group--");
        }

        protected void LoadBusinessType()
        {
            BusinessLayer.Common.BusinessType objBusinessType = new BusinessLayer.Common.BusinessType();
            DataTable dt = objBusinessType.GetAll();
            if (dt != null)
            {
                ddlBusinessType.DataSource = dt;
                ddlBusinessType.DataTextField = "BusinessTypeName";
                ddlBusinessType.DataValueField = "BusinessTypeId";
                ddlBusinessType.DataBind();
            }
            InsertFisrtItem(ddlBusinessType, "--Select Business Type--");
        }

        protected void LoadMemberSMSCategory()
        {
            BusinessLayer.Common.MemberSMSCategory objMemberSMSCategory = new BusinessLayer.Common.MemberSMSCategory();
            DataTable dt = objMemberSMSCategory.MemberSMSCategory_GetAll();
            if (dt != null)
            {
                ddlMemberSMSCategory.DataSource = dt;
                ddlMemberSMSCategory.DataTextField = "MemberSMSCategoryName";
                ddlMemberSMSCategory.DataValueField = "MemberSMSCategoryId";
                ddlMemberSMSCategory.DataBind();
            }
            InsertFisrtItem(ddlMemberSMSCategory, "--Select SMS Category--");
        }
                
        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadState();
            LoadDistrict();
            LoadBlock();
            LoadMembershipCategory();
            LoadMemberGroup();
            LoadBusinessType();
            LoadMemberSMSCategory();
        }

        protected void PopulateMemberMaster()
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            memberMaster = objMemberMaster.GetMemberMasterById(MemberId);
        
            if (memberMaster != null)
            {
                MemberId = memberMaster.MemberId;
                txtMemberName.Text = memberMaster.MemberName.ToString();
                //txtMemberName.Enabled = false;
                txtMemberCode.Text = memberMaster.MemberCode.ToString();
                txtMemberCode.Enabled = false;
                txtMobileNo.Text = memberMaster.MobileNo.ToString();
                txtPhoneNo.Text = memberMaster.PhoneId.ToString();
                txtEmail.Text = memberMaster.Email.ToString();
                txtVATNo.Text = memberMaster.VATNo.ToString();
                txtPANNo.Text = memberMaster.PANNo.ToString();
                txtLICNo.Text = memberMaster.LICNo.ToString();
                txtGSTNo.Text = memberMaster.GSTNo.ToString();//NEW
                txtVillageOrStreet.Text = memberMaster.VillageOrStreet.ToString();
                txtPlotNo.Text = memberMaster.PlotNo.ToString();
                txtKhaitanNo.Text = memberMaster.KhaitanNo.ToString();
                txtMouza.Text = memberMaster.Mouza.ToString();
                txtJLNo.Text = memberMaster.JLNo.ToString();
                txtPO.Text = memberMaster.PO.ToString();
                txtPS.Text = memberMaster.PS.ToString();
                txtPIN.Text = memberMaster.PIN.ToString();

                ddlState.SelectedValue = memberMaster.StateId.ToString();

                if (memberMaster.IsApproved)
                    ddlState.Enabled = false;

                LoadDistrict();

                ddlDistrict.SelectedValue = memberMaster.DistrictId.ToString();

                if (memberMaster.IsApproved)
                    ddlDistrict.Enabled = false;

                LoadBlock();

                ddlBlock.SelectedValue = memberMaster.BlockId.ToString();

                if (memberMaster.IsApproved)
                    ddlBlock.Enabled = false;

                
                txtCompanyName.Text = memberMaster.CompanyName.ToString();
                ddlMembershipCategory.SelectedValue = memberMaster.CategoryId.ToString();
                //ddlMembershipCategory.Enabled = false;

                ddlMemberGroup.SelectedValue = memberMaster.MemberGroupId.ToString();
                //ddlMemberGroup.Enabled = false;
                ddlBusinessType.SelectedValue = memberMaster.BusinessTypeId.ToString();
                //ddlBusinessType.Enabled = false;
                
                txtMembershipDate.Text = memberMaster.MembershipDate.ToString("dd/MM/yyyy");
                txtEffectiveDate.Text = memberMaster.EffectiveDate.ToString("dd/MM/yyyy");

                if (Session["UserType"].ToString().Equals("Admin"))
                {
                    txtMembershipDate.Enabled = true;
                    txtEffectiveDate.Enabled = true;
                }
                else
                {
                    txtMembershipDate.Enabled = false;
                    txtEffectiveDate.Enabled = false;
                }
                
                txtLayerCapacityNos.Text = memberMaster.LayerCapacityNos.ToString();
                txtBroilerCapacityNos.Text = memberMaster.BroilerCapacityNos.ToString();
                txtBreederCapacityNos.Text = memberMaster.BreederCapacityNos.ToString();
                txtEggSellerDailySalesNos.Text = memberMaster.EggSellerDailySalesNos.ToString();
                txtChickenSellerDailySalesNos.Text = memberMaster.ChickenSellerDailySalesNos.ToString();
                txtChickenSellerDailySalesKgs.Text = memberMaster.ChickenSellerDailySalesKgs.ToString();
                txtFeedProducerDailySalesMT.Text = memberMaster.FeedProducerDailySalesMT.ToString();
                txtFeedSellerDailySalesMT.Text = memberMaster.FeedSellerDailySalesMT.ToString();
                txtOtherCategory.Text = memberMaster.OtherCategory.ToString();
                txtRemarks.Text = memberMaster.Remarks.ToString();
                txtWebsite.Text = memberMaster.Website.ToString();
                ImgPhoto.ImageUrl = (memberMaster.ImageName == string.Empty) ? "MemberPhoto/Male.jpg" : "MemberPhoto/" + memberMaster.ImageName;
                chkMember.Checked = memberMaster.IsMember;
                chkMember.Enabled = false;

                txtLayerCapacityNos.Enabled = chkMember.Checked;
                txtBreederCapacityNos.Enabled = chkMember.Checked;
                txtChickenSellerDailySalesNos.Enabled = chkMember.Checked;
                txtFeedProducerDailySalesMT.Enabled = chkMember.Checked;
                txtBroilerCapacityNos.Enabled = chkMember.Checked;
                txtEggSellerDailySalesNos.Enabled = chkMember.Checked;
                txtChickenSellerDailySalesKgs.Enabled = chkMember.Checked;
                txtFeedSellerDailySalesMT.Enabled = chkMember.Checked;
                txtOtherCategory.Enabled = chkMember.Checked;
                txtRemarks.Enabled = chkMember.Checked;
                
                chkIsGovtMember.Checked = memberMaster.IsGovtMember;
                chkIsGovtMember.Enabled = false;

                if (memberMaster.MemberSMSCategoryId == 0)
                    ddlMemberSMSCategory.SelectedIndex = 0;
                else
                    ddlMemberSMSCategory.SelectedValue = Convert.ToString(memberMaster.MemberSMSCategoryId);

                txtMobileNo2.Text = memberMaster.MobileNo2;
                txtNarration.Text = memberMaster.Narration;
                txtMembershipCategoryEffectiveDate.Text = memberMaster.MembershipCategoryEffectiveDate.ToString("dd/MM/yyyy");
                chkExecutiveMember.Checked = memberMaster.IsExecutiveMember;

                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        private void PopulateMemberDocument()
        {
            BusinessLayer.Common.MemberDocument objMemberDocument = new BusinessLayer.Common.MemberDocument();
            Entity.Common.MemberDocument memberDocument = new Entity.Common.MemberDocument();
            memberDocument = objMemberDocument.GetAllById(MemberId);

            if (memberDocument != null)
            {
                txtDocName_1.Text = memberDocument.DocName_1;
                txtDocName_2.Text = memberDocument.DocName_2;
                txtDocName_3.Text = memberDocument.DocName_3;

                if (memberDocument.DocFile_1 != null && !string.IsNullOrEmpty(memberDocument.DocFile_1.Trim()))
                    lblDocFile_1.Text = Server.HtmlDecode(@"<a href='MemberDocument/" + memberDocument.DocFile_1.Trim() + "' target='_blank'>Download File</a>");

                if (memberDocument.DocFile_2 != null && !string.IsNullOrEmpty(memberDocument.DocFile_2.Trim()))
                    lblDocFile_2.Text = Server.HtmlDecode(@"<a href='MemberDocument/" + memberDocument.DocFile_2.Trim() + "' target='_blank'>Download File</a>");

                if (memberDocument.DocFile_3 != null && !string.IsNullOrEmpty(memberDocument.DocFile_3.Trim()))
                    lblDocFile_3.Text = Server.HtmlDecode(@"<a href='MemberDocument/" + memberDocument.DocFile_3.Trim() + "' target='_blank'>Download File</a>");
            }
        }
               
        public bool IsValidPhoto()
        {
            bool IsValid = true;
            if (uploadImage.PostedFile.FileName != null && uploadImage.PostedFile.ContentLength > 0)
            {
                string fn = uploadImage.FileName;
                string fileExt = System.IO.Path.GetExtension(fn);
                if (fileExt.ToUpper().Equals(".JPG") || fileExt.ToUpper().Equals(".JPEG") || fileExt.ToUpper().Equals(".PNG"))
                {
                    string sm = Server.MapPath("");
                    Photo = fileExt;
                }
                else
                {
                    IsValid = false;
                }
            }
            else
            {
                Photo = "";
            }
            return IsValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidPhoto())
                {
                    BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                    memberMaster.MemberId = MemberId;
                    memberMaster.MemberName = txtMemberName.Text.Trim();
                    memberMaster.MemberCode = txtMemberCode.Text.Trim();
                    memberMaster.MobileNo = txtMobileNo.Text.Trim();
                    memberMaster.PhoneId = txtPhoneNo.Text.Trim();
                    memberMaster.Email = txtEmail.Text.Trim();
                    memberMaster.VATNo = txtVATNo.Text.Trim();
                    memberMaster.PANNo = txtPANNo.Text.Trim();
                    memberMaster.LICNo = txtLICNo.Text.Trim();
                    memberMaster.GSTNo = txtGSTNo.Text.Trim();//NEW
                    memberMaster.VillageOrStreet = txtVillageOrStreet.Text.Trim();
                    memberMaster.PlotNo = txtPlotNo.Text.Trim();
                    memberMaster.KhaitanNo = txtKhaitanNo.Text.Trim();
                    memberMaster.Mouza = txtMouza.Text.Trim();
                    memberMaster.JLNo = txtJLNo.Text.Trim();
                    memberMaster.PO = txtPO.Text.Trim();
                    memberMaster.PS = txtPS.Text.Trim();
                    memberMaster.PIN = txtPIN.Text.Trim();
                    memberMaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue);
                    memberMaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    memberMaster.StateId = Convert.ToInt32(ddlState.SelectedValue);
                    memberMaster.CompanyName = txtCompanyName.Text.Trim();
                    memberMaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue);
                    memberMaster.IsMember = chkMember.Checked;
                    memberMaster.MemberGroupId = Convert.ToInt32(ddlMemberGroup.SelectedValue);
                    memberMaster.BusinessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue);

                    memberMaster.MembershipDate = Convert.ToDateTime(txtMembershipDate.Text.Trim().Split('/')[2] + "-" + txtMembershipDate.Text.Trim().Split('/')[1] + "-" + txtMembershipDate.Text.Trim().Split('/')[0]);
                    memberMaster.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim().Split('/')[2] + "-" + txtEffectiveDate.Text.Trim().Split('/')[1] + "-" + txtEffectiveDate.Text.Trim().Split('/')[0]);
                    //memberMaster.OpBal = (txtOpBal.Text.Trim().Length > 0) ? Convert.ToDecimal(txtOpBal.Text.Trim()) : 0;
                    //memberMaster.DrORCr = ddlDRorCR.SelectedValue;
                    memberMaster.LayerCapacityNos = (txtLayerCapacityNos.Text.Trim().Length > 0) ? txtLayerCapacityNos.Text.Trim() : "";
                    memberMaster.BroilerCapacityNos = (txtBroilerCapacityNos.Text.Trim().Length > 0) ? txtBroilerCapacityNos.Text.Trim() : "";
                    memberMaster.BreederCapacityNos = (txtBreederCapacityNos.Text.Trim().Length > 0) ? txtBreederCapacityNos.Text.Trim() : "";
                    memberMaster.EggSellerDailySalesNos = (txtEggSellerDailySalesNos.Text.Trim().Length > 0) ? txtEggSellerDailySalesNos.Text.Trim() : "";
                    memberMaster.ChickenSellerDailySalesNos = (txtChickenSellerDailySalesNos.Text.Trim().Length > 0) ? txtChickenSellerDailySalesNos.Text.Trim() : "";
                    memberMaster.ChickenSellerDailySalesKgs = (txtChickenSellerDailySalesKgs.Text.Trim().Length > 0) ? txtChickenSellerDailySalesKgs.Text.Trim() : "";
                    memberMaster.FeedProducerDailySalesMT = (txtFeedProducerDailySalesMT.Text.Trim().Length > 0) ? txtFeedProducerDailySalesMT.Text.Trim() : "";
                    memberMaster.FeedSellerDailySalesMT = (txtFeedSellerDailySalesMT.Text.Trim().Length > 0) ? txtFeedSellerDailySalesMT.Text.Trim() : "";
                    memberMaster.OtherCategory = txtOtherCategory.Text.Trim();
                    memberMaster.Remarks = txtRemarks.Text.Trim();
                    memberMaster.UserId = 0;// Convert.ToInt32(Session["UserId"].ToString());
                    memberMaster.CompanyId = 0;// Convert.ToInt32(Session["CompanyId"].ToString());
                    memberMaster.BranchId = 0;// Convert.ToInt32(Session["BranchId"].ToString());
                    memberMaster.FinYearId = 0;// Convert.ToInt32(Session["FinYrID"].ToString());
                    memberMaster.DataFlow = 0;// Convert.ToInt32(Session["DataFlow"].ToString());
                    memberMaster.Website = txtWebsite.Text.Trim();
                    memberMaster.ImageExt = Photo;
                    memberMaster.IsGovtMember = chkIsGovtMember.Checked;
                    memberMaster.MemberSMSCategoryId = (ddlMemberSMSCategory.SelectedIndex == 0) ? 0 : int.Parse(ddlMemberSMSCategory.SelectedValue);
                    memberMaster.MobileNo2 = txtMobileNo2.Text.Trim();
                    memberMaster.Narration = txtNarration.Text.Trim();
                    memberMaster.MembershipCategoryEffectiveDate = Convert.ToDateTime(txtMembershipCategoryEffectiveDate.Text.Trim().Split('/')[2] + "-" + txtMembershipCategoryEffectiveDate.Text.Trim().Split('/')[1] + "-" + txtMembershipCategoryEffectiveDate.Text.Trim().Split('/')[0]);

                    memberMaster.IsExecutiveMember = chkExecutiveMember.Checked;
                    objMemberMaster.Save(memberMaster);
                    //txtMemberCode.Text = memberMaster.MemberCode;

                    if (Photo.Trim() != "")
                    {
                        string ff = (Server.MapPath("") + "\\MemberPhoto\\" + memberMaster.MemberId.ToString().Replace("/", "-") + Photo);
                        uploadImage.PostedFile.SaveAs(ff);
                    }

                    //Document Upload Section Start
                    if (MemberId.Equals(0))
                        Mode = "ADD";
                    else
                        Mode = "EDIT";

                    SaveDocument(memberMaster.MemberId);
                    //Document Upload Section End
                    
                    Message.IsSuccess = true;
                    Message.Text = (MemberId == 0) ? "Member Registration Successfully Completed." : "Member Details Updated Successfully.";
                    Message.Show = true;
                    btnSave.Visible = false;
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Photo should be in .jpg or .jpeg or .png format";
                    Message.Show = true;
                }
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save!!!";
            }
            Message.Show = true;
        }

        private void SaveDocument(int ID)
        {
            BusinessLayer.Common.MemberDocument objMemberDocument = new BusinessLayer.Common.MemberDocument();
            Entity.Common.MemberDocument memberDocument = new Entity.Common.MemberDocument();
            memberDocument.Mode = Mode;
            memberDocument.MemberId = ID;
            memberDocument.DocName_1 = txtDocName_1.Text.Trim();
            memberDocument.DocName_2 = txtDocName_2.Text.Trim();
            memberDocument.DocName_3 = txtDocName_3.Text.Trim();

            if (fileUploadDocument_1.PostedFile.FileName != null && fileUploadDocument_1.PostedFile.ContentLength > 0)
                memberDocument.DocFile_1 = System.IO.Path.GetExtension(fileUploadDocument_1.FileName);
            else
                memberDocument.DocFile_1 = "";

            if (fileUploadDocument_2.PostedFile.FileName != null && fileUploadDocument_2.PostedFile.ContentLength > 0)
                memberDocument.DocFile_2 = System.IO.Path.GetExtension(fileUploadDocument_2.FileName);
            else
                memberDocument.DocFile_2 = "";

            if (fileUploadDocument_3.PostedFile.FileName != null && fileUploadDocument_3.PostedFile.ContentLength > 0)
                memberDocument.DocFile_3 = System.IO.Path.GetExtension(fileUploadDocument_3.FileName);
            else
                memberDocument.DocFile_3 = "";

            objMemberDocument.Save(memberDocument);

            if (!string.IsNullOrEmpty(memberDocument.DocFile_1.Trim()))
            {
                string ff = (Server.MapPath("") + "\\MemberDocument\\" + ID.ToString() + "_1" + memberDocument.DocFile_1.Trim());
                fileUploadDocument_1.PostedFile.SaveAs(ff);
            }

            if (!string.IsNullOrEmpty(memberDocument.DocFile_2.Trim()))
            {
                string ff = (Server.MapPath("") + "\\MemberDocument\\" + ID.ToString() + "_2" + memberDocument.DocFile_2.Trim());
                fileUploadDocument_2.PostedFile.SaveAs(ff);
            }

            if (!string.IsNullOrEmpty(memberDocument.DocFile_3.Trim()))
            {
                string ff = (Server.MapPath("") + "\\MemberDocument\\" + ID.ToString() + "_3" + memberDocument.DocFile_3.Trim());
                fileUploadDocument_3.PostedFile.SaveAs(ff);
            }
        }

        protected void chkMember_CheckedChanged(object sender, EventArgs e)
        {
            txtLayerCapacityNos.Text = string.Empty;
            txtBreederCapacityNos.Text = string.Empty;
            txtChickenSellerDailySalesNos.Text = string.Empty;
            txtFeedProducerDailySalesMT.Text = string.Empty;
            txtBroilerCapacityNos.Text = string.Empty;
            txtEggSellerDailySalesNos.Text = string.Empty;
            txtChickenSellerDailySalesKgs.Text = string.Empty;
            txtFeedSellerDailySalesMT.Text = string.Empty;
            txtOtherCategory.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ddlMemberGroup.SelectedIndex = 0;
            ddlBusinessType.SelectedIndex = 0;

            txtLayerCapacityNos.Enabled = chkMember.Checked;
            txtBreederCapacityNos.Enabled = chkMember.Checked;
            txtChickenSellerDailySalesNos.Enabled = chkMember.Checked;
            txtFeedProducerDailySalesMT.Enabled = chkMember.Checked;
            txtBroilerCapacityNos.Enabled = chkMember.Checked;
            txtEggSellerDailySalesNos.Enabled = chkMember.Checked;
            txtChickenSellerDailySalesKgs.Enabled = chkMember.Checked;
            txtFeedSellerDailySalesMT.Enabled = chkMember.Checked;
            txtOtherCategory.Enabled = chkMember.Checked;
            txtRemarks.Enabled = chkMember.Checked;
            ddlMemberGroup.Enabled = chkMember.Checked;
            ddlBusinessType.Enabled = chkMember.Checked;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackPage);
        }
    }
}