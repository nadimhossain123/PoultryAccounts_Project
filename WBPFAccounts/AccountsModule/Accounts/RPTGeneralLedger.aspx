<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTGeneralLedger.aspx.cs" Inherits="CollegeERP.Accounts.RPTGeneralLedger"
    Title="General Ledger Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            General Ledger Report</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
    <div style="width: 400px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td width="30%" align="left" class="label">
                    Ledger Name
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txtLedgerNameVw" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%" align="left" class="label">
                    A/c Group
                </td>
                <td width="30%" align="left">
                    
                    <asp:ComboBox ID="ddlGroupVw" runat="server" CssClass="WindowsStyle" AutoPostBack="true" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="150px" OnSelectedIndexChanged="ddlGroupVw_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td width="30%" align="left" class="label">
                    A/c Sub Group
                </td>
                <td width="30%" align="left">
                    <asp:ComboBox ID="ddlSubGroupVw" runat="server" CssClass="WindowsStyle" AutoPostBack="false" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="150px">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td width="30%"></td>
                <td width="30%" align="left">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="button"
                        Text="Search"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnPrint" runat="server" CssClass="button"
                        Text="Print"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="95%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="gdGenLedger" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                    OnRowDataBound="gdGenLedger_RowDataBound" Width="100%" OnRowCommand="gdGenLedger_RowCommand"
                    OnPageIndexChanging="gdGenLedger_PageIndexChanging" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name"></asp:BoundField>
                        <asp:BoundField DataField="LedgerType" HeaderText="Ledger Type"></asp:BoundField>
                        <asp:BoundField DataField="MAINGROUP" HeaderText="Main Group"></asp:BoundField>
                        <asp:BoundField DataField="SUBGROUP" HeaderText="Sub Group"></asp:BoundField>
                        <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="YearlyDR" HeaderText="Yearly DR">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="YearlyCR" HeaderText="Yearly CR">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CostCenterApplied" Visible="False" HeaderText="Cost Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Active" Visible="False" HeaderText="Active">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" CommandName="btnEdit" ImageUrl="~/Images/edit_icon.gif"
                                    runat="server" />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("LedgerID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
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
