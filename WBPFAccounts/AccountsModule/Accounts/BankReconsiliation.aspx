<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="BankReconsiliation.aspx.cs" Inherits="CollegeERP.Accounts.BankReconsiliation" 
    Title="Bank Reconsiliation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
		
		function Validation()
		{
		    var gv = document.getElementById('<%=gvBnkReconsilition.ClientID%>');
            var rowCount = gv.rows.length - 1;
            
            if (rowCount == 0) {
                alert("No Item For Reconsilation");
                return false;
            }
            else if (!Checkbox_Validation())
		    {
		        alert('Please Select Atleast One Item');
		        return false;
		    }
		    else if (document.getElementById('<% = ddlBRLedgName.ClientID%>').selectedIndex == 0)
		    {
		      alert("Please Select Bank Ledger");
		      return false;  
		    }
		    else if (document.getElementById('<% = txtClearDate.ClientID%>').value == '' )
		    {
		        alert('Enter Clear Date');
		        return false;
		    }
		    else
		    {
		        return confirm('Are You Sure?');
		    } 
		}
		
		function ClearValidation()
		{
		    var gv = document.getElementById('<%=gvBRView.ClientID%>');
            var rowCount = gv.rows.length - 1;
            
            if (rowCount == 0) {
                alert("Please Select Atleast One Item");
                return false;
            }
            else if (!ClearCheckbox_Validation())
		    {
		        alert('Please Select Atleast One Item');
		        return false;
		    }
		    else
		    {
		        return confirm('Are You Sure?');
		    } 
		}
		
		function ClearCheckbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=gvBRView.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox' && arr[i].checked == true) {
                    flag = 1;
                    break;
                }
            }

            if (flag == 0) {
                return false;
            }
            else {
                return true;
            }
        }  
		
		function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=gvBnkReconsilition.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox' && arr[i].checked == true) {
                    flag = 1;
                    break;
                }
            }

            if (flag == 0) {
                return false;
            }
            else {
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
            Bank Reconsilation</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_orange-theme"
        ActiveTabIndex="1">
        <asp:TabPanel runat="server" ID="TabPanelView">
            <ContentTemplate>
                <br />
                <uc3:Message ID="Message1" runat="server" />
                
                <table class="table" width="95%" align="center">
                    <tr>
                        <td class="label" align="left" width="12%">
                            Cheque No. :
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtChequeNo" CssClass="textbox" Width="136px" runat="server" MaxLength="200"></asp:TextBox>
                        </td>
                        <td class="label" align="left" width="12%">
                            Clear Date From :
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtClearFromDate" Width="137px" CssClass="textbox" runat="server"
                                MaxLength="12"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtClearFromDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td class="label" align="left" width="12%">
                            Clear Date To :
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtClearToDate" Width="137px" CssClass="textbox" runat="server"
                                MaxLength="12"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtClearToDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="12%">
                            Bank Ledger Name :
                        </td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlBRLedgNameView" CssClass="dropdownList" runat="server" Width="137px"
                                DataValueField="LedgerID" DataTextField="LedgerName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" colspan="4">
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                                CssClass="button"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="table" width="95%" align="center">
                    <tbody>
                        <tr>
                            <td>
                                <asp:GridView ID="gvBRView" runat="server" OnPageIndexChanging="gvBRView_PageIndexChanging"
                                    Width="100%" AutoGenerateColumns="False" DataKeyNames="BRHeaderID" AllowSorting="false"
                                    AllowPaging="True" PageSize="20">
                                    <Columns>
                                     <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReferenceDocDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
                                        </asp:BoundField>
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
                                        <asp:BoundField DataField="ClearDate" HeaderText="Clear Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    </Columns>
                                    <PagerSettings Mode="Numeric" PageButtonCount="8" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"
                                        CssClass="button" OnClientClick="return ClearValidation();"></asp:Button>
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To Excel"
                                    OnClick="btnExportExcel_Click" />
                            </td>
                        </tr>
                </table>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Show Bank Reconciliation</b>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanelEntry">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <table class="table" width="95%" align="center">
                            <tr>
                                <td class="label" align="left" width="15%">
                                    Bank Ledger :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBRLedgName" CssClass="dropdownList" runat="server" Width="140px"
                                        DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="True"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBRLedgName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" align="left" width="15%">
                                    Clear Date :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtClearDate" CssClass="textbox" Width="140px" runat="server" MaxLength="12"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="cal_Theme1"
                                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtClearDate"
                                        OnClientDateSelectionChanged="" Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                        <h6 align="left" style="color: #00356A;">
                            Reconsilation List</h6>
                        <table class="table" width="95%" align="center">
                            <tr>
                                <td valign="top" width="100%" align="center">
                                    <asp:GridView ID="gvBnkReconsilition" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="false" DataKeyNames="BRHeaderID" AllowPaging="false" GridLines="None"
                                        ShowFooter="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ReferenceDocDate" HeaderText="Voucher Date" DataFormatString="{0:dd MMM yyyy}">
                                            </asp:BoundField>
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
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save Reconsilation"
                                        CssClass="button" OnClientClick="return Validation();"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Enter Bank Reconciliation</b>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
