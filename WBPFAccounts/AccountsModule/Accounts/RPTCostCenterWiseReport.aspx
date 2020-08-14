<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTCostCenterWiseReport.aspx.cs" Inherits="CollegeERP.Accounts.RPTCostCenterWiseReport"
    Title="Cost Center Wise Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlCostCenter.ClientID%>').selectedIndex == 0){
                alert("Please Select Cost Center"); return false;
            }
            else {return true;}
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Cost Center Wise Report</h5>
    </div>
    <div style="width: 950px;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" width="15%" class="label">
                   From Date:
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="150px" CssClass="textbox">
                    </asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" width="15%" class="label">
                    To Date:
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtToDate" runat="server" Width="150px" CssClass="textbox">
                    </asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" width="15%" class="label">
                    Cost Center:
                </td>
                <td align="left" width="25%">
                    <asp:DropDownList ID="ddlCostCenter" runat="server" Width="200px" CssClass="dropdownList">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="dgvReport" runat="server" Width="100%" AutoGenerateColumns="false"
                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" ShowFooter="true" 
                        onrowdatabound="dgvReport_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" />
                            <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" />
                            <asp:TemplateField HeaderText="Ledger Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDrAmount" runat="server" Text='<%# Eval("LedgerName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <div style="text-align:right;">
                            <asp:Label ID="lblTotalText" Text="Total = " runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DRAmount">
                            <ItemTemplate>
                                <asp:Label ID="lblDrAmount" runat="server" Text='<%# Eval("DRAmount").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblDrTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CRAmount">
                            <ItemTemplate>
                                <asp:Label ID="lblCrAmount" runat="server" Text='<%# Eval("CRAmount").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCrTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        </Columns>
                        <EmptyDataTemplate>
                            <h2>
                                Sorry! No Record Found.</h2>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                        OnClick="btnExportExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
