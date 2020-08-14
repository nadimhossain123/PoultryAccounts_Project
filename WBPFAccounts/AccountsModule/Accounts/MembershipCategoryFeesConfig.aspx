<%@ Page Title="Fees" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MembershipCategoryFeesConfig.aspx.cs" Inherits="AccountsModule.Accounts.MembershipCategoryFeesConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Membership Category Fees Configuration
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 550px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                                Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ddlMembershipCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgvFeesHead" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" GridLines="None" DataKeyNames="FeesHeadId">
                                <Columns>
                                    <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Style="text-align: right;
                                                padding-right: 8px;" Width="120px" Text='<%#Bind("Amount") %>'></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftbAmount" runat="server" TargetControlID="txtAmount"
                                                FilterType="Custom" ValidChars="0123456789.">
                                            </asp:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataTemplate>
                                    No Fees Head Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
