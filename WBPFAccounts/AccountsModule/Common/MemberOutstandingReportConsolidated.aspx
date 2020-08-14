<%@ Page Title="Consolidated Outstanding Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberOutstandingReportConsolidated.aspx.cs"
    Inherits="AccountsModule.Common.MemberOutstandingReportConsolidated" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validation() {
            if (document.getElementById("<%= ddlState.ClientID %>").selectedIndex == 0) {
                alert("Please Select State");
                return false;
            }
            else if (document.getElementById("<%= ddlDistrict.ClientID %>").selectedIndex == 0) {
                alert("Please Select District");
                return false;
            }
            else if (document.getElementById("<%= ddlBlock.ClientID %>").selectedIndex == 0) {
                alert("Please Select Block");
                return false;
            }
            else {
                return true;
            }
        }

        function CheckAll(obj) {
            var Grid = document.getElementById("<%= dgvMemberOutstanding.ClientID %>");
            var Rows = Grid.rows;

            for (var i = 1; i < Rows.length; i++) {
                Rows[i].getElementsByTagName("input")[0].checked = obj.checked;
            }
        }

        function SendMailValidation() {
            var Grid = document.getElementById("<%= dgvMemberOutstanding.ClientID %>");
            var Rows = Grid.rows;
            var checkedItem = 0;

            for (var i = 1; i < Rows.length; i++) {
                if (Rows[i].getElementsByTagName("input")[0].checked) {
                    checkedItem++;
                }
            }

            if (checkedItem == 0) {
                alert("Please Select Member");
                return false;
            }
            else {
                return confirm("Do you want to send mail?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Consolidated Outstanding Report</h5>
    </div>
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
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" 
                        onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" 
                        TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" colspan="2">
                        <%--OnClientClick="return Validation();"--%>
                    To Date :</td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <%--<td align="left">
                    Member :</td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="150px"></asp:TextBox>
                </td>--%>
                <td align="left" width="20%" class="label">
                    Business Type:
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlBusinessType" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="33">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="left">
                    Report Type:</td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlReportType" runat="server" Width="152px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="33">
                        <asp:ListItem  Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Due" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td align="left" colspan="8"></td><td align="right" colspan="2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" 
                        OnClick="btnSearch_Click" />
                </td>

            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">
                <asp:Label id="Label1" runat="server" ></asp:Label>
                <asp:Label id="Label2" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:CheckBox ID="ChkSelectAll" runat="server" Text="Select All" onClick="return CheckAll(this);" />
                </td>
            </tr>
            <tr>
                <td align="center">
                   <asp:Panel ID="myPanel" runat="server" ScrollBars="Both" Height="500" Width="1200">
                   
                       <asp:GridView ID="dgvMemberOutstanding" runat="server" AutoGenerateColumns="false"
                        Width="100%" DataKeyNames="MemberId" AllowPaging="true" PageSize="200"  OnPageIndexChanging="dgvMemberOutstanding_PageIndexChanging">
                        <Columns>
                        <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="MemberId" HeaderText="ID" />--%>
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <%--<asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="AdmissionFees" HeaderText="Admission Fees" />
                            <asp:BoundField DataField="AdmissionFeesTax" HeaderText="Admission Fees Tax" />--%>
                            <asp:BoundField DataField="RenewalFees" HeaderText="Renewal Fees" />
                            <%--<asp:BoundField DataField="RenewalFeesTax" HeaderText="Renewal Fees Tax" />
                            <asp:BoundField DataField="Total" HeaderText="Total" />--%>
                            <%--<asp:BoundField DataField="DevFeeAmount" HeaderText="Development Fees" />--%>
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                No Member Record Found</p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                           <PagerStyle CssClass="paging" HorizontalAlign="Left"/>
                    </asp:GridView>
                      
                   </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSendEmail" runat="server" CssClass="button" Text="Send Mail" OnClientClick="return SendMailValidation();"
                        OnClick="btnSendEmail_Click" />&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
