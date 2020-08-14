<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RenewalFeeAdjustmentDetails.aspx.cs" Inherits="AccountsModule.Common.RenewalFeeAdjustmentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Renewal Fee Adjustment Details</h5>
    </div>
    <div style="width: 98%;">
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
                        DataKeyNames="MemberId" 
                        AllowPaging="false" PageSize="20" >
                        <Columns>
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                            <asp:BoundField DataField="Month" HeaderText="Month" />
                            <asp:BoundField DataField="Year" HeaderText="Year" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                            <asp:BoundField DataField="FinalAmount" HeaderText="Adjusted Amount" />
                            <asp:BoundField DataField="TaxAmount" HeaderText="Adjusted Tax Amount" />
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
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
