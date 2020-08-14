<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RenewalFeeAdjustment.aspx.cs" Inherits="AccountsModule.Common.RenewalFeeAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Renewal Fee Adjustment</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 700px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center">
                    
                    <tr>
                        <td width="20%" class="label">
                            Member Name:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="300px"
                                AutoCompleteMode="SuggestAppend" DataValueField="MemberId" DataTextField="MemberName"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="dgvMemberOutstanding" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="Both" AllowPaging="false" DataKeyNames="FeesHeadId">
                                <Columns>
                                    <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head" />
                                    <asp:BoundField DataField="FeesOutstandingAmount" HeaderText="Fees Outstanding" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TaxOutstandingAmount" HeaderText="Tax Outstanding" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField HeaderText="Adjustment Fees Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFeesPaymentAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("FeesPaymentAmount") %>'
                                                onkeyup="CalculateTotalAmount()" onkeypress="return AmountOnly('txtFeesPaymentAmount',this);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adjustment Tax Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaxPaymentAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("TaxPaymentAmount") %>'
                                                onkeyup="CalculateTotalAmount()" onkeypress="return AmountOnly('txtTaxPaymentAmount',this);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Found
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Adjust Fee" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" Width="120px" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
