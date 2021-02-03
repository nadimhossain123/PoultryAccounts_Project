<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DevelopmentFeeAdjustment.aspx.cs" Inherits="AccountsModule.Common.DevelopmentFeeAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById("<%= ddlMonth.ClientID %>").selectedIndex == 0) {
                alert("Please Select Month");
                return false;
            }
            else if (document.getElementById("<%= ddlYear.ClientID %>").selectedIndex == 0) {
                alert("Please Select Year");
                return false;
            }
            else {
                return true;
            }
        }

        function ChangeCSS(Obj) {
            var row = Obj.parentNode.parentNode;
            if (Obj.checked)
                row.className = 'SelectedRowStyle';
            else
                row.className = 'RowStyle';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Development Fee Adjustment</h5>
    </div>
    <div style="width: 98%;" align="center">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" class="label">
                    State:
                </td>
                <td align="left">
                    District:
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left" class="label">
                    Block:
                </td>
                <td align="left" class="label">
                    Membership Category:
                </td>
                <td align="left" class="label">
                    Month
                </td>
                <td align="left" class="label">
                    Year
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="label">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="100px"
                        DataValueField="Month" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="100px">
                        <asp:ListItem Value="0" Text="--YEAR--"></asp:ListItem>
                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" OnClick="btnSearch_Click"
                        Text="Search" Width="120px" OnClientClick="return Validation();"/>
                </td>
            </tr>
            <tr>
                <td align="left" class="label" colspan="8">
                    <asp:Label ID="lblTotalMemberCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvDevelopmentFee" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="BillId" EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelect" runat="server" onclick="ChangeCSS(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="15px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member" />
                            <asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="Month" HeaderText="Month" />
                            <asp:BoundField DataField="Year" HeaderText="Year" />
                            <asp:BoundField DataField="BlockName" HeaderText="Block" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                            <asp:TemplateField HeaderText="Bill Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBillAmount" CssClass="textbox" onkeypress="return AmountOnly('txtTaxPaymentAmount',this);"
                                        runat="server" Text='<%# Bind("FinalAmount") %>'></asp:TextBox>
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
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="UPDATE BILL AMOUNT"
                        Width="200px" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
