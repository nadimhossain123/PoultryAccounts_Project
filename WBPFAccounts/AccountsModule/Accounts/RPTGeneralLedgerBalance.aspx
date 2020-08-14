<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTGeneralLedgerBalance.aspx.cs" Inherits="CollegeERP.Accounts.RPTGeneralLedgerBalance"
    Title="General Ledger Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            General Ledger Balance Report</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 400px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="30%" align="left" class="label">
                            Ledger
                        </td>
                        <td width="30%" align="left">
                            <asp:ComboBox ID="ddlLedger" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="160px" OnSelectedIndexChanged="ddlBalanceType_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%" align="left" class="label">
                            From Date
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%" align="left" class="label">
                            To Date
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td width="30%" align="left">
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="button"
                                Text="Search"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvGeneralLedger" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            AllowSorting="false" Width="100%">
                            <Columns>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="Trans_Date"
                                    HeaderText="Date">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TransCode" HeaderText="Doc. No.">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Dr_Amount"
                                    HeaderText="Dr Amount">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Cr_Amount"
                                    HeaderText="Cr Amount">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataFormatString="{0:F2}" DataField="Cr_Amount"
                                    HeaderText="Cr Amount">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataFormatString="{0:F2}" DataField="Cr_Amount"
                                    HeaderText="Cr Amount">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataFormatString="{0:F2}" DataField="Cr_Amount"
                                    HeaderText="Cr Amount">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
