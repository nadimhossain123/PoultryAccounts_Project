<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MemberBillDetails.aspx.cs" Inherits="AccountsModule.Common.MemberBillDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlMonth.ClientID %>').selectedIndex == 0) {
                alert('Please Select Month');
                return false;
            }
            else if (document.getElementById('<%=ddlYear.ClientID %>').selectedIndex == 0) {
                alert('Please Select Year');
                return false;
            }
            else if (document.getElementById('<%=ddlState.ClientID %>').selectedIndex == 0) {
                alert('Please Select State');
                return false;
            }
            else if (document.getElementById('<%=ddlDistrict.ClientID %>').selectedIndex == 0) {
                alert('Please Select District');
                return false;
            }
            else {
                return confirm('Are You Sure');
            }
        }
    </script>
    <script type="text/javascript">
        function openpopup(poplocation) {
            var popposition = 'left = 200, top=50, width=1000,align=center, height=600,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Monthly Member Bill Details</h5>
    </div>
    <div style="width: 99%;">
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
                <td align="left" colspan="2">
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
                <td width="7%" align="left" class="label" colspan="2">
                    Membership Category:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                        Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="7%" align="left" class="label">
                    Month:
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="Month" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="2">
                    <%--OnClientClick="return Validation();"--%>
                    Year :
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="150px">
                        <asp:ListItem Value="0" Text="--SELECT YEAR--"></asp:ListItem>
                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="2">
                    &nbsp;
                </td>
                <td align="left" colspan="2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation();"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <%--<tr>
                <td align="left">
                    <asp:CheckBox ID="ChkSelectAll" runat="server" Text="Select All" onClick="return CheckAll(this);" />
                </td>
            </tr>--%>
            <tr>
                <td align="left">
                    <asp:GridView ID="dgvMemberMonthlyBill" runat="server" AutoGenerateColumns="false"
                        Width="100%" DataKeyNames="BillId" AllowPaging="false" 
                        onrowdatabound="dgvMemberMonthlyBill_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="VillageOrStreet" HeaderText="Address" />
                            <asp:BoundField DataField="Month" HeaderText="Month" />
                            <asp:BoundField DataField="Year" HeaderText="Year" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="FinalAmount" HeaderText="Amount" />
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Renewal Bill">
                                <ItemTemplate>
                                    <asp:Button ID="btnPayment" runat="server" Text="Print Bill" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                No Member Record Found</p>
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
                    <asp:Button ID="btnSendEmail" runat="server" CssClass="button" Text="Send Mail" 
                        OnClientClick="return SendMailValidation();" onclick="btnSendEmail_Click"
                         />&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
