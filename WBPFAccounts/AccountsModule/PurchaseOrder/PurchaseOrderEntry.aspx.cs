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

namespace CollegeERP.PurchaseOrder
{
    public partial class PurchaseOrderEntry : System.Web.UI.Page
    {
        public int PurchaseBillId
        {
            get { return Convert.ToInt32(ViewState["PurchaseBillId"]); }
            set { ViewState["PurchaseBillId"] = value; }
        }
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadSupplier();
                LoadPurchaseLedger();
                ClearControls();
                LoadPOList();
                getPurchaseBill();
            }
        }

        protected void LoadSupplier()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('SUP')";

            if (dv != null)
            {
                ddlSupplier.DataSource = dv;
                ddlSupplier.DataBind();
            }
            ddlSupplier.Items.Insert(0, li);
        }

        protected void LoadPurchaseLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('PUR')";

            if (dv != null)
            {
                ddlPurchaseLedger.DataSource = dv;
                ddlPurchaseLedger.DataBind();
            }
            ddlPurchaseLedger.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            PurchaseBillId = 0;
            Message.Show = false;
            btnSave.Text = "Save";

            txtBillNo.Text = "";
            txtBillDate.Text = "";
            txtBillAmt.Text = "";
            ddlSupplier.SelectedIndex = 0;
            ddlPurchaseLedger.SelectedIndex = 0;

            txtBillDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }

        protected void LoadPOList()
        {
            string BillNo = txtBillNoSearch.Text.Trim();
            string FromDate = txtFromDate.Text.Trim();
            string ToDate = txtToDate.Text.Trim();

            BusinessLayer.PurchaseOrder.PurchaseOrderEntry ObjPO = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
            DataView dv = new DataView(ObjPO.GetAll(BillNo, FromDate, ToDate));

            if (dv != null)
            {
                dv.RowFilter = "CompanyID_FK=" + Convert.ToInt32(Session["CompanyId"].ToString()) + " and FinYearID_FK=" + Convert.ToInt32(Session["FinYrID"].ToString());
                dgvPO.DataSource = dv.ToTable();
                dgvPO.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPOList();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void LoadPODetails()
        {
            BusinessLayer.PurchaseOrder.PurchaseOrderEntry ObjPO = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
            Entity.PurchaseOrder.PurchaseOrderEntry PO = new Entity.PurchaseOrder.PurchaseOrderEntry();
            PO = ObjPO.GetAllById(PurchaseBillId);
            if (PO != null)
            {
                txtBillNo.Text = PO.BillNo;
                txtBillDate.Text = PO.BillDate.ToString("dd MMM yyyy");
                txtBillAmt.Text = PO.BillAmount.ToString("#0.00");
                ddlSupplier.SelectedValue = PO.SupplierLedgerID_FK.ToString();
                ddlPurchaseLedger.SelectedValue = PO.PurchaseLedgerID_FK.ToString();
                txtNarration.Text = PO.Narration.ToString();
               
                btnSave.Text = "Update";
                Message.Show = false;
            }
        }

        protected void dgvPO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PurchaseBillId = int.Parse(dgvPO.DataKeys[e.NewEditIndex].Value.ToString());
            LoadPODetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strValues = txtBillDate.Text;
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            {
                if (Validate())
                {
                    BusinessLayer.PurchaseOrder.PurchaseOrderEntry ObjPO = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
                    Entity.PurchaseOrder.PurchaseOrderEntry PO = new Entity.PurchaseOrder.PurchaseOrderEntry();
                    PO.PurchaseBillId = PurchaseBillId;
                    PO.BillNo = txtBillNo.Text.Trim();

                    PO.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim());
                    
                    PO.BillAmount = decimal.Parse(txtBillAmt.Text.Trim());
                    PO.SupplierLedgerID_FK = int.Parse(ddlSupplier.SelectedValue);
                    PO.PurchaseLedgerID_FK = int.Parse(ddlPurchaseLedger.SelectedValue.Trim());
                    PO.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString());
                    PO.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString());
                    PO.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString());
                    PO.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString());
                    PO.Narration = txtNarration.Text.Trim();
                    PO.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                    PO.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);

                    int RowsAffected = ObjPO.Save(PO);
                    if (RowsAffected != -1)
                    {
                        Message.IsSuccess = true;
                        Message.Text = "Data Saved Successfully";
                        ClearControls();
                        LoadPOList();
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can Not Save. Duplicate Bill No is Not Allowed. Please Check";
                    }
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
            }
            Message.Show = true;
        }

        protected bool Validate()
        {
            bool result = true;
            string error = "";
            string strValues = txtBillDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                error = "Sorry! Bill Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtBillDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                error = "Sorry! Future Bill Date is Not Allowed.";
            }

            if (result == false)
            {
                Message.IsSuccess = false;
                Message.Text = error;
            }
            return result;

        }

        private void getPurchaseBill()
        {
            BusinessLayer.PurchaseOrder.PurchaseOrderEntry objPOE = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
            DataTable dt;
            dt = objPOE.GetPurchaseBill();
            if (dt != null)
            {
                gvPurchaseBill.DataSource = dt;
                gvPurchaseBill.DataBind();
            }
        }

        protected void gvPurchaseBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchaseBill.PageIndex = e.NewPageIndex;
            getPurchaseBill();
        }
        protected void gvPurchaseBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            //if (e.CommandName.ToString() == "Delete")
            //{
            //    BusinessLayer.PurchaseOrder.PurchaseOrderEntry objPOE = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
            //    int PurchaseBillId = int.Parse(e.CommandArgument.ToString());
            //    objPOE.Delete(PurchaseBillId);
                
            //}
            //getPurchaseBill();
        }
        protected void gvPurchaseBill_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            //*****************
            BusinessLayer.PurchaseOrder.PurchaseOrderEntry objPOE = new BusinessLayer.PurchaseOrder.PurchaseOrderEntry();
            int PurchaseBillId = int.Parse(gvPurchaseBill.DataKeys[e.RowIndex].Value.ToString());
            objPOE.Delete(PurchaseBillId);
            getPurchaseBill();
        }
    }
}
