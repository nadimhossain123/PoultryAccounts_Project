<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="AccountsModule.Login" Title="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // window.onload=CheckBrowser();
        function CheckBrowser() {
            var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome');
            if (is_chrome == -1)
                window.location.href = 'GoogleCrome.htm';
        }
        function Validation() {
            if (document.getElementById('<%=txtUserName.ClientID%>').value.trim() == '') {
                alert("Please Enter User ID");
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID%>').value.trim() == '') {
                alert("Please Enter Password");
                return false;
            }
            else { return true; }
        }        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="TC1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <br />
    <br />
    <div align="center">
        <div id="login-box">
            <h2>
                Login</h2>
            <br />
            <asp:RadioButtonList ID="rbtnLoginAs" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                <asp:ListItem Value="Member" Text="Member"></asp:ListItem>
                <asp:ListItem Value="Agent" Text="Area Manager"></asp:ListItem>
            </asp:RadioButtonList>
            <div id="login-box-name" style="margin-top: 1px;">
                Username</div>
            <div id="login-box-field" style="margin-top: 1px;">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-login" Width="170px"></asp:TextBox>
            </div>
            <div id="login-box-name">
                Password</div>
            <div id="login-box-field">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-login" Width="170px"
                    TextMode="Password"></asp:TextBox>
            </div>
            <br />
            <br />
            <br />
            <asp:ImageButton ID="btnLogIn" runat="server" ImageUrl="~/Images/login-btn.png" OnClientClick="javascript:return Validation()"
                OnClick="btnLogIn_Click" />
            <br />
            <br />
        </div>
        <br />
        <br />
        New Member? <a href="Common/AddEditMemberMaster.aspx?Back=../Login.aspx">Register Now</a>
    </div>
    <a id="lnk" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopUp" runat="server" TargetControlID="lnk" PopupControlID="pnlPopup"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" Width="350px" Height="250px" CssClass="modalPopUp"
        ScrollBars="Vertical">
        <div class="title">
            <h5>
                Select Module</h5>
        </div>
        <table width="96%" align="center" class="table">
            <tr>
                <td>
                    <asp:RadioButtonList ID="radioListModule" runat="server" RepeatDirection="Vertical"
                        RepeatColumns="1">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnContinue" runat="server" CssClass="button" Text="Continue" OnClick="btnContinue_Click" />&nbsp;
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
