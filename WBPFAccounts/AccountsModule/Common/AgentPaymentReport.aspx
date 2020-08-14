<%@ Page Title="Agent Payment Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="AgentPaymentReport.aspx.cs" Inherits="AccountsModule.Common.AgentPaymentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Area Manager Payment Report</h5>
    </div>
    <div style="width: 99%;">
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="15%" class="label">Area Manager Name</td>
                <td width="15%" class="label">From Date</td>
                <td width="15%" class="label">To Date</td>
                <td width="15%" class="label">Approval Status</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlAgent" runat="server" CssClass="dropdownList" Width="160px" DataValueField="AgentId" DataTextField="AgentName"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderFromDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="dropdownList" Width="150px">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Approved</asp:ListItem>
                        <asp:ListItem>Not Approved</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search"
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
                            GridLines="Both" AllowPaging="false" PageSize="50"
                            ShowFooter="true" OnPageIndexChanging="dgvPaymentReport_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-Wrap="false"
                                    FooterText="<b>Total</b>" />
                                <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}"
                                    NullDisplayText="" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-Wrap="false" />

                                <asp:BoundField DataField="PaymentAmount" HeaderText="Payment Amount"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="AgentCode" HeaderText="Area Manager Code" ItemStyle-Wrap="false" />

                                <asp:BoundField DataField="AgentName" HeaderText="Area Manager Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="IsApproved" HeaderText="Approval Status" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-Wrap="true" />

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
