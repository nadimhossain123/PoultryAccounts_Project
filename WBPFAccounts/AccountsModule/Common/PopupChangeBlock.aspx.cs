using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Text;

namespace AccountsModule.Common
{
    public partial class PopupChangeBlock : System.Web.UI.Page
    {
        public int OldMemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;

                if (Request.QueryString["memberId"] != null && Request.QueryString["memberId"].ToString().Length > 0)
                {
                    OldMemberId = Convert.ToInt32(Request.QueryString["memberId"].ToString());
                    PopulateDropDownLists();
                }
            }
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();

            int blockid = Convert.ToInt32(ddlBlock.SelectedValue);
            DataTable dt = new DataTable();
            dt = objMemberMaster.GetDistrictAndStateByBlockId(blockid);

            ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();
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
            ddlState.Items.Insert(0, "");
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
            ddlDistrict.Items.Insert(0, "");
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
            ddlBlock.Items.Insert(0, "");
        }

        protected void LoadDRorCR()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Rows.Add();
            dt.Rows[0]["ID"] = "DR";
            dt.Rows[0]["Name"] = "DR";
            dt.Rows.Add();
            dt.Rows[1]["ID"] = "CR";
            dt.Rows[1]["Name"] = "CR";
            dt.AcceptChanges();

            ddlDRorCR.DataSource = dt;
            ddlDRorCR.DataTextField = "Name";
            ddlDRorCR.DataValueField = "ID";
            ddlDRorCR.DataBind();
        }

        protected bool ValidateComboBoxes()
        {
            bool result = true;
            string ErrorText = "";

            if (txtVillageOrStreet.Text==string.Empty)
            {
                result = false;
                ErrorText = "Please Enter Village/Street";
            }

            if (txtPO.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Enter PO";
            }

            if (txtPS.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Enter PS";
            }

            if (txtPIN.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Enter PIN";
            }

            if (txtEffectiveDate.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Enter Effective Date";
            }

            if (ddlBlock.SelectedValue == "0" || ddlBlock.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Block";
            }

            if (ddlDistrict.SelectedValue == "0" || ddlDistrict.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Dsitrict";
            }

            if (ddlState.SelectedValue == "0" || ddlState.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select State";
            }

            if (ddlDRorCR.SelectedValue == "0" || ddlDRorCR.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Dr/Cr";
            }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void Clear()
        {
            txtJLNo.Text = string.Empty;
            txtKhaitanNo.Text = string.Empty;
            txtMouza.Text = string.Empty;
            txtPlotNo.Text = string.Empty;
            txtPO.Text = string.Empty;
            txtPS.Text = string.Empty;
            txtVillageOrStreet.Text = string.Empty;
            ddlBlock.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            txtPIN.Text = string.Empty;
            txtEffectiveDate.Text = string.Empty;
            txtOpBal.Text = string.Empty;
            ddlDRorCR.SelectedIndex = 0;
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
            LoadDRorCR();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateComboBoxes())
                {
                    BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                    Entity.Common.MemberMaster memberMaster = new Entity.Common.MemberMaster();

                    memberMaster.OldMemberId = OldMemberId;
                    memberMaster.MemberCode = txtMemberCode.Text.Trim();
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
                    memberMaster.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim());
                    memberMaster.OpBal = (txtOpBal.Text.Trim().Length > 0) ? Convert.ToDecimal(txtOpBal.Text.Trim()) : 0;
                    memberMaster.DrORCr = ddlDRorCR.SelectedValue;
                    memberMaster.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    memberMaster.CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
                    memberMaster.BranchId = Convert.ToInt32(Session["BranchId"].ToString());
                    memberMaster.FinYearId = Convert.ToInt32(Session["FinYrID"].ToString());
                    memberMaster.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString());

                    objMemberMaster.MemberMaster_BlockChange_Save(memberMaster);
                    txtMemberCode.Text = memberMaster.MemberCode;
                    //CreateLedgerForMember(memberMaster.MemberId);

                    try
                    {
                        //Sending Message
                        StringBuilder sb = new StringBuilder();
                        sb.Append(@"Block Changed and New Member Saved Successfully. New Portal " + "%0a");
                        sb.Append(@"Username: " + txtMemberCode.Text + "%0a Password: " + txtMemberCode.Text + "%0a");
                        string message = (sb.ToString().Length > 158) ? sb.ToString().Substring(0, 158) : sb.ToString();
                        string API = string.Format("http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=admin@fourfusionsolutions.com:solution2012&senderID=BIGBOS&receipientno={0}&msgtxt={1}&state=1", Session["MobileNo"].ToString(), message);
                        System.Net.WebRequest request = System.Net.HttpWebRequest.Create(API);
                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        System.IO.Stream s = (System.IO.Stream)response.GetResponseStream();
                        System.IO.StreamReader readStream = new System.IO.StreamReader(s);
                        String dataString = readStream.ReadToEnd();
                        response.Close();
                        s.Close();
                        readStream.Close();
                    }
                    catch { Exception ex; }

                    Clear();
                    Message.IsSuccess = true;
                    Message.Text = "Block Changed and New Member Saved Successfully.";

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script>PageRedirect()</script>", false);
                }
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Change Block and Save New Member!!!";
            }
            Message.Show = true;
        }
    }
}