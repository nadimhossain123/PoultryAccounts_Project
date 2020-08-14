<%@ Page Title="Add/Edit Bird Price" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditBirdPrice.aspx.cs" Inherits="AccountsModule.Common.AddEditBirdPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 840px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Select a date: <span class="req">*</span>
                </td>
                <td align="left" width="20%" class="label">
                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="140px">
                        <asp:ListItem Value="0">MONTH</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:Button ID="btnGetPrice" runat="server" Text="Get Price" CssClass="button" 
                        onclick="btnGetPrice_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgvBirdPrice" runat="server" Width="85%" AutoGenerateColumns="false"
            AllowPaging="false" AllowSorting="false" DataKeyNames="DistrictId">
            <Columns>
                <asp:TemplateField HeaderText="Distric">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "DistrictName")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Farm Rate" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFarmRate" runat="server" Text='<%#Bind("FarmRate") %>' Width="100px"
                            onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Retailer Rate" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRetailerRate" runat="server" Text='<%#Bind("RetailerRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Broiler Rate" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBroilerRate" runat="server" Text='<%#Bind("BroilerRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dressed Rate" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDressedRate" runat="server" Text='<%#Bind("DressedRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
        </asp:GridView><br />
        <table width="70%">
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                    <b>NECC Egg Rate:</b>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblSB" runat="server" Text="South Bengal"></asp:Label>
                    <asp:TextBox ID="txtNECCEggRate" runat="server" CssClass="textbox" Text='<%#Bind("NECCPrice") %>'></asp:TextBox><br />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblN" runat="server" Text="Nadia"></asp:Label>
                    <asp:TextBox ID="txtNECCEggRate2" runat="server" CssClass="textbox" Text='<%#Bind("NECCPrice") %>'></asp:TextBox><br />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblNB" runat="server" Text="North Bengal"></asp:Label>
                    <asp:TextBox ID="txtNECCEggRate3" runat="server" CssClass="textbox" Text='<%#Bind("NECCPrice") %>'></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                    <b>Relevant Parameters:</b>
                </td>
                <td align="justify" style="width: 50%">
                Avg. body weight below<br />
                    <asp:TextBox ID="txtbelowWt" runat="server" CssClass="textbox" Text='<%#Bind("belowWt") %>'></asp:TextBox>kg.
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                </td>
                <td align="justify" style="width: 50%">
                    =Dist. Declare rate + Rs. <br />
                    <asp:TextBox ID="txtbelowAddRate" runat="server" CssClass="textbox" Text='<%#Bind("belowAddRate") %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                </td>
                <td align="justify" style="width: 50%">
                    Avg. body weight over<br />
                    <asp:TextBox ID="txtoverWt" runat="server" CssClass="textbox" Text='<%#Bind("overWt") %>'></asp:TextBox>kg.
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                </td>
                <td align="justify" style="width: 50%">
                    =Dist. Declare rate - Rs.<br />
                    <asp:TextBox ID="txtoverAddRate" runat="server" CssClass="textbox" Text='<%#Bind("overAddRate") %>'></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="60%">
            <tr>
                <td style="width: 20%">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />
                </td>
                <td style="width: 20%">
                    <asp:Button ID="btnSaveSend" runat="server" CssClass="button" Width="140px" Text="Save and Send SMS"
                        OnClick="btnSaveSend_Click" />
                </td>
                <td style="width: 20%">
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                        onclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
