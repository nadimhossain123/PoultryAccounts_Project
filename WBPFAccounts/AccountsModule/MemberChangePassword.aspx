<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterMember.Master"
    AutoEventWireup="true" CodeBehind="MemberChangePassword.aspx.cs" Inherits="AccountsModule.MemberChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtPassword.ClientID%>'), "Password", 1)) return false;
            return true;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Change Password</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 680px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            New Password:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" Width="180px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="left" width="50%">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Change Password"
                                OnClientClick="return Validation();" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
