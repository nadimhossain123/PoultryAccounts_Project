<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SMSUnlock.aspx.cs" Inherits="AccountsModule.SMS.SMSUnlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="title">
        <h5>
            Unlock SMS Now</h5></div>
        <asp:Button ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click" />
        <br />
        <br />
    </center>
</asp:Content>
