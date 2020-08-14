<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="FeesHeadMaster.aspx.cs" Inherits="AccountsModule.Accounts.FeesHeadMaster"
    Title="Fees Head Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFeesHeadName.ClientID%>'), "Head Name", 1)) return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Fees Head Master</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 950px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table align="center" width="100%" class="table">
                    <tr>
                        <td align="left" width="30%" class="label">
                            Fees Head Name:<span class="req">*</span>
                        </td>
                        <td align="left" width="70%">
                            <asp:TextBox ID="txtFeesHeadName" runat="server" CssClass="textbox" Width="250px"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Frequency:
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="dropdownList" Width="260px">
                                <asp:ListItem Value="O" Text="One Time Applicable"></asp:ListItem>
                                <asp:ListItem Value="M" Text="Monthly"></asp:ListItem>
                                <asp:ListItem Value="Y" Text="Yearly"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Fees Type:
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlFeesType" runat="server" CssClass="dropdownList" Width="260px">
                                <asp:ListItem Value="1" Text="Related With Membership Category"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Related With Member"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Mandatory To Continue Service:
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsMandatory" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Active:
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Applicable Tax Head:
                        </td>
                        <td align="left" width="70%">
                            <asp:CheckBoxList ID="CheckBoxListTax" runat="server" DataValueField="TaxId" DataTextField="TaxHead"
                                RepeatDirection="Horizontal" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                                OnClientClick="return Validation()" Text="Save" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                Text="Cancel" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvFeesHead" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                Width="100%" DataKeyNames="FeesHeadId" OnRowEditing="dgvFeesHead_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head Name" />
                                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" />
                                    <asp:BoundField DataField="FeesType" HeaderText="Fees Type" />
                                    <asp:TemplateField HeaderText="Mandatory To Continue Service">
                                        <ItemTemplate>
                                            <%#Eval("IsMandatory").ToString().Equals("True") ? "Yes" : "No" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <%#Eval("IsActive").ToString().Equals("True") ? "Yes" : "No" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Tax" HeaderText="Tax" />
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
