<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTCashBankVoucher.aspx.cs" Inherits="CollegeERP.Accounts.RPTCashBankVoucher"
    Title="Receipt/Payment Voucher Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '')
            {
                alert('Enter From Date');
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '')
            {
                alert('Enter To Date');
                return false;
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
            Receipt/Payment Voucher Report</h5>
    </div>
    <div style="width: 550px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
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
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="95%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="gvCBVDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    Width="100%" DataKeyNames="CBVHeaderID">
                    <Columns>
                        <asp:BoundField DataField="VoucherDate" HeaderText="Date" DataFormatString="{0: dd/MM/yyyy}" />
                        <asp:BoundField DataField="CBVoucherNo" HeaderText="Voucher No" HtmlEncode="false" />
                        <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Book" />
                        <asp:BoundField DataField="Payment" HeaderText="Payment" HtmlEncode="false" />
                        <asp:BoundField DataField="DRAmount" HeaderText="Amount Dr" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n}" />
                        <asp:BoundField DataField="CRAmount" HeaderText="Amount Cr" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n}" />
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
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                    OnClick="btnExportExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
