<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupChangeBlock.aspx.cs"
    Inherits="AccountsModule.Common.PopupChangeBlock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Block</title>
    <link href="/Styles/reset.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/style.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/blue.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/Control.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">
        function PageRedirect() {
            window.opener.document.location.href = 'MemberMasterInfo.aspx';
            //            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="margin: 0px 20px">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <div style="clear: both">
        </div>
        <div style="clear: both">
        </div>
        <table width="49%" align="left">
            <tr>
                <td align="left" width="35%" class="label">
                    New Member Code
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtMemberCode" CssClass="textbox_pink" runat="server" Width="260px"
                        Text="AUTO GENERATED" ReadOnly="true" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New Village/Street <span class="req">*</span>
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtVillageOrStreet" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New Plot No.
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtPlotNo" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New Khaitan No.
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtKhaitanNo" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New Mouza
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtMouza" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New JL No.
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtJLNo" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New P.O. <span class="req">*</span>
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtPO" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="7"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="49%" align="right">
            <tr>
                <td align="left" width="35%" class="label">
                    New P.S. <span class="req">*</span>
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtPS" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="35%" class="label">
                    New PIN <span class="req">*</span>
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtPIN" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="60%" class="label">
                    New Block <span class="req">*</span>
                </td>
                <td align="left" width="40%">
                    <asp:ComboBox ID="ddlBlock" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                        TabIndex="10" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        Width="185px" Style="margin-bottom: 4px; margin-top: -6px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="60%" class="label">
                    New District <span class="req">*</span>
                </td>
                <td align="left" width="40%">
                    <asp:ComboBox ID="ddlDistrict" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                        TabIndex="11" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        Width="185px" Style="margin-bottom: 4px;">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="60%" class="label">
                    New State <span class="req">*</span>
                </td>
                <td align="left" width="40%">
                    <asp:ComboBox ID="ddlState" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                        TabIndex="12" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        Width="185px" Style="margin-bottom: 4px;">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="60%" class="label">
                    New Effective Date <span class="req">*</span>
                </td>
                <td align="left" width="40%">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textbox_required" Width="180px"
                        onkeydown="return false" TabIndex="13"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtEffectiveDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="60%" class="label">
                    New Opening Balance <span class="req">*</span>
                </td>
                <td align="left" width="40%">
                    <asp:TextBox ID="txtOpBal" runat="server" CssClass="textbox_required" Width="180px"
                        TabIndex="14"></asp:TextBox>
                    <asp:ComboBox ID="ddlDRorCR" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="50px" Style="position: relative;
                        top: -4px; margin-left: 2px;" TabIndex="15">
                    </asp:ComboBox>
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="100%" align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" TabIndex="16" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
