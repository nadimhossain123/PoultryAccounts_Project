<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SMSMemberList.aspx.cs" Inherits="AccountsModule.Common.SMSMemberList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            SMS Member Details (Payment Collection)</h5>
    </div>
    <div style="width: 100%;">
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="15%" class="label">Member Name</td>
                        <td width="15%" class="label">Mobile No</td>
                        <td width="15%" class="label">District</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" MaxLength="20" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" Width="150px" DataValueField="DistrictId" DataTextField="DistrictName"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" OnClick="btnSearch_Click"
                                Text="Search" Width="120px" />&nbsp;
                            <a href="MemberMasterInfo.aspx" class="button" style="text-decoration:none;"><< Back to Member List (Payment Collection)</a>&nbsp;
                            <a href="SMSPaymentExcelUpload.aspx" class="button" style="text-decoration:none;" id="lnkBulkUpload" runat="server">Bulk Upload</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" colspan="4">
                            <asp:Label ID="lblTotalMemberCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="30"
                                DataKeyNames="SMSMemberId,IsActive" OnRowEditing="dgvMemberMaster_RowEditing"
                                OnRowDataBound="dgvMemberMaster_RowDataBound" OnPageIndexChanging="dgvMemberMaster_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FullMemberName" HeaderText="Parent Member" />
                                    <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" />
                                    <asp:BoundField DataField="MemberType" HeaderText="Member Type" />
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <%--<asp:BoundField DataField="SMSRenewalStatus" HeaderText="Renewal Status" />--%>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="SMS Payment">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSMSPayment" runat="server" Text="SMS Payment" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
