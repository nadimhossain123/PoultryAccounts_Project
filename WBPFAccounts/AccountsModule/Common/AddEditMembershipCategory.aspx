<%@ Page Title="Add/Edit Membership Category" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="AddEditMembershipCategory.aspx.cs" Inherits="AccountsModule.Common.AddEditMembershipCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtCategoryName.ClientID%>').value == '') {
                alert("Please Enter Category Name");
                return false;
            }
            else { return true; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Membership Category Details</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 740px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Category Name<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            SMS Applicable
                        </td>
                        <td align="left" width="60%">
                            <asp:CheckBox ID="chkSMSApplicable" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Remarks
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Width="550px"
                                Height="40px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td width="30%" align="left">
                        </td>
                        <td width="70%" align="right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click"
                                OnClientClick="javascript:return Validation()" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMembershipCategory" runat="server" AutoGenerateColumns="false"
                                Width="100%" AllowPaging="false" DataKeyNames="MembershipCategoryId" OnRowEditing="dgvMembershipCategory_RowEditing">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="10px">
                                        <HeaderTemplate>
                                            S.No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                    <%--<asp:TemplateField ShowHeader="false" HeaderStyle-Width="85px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <HeaderTemplate>
                                            SMS Applicable</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSMSApplicable" runat="server" Checked='<%# Eval("SMSApplicable")%>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    --%><asp:TemplateField ShowHeader="false" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
