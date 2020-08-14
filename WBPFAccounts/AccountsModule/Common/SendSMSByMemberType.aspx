<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SendSMSByMemberType.aspx.cs" Inherits="AccountsModule.Common.SendSMSByMemberType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Send SMS to different Member Type</h5>
    </div>
    <div style="width: 840px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">&nbsp To, Member Type: <span class="req">*</span>
                </td>
                <td align="left" width="20%" class="label">
                    <asp:DropDownList ID="ddlRegType" runat="server" CssClass="dropdownList" Width="100%">
                        <asp:ListItem Text="ALL" Value="1"></asp:ListItem>
                        <asp:ListItem Text="PAID" Value="2"></asp:ListItem>
                        <asp:ListItem Text="FREE" Value="3"></asp:ListItem>
                        <asp:ListItem Text="GOVT" Value="4"></asp:ListItem>
                        <asp:ListItem Text="CORE" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%"></td>
                <td align="left" width="20%"></td>
                <td align="left" width="20%"></td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">Enter Mobile Numbers: (Keep this field blank to fetch mobile numbers from database)<br />
                    <asp:HiddenField ID="Hidden1" runat="server" />
                    <asp:TextBox ID="txtMobiles" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">Message Body:<span class="req">*</span><br />
                    <asp:TextBox ID="txtMessageBody" runat="server" TextMode="MultiLine" Height="250px"
                        Width="100%"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="btnSentSMS" runat="server" Text="Sent SMS" CssClass="button" Style="float: right"
                        OnClick="btnSentSMS_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
