using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
{
    public partial class PopUpMemberListPrint : System.Web.UI.Page
    {
        public int StateId
        {
            get { return Convert.ToInt32(ViewState["StateId"]); }
            set { ViewState["StateId"] = value; }
        }

        public int DistrictId
        {
            get { return Convert.ToInt32(ViewState["DistrictId"]); }
            set { ViewState["DistrictId"] = value; }
        }
        
        public int BlockId
        {
            get { return Convert.ToInt32(ViewState["BlockId"]); }
            set { ViewState["BlockId"] = value; }
        }

        public int CategoryId
        {
            get { return Convert.ToInt32(ViewState["CategoryId"]); }
            set { ViewState["CategoryId"] = value; }
        }

        public string MemberName
        {
            get { return Convert.ToString(ViewState["MemberName"]); }
            set { ViewState["MemberName"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["StateId"] != null && Request.QueryString["StateId"].ToString().Length > 0)
                    StateId = Convert.ToInt32(Request.QueryString["StateId"].ToString());
                else
                    StateId = 0;

                if (Request.QueryString["DistrictId"] != null && Request.QueryString["DistrictId"].ToString().Length > 0)
                    DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                else
                    DistrictId = 0;

                if (Request.QueryString["BlockId"] != null && Request.QueryString["BlockId"].ToString().Length > 0)
                    BlockId = Convert.ToInt32(Request.QueryString["BlockId"].ToString());
                else
                    BlockId = 0;

                if (Request.QueryString["CategoryId"] != null && Request.QueryString["CategoryId"].ToString().Length > 0)
                    CategoryId = Convert.ToInt32(Request.QueryString["CategoryId"].ToString());
                else
                    CategoryId = 0;

                if (Request.QueryString["MemberName"] != null && Request.QueryString["MemberName"].ToString().Length > 0)
                    MemberName = Request.QueryString["MemberName"].ToString();
                else
                    MemberName = "";

                LoadMemberMaster();
            }
        }

        protected void LoadMemberMaster()
        {
            BusinessLayer.Common.MemberMasterPrint objMemberMaster = new BusinessLayer.Common.MemberMasterPrint();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            
                membermaster.BlockId = BlockId;
           
                membermaster.DistrictId = DistrictId;
            
                membermaster.StateId = StateId;
            
                membermaster.CategoryId = CategoryId;

                membermaster.MemberName = MemberName;

            DataTable dt = objMemberMaster.MemberMaster_GetAll_ForPrint(membermaster);
            if (dt != null)
            {
                grdMemberPrint.DataSource = dt;
                grdMemberPrint.DataBind();
            }
        }
    }
}