<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterAdmin.Master" CodeBehind="MemberDevelopmentFeesDetails.aspx.cs" Inherits="AccountsModule.Common.MemberDevelopmentFeesDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlMember.ClientID %>').selectedIndex == 0) {
                alert('Please Select Member');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Monthly Member Development Fee Bill Details</h5>
    </div>
    <div style="width: 99%;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="10%" class="label">Member Name:
                </td>
                <td>
                    <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="350px"
                        DropDownStyle="DropDown" Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        DataValueField="MemberId" DataTextField="MemberName">
                    </asp:ComboBox>
                </td>
                <td width="7%" class="label">From Month:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="Month" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">To Month:
                </td>
                <td>
                    <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="Month" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation();"
                        OnClick="btnSearch_Click" Width="150px" />
                </td>
            </tr>
        </table>
        <table width="60%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvMemberDevelopmentFee" runat="server" AutoGenerateColumns="false"
                        Width="100%" DataKeyNames="BillId" AllowPaging="false" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center" HeaderText="SL NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNo" runat="server" Text='<%#Bind("RowNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MonthName" HeaderText="Month" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Bill No" >
                                <ItemTemplate>
                                    <asp:Label ID="lblBillNo" runat="server" Text='<%#Bind("BillNo") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:Label runat="server" Text="Opening Balance"></asp:Label><hr />
                                    <asp:Label runat="server" Text="Total"></asp:Label>
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="TaxAmount" HeaderText="Tax Amount" ItemStyle-HorizontalAlign="Center"/>--%>
                            <asp:TemplateField HeaderText="Bill Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinalAmount" runat="server" Text='<%#Bind("FinalAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:Label ID="lblOpeningBalance" runat="server"></asp:Label><hr />
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                No Member Record Found
                            </p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <FooterStyle CssClass="FooterStyle"/>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Bill"
                        OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
