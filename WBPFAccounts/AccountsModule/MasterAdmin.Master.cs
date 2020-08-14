using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace AccountsModule
{
    public partial class MasterAdmin : System.Web.UI.MasterPage
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrTitle.Text = GetEmployeeName() + GetCompanyType() + GetFinYr();

                if (Session["Module"].ToString().Equals("M"))
                {
                    btnModuleName.Text = "Member";
                }
                else if (Session["Module"].ToString().Equals("AC"))
                {
                    btnModuleName.Text = "Accounts";
                }
                else if (Session["Module"].ToString().Equals("S"))
                {
                    btnModuleName.Text = "SMS Module";
                }
            }
        }

        private string GetEmployeeName()
        {
            int EmployeeId = Convert.ToInt32(Session["UserId"].ToString());
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                return Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;
            }
            else { return ""; }
        }

        private string GetCompanyType()
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            Entity.Common.Company Company = new Entity.Common.Company();
            Company = ObjCompany.GetAllById(CompanyId);
            if (Company != null)
            {
                return "/" + Company.CompanyType;
            }
            else { return ""; }
        }

        private string GetFinYr()
        {
            string strValues = Session["FinYrID"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return "/" + ds.Tables[0].Rows[0]["StartYear"].ToString() + "-" + ds.Tables[0].Rows[0]["EndYear"].ToString();
            }
            else { return ""; }

        }

        protected void btnModuleName_Click(object sender, EventArgs e)
        {
            if (Session["Module"].ToString().Equals("M"))
                Session["Module"] = "AC";
            else if (Session["Module"].ToString().Equals("AC"))
                Session["Module"] = "M";

            Response.Redirect("~/Default.aspx");
        }
    }
}
