using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusinessLayer.Accounts;

namespace AccountsModule
{
    public partial class Login : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlPopup.Style.Add("display", "none");

            if (!IsPostBack)
            {
                Session["UserId"] = null;
                Session["UserName"] = null;
                Session["UserType"] = null;
                Session["CompanyId"] = null;
                Session["BranchID"] = null;
                Session["FinYrID"] = null;
                Session["DataFlow"] = null;
                Session["Module"] = null;
                Session["StateId"] = null;
                Session["DistrictId"] = null;
                Session["BlockId"] = null;

                txtUserName.Focus();
                rbtnLoginAs.SelectedValue = "Admin";

                //Added By BASIR --> For Renewal Subscribtions End Reminder
                BusinessLayer.SendNotificationSMS ObjSms = new BusinessLayer.SendNotificationSMS();
                ObjSms.SendSMS();
            }
        }

        protected void btnLogIn_Click(object sender, ImageClickEventArgs e)
        {
            if (rbtnLoginAs.SelectedValue == "Admin")
            {
                AdminLogin();
            }
            else if (rbtnLoginAs.SelectedValue == "Member")
            {
                MemberLogin();
            }
            else if (rbtnLoginAs.SelectedValue == "Agent")
            {
                AgentLogin();
            }
        }

        protected void AdminLogin()
        {
            string u = txtUserName.Text.Trim();
            string p = txtPassword.Text.Trim();
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataTable dt = new DataTable();
            try
            {
                dt = ObjEmployee.AuthenticateUser(u, "Admin");

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Password"].ToString() == p)
                    {
                        int UserId = int.Parse(dt.Rows[0]["EmployeeId"].ToString());
                        int CompanyId = int.Parse(dt.Rows[0]["CompanyId"].ToString());
                        FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                                   1,
                                                                   UserId.ToString(),
                                                                   DateTime.Now,
                                                                   DateTime.Now.AddMinutes(300),
                                                                   false,
                                                                   dt.Rows[0]["roles"].ToString(),
                                                                   FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(Authticket);
                        HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        if (Authticket.IsPersistent)
                            Authcookie.Expires = Authticket.Expiration;
                        Response.Cookies.Add(Authcookie);
                        Session["UserId"] = UserId;
                        Session["UserType"] = "Admin";
                        Session["UserName"] = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["MiddleName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                        Session["CompanyId"] = CompanyId;
                        //Session["BranchID"] = GetBranchId(UserId);
                        Session["BranchID"] = 1;
                        Session["DataFlow"] = 1;
                        Session["FinYrID"] = GetCurrentFinYrID();
                        Session["EmpRole"] = dt.Rows[0]["roles"].ToString();
                        Session["UserMode"] = "Admin";
                        Session.Timeout = 300;

                        string strValues = Session["FinYrID"].ToString();
                        DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                            Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
                        }
                        if (txtUserName.Text == "001")
                        {
                            Session["Module"] = "S";
                            Response.Redirect("default.aspx");
                        }

                        //////  Checking for Development Fee Generate - added by _BASIR BAIDYA/////////
                        int[] a = { 1, 2, 3, 4, 5 };
                        DateTime dateTime = DateTime.Now.AddHours(12).AddMinutes(30);
                        int Month = dateTime.Month;
                        if (a.Contains(dateTime.Day))
                        {
                            //GoDevelopmentFeeGenerate();
                        }

                        ShowModulePopup(dt.Rows[0]["AccessModule"].ToString());
                    }
                }
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
            catch(Exception ex)
            { }
        }

        protected void MemberLogin()
        {
            string u = txtUserName.Text.Trim();
            string p = txtPassword.Text.Trim();
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataTable dt = new DataTable();
            try
            {
                dt = ObjEmployee.AuthenticateUser(u, "Member");

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Password"].ToString() == p)
                    {
                        int UserId = int.Parse(dt.Rows[0]["MemberId"].ToString());
                        int CompanyId = int.Parse(dt.Rows[0]["CompanyId"].ToString());
                        FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                                   1,
                                                                   UserId.ToString(),
                                                                   DateTime.Now,
                                                                   DateTime.Now.AddMinutes(180),
                                                                   false,
                                                                   dt.Rows[0]["roles"].ToString(),
                                                                   FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(Authticket);
                        HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        if (Authticket.IsPersistent)
                            Authcookie.Expires = Authticket.Expiration;
                        Response.Cookies.Add(Authcookie);
                        Session["UserId"] = UserId;
                        Session["UserType"] = "Member";
                        Session["UserName"] = dt.Rows[0]["MemberName"].ToString();
                        Session["CompanyId"] = CompanyId;
                        //Session["BranchID"] = GetBranchId(UserId);
                        Session["BranchID"] = 1;
                        Session["DataFlow"] = 1;
                        Session["FinYrID"] = GetCurrentFinYrID();
                        Session["EmpRole"] = dt.Rows[0]["roles"].ToString();
                        Session["UserMode"] = "Member";
                        Session.Timeout = 180;

                        string strValues = Session["FinYrID"].ToString();
                        DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                            Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
                        }
                        Response.Redirect("MemberDefault.aspx");
                    }
                }
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
            catch
            { }
        }

        private void AgentLogin()
        {
            string u = txtUserName.Text.Trim();
            string p = txtPassword.Text.Trim();
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataTable dt = new DataTable();

            try
            {
                dt = ObjEmployee.AuthenticateUser(u, "Agent");

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Password"].ToString() == p)
                    {
                        int UserId = int.Parse(dt.Rows[0]["AgentId"].ToString());
                        int CompanyId = int.Parse(dt.Rows[0]["CompanyId"].ToString());
                        FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                                   1,
                                                                   UserId.ToString(),
                                                                   DateTime.Now,
                                                                   DateTime.Now.AddMinutes(180),
                                                                   false,
                                                                   dt.Rows[0]["roles"].ToString(),
                                                                   FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(Authticket);
                        HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        if (Authticket.IsPersistent)
                            Authcookie.Expires = Authticket.Expiration;
                        Response.Cookies.Add(Authcookie);
                        Session["UserId"] = UserId;
                        Session["UserType"] = "Agent";
                        Session["UserName"] = dt.Rows[0]["AgentName"].ToString();
                        Session["CompanyId"] = CompanyId;
                        //Session["BranchID"] = GetBranchId(UserId);
                        Session["BranchID"] = 1;
                        Session["DataFlow"] = 1;
                        Session["FinYrID"] = GetCurrentFinYrID();
                        Session["EmpRole"] = dt.Rows[0]["roles"].ToString();
                        Session["UserMode"] = "Agent";
                        Session["StateId"] = dt.Rows[0]["StateId"].ToString();
                        Session["DistrictId"] = dt.Rows[0]["DistrictId"].ToString();
                        Session["BlockId"] = dt.Rows[0]["BlockId"].ToString();
                        Session["AgentLedgerId"] = dt.Rows[0]["LedgerId"].ToString();

                        Session.Timeout = 180;

                        string strValues = Session["FinYrID"].ToString();
                        DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                            Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
                        }
                        Response.Redirect("AgentDefault.aspx");
                    }
                }
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
            catch
            {
            }
        }

        protected int GetBranchId(int EmpId)
        {
            return 1;
        }

        protected int GetCurrentFinYrID()
        {
            int CurrentFnYr = 0;
            if (DateTime.Now.Month < 4)
            {
                CurrentFnYr = DateTime.Now.Year - 1;
            }
            else
            {
                CurrentFnYr = DateTime.Now.Year;
            }
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstFinancialYear", ""));
            dv.RowFilter = "StartYear=" + CurrentFnYr;
            return int.Parse(dv.ToTable().Rows[0]["FinYearID"].ToString());
        }

        private string GetModuleName(string ModuleShortName)
        {
            string ModuleName = string.Empty;

            switch (ModuleShortName)
            {
                case "M": ModuleName = "Member Module"; break;
                case "AC": ModuleName = "Accounts Module"; break;
            }
            return ModuleName;
        }

        private void ShowModulePopup(string AccessModule)
        {
            radioListModule.Items.Clear();

            for (int i = 0; i < AccessModule.Split(',').Length; i++)
            {
                radioListModule.Items.Add(new ListItem(GetModuleName(AccessModule.Split(',')[i]), AccessModule.Split(',')[i]));
            }
            radioListModule.SelectedIndex = 0;

            pnlPopup.Style.Add("display", "block");
            ModalPopUp.Show();
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Session["Module"] = radioListModule.SelectedValue;
            Response.Redirect("Default.aspx");
        }

        private void GoDevelopmentFeeGenerate()
        {
            DateTime dateTime = DateTime.Now.AddHours(12).AddMinutes(30);
            Entity.Common.MemberBill bill = new Entity.Common.MemberBill();
            bill.Month = dateTime.Month;
            bill.Year = dateTime.Year;
            bill.MemberId = 0;
            bill.CreatedBy = Convert.ToInt32(Session["UserId"].ToString()); //Int32.Parse(Context.User.Identity.Name);
            BusinessLayer.Common.MemberBill objBill = new BusinessLayer.Common.MemberBill();
            bool IsGenerate = objBill.CheckingDevelopmentFeeGeneration(bill);
            if (IsGenerate == false)
            {
                objBill.DevelopmentFeeGenerate(bill);
                BusinessLayer.Common.FeeNotification notification = new BusinessLayer.Common.FeeNotification();
                notification.MailToMember();
            }
        }
    }
}
