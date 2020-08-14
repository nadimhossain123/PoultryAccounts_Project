using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace AccountsModule.Common
{
    public partial class ShowBirdPricePerMonthPublicView : System.Web.UI.Page
    {
        int Count1 = 0;

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["showMaster"] != null && Convert.ToBoolean(Request.QueryString["showMaster"]) == false)
        //        this.MasterPageFile = "../EmptyMaster.Master";
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            //{
                //Response.Redirect("../Login.aspx");
            //}

            if (!IsPostBack)
            {
                LoadYear();
                
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                int Month = int.Parse(ddlMonth.SelectedValue.Trim());
                int Year = int.Parse(ddlYear.SelectedValue.Trim());
                dgvMonthlyPrice.Visible = false;
                LoadMonthlyPrice();
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void LoadYear()
        {
            for (int i = 2000; i <= 2050; i++)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            InsertFisrtItem(ddlYear, "YEAR");
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            LoadMonthlyPrice();
        }

        protected void LoadMonthlyPrice()
        {
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int DaysCount = DateTime.DaysInMonth(Year, Month);

            BusinessLayer.Common.District Objdistrict = new BusinessLayer.Common.District();
            DataTable dt = Objdistrict.SMSDistrictGetAll();

            int RowCount = dt.Rows.Count;

            DataTable dtMonthly = new DataTable();
            dtMonthly.Columns.Add("DistName", typeof(string));
            for (int k = 1; k <= 31; k++)
            {
                dtMonthly.Columns.Add(k.ToString(), typeof(string));
            }

            dtMonthly.Columns.Add("Month", typeof(int));
            dtMonthly.Columns.Add("Avg", typeof(string));
            dtMonthly.Columns.Add("NeccPrice", typeof(string));


            foreach (DataRow drDist in dt.Rows)
            {
                string str;

                int DistrictId = Convert.ToInt32(drDist["DistrictId"].ToString());
                BusinessLayer.Common.BirdPrice ObjBirdPrice = new BusinessLayer.Common.BirdPrice();
                DataTable dt1 = ObjBirdPrice.GetAllMonthly(Month, Year, DistrictId);
                DateTime TodayDate = DateTime.Now;
                int monthNo = TodayDate.Month;
                
                //Remove today rate

                if (monthNo == Month)
                {
                    int Maxrow = dt1.Rows.Count - 1;
                    dt1.Rows.RemoveAt(Maxrow);
                    dt1.AcceptChanges();
                }
                //DateTime LocalDate = DateTime.Now.AddHours(10);

                //if (dt1 != null && dt1.Rows.Count > 0)
                //{
                //    dt1 = dt1.AsEnumerable().Where(r =>
                //    !(
                //        Convert.ToDateTime(r["date"]).Day == LocalDate.Day
                //        && Convert.ToDateTime(r["date"]).Month == LocalDate.Month
                //        && Convert.ToDateTime(r["date"]).Year == LocalDate.Month)).CopyToDataTable();
                //}

                //if (dt1 != null && dt1.Rows.Count > 0)
                //{
                //    dt1 = dt1.AsEnumerable().Where(r => 
                //    !(
                //        Convert.ToDateTime(r["date"]).Day == DateTime.Now.Day
                //        && Convert.ToDateTime(r["date"]).Month == DateTime.Now.Month
                //        && Convert.ToDateTime(r["date"]).Year == DateTime.Now.Year)).CopyToDataTable();
                //}

                Count1++;

                DataRow dr = dtMonthly.NewRow();

                if (dt1.Rows.Count != 0 && dt != null)
                {
                    int TotalFramRate = 0, TotalRetailerRate = 0, TotalDressedRate = 0, TotalBroilerRate = 0;
                    double AvgFramRate, AvgRetailerRate, AvgDressedRate, AvgBroilerRate;

                    int Count = 0;
                    int j = 0, K = 0;



                    dr["DistName"] = str = (dt1.Rows[Count]["DistrictName"].ToString().Length == 0) ? "--" : dt1.Rows[Count]["DistrictName"].ToString();




                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        DateTime Date = Convert.ToDateTime(dr1["Date"].ToString());
                        int Day = int.Parse(Date.Day.ToString());

                        while (Day > Count)
                        {
                            if (Day == Count + 1)
                            {
                                dr[Count + 1] = (dt1.Rows[j]["FarmRate"].ToString() + "/" + dt1.Rows[j]["RetailerRate"].ToString() + "/" + dt1.Rows[j]["BroilerRate"].ToString() + "/" + dt1.Rows[j]["DressedRate"].ToString());
                                TotalFramRate = TotalFramRate + Convert.ToInt32(dt1.Rows[j]["FarmRate"].ToString());
                                TotalRetailerRate = TotalRetailerRate + Convert.ToInt32(dt1.Rows[j]["RetailerRate"].ToString());
                                TotalDressedRate = TotalDressedRate + Convert.ToInt32(dt1.Rows[j]["DressedRate"].ToString());
                                TotalBroilerRate = TotalBroilerRate + Convert.ToInt32(dt1.Rows[j]["BroilerRate"].ToString());
                                Count++;
                                j++;
                                K = K + 1;
                            }
                            else
                            {

                                for (int i = Count + 1; i < Day; i++)
                                {
                                    dr[Count + 1] = "--" + "\n" + "--";
                                    Count++;
                                }
                            }
                        }
                    }
                    AvgFramRate = TotalFramRate / K;
                    AvgRetailerRate = TotalRetailerRate / K;
                    AvgDressedRate = TotalDressedRate / K;
                    AvgBroilerRate = TotalBroilerRate / K;
                    dr["Avg"] = AvgFramRate.ToString() + "/" + AvgRetailerRate.ToString() + "/" + AvgBroilerRate.ToString() + "/" + AvgDressedRate.ToString();

                    while (Count < 31)
                    {

                        dr[Count + 1] = "--" + "\n" + "--";
                        Count++;

                    }
                    dr["Month"] = Month;



                    dtMonthly.Rows.Add(dr);
                    dtMonthly.AcceptChanges();
                }
            }

            if (dtMonthly.Rows.Count != 0)
            {
                dgvMonthlyPrice.Visible = true;
                DataRow dr2 = dtMonthly.NewRow();
                BusinessLayer.Common.EggPrice ObjEggprice = new BusinessLayer.Common.EggPrice();
                DataTable dt2 = ObjEggprice.GetEggPrice(Month, Year);

                DateTime TodayDate = DateTime.Now;
                int monthNo = TodayDate.Month;

                //Remove today bird price
                if (monthNo == Month)
                {
                    int Maxrow = dt2.Rows.Count - 1;
                    dt2.Rows.RemoveAt(Maxrow);
                    dt2.AcceptChanges();
                }

                //DateTime LocalDate = DateTime.Now.AddHours(10);

                //if (dt2 != null && dt2.Rows.Count > 0)
                //{
                //    dt2 = dt2.AsEnumerable().Where(r =>
                //    !(
                //        Convert.ToDateTime(r["date"]).Day == LocalDate.Day
                //        && Convert.ToDateTime(r["date"]).Month == LocalDate.Month
                //        && Convert.ToDateTime(r["date"]).Year == LocalDate.Month)).CopyToDataTable();
                //}

                //if (dt2 != null && dt2.Rows.Count > 0)
                //{
                //    dt2 = dt2.AsEnumerable().Where(r =>
                //    !(
                //        Convert.ToDateTime(r["date"]).Day == DateTime.Now.Day
                //        && Convert.ToDateTime(r["date"]).Month == DateTime.Now.Month
                //        && Convert.ToDateTime(r["date"]).Year == DateTime.Now.Year)).CopyToDataTable();
                //}

                dr2["DistName"] = "NECC Egg Rate";
                int j = 1;
                foreach (DataRow DrNecc in dt2.Rows)
                {

                    dr2[j] = DrNecc["NECCPrice"].ToString().Length == 0 ? "--" : DrNecc["NECCPrice"].ToString();
                    j++;


                }
                Decimal AvgNECCPrice = Convert.ToDecimal(dt2.Compute("AVG([NECCPrice])", "")); 

                dr2["Avg"] = AvgNECCPrice.ToString("0.00");

                dtMonthly.Rows.Add(dr2);
                

                ViewState["RowCount"] = dtMonthly.Rows.Count;
                dgvMonthlyPrice.DataSource = dtMonthly;
                dgvMonthlyPrice.DataBind();
            }
            else
            {
                dgvMonthlyPrice.Visible = false;
            }
        }

        protected void dgvMonthlyPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == Convert.ToInt32(ViewState["RowCount"].ToString()) - 1)
                {
                    string hy = "";
                    for (int i = 1; i <= 31; i++)
                    {
                        hy = "hy" + i;
                        //if (((HyperLink)e.Row.FindControl(hy)) != null)
                            ((HyperLink)e.Row.FindControl(hy)).NavigateUrl = "#";
                        ((HyperLink)e.Row.FindControl(hy)).Enabled = false;
                    }
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
           Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=PoultryProductPriceReport.xls");
            Response.ContentType = "application/excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvMonthlyPrice.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
           // base.VerifyRenderingInServerForm(control);
        }


    }
}