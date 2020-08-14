<%@ Page Title="Service Tax Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="ServiceTaxReport.aspx.cs" Inherits="AccountsModule.Common.ServiceTaxReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="Tool1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Service Tax Report</h5>
    </div>
    <table class="table" width="95%" align="center">
        <tr>
            <td class="label" align="left" width="15%">
                Fees Head:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlFeesHead" runat="server" Width="190px" class="dropdownList"
                    DataValueField="FeesHeadId" DataTextField="FeesHeadName">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label" align="left" width="15%">
                From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDate" CssClass="textbox" Width="180px" runat="server" onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="label" align="left" width="15%">
                To Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDate" CssClass="textbox" Width="180px" runat="server" onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                    Enabled="True">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td width="15%">
            </td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" align="center">
        <tr>
            <td>
                <asp:GridView ID="dgvServiceTax" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    GridLines="Both" CellPadding="0" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="PaymentAmount" HeaderText="Amount Received" DataFormatString="{0:F2}"
                            ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="TaxPaymentAmount" HeaderText="Service Tax" DataFormatString="{0:F2}"
                            ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel"
                    OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
