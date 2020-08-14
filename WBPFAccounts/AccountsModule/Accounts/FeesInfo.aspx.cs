using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace AccountsModule.Accounts
{
    public partial class FeesInfo : System.Web.UI.Page
    {
        int headerCount = 0;
        int width = 50;
        DataTable dt = new DataTable();
        int intHeaderID = 0;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
                LoadMembershipCategory();
        }

        private void populatefees()
        {
            //--------------------------------------------------------
            panelfeesDetails1.Controls.Add(new LiteralControl("<table style='width:60%;border-collapse:collapse;'  rules='all'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl("<tr class='HeaderStyle'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl("<th scope='col'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl(" Fees"));
            panelfeesDetails1.Controls.Add(new LiteralControl("</th>"));

            //if (courseType == 1 || courseType == 3)
            //{
            //    headerCount = 4;
            //    width = 80;
            //}
            //else
            //{
            //    headerCount = 8;
            //    width = 50;
            //}

            headerCount = 1;
            width = 50;

            ////////CREATE HEADER .////////////////////////////
            createHeader(headerCount, width);
            panelfeesDetails1.Controls.Add(new LiteralControl("</tr>"));
            ////////END HEADER .////////////////////////////
            int intItemCount = 0;
            string strFeesName = "";

            BusinessLayer.Accounts.StreamGroup stremGrp = new BusinessLayer.Accounts.StreamGroup();
            Entity.Accounts.StreamGroup estremGrp = new Entity.Accounts.StreamGroup();

            //estremGrp.co = courseType;
            estremGrp.intMode = 1;
            estremGrp.feesHeaderID = (ddlMembershipCategory.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
            //estremGrp.stream_type_i

            dt = new DataTable();
            dt = stremGrp.GetFeesHead(estremGrp);

            if (dt.Rows.Count > 0)
            {
                intItemCount = dt.Rows.Count;
                for (int i = 0; i < intItemCount; i++)
                {
                    strFeesName = dt.Rows[i]["fees"].ToString();
                    int item = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    panelfeesDetails1.Controls.Add(new LiteralControl("<tr class='RowStyle'>"));
                    createBody(headerCount, width, item, strFeesName, dt, i);
                    panelfeesDetails1.Controls.Add(new LiteralControl("</tr>"));
                }
            }
            panelfeesDetails1.Controls.Add(new LiteralControl("</table>"));
            ;
        }

        private void createBody(int headerCount, int width, int item, string strFeesName, DataTable dt, int Pevi)
        {
            panelfeesDetails1.Controls.Add(new LiteralControl("<td>"));
            panelfeesDetails1.Controls.Add(new LiteralControl(strFeesName));
            panelfeesDetails1.Controls.Add(new LiteralControl("</td>"));
            for (int i = 0; i < headerCount; i++)
            {
                panelfeesDetails1.Controls.Add(new LiteralControl("<td scope='col' style='width: " + width + "px'>"));
                Label lbl = new Label();
                lbl.ID = "lblFees_" + item.ToString() + "_" + i.ToString();
                lbl.Width = width;
                lbl.Text = (dt.Rows[Pevi]["Amount"].ToString().Length > 0) ? dt.Rows[Pevi]["Amount"].ToString() : "0";
                panelfeesDetails1.Controls.Add(lbl);
                panelfeesDetails1.Controls.Add(new LiteralControl("</td>"));
            }

        }

        private void createHeader(int headerCount, int width)
        {
            for (int i = 0; i < headerCount; i++)
            {
                panelfeesDetails1.Controls.Add(new LiteralControl("<th scope='col' style='width: " + width + "px'>"));
                panelfeesDetails1.Controls.Add(new LiteralControl("Amount"));
                panelfeesDetails1.Controls.Add(new LiteralControl("</th>"));
            }
        }

        protected void InsertFisrtItem(DropDownList ddlList, string text)
        {
            ListItem item = new ListItem(text, "0");
            ddlList.Items.Insert(0, item);
        }

        protected void LoadMembershipCategory()
        {
            BusinessLayer.Common.MembershipCategory objMembershipCategory = new BusinessLayer.Common.MembershipCategory();
            DataTable dt = objMembershipCategory.GetAll();
            if (dt != null)
            {
                ddlMembershipCategory.DataSource = dt;
                ddlMembershipCategory.DataTextField = "CategoryName";
                ddlMembershipCategory.DataValueField = "MembershipCategoryId";
                ddlMembershipCategory.DataBind();
            }
            InsertFisrtItem(ddlMembershipCategory, "Select");
        }

        protected void ddlMembershipCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            populatefees();
        }
    }
}