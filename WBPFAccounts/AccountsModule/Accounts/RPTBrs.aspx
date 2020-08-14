<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTBrs.aspx.cs" Inherits="CollegeERP.Accounts.RPTBrs" Title="BRS Report"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
		
		function Validation()
		{
		    if (document.getElementById('<% = ddlBRLedgName.ClientID%>').selectedIndex == 0)
		    {
		      alert("Please Select Bank Ledger");
		      return false;  
		    }
		    else if (document.getElementById('<% = txtToDate.ClientID%>').value == '' )
		    {
		        alert('Enter Date');
		        return false;
		    }
		    else
		    {
		        return true;
		    } 
		}
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Bank Reconsilation Report</h5>
    </div>
    <table class="table" width="95%" align="center">
        <tr>
            <td class="label" align="left" width="15%">
                Bank Ledger :
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlBRLedgName" CssClass="dropdownList" runat="server" Width="140px"
                    DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label" align="left" width="15%">
                To Date :
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
            <td width="15%">
            </td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table class="table" width="95%" align="center">
        <tr>
            <td valign="top" width="100%" align="center">
                <asp:GridView ID="gvBnkReconsilition" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowSorting="false" AllowPaging="false" GridLines="None" ShowFooter="false">
                    <Columns>
                        <asp:BoundField DataField="ReferenceDocDate" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="DocumentNo" HeaderText="Document No" HtmlEncode="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No"></asp:BoundField>
                        <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date"></asp:BoundField>
                        <asp:BoundField DataField="DrAmount" HeaderText="Amount Dr." DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CrAmount" HeaderText="Amount Cr." DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ClearDate" HeaderText="Clear Date" />
                    </Columns>
                    <EmptyDataTemplate>
                        <h2>
                            No Item For Reconsilation</h2>
                    </EmptyDataTemplate>
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
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="button"
                    OnClick="btnExportToExcel_Click"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
