<%@ Page Title="Send Custome SMS" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CustomeSMS.aspx.cs" Inherits="AccountsModule.Common.CustomeSMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Send Custome SMS</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 840px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="5%" class="label">
                            Member SMS Category
                        </td>
                        <td align="left" width="20%" class="label">
                            <asp:DropDownList ID="ddlMemberSMSCategory" runat="server" CssClass="dropdownList" Width="140px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" width="60%" colspan="2">
                            <asp:CheckBox ID="chkIsGovtMember" runat="server" Text="Include Govt. Members" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            Enter Mobile Numbers: (Keep this field blank to fetch mobile numbers from database)<br />
                            <asp:HiddenField ID="Hidden1" runat="server" />
                            <asp:TextBox ID="txtMobiles" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left">
                            Message Body:<span class="req">*</span><br />
                            <asp:TextBox ID="txtMessageBody" runat="server" TextMode="MultiLine" Height="150px"
                                Width="100%"></asp:TextBox><br /><br />
                            <asp:Button ID="btnSentSMS" runat="server" Text="Sent SMS" CssClass="button" 
                                Style="float: right" onclick="btnSentSMS_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
