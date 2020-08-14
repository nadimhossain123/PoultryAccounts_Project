using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AccountsModule.Common
{
    public partial class MemberIDCard : System.Web.UI.Page
    {
        public int MemberId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    MemberId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadIDCardDetails();
                    Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.print()", true);
                }
            }
        }

        protected void LoadIDCardDetails()
        {
            BusinessLayer.Common.MemberMaster objMember = new BusinessLayer.Common.MemberMaster();
            Entity.Common.MemberMaster membermaster = new Entity.Common.MemberMaster();
            membermaster = objMember.GetMemberMasterById(MemberId);
            if (membermaster!=null)
            {
                ImgID.ImageUrl = "MemberPhoto/"+membermaster.ImageName;
                ltrMemberName.Text = "Name : <b><span style='color:#27B3DE;'> " + membermaster.MemberName + "</span></b>";
                ltrMemberCode.Text = "Member Code : <span style='color:#3F7FC0;'>" + membermaster.MemberCode + "</span>";
                ltrMembershipCategory.Text = "Membership Category : <span style='color:#3F7FC0;'>" + membermaster.CategoryName + "</span>";
                ltrCompanyName.Text = "Company Name : <span style='color:#3F976A;'>" + membermaster.CompanyName + "</span>";
                ltrValid.Text = "Valid upto : <span style='color:#3F976A;'>" + membermaster.CreateDate.AddYears(1).ToString("MMM yyyy") + "</span>";

                //ltrMobile.Text = "Ph.(Member) : <span style='color:#27B3DE;'>" + membermaster.MobileNo + "</span>";
                
            }
        }
    }
}
