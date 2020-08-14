<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SMSReport.aspx.cs" Inherits="AccountsModule.Common.SMSReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contener">
        <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
        </asp:ToolkitScriptManager>
        <div>
            <table width="70%" align="center">
                <tr>
                    <td>
                        Month:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdown" Width="120px">
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
                    <td>
                        Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdown" Width="120px" DataValueField="Year"
                            DataTextField="Year">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="dlReport" runat="server" Width="100%" AutoGenerateColumns="false"
                AllowPaging="false">
                <Columns>
                    <asp:BoundField DataField="TriggerDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="NoOfTrigger" HeaderText="SMS Sent" />
                </Columns>
                <HeaderStyle CssClass="HeaderStyle" />
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </div>
        <div style="float: right;">
            <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" Text="Export To Excel"
                OnClick="btnExportToExcel_Click" />
        </div>
    </div>
</asp:Content>
