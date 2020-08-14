using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using BusinessLayer.Accounts;
using CollegeERP.Accounts;

namespace AccountsModule.Common
{
    public partial class DevelopmentFeeMemberReport : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        //public DateTime FromDate;
        //public DateTime ToDate;


        public DateTime FromDate
        {
            get { return Convert.ToDateTime(ViewState["FromDate"].ToString()); }
            set { ViewState["FromDate"] = value; }
        }
        public DateTime ToDate
        {
            get { return Convert.ToDateTime(ViewState["ToDate"].ToString()); }
            set { ViewState["ToDate"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    Message.Show = false;
                    btnDownload.Visible = false;
                    btnPrint.Visible = false;
                    string strValues = Session["FinYrID"].ToString();
                    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartYear"].ToString() + "-04-01");
                        ToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndYear"].ToString() + "-03-31");
                    }
                    else
                    {
                        if (DateTime.Now.Month >= 4 && DateTime.Now.Month <= 12)
                        {
                            FromDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-04-01");
                            ToDate = Convert.ToDateTime((DateTime.Now.Year + 1).ToString() + "-03-31");
                        }
                        else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
                        {
                            FromDate = Convert.ToDateTime((DateTime.Now.Year - 1).ToString() + "-04-01");
                            ToDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-03-31");
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void PopulateDropDownLists()
        {
            LoadStates();
            LoadDistricts();
            LoadBlock();
            LoadMembershipCategory();
            LoadBusinessType();
        }

        protected void LoadStates()
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
            InsertFisrtItem(ddlState, "Any State");
        }

        protected void LoadDistricts()
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
            InsertFisrtItem(ddlDistrict, "Any District");
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
            InsertFisrtItem(ddlBlock, "Any Block");
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
            InsertFisrtItem(ddlMembershipCategory, "Any Category");
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
        protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        protected void LoadMemberList()
        {
            
            int StateId = Convert.ToInt32(ddlState.SelectedValue);
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            int BlockId = Convert.ToInt32(ddlBlock.SelectedValue);
            int CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue);
            int BusinessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue);
            string MemberName = txtMemberName.Text.Trim();
            string MobileNo = txtMobile.Text.Trim();
            int FinYearId = Convert.ToInt32(Session["FinYrID"]);
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
            DataTable dt = objMemberMaster.DevelopmentMemberGetAll(StateId, DistrictId, BlockId, CategoryId, MemberName, MobileNo, BusinessTypeId, FromDate,ToDate);
            
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dgvMemberMaster.DataSource = dv;
                dgvMemberMaster.DataBind();
                lblTotalMemberCount.Text = Server.HtmlDecode("<b>Total Member Count: " + dv.ToTable().Rows.Count.ToString() + "</b>");
            }
            if (dt.Rows.Count > 0)
            {
                btnDownload.Visible = true;
                btnPrint.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
                btnPrint.Visible = false;
            }
        }

        //protected void dgvMemberMaster_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    int MemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[e.NewEditIndex].Values["MemberId"]);
        //    Response.Redirect("AddEditMemberMaster.aspx?id=" + MemberId + "&Back=MemberMasterInfo.aspx");
        //}

        //protected void dgvMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    dgvMemberMaster.PageIndex = e.NewPageIndex;
        //    LoadMemberList();
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            Message.Show = false;
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (dgvMemberMaster.Rows.Count > 0)
            {
                string[] _header = new string[5];
                _header[0] = "DEVELOPMENT FEE MEMBER LIST";
                _header[1] = "STATE: " + ddlState.SelectedItem.ToString();
                _header[2] = "DISTRICT: " + ddlDistrict.SelectedItem.ToString();
                _header[3] = "BLOCK: " + ddlBlock.SelectedItem.ToString();
                _header[4] = lblTotalMemberCount.Text;

                string[] _footer = new string[0];
                string file = "MEMBER_LIST";

                BusinessLayer.Common.Excel.SaveExcel(_header, dgvMemberMaster, _footer, file);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "<u>DEVELOPMENT FEE OUTSTANDING LIST</u>";
            string[] _header = new string[3];
            _header[0] = "";
            _header[1] = "PRINTED ON " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[1];
            _footer[0] = "";
            //Session[clsGlobalVariable.sesColumnToBeHidden] = "11,12,13,14";

            Print.ReportPrint(Title, _header, dgvMemberMaster, _footer);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LIST", "window.open('../Accounts/RPTShowGrid.aspx" + "','','height=700,width=1000')", true);
        }
    }
}