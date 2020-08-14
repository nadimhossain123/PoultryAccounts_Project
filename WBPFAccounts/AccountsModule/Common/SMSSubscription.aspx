<%@ Page Title="SMS Subscription" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="SMSSubscription.aspx.cs" Inherits="AccountsModule.Common.SMSSubscription" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .div
        {
            -moz-border-radius: 10px/10px;
            -webkit-border-radius: 10px 10px;
            border-radius: 2px;
            width: 10px;
            height: 10px;
            position:absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            SMS Subscription of Member/Non-member</h5>
    </div>
    <div style="width: 97%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc3:Message ID="Message" runat="server" />
                <br />
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
                        <td width="35%" align="left" style="padding-bottom: 5px;" colspan="6">
                            <asp:TextBox ID="txtMemberName" CssClass="textbox_pink" runat="server" Width="140px"></asp:TextBox>
                        </td>
                        <td align="left" colspan="2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="MemberId,active,IsPriority" AllowPaging="true" PageSize="20" OnPageIndexChanging="dgvMemberMaster_PageIndexChanging"
                                OnRowDataBound="dgvMemberMaster_RowDataBound" OnRowCommand="dgvMemberMaster_RowCommand">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSubscribeAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSubscribeAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSubscribe" runat="server" AutoPostBack="True" OnCheckedChanged="chkSubscribe_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            S.No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriority" runat="server" style="white-space:nowrap"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MemberSMSCategoryName" HeaderText="SMS Category" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                                    <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" />
                                    <asp:BoundField DataField="MemberName" HeaderText="Member/Non-member Name" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="IsMember" HeaderText="Member" />
                                    <asp:BoundField DataField="IsGovtMember" HeaderText="Govt." />
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnBlock" runat="server" CssClass="button" Text="Block"
                                                onclick="btnBlock_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="15px" />
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
                <tr>
                    <td>
                        <div class="div" style="border: solid 1px #E5D254; background:#E5D254;"></div>&nbsp;&nbsp;&nbsp;&nbsp; Blocked Members
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="div" style="border: solid 1px #f93b3f; background:#f93b3f;"></div>&nbsp;&nbsp;&nbsp;&nbsp; Inactive Members
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>H.P.</b>&nbsp;&nbsp;&nbsp;&nbsp; High Priority
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>N.P.</b>&nbsp;&nbsp;&nbsp;&nbsp; Normal Priority
                    </td>
                </tr>                
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
