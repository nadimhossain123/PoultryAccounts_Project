﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace AccountsModule.Common
{
    public partial class MemberPaymentExcelUpload : System.Web.UI.Page
    {
        BusinessLayer.Common.Excel objExcel = new BusinessLayer.Common.Excel();
        DataTable dtError, dtList;
        string strMsg = string.Empty;
        string[] ArrColumnConfig = new string[] 
                            { "Agent Code|AgentCode|Text|Yes",
                              "Member Code|MemberCode|Text|Yes",
                              "Payment Date|PaymentDate|Date|Yes",
                              "Admission Fees Amount|AdmissionFeesAmount|Decimal|Yes",
                              "Admission Fees Tax|AdmissionFeesTax|Decimal|Yes",
                              "Renewal Fees Amount|RenewalFeesAmount|Decimal|Yes",
                              "Renewal Fees Tax|RenewalFeesTax|Decimal|Yes",
                              "Development Fees Amount|DevelopmentFeesAmount|Decimal|Yes",
                              "Narration|Narration|Text|No" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                dtError = new DataTable();
                dtError.Columns.Add("ROW_NO", typeof(int));
                dtError.Columns.Add("MESSAGE", typeof(string));

                Message.Show = false;
                BindUploadList();
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                dtError.Rows.Clear();
                dgvError.DataSource = null;
                dgvError.DataBind();

                string UploadPath = Server.MapPath("~/") + "\\ExcelUpload\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

                if (fu.PostedFile.FileName != null && fu.PostedFile.ContentLength > 0)
                {
                    string fn = fu.FileName;
                    string fileExt = System.IO.Path.GetExtension(fn);

                    if (fileExt.Equals(".xlsx"))
                    {
                        fu.PostedFile.SaveAs(UploadPath);
                        DataTable dtData = objExcel.ReadExcel(UploadPath, ".xlsx");

                        if (dtData != null && dtData.Rows.Count > 0)
                        {
                            bool IsValidated = ValidateExcel(ref dtData);

                            if (IsValidated)
                            {
                                for (int c = 0; c < dtData.Columns.Count; c++)
                                    dtData.Columns[c].ColumnName = ArrColumnConfig[c].Split('|')[1].Trim();

                                objExcel.BulkUpload(dtData, "tempPaymentExcelUpload");
                                string xml = objExcel.ValidatePaymentExcelUpload(Convert.ToInt32(Session["UserId"]));

                                int msgcode = Convert.ToInt32(GetNodeInnerXml(xml, "MSGCODE"));
                                string msgtext = GetNodeInnerXml(xml, "MSGTEXT");
                                string validationXml = GetNodeOuterXml(xml, "VALIDATION");

                                if (msgcode == 1)
                                {
                                    if (!string.IsNullOrEmpty(validationXml))
                                    {
                                        using (DataSet dsError = new DataSet())
                                        {
                                            StringReader sr = new StringReader(validationXml);
                                            dsError.ReadXml(sr);
                                            dtError = dsError.Tables[0].Copy();

                                            dgvError.DataSource = dtError;
                                            dgvError.DataBind();
                                        }
                                    }
                                    ShowMessage(msgtext, false);
                                }
                                else if (msgcode == 0)
                                {
                                    ShowMessage(msgtext, true);
                                    BindUploadList();
                                }

                                DeleteExcel(UploadPath);
                            }
                            else
                            {
                                ShowMessage("Excel format error", false);
                                dgvError.DataSource = dtError;
                                dgvError.DataBind();
                                DeleteExcel(UploadPath);
                            }
                        }
                        else
                        {
                            ShowMessage("Empty excel file", false);
                            DeleteExcel(UploadPath);
                        }
                    }
                    else
                    {
                        ShowMessage("Select excel file (.xlsx) to upload", false);
                    }
                }
                else
                {
                    ShowMessage("Select file", false);
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }

        private void ShowMessage(string Text, bool IsSuccess)
        {
            Message.IsSuccess = IsSuccess;
            Message.Text = Text;
            Message.Show = true;
        }

        private void DeleteExcel(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
        }

        private bool ValidateExcel(ref DataTable dtValidate)
        {
            bool IsValidated = true;

            if (dtValidate.Columns.Count == ArrColumnConfig.Length)
            {
                for (int c = 0; c < dtValidate.Columns.Count; c++)
                {
                    if (!dtValidate.Columns[c].ColumnName.ToUpper().Trim().Equals(ArrColumnConfig[c].Trim().Split('|')[0].ToUpper()))
                    {
                        IsValidated = false;
                        dtError.Rows.Add(0, "Invalid column '" + ArrColumnConfig[c].Trim().Split('|')[0] + "' at position " + Convert.ToString(c + 1));
                    }
                }
            }
            else
            {
                IsValidated = false;
                dtError.Rows.Add(0, "Number of columns not matched");
            }

            if (IsValidated)
            {
                for (int i = 0; i < dtValidate.Rows.Count; i++)
                {
                    for (int c = 0; c < dtValidate.Columns.Count; c++)
                    {
                        string ColumnHeading = ArrColumnConfig[c].Trim().Split('|')[0];
                        string DataType = ArrColumnConfig[c].Trim().Split('|')[2].ToUpper();
                        string Mandatory = ArrColumnConfig[c].Trim().Split('|')[3].ToUpper();
                        string Value = dtValidate.Rows[i][c].ToString().Trim();

                        if (DataType.Equals("DECIMAL") && !string.IsNullOrEmpty(Value))
                        {
                            if (!objExcel.IsDecimal(Value))
                            {
                                IsValidated = false;
                                if (dtError.Select("ROW_NO = " + (i + 2).ToString()).Length == 0)
                                {
                                    dtError.Rows.Add(i + 2, "Column '" + ColumnHeading + "' should contain value of number type(18,2)");
                                }
                                else
                                {
                                    dtError.AsEnumerable().Where(r => Convert.ToInt32(r["ROW_NO"]) == (i + 2)).ToList().ForEach(r => r["MESSAGE"] = r["MESSAGE"].ToString() + ", Column '" + ColumnHeading + "' should contain value of number type(18,2)");
                                }
                            }
                        }
                        else if (DataType.Equals("DATE") && !string.IsNullOrEmpty(Value))
                        {
                            if (!objExcel.IsDate(Value))
                            {
                                IsValidated = false;
                                if (dtError.Select("ROW_NO = " + (i + 2).ToString()).Length == 0)
                                {
                                    dtError.Rows.Add(i + 2, "Column '" + ColumnHeading + "' should contain value of date type(YYYY-MM-DD)");
                                }
                                else
                                {
                                    dtError.AsEnumerable().Where(r => Convert.ToInt32(r["ROW_NO"]) == (i + 2)).ToList().ForEach(r => r["MESSAGE"] = r["MESSAGE"].ToString() + ", Column '" + ColumnHeading + "' should contain value of date type(YYYY-MM-DD)");
                                }
                            }
                        }

                        if (Mandatory.Equals("YES") && string.IsNullOrEmpty(Value))
                        {
                            IsValidated = false;
                            if (dtError.Select("ROW_NO = " + (i + 2).ToString()).Length == 0)
                            {
                                dtError.Rows.Add(i + 2, "Column '" + ColumnHeading + "' is blank");
                            }
                            else
                            {
                                dtError.AsEnumerable().Where(r => Convert.ToInt32(r["ROW_NO"]) == (i + 2)).ToList().ForEach(r => r["MESSAGE"] = r["MESSAGE"].ToString() + ", Column '" + ColumnHeading + "' is blank");
                            }
                        }
                    }
                }
            }

            return IsValidated;
        }

        private string GetNodeInnerXml(string InputXml, string NodeName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(InputXml);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(NodeName);

            string NodeInnerXml = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                NodeInnerXml = node.InnerXml;
            }

            return NodeInnerXml;
        }

        private string GetNodeOuterXml(string InputXml, string NodeName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(InputXml);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(NodeName);

            string NodeOuterXml = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                NodeOuterXml = node.OuterXml;
            }

            return NodeOuterXml;
        }

        private void BindUploadList()
        {
            //dgvError.DataSource = null;
            //dgvError.DataBind();

            BusinessLayer.Common.MemberPayment objMemberPayment = new BusinessLayer.Common.MemberPayment();
            DataTable dt = objMemberPayment.GetFeesHeadWisePaymentReport("", 0, "All", DateTime.MinValue, DateTime.MinValue, 0,0);
            DataView dv = new DataView(dt);
            dv.RowFilter = "IsExcelUpload = 'True'";

            dtList = dv.ToTable();

            dgvPaymentReport.DataSource = dtList;
            dgvPaymentReport.DataBind();
            BindFooter();

            if (dtList.Rows.Count > 0)
                btnDownload.Visible = true;
            else
                btnDownload.Visible = false;            
        }

        protected void dgvPaymentReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PaymentId = Convert.ToInt32(dgvPaymentReport.DataKeys[e.Row.RowIndex].Values["PaymentId"].ToString());
                string PaymentNo = dgvPaymentReport.DataKeys[e.Row.RowIndex].Values["PaymentNo"].ToString();

                ((ImageButton)e.Row.FindControl("ImgEdit")).Attributes.Add("OnClick", "openpopup('MemberPayment.aspx?PaymentId=" + PaymentId + "'); return false;");
                Button btnPayment = (Button)e.Row.FindControl("btnPrint");
                btnPayment.Attributes.Add("OnClick", "openpopup('renewal-bill.aspx?PaymentNo=" + PaymentNo + "'); return false;");
            }
        }

        protected void dgvPaymentReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.Common.MemberPayment ObjMemberPayment = new BusinessLayer.Common.MemberPayment();
            int Id = Convert.ToInt32(dgvPaymentReport.DataKeys[e.RowIndex].Values["PaymentId"].ToString());
            ObjMemberPayment.Delete(Id);
            BindUploadList();
            ShowMessage("Payment detail deleted successfully", true);
        }

        protected void dgvPaymentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaymentReport.PageIndex = e.NewPageIndex;
            BindUploadList();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            BindUploadList();
            dgvPaymentReport.AllowPaging = false;
            dgvPaymentReport.DataBind();
            BindFooter();

            PrepareGridViewForExport(dgvPaymentReport);
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 1].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 2].Visible = false;
            dgvPaymentReport.Columns[dgvPaymentReport.Columns.Count - 3].Visible = false;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MemberPaymentUploadList.xls");
           Response.ContentType =  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            dgvPaymentReport.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
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
                if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Button))
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        private void BindFooter()
        {
            decimal TotalAdmissionFees = 0, TotalAdmissionFeesTax = 0, TotalRenewalFees = 0, TotalRenewalFeesTax = 0, Total = 0, TotalDevelopmentFee = 0;

            dtList.AsEnumerable().ToList().ForEach(row =>
            {
                Total += Convert.ToDecimal(row["PaymentAmount"].ToString());
                TotalAdmissionFees += Convert.ToDecimal(row["AdmissionFeesPaymentAmount"].ToString());
                TotalRenewalFees += Convert.ToDecimal(row["RenewalFeesPaymentAmount"].ToString());
                TotalAdmissionFeesTax += Convert.ToDecimal(row["AdmissionFeesTaxPaymentAmount"].ToString());
                TotalRenewalFeesTax += Convert.ToDecimal(row["RenewalFeesTaxPaymentAmount"].ToString());
                TotalDevelopmentFee += Convert.ToDecimal(row["DevelopmentPaymentAmount"].ToString());
            });

            if (dtList.Rows.Count > 0)
            {
                dgvPaymentReport.FooterRow.Cells[6].Text = TotalAdmissionFees.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[7].Text = TotalAdmissionFeesTax.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[8].Text = TotalRenewalFees.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[9].Text = TotalRenewalFeesTax.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[10].Text = TotalDevelopmentFee.ToString("F2");
                dgvPaymentReport.FooterRow.Cells[11].Text = Total.ToString("F2");
            }
        }
    }
}