<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="website-committee-report.aspx.cs" Inherits="AccountsModule.Common.website_committee_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://www.wbpoultryfederation.org/styles/style.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" href="http://www.wbpoultryfederation.org/styles/grid.css"
        type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">

        function Validation() {
            if (document.getElementById("<%= txtToDate.ClientID %>").value.trim() == '') {
                alert("Please Enter To Date");
                return false;
            }
            <%--else if (document.getElementById("<%= ddlDistrict.ClientID %>").selectedIndex == 0) {
                alert("Please Select District");
                return false;
            }
            else if (document.getElementById("<%= ddlBlock.ClientID %>").selectedIndex == 0) {
                alert("Please Select Block");
                return false;
            }--%>
            else {
                return true;
            }
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 900px;">
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="7%" align="left" class="label">
                    State:
                </td>
                <td width="8%" align="left" class="label">
                    District:
                </td>
                <td width="7%" align="left" class="label">
                    Block:
                </td>
                <td width="7%" align="left" class="label">
                    Membership Category:
                </td>
                <td width="7%" align="left" class="label">
                    To Date:
                </td>
            </tr>
            <tr>
                <td width="7%" align="left" class="label">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="8%" align="left" class="label">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="10%" align="left" class="label" colspan="2">
                    Member Name:
                </td>
                <td width="7%" align="left" class="label">
                    Member Code:
                </td>
                <td width="7%" align="left" class="label">
                    Mobile No:
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" width="10%" colspan="2"><asp:TextBox ID="txtMemberName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox></td>
                <td align="left"><asp:TextBox ID="txtMemberCode" runat="server" CssClass="textbox"></asp:TextBox></td>
                <td align="left"><asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox"></asp:TextBox></td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">
                    <asp:GridView ID="dgvMemberOutstanding" runat="server" AutoGenerateColumns="false"
                        Width="100%" DataKeyNames="MemberId" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <%--<asp:BoundField DataField="AdmissionFees" HeaderText="Admission Fees" />
                            <asp:BoundField DataField="AdmissionFeesTax" HeaderText="Admission Fees Tax" />--%>
                            <asp:BoundField DataField="RenewalFees" HeaderText="Renewal Fees" />
                            <asp:BoundField DataField="RenewalFeesTax" HeaderText="Renewal Fees Tax" />
                            <asp:BoundField DataField="Total" HeaderText="Total" />
                            <asp:BoundField DataField="DevFeeAmount" HeaderText="Development Fees" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p>No Member Record Found</p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="grid-header" />
                        <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle CssClass="grid-row-alt" />
                        <PagerStyle CssClass="pagin" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>

