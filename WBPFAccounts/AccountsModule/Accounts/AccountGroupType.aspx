<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AccountGroupType.aspx.cs" Inherits="CollegeERP.Accounts.AccountGroupType"
    Title="Accounts Group Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlFirstAccountType.ClientID%>'), "First A/c Type", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSecondAccountType.ClientID%>'), "Second A/c Type", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtGroupType.ClientID%>'), "Third Group", 1)) return false;
            return true;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Accounts Group Type</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 840px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="25%" class="label">
                            First A/C Type
                        </td>
                        <td align="left" width="25%" class="label">
                            Second A/C Type
                        </td>
                        <td align="left" width="25%" class="label">
                            Third A/C Type
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="25%">
                            <asp:DropDownList ID="ddlFirstAccountType" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                Width="170px" AppendDataBoundItems="True" 
                                onselectedindexchanged="ddlFirstAccountType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="25%">
                            <asp:DropDownList ID="ddlSecondAccountType" runat="server" AppendDataBoundItems="True"
                                CssClass="dropdownList" Width="170px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtGroupType" runat="server" CssClass="textbox" Width="170px"></asp:TextBox>
                        </td>
                        <td align="left" width="25%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                OnClientClick="return Validation()" onclick="btnSave_Click">
                            </asp:Button>
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="button" 
                                onclick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvGroupType" runat="server" AllowSorting="false" AllowPaging="True"
                                AutoGenerateColumns="False" Width="100%" PageSize="30" 
                                onpageindexchanging="dgvGroupType_PageIndexChanging" 
                                onrowcommand="dgvGroupType_RowCommand" 
                                onrowdatabound="dgvGroupType_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="FirstGroupType" HeaderText="First Gr. Type">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SecondGroupType" HeaderText="Second Gr. Type">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ThirdGroupType" HeaderText="Third Gr. Type">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GroupTypeID" Visible="False" HeaderText="GroupTypeID"
                                       ></asp:BoundField>
                                    <asp:BoundField DataField="UnderGroupTypeID" Visible="False" HeaderText="UnderGroupTypeID">
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" CommandName="btnEdit" ImageUrl="~/Images/edit_icon.gif"
                                                runat="server" />
                                            <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("GroupTypeID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnGroupTypeID" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
