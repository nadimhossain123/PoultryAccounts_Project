﻿<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="NECCMember.aspx.cs" Inherits="AccountsModule.Common.NECCMember" %>


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
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtMemberName.ClientID %>').value == '')
                return ShowMsg('Enter Member name');

            else if (document.getElementById('<%=txtMobileNo.ClientID %>').value == '')
                return ShowMsg('Enter Mobile No');

            else if (document.getElementById('<%=ddlDistrict2.ClientID %>').selectedIndex == 0)
                return ShowMsg('Select District');

           



            else
                return confirm("Are You Sure?");
        }

        function ShowMsg(str) {
            alert(str);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Add New NECC Member</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <br />
    <table width="80%" align="center">
       
        <tr>
            <td align="left">&nbsp;
            </td>
            <td align="left" colspan="3">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <span style="color: Red">*</span>Member Name :
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtMemberName" runat="server" CssClass="textbox" MaxLength="200"
                    Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span style="color: Red">*</span>Mobile No
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="20" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">Address :
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="60px" MaxLength="200"
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
           <td align="left"><span style="color: Red">*</span>District :</td>
            <td align="left">
                <asp:DropDownList ID="ddlDistrict2" runat="server" CssClass="dropdownList" Width="150px" DataValueField="DistrictId" DataTextField="DistrictName"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">Is Active :
            </td>
            <td align="left">
                <asp:CheckBox ID="chkIsActive" runat="server" />
            </td>
           
        </tr>
        <tr>
            <td align="left">Remarks :
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" MaxLength="500" Width="500px"
                    Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="left" colspan="3">
                <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                    OnClientClick="return Validation()" Text="Save" Width="120px" />
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel"
                    Width="120px" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <div style="width: 98%;">
        <table width="100%">
            <tr>
                <td>Member Name :</td>
                <td>Mobile No</td>
                <%--<td>Registration Type</td>--%>
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
                <%--<td>
                    <asp:DropDownList ID="ddlSearchRegType" runat="server" CssClass="dropdownList"
                        Width="100px">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                        <asp:ListItem Text="PAID" Value="2"></asp:ListItem>
                        <asp:ListItem Text="FREE" Value="3"></asp:ListItem>
                        <asp:ListItem Text="GOVT" Value="4"></asp:ListItem>
                        <asp:ListItem Text="CORE" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
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
                    <asp:GridView ID="dgvNECCMember" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="NECCMemberId" OnPageIndexChanging="dgvNECCMember_PageIndexChanging"
                        OnRowDeleting="dgvNECCMember_RowDeleting" OnRowEditing="dgvNECCMember_RowEditing"
                        CellPadding="4" AllowPaging="True" PageSize="50" OnRowDataBound="dgvNECCMember_RowDataBound">
                        <Columns>
                             <asp:TemplateField HeaderText="Sl No." HeaderStyle-Width="15px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="15px"></HeaderStyle>
                        </asp:TemplateField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/Images/edit_icon.gif" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.gif"
                                Visible="false" />
                            <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="HidIsActive" runat="server" Value='<%#Bind("IsActive") %>'>
                                </asp:HiddenField>
                            </ItemTemplate>
                             </asp:TemplateField>
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
