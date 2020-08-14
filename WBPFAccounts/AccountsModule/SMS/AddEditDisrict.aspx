<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditDisrict.aspx.cs" Inherits="AccountsModule.SMS.AddEditDisrict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            <u>ADD NEW DISTRCT</u></h5>
    </div>
    <table align="center" width="80%">
        <tr>
            <td align="right" width="40%">
                <asp:Label ID="lbldistrictname" runat="server" Text="District Name*" CssClass="label"></asp:Label>
            </td>
            <td align="right">
                <asp:TextBox ID="txtdistrictname" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvdistrictname" runat="server" ErrorMessage="District cannot be blank"
                    ControlToValidate="txtdistrictname" CssClass="label"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" width="40%">
                <asp:Label ID="lblShortName" runat="server" Text="ShortName*" CssClass="label"></asp:Label>
            </td>
            <td align="right">
                <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvshortname" runat="server" ErrorMessage="Shortname cannot be blank"
                    ControlToValidate="txtShortName" CssClass="label"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%" style="padding-left: 30px">
                <asp:Button ID="btnsubmit" runat="server" Text="submit" CssClass="button" OnClick="btnsubmit_Click" />
            </td>
            <td align="left" style="padding-right: 30px">
                <asp:Button ID="btncancel" runat="server" Text="cancel" CssClass="button" OnClick="btncancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <table width="80%" align="center">
        <tr>
            <td align="center">
                <asp:GridView ID="dgvdistrict" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                    DataKeyNames="DistrictId" OnRowEditing="dgvdistrict_RowEditing" CssClass="GridViewStyle">
                    <Columns>
                        <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" ControlStyle-CssClass="HeaderStyle a" />
                        <asp:BoundField DataField="ShortName" HeaderText="ShortName" ControlStyle-CssClass="HeaderStyle a" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/images/edit-validated-icon.png"
                                    Width="25px" Height="25px" CommandName="Edit" ToolTip="Edit" BackColor="#D7A793"
                                    CausesValidation="false" />
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
    </table>
    </div>
</asp:Content>
