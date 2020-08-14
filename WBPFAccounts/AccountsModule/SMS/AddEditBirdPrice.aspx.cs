using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.SMS
{
    public partial class AddEditBirdPrice : System.Web.UI.Page
    {
        public string CurrentProvider
        {
            get { return ViewState["CurrentProvider"].ToString(); }
            set { ViewState["CurrentProvider"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                //if (!HttpContext.Current.User.IsInRole(Entity.Permission.STAFF_SALARY))
                //{
                //    Response.Redirect("../Unauthorized.aspx");
                //}
                //((Label)Master.FindControl("lblTitle")).Text = "Manage Employee Salary";
                ddlDays.SelectedValue = DateTime.Now.Day.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                LoadBirdPrice();

            }

        }

        protected void LoadBirdPrice()
        {
            int Day = int.Parse(ddlDays.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            //string SDate = (Month + "/" + Day + "/" + Year).ToString();
            string SDate = (Year + "-" + Month + "-" + Day).ToString();
            DateTime DDate = Convert.ToDateTime(SDate);
            BusinessLayer.SMS.BirdPrice ObjBirdPrice = new BusinessLayer.SMS.BirdPrice();
            DataTable dt = ObjBirdPrice.GetAll(DDate);
            if (dt != null)
            {
                dgvBirdPrice.DataSource = dt;
                dgvBirdPrice.DataBind();
            }
            else
            {

            }

            BusinessLayer.SMS.EggPrice ObjEggPrice = new BusinessLayer.SMS.EggPrice();
            Entity.SMS.EggPrice EggPrice = new Entity.SMS.EggPrice();
            EggPrice = ObjEggPrice.GetAllById(DDate);

            if (EggPrice != null)
            {

                txtNECCEggRate.Text = EggPrice.NECCPrice.ToString();
                txtbelowWt.Text = EggPrice.belowWt.ToString();
                txtoverWt.Text = EggPrice.overWt.ToString();
                txtbelowAddRate.Text = EggPrice.belowAddRate.ToString();
                txtoverAddRate.Text = EggPrice.overAddRate.ToString();
            }



        }

        protected void SaveBirdRate()
        {
            int Day = int.Parse(ddlDays.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            //string SDate = Month + "/" + Day + "/" + Year;
            string SDate = Year + "-" + Month + "-" + Day;

            DateTime DDate = Convert.ToDateTime(SDate);

            DataTable DtBirdPrice = new DataTable();
            DtBirdPrice.Columns.Add("DistrictId", typeof(int));
            DtBirdPrice.Columns.Add("FarmRate", typeof(int));
            DtBirdPrice.Columns.Add("RetailerRate", typeof(int));
            DtBirdPrice.Columns.Add("DressedRate", typeof(int));
            DtBirdPrice.Columns.Add("BroilerRate", typeof(int));
            DtBirdPrice.Columns.Add("Date", typeof(DateTime));

            DataRow dr;
            TextBox Txt;
            foreach (GridViewRow DGR in dgvBirdPrice.Rows)
            {
                dr = DtBirdPrice.NewRow();
                dr["DistrictId"] = int.Parse(dgvBirdPrice.DataKeys[DGR.RowIndex].Value.ToString());

                Txt = (TextBox)DGR.FindControl("txtFarmRate");
                dr["FarmRate"] = (Txt.Text.Trim().Length == 0) ? 0 : int.Parse(Txt.Text.Trim());

                Txt = (TextBox)DGR.FindControl("txtRetailerRate");
                dr["RetailerRate"] = (Txt.Text.Trim().Length == 0) ? 0 : int.Parse(Txt.Text.Trim());

                Txt = (TextBox)DGR.FindControl("txtDressedRate");
                dr["DressedRate"] = (Txt.Text.Trim().Length == 0) ? 0 : int.Parse(Txt.Text.Trim());

                Txt = (TextBox)DGR.FindControl("txtBroilerRate");
                dr["BroilerRate"] = (Txt.Text.Trim().Length == 0) ? 0 : int.Parse(Txt.Text.Trim());

                dr["Date"] = DDate;

                DtBirdPrice.Rows.Add(dr);
                DtBirdPrice.AcceptChanges();
            }

            BusinessLayer.SMS.BirdPrice ObjBirdPrice = new BusinessLayer.SMS.BirdPrice();

            ObjBirdPrice.Save(DDate, DtBirdPrice);


            BusinessLayer.SMS.EggPrice ObjEggPrice = new BusinessLayer.SMS.EggPrice();
            Entity.SMS.EggPrice Eggprice = new Entity.SMS.EggPrice();

            Eggprice.date = DDate;
            Eggprice.NECCPrice = decimal.Parse(txtNECCEggRate.Text.Trim());
            Eggprice.belowWt = decimal.Parse(txtbelowWt.Text.Trim());
            Eggprice.overWt = decimal.Parse(txtoverWt.Text.Trim());
            Eggprice.belowAddRate = decimal.Parse(txtbelowAddRate.Text.Trim());
            Eggprice.overAddRate = decimal.Parse(txtoverAddRate.Text.Trim());

            ObjEggPrice.Save(Eggprice);

        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            SaveBirdRate();
            ShowMsg("BirdPrice Saved Successfully");
        }

        protected void ShowMsg(string message)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: alert('" + message + "'); ", true);
        }

        protected void GetPriceBtn_Click(object sender, EventArgs e)
        {
            LoadBirdPrice();
        }
        protected void BtnSaveSend_Click(object sender, EventArgs e)
        {

            SaveBirdRate();

            Response.Redirect("SendSMS1.aspx");
        }
    }
}