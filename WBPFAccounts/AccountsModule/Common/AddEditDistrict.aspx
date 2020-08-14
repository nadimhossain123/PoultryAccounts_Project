<%@ Page Title="Add/Edit District" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditDistrict.aspx.cs" Inherits="AccountsModule.Common.AddEditDistrict" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlState.ClientID%>').value == '') {
                alert("Please Enter State");
                return false;
            }
            if (document.getElementById('<%=txtCode.ClientID%>').value == '') {
                alert("Please Enter Code");
                return false;
            }
            if (document.getElementById('<%=txtDistrictName.ClientID%>').value == '') {
                alert("Please Enter District Name");
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
            District Details</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 740px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            State<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:ComboBox ID="ddlState" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="185px" style="margin-bottom:10px;">
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
                            District Name<span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtDistrictName" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
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
                            <asp:GridView ID="dgvDistrict" runat="server" AutoGenerateColumns="false" Width="70%"
                                AllowPaging="false" DataKeyNames="DistrictId" OnRowEditing="dgvDistrict_RowEditing">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            S.No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StateName" HeaderText="State" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
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
