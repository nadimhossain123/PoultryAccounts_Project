<%@ Page Title="Fees Info" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="FeesInfo.aspx.cs" Inherits="AccountsModule.Accounts.FeesInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Fees Info
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Membership Category
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                                    Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlMembershipCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <h6 align="left" style="color: #00356A;">
                    Fees Details</h6>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left">
                                <%--<a rel="divAddFeesHead" class="poplight" onclick="popUp('350','divAddFeesHead')">New
                                    Fees Head </a>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--   <asp:Panel ID="panelfeesDetails" runat="server">
                                </asp:Panel>--%>
                                <asp:PlaceHolder ID="panelfeesDetails1" runat="server"></asp:PlaceHolder>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
