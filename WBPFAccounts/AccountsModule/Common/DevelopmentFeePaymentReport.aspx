<%@ Page Title="Development Fee Payment Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="DevelopmentFeePaymentReport.aspx.cs" Inherits="AccountsModule.Common.DevelopmentFeePaymentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Development Fee Payment Report</h5>
    </div>
    <div style="width: 99%;">
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="14%" class="label">From Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderFromDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="14%" class="label">To Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="label">Payment No:
                </td>
                <td>
                    <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="textbox" Width="290px"></asp:TextBox>
                </td>
                <td class="label">Payment Mode:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="200px">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Bank</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>Bank Draft</asp:ListItem>
                        <asp:ListItem>Online Payment</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">Cash/Bank Ledger:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCashBankLedger" runat="server" CssClass="dropdownList" Width="300px"
                        DataValueField="LedgerId" DataTextField="LedgerName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">Member Name:
                </td>
                <td colspan="3">
                    <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="300px"
                        DropDownStyle="DropDown" Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        DataValueField="MemberId" DataTextField="MemberName">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Payment Report"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" align="center">
            <tr>
                <td>
                    <asp:Panel ID="Pnl" runat="server" Width="1200px" ScrollBars="Horizontal">
                        <asp:GridView ID="dgvPaymentReport" runat="server" Width="100%" AutoGenerateColumns="false"
                            GridLines="Both" DataKeyNames="PaymentId" AllowPaging="true" PageSize="30"
                            ShowFooter="true" OnPageIndexChanging="dgvPaymentReport_PageIndexChanging" 
                            >
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-Wrap="false"
                                    FooterText="<b>Total</b>" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}"
                                    NullDisplayText="" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="DevelopmentFeesPaymentAmount" HeaderText="Payment Amount"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Report"
                        OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
