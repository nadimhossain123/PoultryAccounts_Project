<%@ Page Title="Area Manager Detail" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="AddEditAgent.aspx.cs" Inherits="AccountsModule.Common.AddEditAgent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtAgentName.ClientID%>').value.trim() == '') {
                alert("Please Enter Area Manager Name");
                return false;
            }
            else if (document.getElementById('<%=ddlState.ClientID%>').selectedIndex == 0) {
                alert("Please Select State");
                return false;
            }
            else if (document.getElementById('<%=ddlDistrict.ClientID%>').selectedIndex == 0) {
                alert("Please Select District");
                return false;
            }
            else if (document.getElementById('<%=ddlBlock.ClientID%>').selectedIndex == 0) {
                alert("Please Select Block");
                return false;
            }
            else {
                return confirm("Do You Want To Save/Update?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Area Manager Detail</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 1000px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <br />
                <span style="float: left; color: Blue; font-size: 14px;"><b>Area Manager Detail</b></span><br />
                <br />
                <hr />
                <br />
                <table width="80%" align="center" class="table">
                    <tr>
                        <td width="20%" class="label">
                            Area Manager Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgentCode" runat="server" CssClass="textbox" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Area Manager Name:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgentName" runat="server" CssClass="textbox" Width="250px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Address:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" Width="250px" Height="65px"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Phone No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox" Width="250px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            State:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" Width="260px"
                                DataValueField="StateId" DataTextField="StateName" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            District:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" Width="260px"
                                DataValueField="DistrictId" DataTextField="DistrictName" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Block:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="260px"
                                DataValueField="BlockId" DataTextField="BlockName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Bank Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" Width="250px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Branch Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="textbox" Width="250px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Branch Address:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchAddress" runat="server" CssClass="textbox" Width="250px"
                                Height="65px" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            IFSC Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="textbox" Width="250px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Active:
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <span style="float: left; color: Blue; font-size: 14px;"><b>Area Manager List</b></span><br />
                <br />
                <hr />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="12%" class="label">
                            Area Manager Name:
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtAgentNameSearch" runat="server" CssClass="textbox" Width="250px"
                                MaxLength="100"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="dgvAgentMaster" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="true" PageSize="20" DataKeyNames="AgentId,IsActive"
                                OnPageIndexChanging="dgvAgentMaster_PageIndexChanging" OnRowDataBound="dgvAgentMaster_RowDataBound"
                                OnRowEditing="dgvAgentMaster_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="AgentCode" HeaderText="Area Manager Code" />
                                    <asp:BoundField DataField="AgentName" HeaderText="Area Manager Name" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" />
                                    <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                                    <asp:BoundField DataField="StateName" HeaderText="State" />
                                    <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                    <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                    <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                    <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Active" />
                                    <asp:TemplateField ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <p>
                                        No Area Manager Record Found</p>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
