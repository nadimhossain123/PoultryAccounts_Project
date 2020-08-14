<%@ Page Language="C#" AutoEventWireup="true" Inherits="CollegeERP.Accounts.RPTProfitLoss"
    MasterPageFile="~/MasterAdmin.Master" CodeBehind="RPTProfitLoss.aspx.cs" Title="Balance Sheet Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="Tool1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>P/L & Balance Sheet Report</h5>
    </div>
    <table class="table" width="95%" align="center">
        <tr>
            <td class="label" align="left" width="15%">From Date :
            </td>
            <td align="left">
                <asp:TextBox ID="txtfrmdate" CssClass="textbox" Width="140px" runat="server" MaxLength="12"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtfrmdate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="label" align="left" width="15%">To Date :
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDate" CssClass="textbox" Width="140px" runat="server" MaxLength="12"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td width="15%"></td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <asp:TabContainer ID="tcGeneralLedger" runat="server" ActiveTabIndex="1" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="tpView" runat="server">
            <ContentTemplate>
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Literal ID="ltrPLHeading" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" width="100%">
                            <asp:GridView ID="gvPL" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ExpenseName" HeaderText="Expenditure" HtmlEncode="false"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="ExpenseAmountDr" HeaderText="Amount(Dr)" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}" HtmlEncode="false" />
                                    <asp:BoundField DataField="ExpenseAmountCr" HeaderText="Amount(Cr)" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}" HtmlEncode="false" />
                                    <asp:BoundField DataField="ExpenseTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="IncomeName" HeaderText="Income" HtmlEncode="false" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="IncomeAmountDr" HeaderText="Amount(Dr)" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}" HtmlEncode="false" />
                                    <asp:BoundField DataField="IncomeAmountCr" HeaderText="Amount(Cr)" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}" HtmlEncode="false" />
                                    <asp:BoundField DataField="IncomeTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
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
                            <asp:Button ID="btnPLPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPLPrint_Click" />
                            &nbsp;
                            <asp:Button ID="btnPLExport" runat="server" CssClass="button" Text="Export To Excel"
                                OnClick="btnPLExport_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <HeaderTemplate>
                <b>P/L Report </b>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <ContentTemplate>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Literal ID="ltrBalanceSheetHeading" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" width="100%">
                            <asp:GridView ID="gvBalanceSheet" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                Width="100%" OnRowDataBound="gvBalanceSheet_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="LiabilitiesName" HeaderText="Liabilities" HtmlEncode="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount1" runat="server" Text='<%# Bind("LiabilitiesAmount", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LiabilitiesTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                                        HtmlEncode="False">
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AssetsName" HeaderText="Assests" HtmlEncode="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamnt2" runat="server" Text='<%# Bind("AssetsAmount", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AssetsTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                                        HtmlEncode="False">
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                            <asp:Button ID="btnBalanceSheetPrint" runat="server" CssClass="button" Text="Print"
                                OnClick="btnBalanceSheetPrint_Click" />
                            &nbsp;
                            <asp:Button ID="btnBalanceSheetExport" runat="server" CssClass="button" Text="Export to Excel"
                                OnClick="btnBalanceSheetExport_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Balance Sheet </b>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>&nbsp;
</asp:Content>
