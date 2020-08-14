<%@ Page Title="Add/Edit Block" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditBlock.aspx.cs" Inherits="AccountsModule.Common.AddEditBlock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Add/Edit Block</title>
<script type="text/javascript">
    function Validation() {
        if (document.getElementById('<%=ddlDistrict.ClientID%>').value == '') {
            alert("Please Enter District");
            return false;
        }
        if (document.getElementById('<%=txtCode.ClientID%>').value == '') {
            alert("Please Enter Code");
            return false;
        }
        if (document.getElementById('<%=txtBlockName.ClientID%>').value == '') {
            alert("Please Enter Block Name");
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
            Block Details</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 740px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            District<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:ComboBox ID="ddlDistrict" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="185px" Style="margin-bottom: 10px;">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Code<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Block Name<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtBlockName" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Remarks
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox_required" Width="550px"
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
                            <asp:GridView ID="dgvBlock" runat="server" AutoGenerateColumns="false" Width="80%"
                                AllowPaging="false" DataKeyNames="BlockId" OnRowEditing="dgvBlock_RowEditing">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            S.No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StateName" HeaderText="State" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                    <asp:TemplateField ShowHeader="false">
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
