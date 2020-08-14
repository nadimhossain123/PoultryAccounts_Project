<%@ Page Title="SMS API Config" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SMSAPIConfig.aspx.cs" Inherits="AccountsModule.Common.SMSAPIConfig" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>SMS API Configuration</h5>
    </div>
    <div style="width: 740px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td>
                    <asp:RadioButtonList ID="radioListAPI" runat="server" DataValueField="APIId" DataTextField="APIName" RepeatColumns="1" RepeatDirection="Vertical" style="font-size:15px; padding:5px;"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
