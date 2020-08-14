<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MemberLogDetails.aspx.cs" Inherits="AccountsModule.Common.MemberLogDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Member Log Details</h5>
    </div>
    <%--<uc3:message id="Message" runat="server" />--%>
    <br />
    <div style="width: 98%;">
        <table width="70%">
            <tr>
                <td>Member Name :</td>
                <td>Mobile No</td>
                <td>From Date:</td>
                <td>To Date:</td>
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
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderStartDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"
                        OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:GridView ID="dgvSMSMemberLogDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="LogId,SMSMemberId" CellPadding="4" AllowPaging="True" PageSize="50"
                          OnPageIndexChanging="dgvSMSMemberLogDetails_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="Action" HeaderText="Action"  ItemStyle-HorizontalAlign="Center"/>
                            <%--<asp:BoundField DataField="SubscriptionStartDate" HeaderText="Start Date" DataFormatString="{0: dd-MMM-yyyy}" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="SubscriptionEndDate" HeaderText="End Date" DataFormatString="{0: dd-MMM-yyyy}" ItemStyle-HorizontalAlign="Center"/>--%>
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By"  ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="CreatedOn" HeaderText="Created Date" DataFormatString="{0: dd MMMM yyyy hh:mm tt}" ItemStyle-HorizontalAlign="Center"/>
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
