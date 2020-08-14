<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAgent.Master" AutoEventWireup="true"
    CodeBehind="SMSMemberView.aspx.cs" Inherits="AccountsModule.Common.SMSMemberView" EnableEventValidation="false" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script type="text/javascript">
        function openpopup(poplocation) {
            var popposition = 'left = 200, top=50, width=1000,align=center, height=600,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
    </script>--%>
    <%--    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtMemberName.ClientID %>').value == '')
                return ShowMsg('Enter Member name');

            else if (document.getElementById('<%=txtMobileNo.ClientID %>').value == '')
                return ShowMsg('Enter Mobile No');

            else if (document.getElementById('<%=ddlRegType.ClientID %>').selectedIndex == 0)
                return ShowMsg('Select Member Type');

            else if (document.getElementById('<%=chkIsActive.ClientID %>').checked == false)
                return ShowMsg('Select Member Status');

            else if (document.getElementById('<%=txtStartDate.ClientID %>').value == '')
                return ShowMsg('Enter Start Date');

            else if (document.getElementById('<%=txtEndDate.ClientID %>').value == '')
                return ShowMsg('Enter End Date');

            else
                return confirm("Are You Sure?");
}

function ShowMsg(str) {
    alert(str);
    return false;
}

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <div class="title">
        <h5>Search Member</h5>
    </div>

    <br />

    <div style="width: 98%;">
        <table width="100%">
            <tr>
                <td>Member Name :</td>
                <td>Mobile No</td>
                <td>Registration Type</td>
                <td>District</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSearchMemberName" runat="server" CssClass="textbox" MaxLength="200"
                        Width="250px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtSearchMobileNo" runat="server" MaxLength="20"
                        CssClass="textbox" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSearchRegType" runat="server" CssClass="dropdownList"
                        Width="100px">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                        <asp:ListItem Text="PAID" Value="2"></asp:ListItem>
                        <asp:ListItem Text="FREE" Value="3"></asp:ListItem>
                        <asp:ListItem Text="GOVT" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" Width="150px" DataValueField="DistrictId" DataTextField="DistrictName"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"
                        OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="dgvSMSMember" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="SMSMemberId" OnPageIndexChanging="dgvSMSMember_PageIndexChanging"
                        CellPadding="4" AllowPaging="True" PageSize="50" OnRowDataBound="dgvSMSMember_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SL" HeaderText="Sl No." />
                            <asp:BoundField DataField="FullMemberName" HeaderText="Parent Member" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="MemberType" HeaderText="Member Type" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0: dd/MM/yyyy}" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0: dd/MM/yyyy}" />
                            <asp:BoundField DataField="SMSRenewalStatus" HeaderText="Renewal Status" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

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
    </div>
</asp:Content>
