using System;
using System.Collections;
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
using System.IO;

namespace AccountsModule.SMS
{
    public partial class AddEditMember : System.Web.UI.Page
    {
        public int MemberId
        {
            get { return Convert.ToInt32(ViewState["MemberId"]); }
            set { ViewState["MemberId"] = value; }
        }
        public string MobNo
        {
            get { return ViewState["MobNo"].ToString(); }
            set { ViewState["MobNo"] = value; }
        }
        public int CurrentPageIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    MemberId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                    LoadMemberDetails();

                    if (Request.QueryString["PageIndex"] != null && Request.QueryString["PageIndex"].ToString().Trim().Length > 0)
                    {
                        CurrentPageIndex = Convert.ToInt32(Request.QueryString["PageIndex"].ToString().Trim());
                        dgvMember.PageIndex = CurrentPageIndex;
                        LoadMemberList();
                    }
                }

                else
                {
                    CurrentPageIndex = 0;
                    ClearControls();
                    LoadMemberList();
                }
            }
        }


        protected void ClearControls()
        {
            MemberId = 0;
            ltrMsg.Text = "";
            txtName.Text = "";
            txtLocation.Text = "";
            txtMobNo.Text = "";
            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            txtRegNo.Text = "";
            txtVoucherDetails.Text = "";
            txtName.Focus();
            btnSave.Text = "Save";

        }

        protected void LoadMemberList()
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            string MemberName = txtSearchName.Text.Trim();
            string MobNo = txtSearchMob.Text.Trim();
            int RegistrationType = Convert.ToInt32(ddlRegTypeSearch.SelectedValue.Trim());
            string ExpirationDate = txtExpirationDate.Text.Trim();
            if (ExpirationDate.Length != 0)
            {
                string[] Arr = ExpirationDate.Split('/');
                ExpirationDate = Arr[1].Trim() + "/" + Arr[0].Trim() + "/" + Arr[2].Trim() + " 00:00:00";
            }

            int SearchType = Convert.ToInt32(ddlExpiration.SelectedValue.Trim());
            DataTable dtMember = ObjMember.GetAll(MemberName, MobNo, RegistrationType, ExpirationDate, SearchType);
            if (dtMember != null)
            {
                dgvMember.DataSource = dtMember;
                dgvMember.DataBind();
            }


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            CurrentPageIndex = 0;
            dgvMember.PageIndex = CurrentPageIndex;
            LoadMemberList();
        }

        protected void LoadMemberDetails()
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            Entity.SMS.Member EntityMember = new Entity.SMS.Member();
            EntityMember = ObjMember.GetAllById(MemberId);
            if (EntityMember != null)
            {
                txtName.Text = EntityMember.MemberName;
                txtLocation.Text = EntityMember.Location;
                txtMobNo.Text = EntityMember.MobileNo;
                txtStartDate.Text = EntityMember.StartDate.ToString("dd/MM/yyyy");
                txtEndDate.Text = EntityMember.EndDate.ToString("dd/MM/yyyy");
                ddlRegType.SelectedValue = EntityMember.RegistrationType.ToString();
                txtRegNo.Text = EntityMember.RegistrationNo.ToString();
                txtVoucherDetails.Text = EntityMember.VoucherDetails;
                btnSave.Text = "Update";
            }
        }

        protected void LoadMemberDetailsByMobNo()
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            Entity.SMS.Member EntityMember = new Entity.SMS.Member();
            EntityMember = ObjMember.GetAllByMobNo(MobNo);
            if (EntityMember != null)
            {
                MemberId = EntityMember.MemberId;
                txtName.Text = EntityMember.MemberName;
                txtLocation.Text = EntityMember.Location;
                txtMobNo.Text = EntityMember.MobileNo;
                txtStartDate.Text = EntityMember.StartDate.ToString("dd/MM/yyyy");
                txtEndDate.Text = EntityMember.EndDate.ToString("dd/MM/yyyy");
                ddlRegType.SelectedValue = EntityMember.RegistrationType.ToString();
                txtRegNo.Text = EntityMember.RegistrationNo.ToString();
                txtVoucherDetails.Text = EntityMember.VoucherDetails;

            }

            if (MemberId != 0)
            {
                btnSave.Text = "Update";
                txtSearchMob.Text = MobNo;
                txtSearchName.Text = "";
                ddlRegTypeSearch.SelectedValue = "1";
                txtExpirationDate.Text = "";
                ddlExpiration.SelectedValue = "1";
                LoadMemberList();
            }
            else
            {
                btnSave.Text = "Save";
                ShowMsg(MobNo + " does not exists");
            }
        }

        protected void ShowMsg(string message)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: alert('" + message + "'); ", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            Entity.SMS.Member EntityMember = new Entity.SMS.Member();
            EntityMember.MemberId = MemberId;
            EntityMember.MemberName = txtName.Text.Trim();
            EntityMember.Location = txtLocation.Text.Trim();
            EntityMember.MobileNo = txtMobNo.Text.Trim();
            EntityMember.RegistrationType = int.Parse(ddlRegType.SelectedValue.Trim());
            EntityMember.RegistrationNo = (txtRegNo.Text.Trim().Length == 0) ? "" : txtRegNo.Text.Trim();
            EntityMember.VoucherDetails = txtVoucherDetails.Text;

            string[] SDate = txtStartDate.Text.Trim().Split('/');
            EntityMember.StartDate = Convert.ToDateTime(SDate[1].Trim() + "/" + SDate[0].Trim() + "/" + SDate[2].Trim());

            string[] EDate = txtEndDate.Text.Trim().Split('/');
            EntityMember.EndDate = Convert.ToDateTime(EDate[1].Trim() + "/" + EDate[0].Trim() + "/" + EDate[2].Trim());

            int RowsUpdated = ObjMember.Save(EntityMember);
            if (RowsUpdated == -1)
            {
                ltrMsg.Text = "CAN NOT ADD. A SAME MOBILE NUMBER EXISTS";
            }
            else
            {
                ltrMsg.Text = "DATA SAVED SUCCESSFULLY";
                if (MemberId == 0)
                {
                    CurrentPageIndex = 0;
                }
                dgvMember.PageIndex = CurrentPageIndex;
                LoadMemberList();
                ClearControls();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 0;
            dgvMember.PageIndex = CurrentPageIndex;
            LoadMemberList();
        }
        protected void dgvMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CurrentPageIndex = e.NewPageIndex;
            dgvMember.PageIndex = CurrentPageIndex;
            LoadMemberList();
        }
        protected void dgvMember_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvMember.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditMember.aspx?id=" + Id + "&PageIndex=" + CurrentPageIndex);

        }
        protected void dgvMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvMember.DataKeys[e.RowIndex].Value);
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            ObjMember.Delete(Id);
            LoadMemberList();
        }
        protected void dgvMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkMemberName");
                lnkbtn.CommandArgument = dgvMember.DataKeys[e.Row.RowIndex].Value.ToString();
                Literal ltrIsExpired = (Literal)e.Row.FindControl("ltrIsExpired");
                string IsExpired = ltrIsExpired.Text.Trim();
                if (IsExpired == "YES")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    lnkbtn.Style.Add("text-decoration", "underline");
                    lnkbtn.ToolTip = "Activate Now";
                    lnkbtn.ForeColor = System.Drawing.Color.White;
                    lnkbtn.Font.Bold = true;

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    lnkbtn.Style.Add("text-decoration", "none");
                    lnkbtn.ToolTip = "Activated";
                    lnkbtn.ForeColor = System.Drawing.Color.White;
                    lnkbtn.Font.Bold = false;

                }


                ((Literal)e.Row.FindControl("ltrSl")).Text = (e.Row.RowIndex + 1).ToString();

                if (Convert.ToInt32(((DataTable)dgvMember.DataSource).Rows[e.Row.RowIndex]["Priority"]).Equals(1))
                    ((CheckBox)e.Row.FindControl("ChkPriority")).Checked = true;
                else
                    ((CheckBox)e.Row.FindControl("ChkPriority")).Checked = false;
            }
        }

        protected void ChkPriority_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int MemberId = Convert.ToInt32(dgvMember.DataKeys[row.RowIndex].Value);
            CheckBox ChkPriority = (CheckBox)dgvMember.Rows[row.RowIndex].FindControl("ChkPriority");

            int Priority;
            if (ChkPriority.Checked)
                Priority = 1;
            else
                Priority = 0;

            BusinessLayer.SMS.Member objMember = new BusinessLayer.SMS.Member();
            objMember.ChangePriority(MemberId, Priority);
        }

        protected void dgvMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Activate"))
            {
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
                ObjMember.QuickUpdate(Id);
                dgvMember.PageIndex = CurrentPageIndex;
                LoadMemberList();
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            BusinessLayer.SMS.Member ObjMember = new BusinessLayer.SMS.Member();
            string MemberName = txtSearchName.Text.Trim();
            string MobNo = txtSearchMob.Text.Trim();
            int RegistrationType = Convert.ToInt32(ddlRegTypeSearch.SelectedValue.Trim());
            string ExpirationDate = txtExpirationDate.Text.Trim();
            if (ExpirationDate.Length != 0)
            {
                string[] Arr = ExpirationDate.Split('/');
                ExpirationDate = Arr[1].Trim() + "/" + Arr[0].Trim() + "/" + Arr[2].Trim() + " 00:00:00";
            }

            int SearchType = Convert.ToInt32(ddlExpiration.SelectedValue.Trim());
            DataTable dt = ObjMember.GetAll(MemberName, MobNo, RegistrationType, ExpirationDate, SearchType);

            //string path = Server.MapPath(@"Report\MemberList.xls");
            //FileInfo fn = new FileInfo(path);
            //if (fn.Exists)
            //{
            //    fn.Delete();
            //}

            //FileStream stream = new FileStream(path, FileMode.Create);
            //BusinessLayer.ExcelWriter writer = new BusinessLayer.ExcelWriter(stream);
            //writer.BeginWrite();
            //writer.WriteCell(0, 0, "Sl");
            //writer.WriteCell(0, 1, "Member Name");
            //writer.WriteCell(0, 2, "Location");
            //writer.WriteCell(0, 3, "Start Date");
            //writer.WriteCell(0, 4, "End Date");
            //writer.WriteCell(0, 5, "Mobile No");
            //writer.WriteCell(0, 6, "Registration Type");
            //writer.WriteCell(0, 7, "Registration No");
            //writer.WriteCell(0, 8, "Voucher");
            //writer.WriteCell(0, 9, "Expired");

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    writer.WriteCell(i + 1, 0, i + 1);
            //    writer.WriteCell(i + 1, 1, dt.Rows[i]["MemberName"].ToString());
            //    writer.WriteCell(i + 1, 2, dt.Rows[i]["Location"].ToString());
            //    writer.WriteCell(i + 1, 3, Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy"));
            //    writer.WriteCell(i + 1, 4, Convert.ToDateTime(dt.Rows[i]["EndDate"].ToString()).ToString("dd/MM/yyyy"));
            //    writer.WriteCell(i + 1, 5, Convert.ToDouble(dt.Rows[i]["MobileNo"].ToString()));
            //    writer.WriteCell(i + 1, 6, dt.Rows[i]["RegistrationType"].ToString());
            //    writer.WriteCell(i + 1, 7, dt.Rows[i]["RegistrationNo"].ToString());
            //    writer.WriteCell(i + 1, 8, dt.Rows[i]["VoucherDetails"].ToString());
            //    writer.WriteCell(i + 1, 9, dt.Rows[i]["IsExpired"].ToString());
            //}
            //writer.EndWrite();
            //stream.Close();
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=MemberList_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls");
            //Response.TransmitFile(path);
            //Response.End();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MemberList"));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            Table table = new Table();
            TableRow row;
            TableCell cell;

            row = new TableRow();
            cell = new TableCell();
            cell.Text = "Sl";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Member Name";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Location";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Start Date";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "End Date";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Mobile No";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Registration Type";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Registration No";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Voucher";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Expired";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            table.Rows.Add(row);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = new TableRow();

                cell = new TableCell();
                cell.Text = (i + 1).ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["MemberName"].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["Location"].ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yyyy");
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = Convert.ToDateTime(dt.Rows[i]["EndDate"].ToString()).ToString("dd/MM/yyyy");
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["MobileNo"].ToString();
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["RegistrationType"].ToString();
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["RegistrationNo"].ToString();
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["VoucherDetails"].ToString();
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dt.Rows[i]["IsExpired"].ToString();
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }
            table.GridLines = GridLines.Both;

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    table.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }

        }
        protected void btnQuickSearch_Click(object sender, EventArgs e)
        {
            MobNo = txtMobNoforQuickSearch.Text.Trim();
            LoadMemberDetailsByMobNo();
        }
    }
}