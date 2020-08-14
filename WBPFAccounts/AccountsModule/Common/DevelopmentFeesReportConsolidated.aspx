<%@ Page Title="Development Fees Report Consolidated" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="DevelopmentFeesReportConsolidated.aspx.cs"
    Inherits="AccountsModule.Common.DevelopmentFeesReportConsolidated" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validation() {
            if (document.getElementById("<%= ddlState.ClientID %>").selectedIndex == 0) {
                alert("Please Select State");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Development Fees Report Consolidated</h5>
    </div>
    <div style="width: 1200px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="10%" align="left" class="label">State:
                </td>
                <td align="left" width="15%">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="8%" align="left" class="label">District:
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">Block:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label" colspan="2">Membership Category:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                        Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Till Date :</td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>Member :</td>
                <td>
                    <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td colspan="4">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">
                    <asp:GridView ID="dgvMemberOutstanding" runat="server" AutoGenerateColumns="false"
                        Width="100%" AllowPaging="false" PageSize="30" OnPageIndexChanging="dgvMemberOutstanding_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="OutstandingAmt" HeaderText="Outstanding Amt" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                No Member Record Found
                            </p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
