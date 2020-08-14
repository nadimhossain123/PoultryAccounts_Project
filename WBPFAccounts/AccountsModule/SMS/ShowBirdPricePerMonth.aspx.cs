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
using System;

namespace AccountsModule.SMS
{
    public partial class ShowBirdPricePerMonth : System.Web.UI.Page
    {
        int Count1 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                int Month = int.Parse(ddlMonth.SelectedValue.Trim());
                int Year = int.Parse(ddlYear.SelectedValue.Trim());
                dgvMonthlyPrice.Visible = false;
                LoadMonthlyPrice();


            }

        }

        protected void GetPriceBtn_Click(object sender, EventArgs e)
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

            BusinessLayer.SMS.district Objdistrict = new BusinessLayer.SMS.district();
            DataTable dt = Objdistrict.GetAll();

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
                BusinessLayer.SMS.BirdPrice ObjBirdPrice = new BusinessLayer.SMS.BirdPrice();
                DataTable dt1 = ObjBirdPrice.GetAllMonthly(Month, Year, DistrictId);

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
                lblmssg.Visible = false;
                DataRow dr2 = dtMonthly.NewRow();
                BusinessLayer.SMS.EggPrice ObjEggprice = new BusinessLayer.SMS.EggPrice();
                DataTable dt2 = ObjEggprice.GetEggPrice(Month, Year);
                dr2["DistName"] = "NECC Egg Rate";
                int j = 1, k = 0;
                int count = dt2.Rows.Count;
                for (j = 1; j <= 31; j++)
                {
                    //if (j == 31)
                    //    k = k - 1;

                    int c = (k < count ? Convert.ToDateTime(dt2.Rows[k]["Date"]).Day : 0);
                    if (c == j)
                    {

                        dr2[j] = dt2.Rows[k]["NECCPrice"].ToString();
                        k++;
                    }
                    else
                    {
                        dr2[j] = "--";
                    }

                }

                dtMonthly.Rows.Add(dr2);

                ViewState["RowCount"] = dtMonthly.Rows.Count;
                dgvMonthlyPrice.DataSource = dtMonthly;
                dgvMonthlyPrice.DataBind();
            }
            else
            {
                dgvMonthlyPrice.Visible = false;
                lblmssg.Visible = true;
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
                        if (((HyperLink)e.Row.FindControl(hy)) != null)
                            ((HyperLink)e.Row.FindControl(hy)).NavigateUrl = "#";
                    }
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}