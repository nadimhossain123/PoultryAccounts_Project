using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountsModule.Common
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
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDay();
                LoadYear();
                Message.Show = false;

                ddlDay.SelectedValue = DateTime.Now.Day.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                
                LoadBirdPrice();
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void LoadDay()
        {
            for (int i = 1; i <= 31; i++)
                ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));

            InsertFisrtItem(ddlDay, "DAY");
        }

        protected void LoadYear()
        {
            for (int i = 2000; i <= 2050; i++)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            InsertFisrtItem(ddlYear, "YEAR");
        }

        protected void LoadBirdPrice()
        {
            int Day = int.Parse(ddlDay.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            //string SDate = (Month + "/" + Day + "/" + Year).ToString();
            string SDate = (Year + "-" + Month + "-" + Day).ToString();
            DateTime DDate = Convert.ToDateTime(SDate);
            BusinessLayer.Common.BirdPrice ObjBirdPrice = new BusinessLayer.Common.BirdPrice();
            DataTable dt = ObjBirdPrice.GetAll(DDate);
            if (dt != null)
            {
                dgvBirdPrice.DataSource = dt;
                dgvBirdPrice.DataBind();
            }
            else
            {

            }

            BusinessLayer.Common.EggPrice ObjEggPrice = new BusinessLayer.Common.EggPrice();
            Entity.Common.EggPrice EggPrice = new Entity.Common.EggPrice();
            EggPrice = ObjEggPrice.GetAllById(DDate);

            if (EggPrice != null)
            {

                txtNECCEggRate.Text = EggPrice.NECCPrice.ToString();
                txtNECCEggRate2.Text = EggPrice.NECCPrice2.ToString();
                txtNECCEggRate3.Text = EggPrice.NECCPrice3.ToString();
                txtbelowWt.Text = EggPrice.belowWt.ToString();
                txtoverWt.Text = EggPrice.overWt.ToString();
                txtbelowAddRate.Text = EggPrice.belowAddRate.ToString();
                txtoverAddRate.Text = EggPrice.overAddRate.ToString();
            }
        }

        protected void SaveBirdRate()
        {
            try
            {
                int Day = int.Parse(ddlDay.SelectedValue.Trim());
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

                BusinessLayer.Common.BirdPrice ObjBirdPrice = new BusinessLayer.Common.BirdPrice();

                ObjBirdPrice.Save(DDate, DtBirdPrice);


                BusinessLayer.Common.EggPrice ObjEggPrice = new BusinessLayer.Common.EggPrice();
                Entity.Common.EggPrice Eggprice = new Entity.Common.EggPrice();

                Eggprice.date = DDate;
                Eggprice.NECCPrice = decimal.Parse(txtNECCEggRate.Text.Trim());
                Eggprice.NECCPrice2 = decimal.Parse(txtNECCEggRate2.Text.Trim());
                Eggprice.NECCPrice3 = decimal.Parse(txtNECCEggRate3.Text.Trim());
                Eggprice.belowWt = decimal.Parse(txtbelowWt.Text.Trim());
                Eggprice.overWt = decimal.Parse(txtoverWt.Text.Trim());
                Eggprice.belowAddRate = decimal.Parse(txtbelowAddRate.Text.Trim());
                Eggprice.overAddRate = decimal.Parse(txtoverAddRate.Text.Trim());

                ObjEggPrice.Save(Eggprice);

                Message.IsSuccess = true;
                Message.Text = "BirdPrice Saved Successfully";
            }
            catch
            {
                Message.IsSuccess = false;
                Message.Text = "Something went wrong!!! Please try again...";
            }
            finally
            {
                Message.Show = true;
            }
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            LoadBirdPrice();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveBirdRate();
        }

        protected void btnSaveSend_Click(object sender, EventArgs e)
        {
            SaveBirdRate();
            Response.Redirect("SendSMS1.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditBirdPrice.aspx");
        }
    }
}