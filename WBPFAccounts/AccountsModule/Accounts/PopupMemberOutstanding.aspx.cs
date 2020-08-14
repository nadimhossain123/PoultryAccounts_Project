using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using CollegeERP.Accounts;

namespace AccountsModule.Accounts
{
    public partial class PopupMemberOutstanding : System.Web.UI.Page
    {
        decimal SumTotBill = 0, SumTotRecd = 0;
        ListItem li = new ListItem("---SELECT---", "0");

        ListItem liS = new ListItem(" ", "0");

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (Session["UserMode"] == "Admin")
            //    this.MasterPageFile = "../MasterAdmin.master";
            //else
            //    this.MasterPageFile = "../MasterMember.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                //if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_OUTSTANDING_REPORT))
                //{
                //    Response.Redirect("../Unauthorized.aspx");
                //}
                Message.Show = false;
                btnPrint.Visible = false;
                btnDownload.Visible = false;
                //btnPayment.Visible = false;

                PopulateDropDownLists();
                if (Session["UserMode"].ToString() == "Member")
                    SetMemberReportViewValidations();
                //else
                //    btnPayment.Visible = false;

                if (Request.QueryString["MemberId"] != null && Request.QueryString["MemberId"].ToString().Length > 0)
                {
                    ddlMember.SelectedValue = Request.QueryString["MemberId"].ToString();
                    btnSearch_Click(btnSearch, e);
                }
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
            LoadMember();
        }

        protected void SetMemberReportViewValidations()
        {
            ddlMember.SelectedValue = Session["UserId"].ToString();
            ddlMember.Enabled = false;
            //ddlBlock.Enabled = false;
            //ddlDistrict.Enabled = false;
            //ddlState.Enabled = false;
            //ddlMembershipCategory.Enabled = false;
        }

        protected void LoadMember()
        {
            ddlMember.Items.Clear();
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            //if (ddlBlock.SelectedIndex == 0)
            //    membermaster.BlockId = 0;
            //else
            //    membermaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());

            //if (ddlDistrict.SelectedIndex == 0)
            //    membermaster.DistrictId = 0;
            //else
            //    membermaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());

            //if (ddlState.SelectedIndex == 0)
            //    membermaster.StateId = 0;
            //else
            //    membermaster.StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());

            //if (ddlMembershipCategory.SelectedIndex == 0)
            //    membermaster.CategoryId = 0;
            //else
            //    membermaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());

            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.GetAll(membermaster);
            if (dt != null)
            {
                ddlMember.DataSource = dt;
                ddlMember.DataTextField = "MemberName";
                ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
            }

            ddlMember.Items.Insert(0, li);
        }

        protected void LoadStates()
        {

            //BusinessLayer.Common.State objState = new BusinessLayer.Common.State();
            //DataTable dtState = new DataTable();
            //dtState = objState.GetAll();
            //if (dtState != null)
            //{
            //    ddlState.DataSource = dtState;
            //    ddlState.DataTextField = "StateName";
            //    ddlState.DataValueField = "StateId";
            //    ddlState.DataBind();
            //}
            //InsertFisrtItem(ddlState, "Select");
        }

        protected void LoadDistricts()
        {

            //BusinessLayer.Common.District objDistrict = new BusinessLayer.Common.District();
            //DataTable dtDistrict = new DataTable();
            //int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            //dtDistrict = objDistrict.GetAll(stateid);
            //if (dtDistrict != null)
            //{
            //    ddlDistrict.DataSource = dtDistrict;
            //    ddlDistrict.DataTextField = "DistrictName";
            //    ddlDistrict.DataValueField = "DistrictId";
            //    ddlDistrict.DataBind();
            //}
            //InsertFisrtItem(ddlDistrict, "Select");
        }

        protected void LoadBlock()
        {
            //BusinessLayer.Common.Block objBlock = new BusinessLayer.Common.Block();
            //int districtid = (ddlDistrict.SelectedIndex == 0) ? 0 : int.Parse(ddlDistrict.SelectedValue);
            //int stateid = (ddlState.SelectedIndex == 0) ? 0 : int.Parse(ddlState.SelectedValue);

            //DataTable dt = objBlock.GetAll(districtid, stateid);
            //if (dt != null)
            //{
            //    ddlBlock.DataSource = dt;
            //    ddlBlock.DataTextField = "BlockName";
            //    ddlBlock.DataValueField = "BlockId";
            //    ddlBlock.DataBind();
            //}
            //InsertFisrtItem(ddlBlock, "Select");
        }

