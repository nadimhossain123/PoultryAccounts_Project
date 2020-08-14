<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditBirdPrice.aspx.cs" Inherits="AccountsModule.SMS.AddEditBirdPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            <u>ADD BIRD PRICE</u></h5>
    </div>
    <table width="75%" align="center">
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="DateLbl" runat="server" CssClass="label" Text="Select a Date"></asp:Label>
            </td>
            <td align="left" style="width: 7%">
                <asp:DropDownList ID="ddlDays" runat="server" CssClass="dropdownList" AutoPostBack="true">
                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" style="width: 7%">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" AutoPostBack="true">
                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" style="width: 7%">
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" AutoPostBack="true">
                    <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                    <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                    <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                    <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                    <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                    <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="15%" align="left">
                <asp:Button ID="GetPriceBtn" runat="server" CssClass="button" Text="Get Price" OnClick="GetPriceBtn_Click" />
            </td>
        </tr>
    </table>
    <div style="text-align: center; width: 75%;">
        <asp:GridView ID="dgvBirdPrice" runat="server" Width="70%" AutoGenerateColumns="false"
            AllowPaging="false" AllowSorting="false" DataKeyNames="DistrictId">
            <Columns>
                <asp:TemplateField HeaderText="Distric">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem,"DistrictName")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Farm Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFarmRate" runat="server" Text='<%#Bind("FarmRate") %>' Width="100px"
                            onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Retailer Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRetailerRate" runat="server" Text='<%#Bind("RetailerRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Broiler Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBroilerRate" runat="server" Text='<%#Bind("BroilerRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dressed Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDressedRate" runat="server" Text='<%#Bind("DressedRate") %>'
                            Width="100px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <RowStyle CssClass="RowStyle" />
            <EmptyDataTemplate>
                No Fees Head Found
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <table width="70%">
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                    <b>NECC Egg Rate:</b>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNECCEggRate" runat="server" Text='<%#Bind("NECCPrice") %>'></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: left;" valign="top">
                    <b>Relevant Parameters:</b>
                </td>
                <td align="justify" style="width: 50%">
                    Avg. body weight below&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtbelowWt" runat="server"
                        Text='<%#Bind("belowWt") %>'></asp:TextBox>
                    kg.<br />
                    =Dist. Declare rate&nbsp;&nbsp;+&nbsp;&nbsp;Rs.<asp:TextBox ID="txtbelowAddRate"
                        runat="server" Text='<%#Bind("belowAddRate") %>'></asp:TextBox><br />
                    Avg. body weight over&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtoverWt"
                        runat="server" Text='<%#Bind("overWt") %>'></asp:TextBox>
                    kg.<br />
                    =Dist. Declare rate &nbsp;&nbsp;-&nbsp;&nbsp;Rs.<asp:TextBox ID="txtoverAddRate"
                        runat="server" Text='<%#Bind("overAddRate") %>'></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="60%">
            <tr>
                <td style="width: 20%">
                    <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" OnClick="BtnSave_Click" />
                </td>
                <td style="width: 20%">
                    <asp:Button ID="BtnSaveSend" runat="server" CssClass="button" Width="140px" Text="Save and Send SMS"
                        OnClick="BtnSaveSend_Click" />
                </td>
                <td style="width: 20%">
                    <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="Cancel" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
