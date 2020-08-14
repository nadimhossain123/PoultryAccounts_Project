<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTLedger.aspx.cs" Inherits="CollegeERP.Accounts.RPTLedger" Title="Ledger Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '') {
                alert('Enter From Date');
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                alert('Enter To Date');
                return false;
            }
            else { return true; }
        }
    </script>
    <style type="text/css">
        .text
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 12px;
            font-weight: normal;
            padding-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            General Book/Ledger Report</h5>
    </div>
    <div style="width: 500px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="30%" align="left" class="label">
                    Book/Ledger
                </td>
                <td width="30%" align="left">
                    <asp:ComboBox ID="ddlLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="350px">
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
                <td width="30%" align="left" class="label">
                    Select Account (Dr) :
                </td>
                <td width="30%" align="left">
                    <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                        Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="250px"
                        DataValueField="LedgerID" DataTextField="LedgerName" TabIndex="1">
                    </asp:ComboBox>
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
                        Text="Search" OnClientClick="return Validation()"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="95%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="gvGeneralLedger" runat="server" AllowPaging="false" AllowSorting="false"
                    AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="TransDate" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="DocumentNo" HeaderText="Document No"></asp:BoundField>
                        <asp:BoundField DataField="ACDescription" HeaderText="A/c Description" HtmlEncode="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="DrAmount" HeaderText="Amount (Dr.)" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CrAmount" HeaderText="Amount (Cr.)" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left" class="text">
                <asp:Label ID="lblOpeningBal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="text">
                <asp:Label ID="lblTotalDr" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="text">
                <asp:Label ID="lblTotalCr" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="text">
                <asp:Label ID="lblClosingBal" runat="server"></asp:Label>
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
</asp:Content>
