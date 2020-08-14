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
    public partial class Fees : System.Web.UI.Page
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
            ////////////////////////////////////////////////////////////////////////////

            if (!IsPostBack)
            {
                //if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.FEES))
                //{
                //    Response.Redirect("../Unauthorized.aspx");
                //}
                LoadMembershipCategory();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    populateFeesDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    intHeaderID = Convert.ToInt32(Request.QueryString["id"].ToString());
                }

            }
            else
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    intHeaderID = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
            }
            populatefees();
            Message.Show = false;
        }

        private void populateFeesDetails(int headerID)
        {
            //BusinessLayer.Accounts.StreamGroup stremGrp = new BusinessLayer.Accounts.StreamGroup();
            //Entity.Accounts.StreamGroup estremGrp = new Entity.Accounts.StreamGroup();

            //estremGrp.intMode = 11;
            //estremGrp.feesID = headerID;
            //DataTable dt = new DataTable();
            //dt = stremGrp.FeesBasedOnID(estremGrp);
            //if (dt.Rows.Count > 0)
            //{
            //    ddlBatch.SelectedValue = dt.Rows[0]["batchID"].ToString();
            //    ddlCourse1.SelectedValue = dt.Rows[0]["CourseId"].ToString();

            //    estremGrp.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            //    estremGrp.intMode = 3;
            //    estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());

            //    DataSet ds = new DataSet();
            //    ds = stremGrp.GetLoad(estremGrp);
            //    if (ds.Tables.Count > 0)
            //    {
            //        ddlStream1.DataSource = ds.Tables[0];
            //        ddlStream1.DataTextField = "stream_name";
            //        ddlStream1.DataValueField = "StreamId";
            //        ddlStream1.DataBind();
            //        ddlStream1.SelectedValue = "0";
            //    }
            //    ddlStream1.SelectedValue = dt.Rows[0]["streamID"].ToString();

            //    txtFeesName.Text = dt.Rows[0]["fees_name"].ToString();
            //    hidCourse_id.Value = dt.Rows[0]["CourseId"].ToString();
            //}
        }

        protected void ClearFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        //ddlSource.SelectedIndex = 0;
                        ddlSource.SelectedValue = "0";
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                    case "RadioButton":
                        RadioButton rdb = (RadioButton)contl;
                        rdb.Checked = false;
                        break;
                    case "CheckBox":
                        CheckBox chk = (CheckBox)contl;
                        chk.Checked = false;
                        break;

                }
                ClearFields(contl.Controls);
            }
            Message.Show = false;
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
            
        }

        private void createBody(int headerCount, int width, int item, string strFeesName, DataTable dt, int Pevi)
        {
            panelfeesDetails1.Controls.Add(new LiteralControl("<td>"));
            panelfeesDetails1.Controls.Add(new LiteralControl(strFeesName));
            panelfeesDetails1.Controls.Add(new LiteralControl("</td>"));
            for (int i = 0; i < headerCount; i++)
            {
                panelfeesDetails1.Controls.Add(new LiteralControl("<td scope='col' style='width: " + width + "px'>"));
                TextBox text = new TextBox();
                text.ID = "txtFees_" + item.ToString() + "_" + i.ToString();
                text.Width = width;
                text.Text = (dt.Rows[Pevi]["Amount"].ToString().Length > 0) ? dt.Rows[Pevi]["Amount"].ToString() : "0";
                text.Attributes.Add("onKeyPress", "return NumbersOnly(event)");
                text.Attributes.Add("onblur", "return isNumber(event)");
                panelfeesDetails1.Controls.Add(text);
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

        protected void btnStramSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Accounts.StreamGroup stremGrp = new BusinessLayer.Accounts.StreamGroup();
            Entity.Accounts.StreamGroup estremGrp = new Entity.Accounts.StreamGroup();

            //courseType = Convert.ToInt32(hidCourse_id.Value.ToString());

            ////--------------------------------------------------------
            //if (intHeaderID == 0)
            //{
            //    estremGrp.intMode = 4;
            //}
            //else
            //{
            //    estremGrp.intMode = 12;
            //}
            ////estremGrp.stream_type_id = Convert.ToInt32(ddlStream1.SelectedValue.ToString());
            ////estremGrp.batch_ID = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            ////estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());
            //estremGrp.FeesName = txtFeesName.Text.Trim();
            estremGrp.feesID = intHeaderID;


            //int IntLastInsertedID = stremGrp.SaveHeader(estremGrp);
            ////-------------------------------------------------------
            //if (IntLastInsertedID > 0)
            //{
            //    stremGrp = new BusinessLayer.Accounts.StreamGroup();
            //    estremGrp = new Entity.Accounts.StreamGroup();
            //    estremGrp.intMode = 1;
            //    //estremGrp.stream_type_id = Convert.ToInt32(ddlStream1.SelectedValue.ToString());
            //    //estremGrp.batch_ID = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            //    //estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());


            //    dt = new DataTable();
            //    dt = stremGrp.GetFeesHead(estremGrp);

            //    if (dt.Rows.Count > 0)
            //    {
            //        if (courseType == 1 || courseType == 3)
            //        {
            //            headerCount = 4;
            //        }
            //        else
            //        {
            //            headerCount = 8;
            //        }
            int rowAffacted = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < headerCount; j++)
                {
                    string tdId = "txtFees_" + dt.Rows[i]["id"].ToString() + "_" + j.ToString();
                    TextBox tx = (TextBox)panelfeesDetails1.FindControl(tdId);
                    if (tx.Text.Length == 0)
                    {
                        tx.Text = "0";
                    }
                    stremGrp = new BusinessLayer.Accounts.StreamGroup();
                    estremGrp = new Entity.Accounts.StreamGroup();
                    estremGrp.intMode = 5;
                    estremGrp.feesID = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    estremGrp.feesHeaderID = Convert.ToInt32(ddlMembershipCategory.SelectedValue.Trim());
                    estremGrp.column_name = j + 1;
                    estremGrp.column_value = Convert.ToInt32(tx.Text.ToString().Trim());
                    rowAffacted += stremGrp.SaveDetails(estremGrp);
                }
            }
            //    }
            if (rowAffacted > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Fees Information Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Duplicate Fees Information Is Not Allowed. This Fees Head Already Exists For This Batch-Course-Stream.";
            }
            Message.Show = true;
            int aa = panelfeesDetails1.Controls.Count;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields(Form.Controls);
            Response.Redirect("Fees.aspx");
            //panelfeesDetails1.Controls.Clear();
        }

        protected void lnktnFeeDetailsView_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script>openpopup('abc.aspx?membershipcode=" + ddlMembershipCategory.SelectedValue.Trim() + "')</script>", false);
        }
    }
}