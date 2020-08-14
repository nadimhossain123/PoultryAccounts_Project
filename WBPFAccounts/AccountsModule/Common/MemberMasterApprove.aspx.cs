using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace AccountsModule.Common
{
    public partial class MemberMasterApprove : System.Web.UI.Page
    {
        string API_INDEX;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["UserType"] != null && Session["UserType"].ToString().Equals("Admin"))
            {
                if (!IsPostBack)
                {
                    PopulateDropDownLists();
                    LoadMemberList();
                    Message.Show = false;
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

        protected void LoadMemberList()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            
            membermaster.BlockId = Convert.ToInt32(ddlBlock.SelectedValue.Trim());
            membermaster.DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue.Trim());
            membermaster.StateId = Convert.ToInt32(ddlState.SelectedValue.Trim());
            membermaster.CategoryId = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            membermaster.MemberName = txtMemberName.Text.Trim();
            
            DataTable dt = objMember.GetAll(membermaster);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "IsApproved Is Null And IsActive Is Null";

                dgvMemberMaster.DataSource = dv;
                dgvMemberMaster.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            txtApprovedMemberName.Text = "";
            txtApprovedMemberCode.Text = "";
            Message.Show = false;
        }

        

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts();
            LoadBlock();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock();
        }

        protected void dgvMemberMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int MemberId = Convert.ToInt32(dgvMemberMaster.DataKeys[e.NewEditIndex].Values["MemberId"]);
            Response.Redirect("AddEditMemberMaster.aspx?id=" + MemberId + "&Back=MemberMasterApprove.aspx");
        }

        protected void dgvMemberMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Button)e.Row.FindControl("btnApprove")).CommandArgument = dgvMemberMaster.DataKeys[e.Row.RowIndex].Values["MemberId"].ToString();
            }
        }

        protected void dgvMemberMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Approve"))
            {
                int MemberId = Convert.ToInt32(e.CommandArgument);
                int CreatedBy = Convert.ToInt32(Session["UserId"].ToString());

                BusinessLayer.Common.MemberMaster objMemberMaster = new BusinessLayer.Common.MemberMaster();
                Entity.Common.MemberMaster memberMaster = new Entity.Common.MemberMaster();
                objMemberMaster.MemberApprove(MemberId, CreatedBy);

                memberMaster = objMemberMaster.GetMemberMasterById(MemberId);
                txtApprovedMemberName.Text = memberMaster.MemberName;
                txtApprovedMemberCode.Text = memberMaster.MemberCode;


                string MobileNo = memberMaster.MobileNo;
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Dear " + memberMaster.MemberName.ToString()+", " + "%0a");
                sb.Append(@"Your membership subscription has been approved."+ "%0a");
                sb.Append(@"Code -" + memberMaster.MemberCode.ToString() + "%0a");
                sb.Append(@"Date -" + memberMaster.MembershipDate.ToString("dd/MM/yyyy") + "%0a");
                sb.Append(@"Category -" + memberMaster.CategoryName.ToString() + "%0a");
                sb.Append(@"Thank You" + "%0a");
                sb.Append(@"West Bengal Poultry Federation");
                string message = sb.ToString();// (sb.ToString().Length > 159) ? sb.ToString().Substring(0, 159) : sb.ToString();

                TriggerSMS(MobileNo, message);
                LoadMemberList();
                Message.IsSuccess = true;
                Message.Text = "Approved";
                Message.Show = true;
            }
        }

        private string GetHTTPAPI(string mobiles,string message)
        {
            string API = "1";

            if (API_INDEX == "1")
                API = string.Format("http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=admin@fourfusionsolutions.com:solution2012&senderID=WBPOLT&receipientno={0}&msgtxt={1}&state=1", mobiles, message);
            else if (API_INDEX == "2")
                API = string.Format("http://www.krishsms.com/PostSms.aspx?userid=WBPOLT&pass=WBPOLT12345&phone={0}&msg={1}&title=WBPOLT", mobiles, message);
            else if (API_INDEX == "3")
                API = string.Format("http://login.tbulksms.com/API/WebSMS/Http/v1.0a/index.php?userid=124294&password=al4145IS&sender=WBPOLT&to={0}&message={1}&reqid=1&format=text&route_id=11&unique=0&msgtype=Unicode", mobiles, message);
            else if (API_INDEX == "4")
                API = string.Format("http://login.hivemsg.com/api/send_transactional_sms.php?username=u1348&msg_token=3PEV69&sender_id=WBPOLT&message={0}&mobile={1}", message, mobiles);

            return API;
        }

        protected void TriggerSMS(string MobileNo, string Message)
        {
            string ROUTE_1 = System.Configuration.ConfigurationSettings.AppSettings["ROUTE_1"];
            API_INDEX = ROUTE_1;
            string dataString;
            string strUrl = GetHTTPAPI(MobileNo, Message);
            try
            {
                WebRequest request1 = HttpWebRequest.Create(strUrl);
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                Stream s1 = (Stream)response1.GetResponseStream();
                StreamReader readStream1 = new StreamReader(s1);
                dataString = readStream1.ReadToEnd();
                response1.Close();
                s1.Close();
                readStream1.Close();
            }
            catch (Exception e) { }
        }
    }
}