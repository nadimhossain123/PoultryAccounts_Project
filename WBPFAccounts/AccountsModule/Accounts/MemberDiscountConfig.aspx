<%@ Page Title="Member Discount Config" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="True" CodeBehind="MemberDiscountConfig.aspx.cs" Inherits="AccountsModule.Accounts.MemberDiscountConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Discount Configuration
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 700px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="350px" AutoCompleteMode="SuggestAppend"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                            </asp:ComboBox>
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
                                AllowPaging="false" GridLines="None" DataKeyNames="FeesHeadId,DiscountType,IsActive"
                                OnRowDataBound="dgvFeesHead_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="Discount Type" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDiscountType" runat="server" CssClass="dropdownList" Width="120px">
                                                <asp:ListItem Value="P" Text="Percentage (%)"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Fixed Amount"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount Amount" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscountAmount" runat="server" CssClass="textbox" Style="text-align: right;
                                                padding-right: 8px;" Width="120px" Text='<%#Bind("DiscountAmount") %>'></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftbDiscountAmount" runat="server" TargetControlID="txtDiscountAmount"
                                                FilterType="Custom" ValidChars="0123456789.">
                                            </asp:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkIsActive" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Narration" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Width="250px" Height="45px" TextMode="MultiLine" style="resize:none;" Text='<%#Bind("Narration") %>'></asp:TextBox>
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
