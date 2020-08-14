<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DevelopmentFeeMemberReport.aspx.cs" Inherits="AccountsModule.Common.DevelopmentFeeMemberReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Development Fee Outstanding Report</h5>
    </div>
    <div style="width: 1200px;">
        <%-- <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>--%>
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
                <td align="right">
                    Mobile :
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" MaxLength="20" Width="200px"></asp:TextBox>
                </td>
                <td align="left" width="20%" class="label">
                    Business Type:
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlBusinessType" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="33">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch0" runat="server" CssClass="button" OnClick="btnSearch_Click"
                        Text="Search" Width="120px" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="label" colspan="8">
                    <asp:Label ID="lblTotalMemberCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                        DataKeyNames="MemberId">
                        <Columns>
                            <asp:BoundField DataField="MemberName" HeaderText="Member" />
                            <asp:BoundField DataField="VillageOrStreet" HeaderText="Address" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="PIN" HeaderText="PIN" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                            <%--<asp:BoundField DataField="Email" HeaderText="Email" />--%>
                            <asp:BoundField DataField="Amount" HeaderText="Monthly Fee" />
                            <asp:BoundField DataField="TotalBill" HeaderText="Outstanding Amount" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                No Record Found</p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click"
                        Width="120px" />
                    &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" Width="120px"
                        OnClick="btnPrint_Click" />
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
