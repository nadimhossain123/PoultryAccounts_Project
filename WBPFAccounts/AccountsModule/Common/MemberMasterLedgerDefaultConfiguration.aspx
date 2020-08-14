<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MemberMasterLedgerDefaultConfiguration.aspx.cs" Inherits="AccountsModule.Common.MemberMasterLedgerDefaultConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlLedgerType.ClientID%>').selectedIndex == 0) {
                alert("Please Enter Ledger Type");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Master Ledger Default Configuration</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 740px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Ledger Type <span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:DropDownList ID="ddlLedgerType" runat="server" CssClass="dropdownList" Width="190px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="CASH">CASH</asp:ListItem>
                                <asp:ListItem Value="CUST">CUSTOMER</asp:ListItem>
                                <asp:ListItem Value="SUP">SUPPLIER</asp:ListItem>
                                <asp:ListItem Value="PUR">PURCHASE</asp:ListItem>
                                <asp:ListItem Value="REIM">REIMBURSEMENT</asp:ListItem>
                                <asp:ListItem Value="TAX">TAX</asp:ListItem>
                                <asp:ListItem Value="OTH">OTHER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Group <span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:ComboBox ID="ddlAccountsGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="185px" Style="margin-bottom: 10px;" 
                                onselectedindexchanged="ddlAccountsGroup_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Sub Group
                        </td>
                        <td align="left" width="60%">
                            <asp:ComboBox ID="ddlAccountsSubGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="185px" Style="margin-bottom: 10px;" DataValueField="GroupID" DataTextField="GroupName">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Cost Center Applicable
                        </td>
                        <td align="left" width="60%">
                            <asp:CheckBox ID="chkCostCenter" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td width="30%" align="left">
                        </td>
                        <td width="70%" align="right">
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" 
                                onclick="btnShow_Click" />
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                                onclick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
