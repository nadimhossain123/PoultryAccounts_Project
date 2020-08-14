<%@ Page Title="SMS Member Expired List" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SMSMemberExpireDetails.aspx.cs" Inherits="AccountsModule.Common.SMSMemberExpireDetails" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>SMS Member Expired List</h5>
    </div>
     <div style="width: 100%;">
        <table width="100%">
            <tr>

            </tr>
            <tr>
                <td>From Date :</td>
                <td>To Date :</td>
                <td>Member Name :</td>
                <td>Mobile No</td>
                <td>Registration Type</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>


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
                        <asp:ListItem Text="CORE" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>
               
                
            
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"
                        OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="dgvSMSMember" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="SMSMemberId" OnPageIndexChanging="dgvSMSMember_PageIndexChanging"
                         OnRowEditing="dgvSMSMember_RowEditing"
                         AllowPaging="True" PageSize="50" OnRowDataBound="dgvSMSMember_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SL" HeaderText="Sl No." />
                            
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                            <asp:BoundField DataField="MemberType" HeaderText="Member Type" />
                           <%-- <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0: dd/MM/yyyy}" />--%>
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0: dd/MM/yyyy}" />
                            <%--<asp:BoundField DataField="SMSRenewalStatus" HeaderText="Renewal Status" />--%>
                           <%-- <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/Images/edit_icon.gif" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.gif"/>--%>
                                 
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
