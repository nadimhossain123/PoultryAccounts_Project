<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTTrailBalance.aspx.cs" Inherits="CollegeERP.Accounts.RPTTrailBalance"
    Title="Trial Balance" %>

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
            Trial Balance</h5>
    </div>
    <div style="width: 550px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
        `   <tr>
                <td width="30%" align="left" class="label">
                    Group Name
                </td>
                <td width="30%" align="left">
                    <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="dropdownList" Width="200px" ></asp:DropDownList>
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
                    With Zero Balance
                </td>
                <td width="30%" align="left">
                    <asp:CheckBox ID="ChkWithZeroBal" runat="server" Checked="true" />
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
    <div style=" height:300px; overflow:scroll">
    <table width="95%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="gvTrialBalnce" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="false" OnRowDataBound="gvTrialBalnce_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Sl" HeaderText="Sl"></asp:BoundField>
                        <asp:BoundField DataField="GroupType" HeaderText="Group Type"></asp:BoundField>
                        <asp:BoundField DataField="GroupName" HeaderText="Group"></asp:BoundField>
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="OpeningBalDr" HeaderText="Opening Bal(Dr)" DataFormatString="{0:n}"
                            ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                        <asp:BoundField DataField="OpeningBalCr" HeaderText="Opening Bal(Cr)" DataFormatString="{0:n}"
                            ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                        <asp:BoundField DataField="WithinDatesDr" HeaderText="Total Debit<br />Within Dates"
                            HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>
                        <asp:BoundField DataField="WithinDatesCr" HeaderText="Total Credit<br />Within Dates"
                            HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>
                        <asp:BoundField DataField="ClosingBalDr" HeaderText="Closing Bal(Dr)" DataFormatString="{0:n}"
                            ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                        <asp:BoundField DataField="ClosingBalCr" HeaderText="Closing Bal(Cr)" DataFormatString="{0:n}"
                            ItemStyle-HorizontalAlign="Right"></asp:BoundField>
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
    </div>
</asp:Content>
