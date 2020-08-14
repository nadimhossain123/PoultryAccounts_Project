<%@ Page Title="Member Approve" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberMasterApprove.aspx.cs" Inherits="AccountsModule.Common.MemberMasterApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member/Non-member Approval</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 1200px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="10%" align="left" class="label">
                            State:
                        </td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="8%" align="left" class="label">
                            District:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="7%" align="left" class="label">
                            Block:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td width="7%" align="left" class="label">
                            Membership Category:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                                Width="160px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left" class="label">
                            Name:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td align="left" colspan="5">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Approved Member Name:
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="txtApprovedMemberName" runat="server" CssClass="textbox_pink" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Approved Member Code:
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="txtApprovedMemberCode" runat="server" CssClass="textbox_pink" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="MemberId" OnRowEditing="dgvMemberMaster_RowEditing" 
                                AllowPaging="false" onrowcommand="dgvMemberMaster_RowCommand" 
                                onrowdatabound="dgvMemberMaster_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="MemberName" HeaderText="Name" />
                                    <asp:BoundField DataField="StateName" HeaderText="State" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                                    <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="IsMember" HeaderText="Member" />
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Approve">
                                        <ItemTemplate>
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" OnClientClick="return confirm('Do you want to approve?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <p>
                                        No Pending Approval</p>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
