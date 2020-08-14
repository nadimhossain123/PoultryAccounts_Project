<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CostCentre.aspx.cs" Inherits="CollegeERP.Accounts.CostCentre" Title="Cost Centre" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtCostCenter.ClientID%>'), "Cost Center", 1)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Cost Centre</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 680px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="30%" class="label">
                            Cost Center Name<span class="req">*</span>
                        </td>
                        <td align="left" width="70%">
                            <asp:TextBox ID="txtCostCenter" runat="server" CssClass="textbox_required" Width="210px"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr><td colspan="2"><br /></td></tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation()"
                                OnClick="btnSave_Click"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvCostCenter" runat="server" AllowSorting="false" AllowPaging="false"
                                AutoGenerateColumns="False" Width="100%" OnRowCommand="gvCostCenter_RowCommand"
                                OnRowDataBound="gvCostCenter_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CostCenterID" Visible="False" HeaderText="CostCenterID">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CostCenterName" HeaderText="Cost Center Name"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="imgbtnEdit" ImageUrl="~/Images/edit_icon.gif"
                                                runat="server" />
                                            <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("CostCenterID") %>' />
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