        protected void LoadMembershipCategory()
        {
            //BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            //DataTable dt = objMembershipCategory.GetAll();
            //if (dt != null)
            //{
            //    ddlMembershipCategory.DataSource = dt;
            //    ddlMembershipCategory.DataTextField = "CategoryName";
            //    ddlMembershipCategory.DataValueField = "MembershipCategoryId";
            //    ddlMembershipCategory.DataBind();
            //}
            //InsertFisrtItem(ddlMembershipCategory, "Select");
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();

            //int blockid = Convert.ToInt32(ddlBlock.SelectedValue);
            //DataTable dt = new DataTable();
            //dt = objMemberMaster.GetDistrictAndStateByBlockId(blockid);

            //ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            //ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();

            LoadMember();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlMember.SelectedValue != "0" && ddlMember.Text != string.Empty)
            {
                Message.Show = false;
                SumTotBill = 0;
                SumTotRecd = 0;

                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                int MemberId = int.Parse(ddlMember.SelectedValue.Trim());
                string FromDate = txtFromDate.Text.Trim();
                string Todate = txtToDate.Text.Trim();

                DataSet ds = ObjFees.GetStudentOutstandingReport(MemberId, FromDate, Todate);
                LoadMemberInfo(ds.Tables[0]);

                if (ds.Tables[1] != null)
                {
                    dgvBill.DataSource = ds.Tables[1];
                    dgvBill.DataBind();
                }

                dgvBill.FooterRow.Cells[4].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("MainAmt")).Sum() ?? 0).ToString();


                dgvBill.FooterRow.Cells[5].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("TaxAmt")).Sum() ?? 0).ToString();


                dgvBill.FooterRow.Cells[6].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("TotBill")).Sum() ?? 0).ToString();



                dgvBill.FooterRow.Cells[7].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("MainRcvd")).Sum() ?? 0).ToString();


                dgvBill.FooterRow.Cells[8].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("TaxRcvd")).Sum() ?? 0).ToString();


                dgvBill.FooterRow.Cells[9].Text = ((from row in ds.Tables[1].AsEnumerable()
                                                    select row.Field<decimal?>("TotRecd")).Sum() ?? 0).ToString();


                dgvBill.FooterRow.Cells[10].Text = (Convert.ToDecimal(dgvBill.FooterRow.Cells[6].Text) - Convert.ToDecimal(dgvBill.FooterRow.Cells[9].Text)).ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    btnDownload.Visible = true;
                    btnPrint.Visible = true;
                }
                else
                {
                    btnDownload.Visible = false;
                    btnPrint.Visible = false;
                    //btnPayment.Visible = false;
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Member";
                Message.Show = true;
            }
        }

        protected void LoadMemberInfo(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<b>Member Code : " + DT.Rows[0]["MemberCode"].ToString() + "</b><br />");
            sb.Append(@"<b>Member Name : " + DT.Rows[0]["MemberName"].ToString() + "</b><br />");
            sb.Append(@"<b>Block : " + DT.Rows[0]["BlockName"].ToString() + "</b><br />");
            sb.Append(@"<b>District : " + DT.Rows[0]["DistrictName"].ToString() + "</b><br />");
            sb.Append(@"<b>State : " + DT.Rows[0]["StateName"].ToString() + "</b><br /><br />");
            ltrStudentInfo.Text = sb.ToString();
        }

        protected void dgvBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //-------------------Add On 08-08-2013
                string fieldVal;
                fieldVal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocumentNo"));
                if (fieldVal.Length == 0)
                {
                    Label lblDate = (Label)e.Row.FindControl("lblDocumentDate");
                    lblDate.Visible = false;
                }
                //-------------------

            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "WWPF";
            _header[1] = "Member Ledger From " + txtFromDate.Text + " To " + txtToDate.Text + " Printed on " + DateTime.Now.ToString();
            _header[2] = "Member: " + ddlMember.SelectedItem.Text;
            _header[3] = "";

            string[] _footer = new string[0];
            string file = "MEMBER_OUTSTANDING_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvBill, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Member Outstanding Report";
            string[] _header = new string[3];
            _header[0] = "Member Ledger From " + txtFromDate.Text + " To " + txtToDate.Text + " Printed on " + DateTime.Now.ToString();
            _header[1] = "Member: " + ddlMember.SelectedItem.Text;
            _header[2] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvBill, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void ddlMembershipCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMember();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
            LoadMember();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
            LoadMember();
        }
    }
}