<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditGeneralMember.aspx.cs" Inherits="AccountsModule.SMS.AddEditGeneralMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="title">
        <h5>
            <u>ADD NEW GENERAL MEMBER</u></h5>
    </div>
    <table width="90%" align="center">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsg" runat="server" Style="font-weight: bold; color: Red;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Member Name:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtName" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Company Name:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtCompanyName" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Village:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtVillage" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                PO:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtPostOffice" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                PS:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtPoliceStation" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Pin:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtPinCode" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Block:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtBlockName" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                District:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtDistrictName" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                State:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtStateName" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Code:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtCode" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Mobile No:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtMobileNo" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Phone No:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtPhoneNo" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
                Member Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdownList" Width="260px"
                    DataValueField="CategoryId" DataTextField="CategoryName">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td align="left" width="70%">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
    </div>
    <br />
    <div>
        <table width="100%" align="center">
            <tr>
                <td align="left">
                    <br />
                    <b>Member Search</b><hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table width="100%" align="left">
                        <tr>
                            <td align="left" class="label" width="15%">
                                Member Name
                            </td>
                            <td align="left" class="label" width="15%">
                                Mobile No
                            </td>
                            <td align="left" class="label">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSearchName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchMob" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Width="1300px" Height="1500px" ScrollBars="Both">
                        <asp:GridView ID="dgvGeneralMember" runat="server" Width="100%" AllowPaging="true"
                            PageSize="30" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" DataKeyNames="MemberId"
                            OnPageIndexChanging="dgvGeneralMember_PageIndexChanging" OnRowDeleting="dgvGeneralMember_RowDeleting"
                            OnRowEditing="dgvGeneralMember_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl" ItemStyle-Width="15px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                <asp:BoundField DataField="Village" HeaderText="Village" />
                                <asp:BoundField DataField="PostOffice" HeaderText="PO" />
                                <asp:BoundField DataField="PoliceStation" HeaderText="PS" />
                                <asp:BoundField DataField="PinCode" HeaderText="Pin" />
                                <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                <asp:BoundField DataField="StateName" HeaderText="State" />
                                <asp:BoundField DataField="Code" HeaderText="Code" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                <asp:CommandField ShowEditButton="true" />
                                <asp:CommandField ShowDeleteButton="true" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataTemplate>
                                No Fees Head Found
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
</asp:Content>
