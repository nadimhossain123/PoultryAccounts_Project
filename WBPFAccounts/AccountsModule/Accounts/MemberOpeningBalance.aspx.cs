using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace AccountsModule.Accounts
{
    public partial class MemberOpeningBalance : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("---SELECT MEMBER---", "0");


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadFeesHead();
                PopulateDropDownLists();
                Message.Show = false;
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
            InsertFisrtItem(ddlState, "Select");
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
            InsertFisrtItem(ddlDistrict, "Select");
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
            InsertFisrtItem(ddlBlock, "Select");
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
            InsertFisrtItem(ddlMembershipCategory, "Select");
        }

        protected void LoadMember()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();

            if (ddlBlock.SelectedIndex == 0)
                membermaster.BlockId = 0;
            else
                membermaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());

            if (ddlDistrict.SelectedIndex == 0)
                membermaster.DistrictId = 0;
            else
                membermaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());

            if (ddlState.SelectedIndex == 0)
                membermaster.StateId = 0;
            else
                membermaster.StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());

            if (ddlMembershipCategory.SelectedIndex == 0)
                membermaster.CategoryId = 0;
            else
                membermaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());

            membermaster.MemberName = string.Empty;

            DataTable dt = objMember.MemberMaster_GetAll_ForReport(membermaster);
            if (dt != null)
            {
                ddlMember.DataSource = dt;
                ddlMember.DataTextField = "MemberName";
                ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
            }

            ddlMember.Items.Insert(0, li);
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();

            //int blockid = Convert.ToInt32(ddlBlock.SelectedValue);
            //DataTable dt = new DataTable();
            //dt = objMemberMaster.GetDistrictAndStateByBlockId(blockid);

            //ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            //ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();

            LoadMember();
        }

        protected void LoadFeesHead()
        {

            BusinessLayer.Accounts.StreamGroup obj = new BusinessLayer.Accounts.StreamGroup();

            DataTable DT = obj.GetAllFeesHead();

            dgvFeesHead.DataSource = DT;
            dgvFeesHead.DataBind();

            Message.Show = false;

        }

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeypress", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
                ((DropDownList)e.Row.FindControl("ddlDrCr")).Attributes.Add("onchange", "javascript:TotalAmount();");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ddlMember.SelectedValue == "0" || ddlMember.Text == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select a Member";
                Message.Show = true;
            }
            else
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                //{
                BusinessLayer.Accounts.StudentOpeningBal objOpeningBal = new BusinessLayer.Accounts.StudentOpeningBal();
                Entity.Accounts.StudentOpeningBal OpBal = new Entity.Accounts.StudentOpeningBal();

                OpBal.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
                OpBal.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString().Trim());
                OpBal.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
                OpBal.StudentId = Convert.ToInt32(ddlMember.SelectedValue.Trim());
                OpBal.BillAmount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                OpBal.CreatedBy = Convert.ToInt32(Session["UserId"].ToString().Trim());

                DataTable DT = new DataTable();
                DT.Columns.Add("FeesHeadId", typeof(int));
                DT.Columns.Add("AmountDr", typeof(decimal));
                DT.Columns.Add("AmountCr", typeof(decimal));
                DataRow DR;

                foreach (GridViewRow GVR in dgvFeesHead.Rows)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                        DropDownList ddlDrCr = (DropDownList)GVR.FindControl("ddlDrCr");
                        decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                        if (Amount > 0)
                        {
                            count++;
                            DR = DT.NewRow();
                            DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[GVR.RowIndex].Value.ToString());
                            DR["AmountDr"] = (ddlDrCr.SelectedValue == "DR") ? Amount : 0;
                            DR["AmountCr"] = (ddlDrCr.SelectedValue == "CR") ? Amount : 0;
                            DT.Rows.Add(DR);
                            DT.AcceptChanges();
                        }
                    }
                }

                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(DT);
                    OpBal.OpeningBalXML = ds.GetXml().Replace("Table1>", "Table>");
                }

                if (ViewState["BillId"] != null && ViewState["BillId"].ToString().Length > 0)
                {
                    OpBal.BillId = int.Parse(ViewState["BillId"].ToString());
                    ViewState.Remove("BillId");
                }
                else
                    OpBal.BillId = 0;

                if (count > 0)
                {
                    int RecCount = objOpeningBal.SaveOpBal(OpBal);
                    if (RecCount > 0)
                    {
                        Message.IsSuccess = true;
                        Message.Text = "Opening Balance Created Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Opening Balance Allready Created";
                    }
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Enter Amount for Atleast One Head";
                }
                //}
                //else
                //{
                //    Message.IsSuccess = false;
                //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
                //}           

                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberOpeningBalance.aspx");
        }

        protected void ddlMembershipCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMember();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFeesHead();
            txtTotalAmt.Text = "0.00";

            BusinessLayer.Accounts.StudentOpeningBal objOpeningBal = new BusinessLayer.Accounts.StudentOpeningBal();
            Entity.Accounts.StudentOpeningBal OpBal = new Entity.Accounts.StudentOpeningBal();

            OpBal.StudentId = int.Parse(ddlMember.SelectedValue);

            DataSet ds = new DataSet();
            ds = objOpeningBal.StudentOpeningBalance_GetById(OpBal);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                decimal dramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["AmountDr"].ToString());
                decimal cramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["AmountCr"].ToString());
                txtTotalAmt.Text = (dramt > cramt) ? Convert.ToString(dramt) : Convert.ToString((cramt * -1));
            }

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (GridViewRow gvr in dgvFeesHead.Rows)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    DropDownList ddlDrCr = (DropDownList)gvr.FindControl("ddlDrCr");
                    int FeesHeadId = int.Parse(dgvFeesHead.DataKeys[gvr.RowIndex].Values[0].ToString());
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (FeesHeadId == int.Parse(dr["FeesHeadId"].ToString()))
                        {
                            decimal AmtDr = Convert.ToDecimal(dr["AmountDr"].ToString());
                            decimal AmtCr = Convert.ToDecimal(dr["AmountCr"].ToString());
                            txtAmount.Text = (AmtDr > AmtCr) ? Convert.ToString(AmtDr) : Convert.ToString(AmtCr);
                            ddlDrCr.SelectedValue = (AmtDr > AmtCr) ? "DR" : "CR";
                        }
                    }
                }
                ViewState["BillId"] = ds.Tables[0].Rows[0]["BillId"].ToString();
                btnSave.Text = "Update";
            }
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