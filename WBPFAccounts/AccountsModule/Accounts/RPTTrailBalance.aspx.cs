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
    public partial class RPTTrailBalance : System.Web.UI.Page
    {
        char chr = Convert.ToChar(130);
        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strValues = "";
        DataSet ds;
        ListItem li = new ListItem("Select", "0");

        protected void Page_PreInit(object sender, EventArgs e)
        {
            
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                txtFromDate.Text = Session["SesFromDate"].ToString();
                txtToDate.Text = Session["SesToDate"].ToString();
                Message.Show = false;
                PopulateGrid();
                calculateTotal();
                BindGroupName();
            }
        }

        private void BindGroupName()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";
            // A/c group population
            strValues = "" + chr.ToString() + "Main Group";
            gf.BindDropDownColumnsBySP(ddlGroupName, "spSelect_MstAccountsGroup", strValues);
            ddlGroupName.Items.Insert(0, li);

            //BusinessLayer.Accounts.RPTTrialBalance ObjAccounts = new BusinessLayer.Accounts.RPTTrialBalance();
            //int GroupTypeLevel = 2;
            //DataTable DT = ObjAccounts.GroupTypeGetAll(GroupTypeLevel);
            //if (DT != null)
            //{
            //    ddlGroupName.DataSource = DT;
            //    ddlGroupName.DataValueField = "GroupTypeID";
            //    ddlGroupName.DataTextField = "GroupType";
            //    ddlGroupName.DataBind();
            //}
            //ddlGroupName.Items.Insert(0, li);
        }

        private void PopulateGrid()
        {
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim() + chr.ToString();
            if (ddlGroupName.SelectedValue == "0")
            {
                strValues += null + chr.ToString();
            }
            else
            {
                strValues += ddlGroupName.SelectedValue + chr.ToString();
            }
            if (ChkWithZeroBal.Checked)
                strValues += "True";
            else
                strValues += "False";

            DataSet ds = gf.ExecuteSelectSP("spRPTTrialBalance", strValues);
            if (ds.Tables[0] != null)
            {
                gvTrialBalnce.DataSource = ds.Tables[0];
                gvTrialBalnce.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (DateRangeValidation())
            {
                PopulateGrid();
                calculateTotal();
                Message.Show = false;
            }
            else
                Message.Show = true;
        }

        protected bool DateRangeValidation()
        {
            bool result = true;
            DateTime minValue = Convert.ToDateTime(Session["SesFromDate"].ToString());
            DateTime maxValue = Convert.ToDateTime(Session["SesToDate"].ToString());

            if (Convert.ToDateTime(txtFromDate.Text) < minValue || Convert.ToDateTime(txtFromDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select From Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            else if (Convert.ToDateTime(txtToDate.Text) < minValue || Convert.ToDateTime(txtToDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select To Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            return result;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>West Bengal Poultry Federation</b>";
            _header[1] = "Trial Balance From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];
            
            string file = "TRIAL_BALANCE_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvTrialBalnce, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Trial Balance Report";
            string[] _header = new string[3];
            _header[0] = "Trial Balance From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[1] = "Printed on " + DateTime.Now.AddHours(12).AddMinutes(30).ToString();
            _header[2] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvTrialBalnce, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void gvTrialBalnce_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //decimal closingCR = Convert.ToDecimal(0.00);
            //Session["closingCR"] = closingCR;

            //decimal closingDR = Convert.ToDecimal(0.00);
            //Session["closingDR"] = closingDR;


            //decimal OppenningCR = Convert.ToDecimal(0.00);
            //Session["OppenningCR"] = OppenningCR;

            //decimal OppenningDR = Convert.ToDecimal(0.00);
            //Session["OppenningDR"] = OppenningDR;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            //}

          

            //for (int i = 0; i < gvTrialBalnce.Rows.Count-1; i++)
            //{
                
              

            //    string opd = gvTrialBalnce.Rows[i].Cells[4].Text;
            //    string opc = gvTrialBalnce.Rows[i].Cells[5].Text;
            //    decimal opbal = Convert.ToDecimal(Convert.ToDecimal(opd) - Convert.ToDecimal(opc));

            //    string cld = gvTrialBalnce.Rows[i].Cells[8].Text;
            //    string clc = gvTrialBalnce.Rows[i].Cells[9].Text;
            //    decimal clbal = Convert.ToDecimal(Convert.ToDecimal(cld) - Convert.ToDecimal(clc));
            //    string GroupType = gvTrialBalnce.Rows[i].Cells[1].Text;
            //    if ((GroupType == "Assets") || (GroupType == "Liabilities"))
            //    {

            //        if (clbal == 0)
            //        {
            //             gvTrialBalnce.Rows[i].Cells[8].Text="0.00";
            //             gvTrialBalnce.Rows[i].Cells[9].Text="0.00";
            //        }
            //        if (clbal > 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[8].Text = Convert.ToString(clbal);
            //            gvTrialBalnce.Rows[i].Cells[9].Text = "0.00";
            //        }
            //        if (clbal < 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[8].Text = "0.00";
            //            gvTrialBalnce.Rows[i].Cells[9].Text = Convert.ToString(clbal).Substring(1,((Convert.ToString(clbal).Length-1)));
            //        }
            //        closingCR = Convert.ToDecimal(Session["closingCR"]);
                    
            //        closingCR = closingCR + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[9].Text);
            //        Session["closingCR"] = closingCR;

            //        closingDR = Convert.ToDecimal(Session["closingDR"]);
            //       closingDR = closingDR + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[8].Text);
            //       Session["closingDR"] = closingDR;

            //        if (opbal == 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[4].Text = "0.00";
            //            gvTrialBalnce.Rows[i].Cells[5].Text = "0.00";
                    
            //        }
            //        if (opbal > 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[4].Text = Convert.ToString(opbal);
            //            gvTrialBalnce.Rows[i].Cells[5].Text = "0.00";
            //        }
            //        if (opbal < 0)
            //        {

            //            gvTrialBalnce.Rows[i].Cells[4].Text = "0.00";
            //            gvTrialBalnce.Rows[i].Cells[5].Text = Convert.ToString(opbal).Substring(1,((Convert.ToString(opbal).Length-1)));
                        
            //        }
            //    }
            //    if ((GroupType == "Expenditure") || (GroupType == "Income"))
            //    {
            //        if (clbal == 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[8].Text = "0.00";
            //            gvTrialBalnce.Rows[i].Cells[9].Text = "0.00";
            //        }
            //        if (clbal > 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[8].Text = Convert.ToString(clbal);
            //            gvTrialBalnce.Rows[i].Cells[9].Text = "0.00";
            //        }
            //        if (clbal < 0)
            //        {
            //            gvTrialBalnce.Rows[i].Cells[8].Text = "0.00";
            //            gvTrialBalnce.Rows[i].Cells[9].Text = Convert.ToString(clbal).Substring(1,((Convert.ToString(clbal).Length-1)));
                        
            //        }

            //       decimal closingCR2 = Convert.ToDecimal(Session["closingCR"]);
            //       closingCR2 = closingCR2 + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[9].Text);
            //       Session["closingCR"] = closingCR2;

            //       decimal closingDR2 = Convert.ToDecimal(Session["closingDR"]);
            //       closingDR2 = closingDR2 + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[8].Text);
            //       Session["closingDR"] = closingDR2;
            //    }

            //    OppenningCR = Convert.ToDecimal(Session["OppenningCR"]);
            //   OppenningCR = OppenningCR + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[5].Text);
            //   Session["OppenningCR"] = OppenningCR;

            //    OppenningDR = Convert.ToDecimal(Session["OppenningDR"]);
            //   OppenningDR = OppenningDR + Convert.ToDecimal(gvTrialBalnce.Rows[i].Cells[4].Text);
            //   Session["OppenningDR"] = OppenningDR;

               
            //}


            

        }

        protected void calculateTotal()
        {


            //gvTrialBalnce.Rows[(gvTrialBalnce.Rows.Count - 1)].Cells[9].Text =Convert.ToString( Convert.ToDecimal(Session["closingCR"]));
            //gvTrialBalnce.Rows[(gvTrialBalnce.Rows.Count - 1)].Cells[8].Text = Convert.ToString(Convert.ToDecimal(Session["closingDR"]));

            //gvTrialBalnce.Rows[(gvTrialBalnce.Rows.Count - 1)].Cells[4].Text = Convert.ToString(Convert.ToDecimal(Session["OppenningDR"]));
            //gvTrialBalnce.Rows[(gvTrialBalnce.Rows.Count - 1)].Cells[5].Text = Convert.ToString(Convert.ToDecimal(Session["OppenningCR"]));
           
        
        }

    }
}
