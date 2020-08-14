<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MembersOutstandingReport.aspx.cs" Inherits="AccountsModule.Accounts.MembersOutstandingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Members Outstanding Report</h5>
    </div>
    <div style="width: 99%;">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="4%" align="left" class="label">
                            State
                        </td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="140px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="4%" align="left" class="label">
                            District
                        </td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="140px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="4%" align="left" class="label">
                            Block
                        </td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="140px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="15%" align="left" class="label">
                            Membership Category :
                        </td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                                Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlMembershipCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" align="left" class="label">
                            Member/Non-member Name
                        </td>
                        <td width="35%" align="left" style="padding-bottom: 5px;" colspan="4">
                            <asp:TextBox ID="txtMemberName" CssClass="textbox_pink" runat="server" Width="140px"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td align="right">
                            
                        </td>
                        <td align="right">
                            <%--<asp:Button ID="btnMemberPrint" runat="server" CssClass="button" Text="Print" OnClick="btnMemberPrint_Click" />--%>
                            <asp:Button ID="btnDownLoad" runat="server" Text="DownLoad" Width="120px" CssClass="button"
                                onclick="btnDownLoad_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            <b>Total Member:
                                <asp:Label ID="lblTotalMember" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="MemberId,active,IsPriority"
                                
                                OnPageIndexChanging="dgvMemberMaster_PageIndexChanging" EnableModelValidation="True"
                                >
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            S.No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                                    <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" />
                                    <asp:BoundField DataField="MemberName" HeaderText="Member/Non-member Name" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:TemplateField HeaderText="Outstanding Details">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Dues") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
