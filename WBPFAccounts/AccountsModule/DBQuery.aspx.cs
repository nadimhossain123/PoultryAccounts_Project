using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class DBQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand command = new SqlCommand(txtQuery.Text,con);
        command.CommandType = System.Data.CommandType.Text;
        con.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        sqlDa.Fill(dt);
        con.Close();

        GridView1.DataSource = dt;
        GridView1.DataBind();

        lblMessage.Text = "Total Record Selected: " + dt.Rows.Count.ToString();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand command = new SqlCommand(txtQuery.Text,con);
        command.CommandType = System.Data.CommandType.Text;
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
}
