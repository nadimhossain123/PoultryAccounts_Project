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
using System.IO;

namespace CollegeERP.Accounts
{
    public partial class BankReconsiliation : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        DataSet ds;
        string strParams;
        char chr = Convert.ToChar(130);
        public string strFilter;
        ListItem li = new ListItem("Select", "0");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                PopulateAllDropDowns();
                ResetControls();
                PopulateHeaderGrid();
                PopulateOutstanding();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        private void PopulateAllDropDowns()
        {
            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('BNK')";

            if (dv != null)
            {
                ddlBRLedgName.DataSource = dv;
                ddlBRLedgName.DataBind();

                ddlBRLedgNameView.DataSource = dv;
                ddlBRLedgNameView.DataBind();
            }

            ddlBRLedgName.Items.Insert(0, li);
            ddlBRLedgNameView.Items.Insert(0, li);

        }

        private void ResetControls()
        {
            ddlBRLedgName.SelectedValue = "0";
            txtClearDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            Message.Show = false;
            Message1.Show = false;
            btnExportExcel.Visible = false;
        }


        private void PopulateHeaderGrid()
        {
            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindGridViewSP(gvBRView, "spSelect_TrnBankReconsiliation", strParams, PrepareSearchFilter());
            if (gvBRView.DataSource != null)
            {
                if (gvBRView.Rows.Count > 0)
                    btnExportExcel.Visible = true;
            }
        }
        
        private void PopulateOutstanding()
        {
            strParams = Session["CompanyID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += ddlBRLedgName.SelectedValue.Trim();
            ds = gf.ExecuteSelectSP("spSelect_TrnBankReconsiliationOutstanding", strParams);
           
            gvBnkReconsilition.DataSource = ds.Tables[0];
            gvBnkReconsilition.DataBind();
        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            strXMLString = "<NewDataSet>";
            for (int i = 0; i < gvBnkReconsilition.Rows.Count; i++)
            {
                CheckBox Chk = (CheckBox)gvBnkReconsilition.Rows[i].FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    strXMLString += "<TrnBankReconsiliationDetail ";
                    strXMLString += " BRHeaderID = \"" + gvBnkReconsilition.DataKeys[i].Value.ToString() + "\"";
                    strXMLString += " />";
                }
            }
            strXMLString += "</NewDataSet>";

            return strXMLString;
        }

        private string PrepareXMLStringForClear()
        {
            string strXMLString = "";
            strXMLString = "<NewDataSet>";
            for (int i = 0; i < gvBRView.Rows.Count; i++)
            { 
                CheckBox Chk = (CheckBox)gvBRView.Rows[i].FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    strXMLString += "<TrnBankReconsiliationDetail ";
                    strXMLString += " BRHeaderID = \"" + gvBRView.DataKeys[i].Value.ToString() + "\"";
                    strXMLString += " />";
                }
            }
            strXMLString += "</NewDataSet>";

            return strXMLString;
        }

        private string PrepareSearchFilter()
        {
            string strFilterString = "";

            if (ddlBRLedgNameView.SelectedValue != "0")
            {
                strFilterString = "BankLedgerID_FK = '" + ddlBRLedgNameView.SelectedValue.ToString() + "'";
            }
            
            if (txtChequeNo.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "ChequeNo = '" + txtChequeNo.Text.Trim() + "'";
                else
                    strFilterString += " AND ChequeNo = '" + txtChequeNo.Text.Trim() + "'";
            }

            if (txtClearFromDate.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "ClearDate >= '" + Convert.ToDateTime(txtClearFromDate.Text.Trim()).ToString("dd-MMM-yyyy") + "'";
                else
                    strFilterString += " AND ClearDate >= '" + Convert.ToDateTime(txtClearFromDate.Text.Trim()).ToString("dd-MMM-yyyy") + "'";
            }
            if (txtClearToDate.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "ClearDate <= '" + Convert.ToDateTime(txtClearToDate.Text.Trim()).ToString("dd-MMM-yyyy") + "'";
                else
                    strFilterString += " AND ClearDate <= '" + Convert.ToDateTime(txtClearToDate.Text.Trim()).ToString("dd-MMM-yyyy") + "'";
            }

            return strFilterString;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            string rtMsg = "";
            string strValues = "";
            string strSPName = "spUpdate_TrnBankReconsiliationClear";

            if (IsValidClearDate())
            {
                strValues += PrepareXMLStringForClear();

                rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);

                if (rtMsg == "True")
                {
                    Message1.IsSuccess = true;
                    Message1.Text = "Your request has been processed successfully!";
                    PopulateOutstanding();
                    PopulateHeaderGrid();
                }
            }
            else
            {
                Message1.IsSuccess = false;
                Message1.Text = "Error";
            }
            Message1.Show = true;
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtMsg = "";
            string strValues = "";
            string strSPName = "spUpdate_TrnBankReconsiliation";

            if (IsValidClearDate())
            {
                strValues = Session["UserId"].ToString() + chr.ToString();
                strValues += txtClearDate.Text.Trim() + chr.ToString();
                strValues += PrepareXMLString();

                rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);

                if (rtMsg == "True")
                {
                    Message.IsSuccess = true;
                    Message.Text = "Your request has been processed successfully!";
                    PopulateOutstanding();
                    PopulateHeaderGrid();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Clear Date Should Not be Less Than Voucher Date. Please Select a Valid Clear Date";
            }
            Message.Show = true;
            Message1.Show = false;
        }

        private bool IsValidClearDate()
        {
            bool IsValid = true;
            DateTime DTClear = Convert.ToDateTime(txtClearDate.Text.Trim());
            DateTime DTVoucher;

            for (int i = 0; i < gvBnkReconsilition.Rows.Count; i++)
            {
                CheckBox Chk = (CheckBox)gvBnkReconsilition.Rows[i].FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    DTVoucher = Convert.ToDateTime(gvBnkReconsilition.Rows[i].Cells[1].Text);
                    if (DTClear < DTVoucher)
                    {
                        IsValid = false;
                        break;
                    }
                }
            }
            return IsValid;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid();
        }

        protected void gvBRView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBRView.PageIndex = e.NewPageIndex;
            PopulateHeaderGrid();
        }

        protected void ddlBRLedgName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateOutstanding();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            string[] _header = new string[2];
            _header[0] = "BRS Statement";
            _header[1] = "";

            string[] _footer = new string[0];
            
            string file = "BRS_REPORT";

            PrepareGridViewForExport(gvBRView);
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=BRSReport.xls");
            Response.ContentType = "application/excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            gvBRView.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();

            //BusinessLayer.Common.Excel.SaveExcel(_header, gvBRView, _footer, file);
        }

        private void PrepareGridViewForExport(Control gv)
        {
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }





    }
}
