<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="NECCSendSMS.aspx.cs" Inherits="AccountsModule.Common.NECCSendSMS" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>NECC Send SMS</h5>
    </div>
    <%--<asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>--%>
    <div style="width: 840px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table" style="display: none">
            <tr>
                <td align="left" width="20%" class="label">Select a date: <span class="req">*</span>
                </td>
                <td align="left" width="20%" class="label">
                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="140px">
                        <asp:ListItem Value="0">MONTH</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <%--<asp:Button ID="btnGetPrice" runat="server" Text="Get Price" CssClass="button"
                        OnClick="btnGetPrice_Click" />--%>
                </td>
            </tr>
            <%--<tr>
                        <td align="left" width="20%" class="label">
                            Member SMS Category
                        </td>
                        <td align="left" width="20%" class="label">
                         <asp:DropDownList ID="ddlMemberSMSCategory" runat="server" CssClass="dropdownList" Width="140px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>--%>
        </table>
        <table width="100%" align="center" class="table">
            <%--<tr>
                        <td align="left" width="60%">
                            <asp:CheckBox ID="chkIsGovtMember" runat="server" Text="Include Govt. Members" />
                        </td>
                    </tr>--%>
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
                    <asp:Button ID="btnSentNECC" runat="server" Text="Sent SMS" CssClass="button" Style="float: right"
                        OnClick="btnSentNECC_Click" />
                </td>
            </tr>
        </table>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
