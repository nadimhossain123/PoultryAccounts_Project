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

namespace CollegeERP.Accounts
{
    public partial class CostCentre : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        public string strFilter;
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                ResetControls();
                string strValues = "";
                strValues = Session["CompanyId"].ToString();
                strValues += chr.ToString() + "";
                genObj.BindGridViewSP(gvCostCenter, "spSelect_MstCostCenter", strValues);

            }
        }

        protected void ResetControls()
        {
            ViewState["ID"] = 0;
            txtCostCenter.Text = "";
            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void PopulatePage(object sender, EventArgs e, int intID)
        {
            char chr = Convert.ToChar(130);
            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + intID.ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstCostCenter", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCostCenter.Text = ds.Tables[0].Rows[0]["CostCenterName"].ToString();
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

            Message.Show = false;
        }

        protected void gvCostCenter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("imgbtnEdit");
                ib.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void gvCostCenter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "imgbtnEdit")
            {
                int rID = Convert.ToInt32(e.CommandArgument);
                HiddenField ib = (HiddenField)gvCostCenter.Rows[rID].FindControl("hdID");
                int intID = Convert.ToInt32(ib.Value);
                ViewState["ID"] = intID;
                PopulatePage(sender, e, intID);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            string rtMsg = "";
            char chr = Convert.ToChar(130);
            string strSPName = "";
            string strValues = "";

            int intID = Convert.ToInt32(ViewState["ID"].ToString());
            if (intID == 0)
                strSPName = "spInsert_MstCostCenter";
            else
            {
                strValues = intID.ToString();
                strSPName = "spUpdate_MstCostCenter";
            }
            // Value for Employee Table
            if (strValues == "")
                strValues = Session["CompanyId"].ToString();
            else
                strValues += chr.ToString() + Session["CompanyId"].ToString();

            strValues += chr.ToString() + txtCostCenter.Text.Trim().ToString();
            strValues += chr.ToString() + Session["BranchID"];
            strValues += chr.ToString() + Session["FinYrID"];
            strValues += chr.ToString() + Session["UserId"];
            rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);
            if (rtMsg == "True")
            {
                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";
            }
            else if (rtMsg == "Duplicate")
            {
                Message.IsSuccess = false;
                Message.Text = "This Cost Center name already exist!";
            }

            ResetControls();
            Message.Show = true;
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + "";
            genObj.BindGridViewSP(gvCostCenter, "spSelect_MstCostCenter", strValues);

        }
    }
}
