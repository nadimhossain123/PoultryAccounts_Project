using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using BusinessLayer.Accounts;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace BusinessLayer.Common
{
    public class Excel:Page
    {
        public Excel()
        {
        }

        public static void SaveExcel(string[] header, System.Web.UI.WebControls.GridView DataGridView, string[] footer, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xls", filename));
            HttpContext.Current.Response.ContentType = "application/excel";

            Table table = new Table();
            TableRow row;
            TableCell cell;
            int ColCount = DataGridView.HeaderRow.Cells.Count;

            //Header
            if (header.Length > 0)
            {
                for (int i = 0; i < header.Length; i++)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    cell.ColumnSpan = ColCount;
                    cell.Text = header[i].Trim();
                    cell.Wrap = true;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }
            }
            //Grid
            for (int i = 0; i < DataGridView.HeaderRow.Cells.Count; i++)
            {
                DataGridView.HeaderRow.Cells[i].Style.Add("background", "#4CB449");
                DataGridView.HeaderRow.Cells[i].Style.Add("color", "#fff");
                DataGridView.HeaderRow.Cells[i].Style.Add("text-decoration", "none");
            }

            table.GridLines = GridLines.Both;
            if (DataGridView.HeaderRow != null)
            {
                table.Rows.Add(DataGridView.HeaderRow);
            }



            foreach (GridViewRow DgvRow in DataGridView.Rows)
            {
                if (DgvRow.Controls.GetType() == typeof(CheckBox))
                {
                    //gv.Controls.Remove(gv.Controls[i]);
                    //gv.Controls.AddAt(i, l);
                }
                else
                {
                    DgvRow.VerticalAlign = VerticalAlign.Top;
                    table.Rows.Add(DgvRow);
                }
            }

            if (DataGridView.FooterRow != null)
            {
                table.Rows.Add(DataGridView.FooterRow);
            }


            //Footer
            if (footer.Length > 0)
            {
                for (int i = 0; i < footer.Length;)
                {
                    row = new TableRow();
                    if (row.Controls.GetType() == typeof(CheckBox))
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        cell = new TableCell();
                        cell.ColumnSpan = ColCount;
                        cell.Text = footer[i].Trim();
                        cell.Wrap = true;
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.VerticalAlign = VerticalAlign.Middle;
                        cell.Font.Bold = true;
                        row.Cells.Add(cell);
                        table.Rows.Add(row);
                        i++;
                    }
                    
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        table.RenderControl(htw);
                        HttpContext.Current.Response.Write(sw.ToString());
                        HttpContext.Current.Response.End();
                    }
                }

            }
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes';"; //for above excel 2007  

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch (Exception ex)
                {
                }
            }
            return dtexcel;
        }

        public bool IsDecimal(string value)
        {
            decimal number;
            if (Decimal.TryParse(value, out number))
                return true;
            else
                return false;
        }

        public bool IsDate(string value)
        {
            string[] formats = {"yyyy-MM-dd"};

            DateTime dateValue;

            if (DateTime.TryParseExact(value, formats,
                              new CultureInfo("en-US"),
                              DateTimeStyles.None,
                              out dateValue))
                return true;
            else
                return false;
        }

        public void BulkUpload(DataTable dtUpload, string TableName)
        {
            DataAccess.Common.Excel.BulkUpload(dtUpload, TableName);
        }

        public string ValidatePaymentExcelUpload(int UserId)
        {
            return DataAccess.Common.Excel.ValidatePaymentExcelUpload(UserId);
        }

        public string ValidateSMSPaymentExcelUpload(int UserId)
        {
            return DataAccess.Common.Excel.ValidateSMSPaymentExcelUpload(UserId);
        }
    }
}
