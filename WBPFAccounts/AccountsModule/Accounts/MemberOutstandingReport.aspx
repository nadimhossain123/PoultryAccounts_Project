<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MemberOutstandingReport.aspx.cs" Inherits="AccountsModule.Accounts.MemberOutstandingReport"
    Title="Member Ledger Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Ledger Report</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <br />
    <table width="85%" align="center" class="table">
        <tr>
            <td width="15%" align="left" class="label">
                Membership Category :
            </td>
            <td align="left" class="style2">
                <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                    Width="140px" AutoPostBack="true"
                    onselectedindexchanged="ddlMembershipCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="4%" align="left" class="label">
                Block
            </td>
            <td align="left" width="12%">
                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" AutoPostBack="true"
                    Width="140px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="4%" align="left" class="label">
                District
            </td>
            <td align="left" width="12%">
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                    Width="140px" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="4%" align="left" class="label">
                State
            </td>
            <td align="left" width="12%">
                <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                    Width="140px" onselectedindexchanged="ddlState_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="15%" align="left" class="label">
                Member Name
            </td>
            <td width="35%" align="left" style="padding-bottom: 5px;">
                <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                    DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                    Width="300px" DataValueField="MemberId" DataTextField="MemberName">
                </asp:ComboBox>
            </td>
            <td width="4%" align="left" class="label">
                From
            </td>
            <td width="12%" align="left">
                <asp:TextBox ID="txtFromDate" runat="server" Width="125px" CssClass="textbox" MaxLength="12"
                    onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td width="4%" align="left" class="label">
                To
            </td>
            <td width="12%" align="left">
                <asp:TextBox ID="txtToDate" runat="server" Width="125px" CssClass="textbox" MaxLength="12"
                    onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td align="left" colspan="2">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Outstanding Report"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="85%" align="center" class="table">
        <tr>
            <td align="left" class="table">
                <asp:Literal ID="ltrStudentInfo" runat="server" Mode="PassThrough"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="dgvBill" runat="server" Width="100%" ShowFooter="true" GridLines="None"
                    AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="dgvBill_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="DocumentNo" HeaderText="Document No" FooterText="<b>*** Closing Balance ***</b>" />
                        <%--<asp:BoundField DataField="DocumentDate" HeaderText="Date" />--%>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDocumentDate" runat="server" Text='<%#Bind("DocumentDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocumentType" HeaderText="Type" />
                        <asp:BoundField DataField="FeesType" HeaderText="Fees Type" />
                        <asp:BoundField DataField="Amt" HeaderText="Amt" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n}" />
                        <asp:TemplateField HeaderText="Tot Bill">
                            <ItemTemplate>
                                <asp:Label ID="lblTotBill" runat="server" Text='<%#Bind("TotBill","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSumTotBill" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle HorizontalAlign="Right" Font-Bold="true"></FooterStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tot Recd">
                            <ItemTemplate>
                                <asp:Label ID="lblTotRecd" runat="server" Text='<%#Bind("TotRecd","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSumTotRecd" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle HorizontalAlign="Right" Font-Bold="true"></FooterStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance Amt">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" Text='<%#Bind("Balance","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSumBalance" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle HorizontalAlign="Right" Font-Bold="true"></FooterStyle>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="height: 10px; width: 100%;">
                            <tr align="left" class="HeaderStyle">
                                <th scope="col">
                                    No Records Found
                                </th>
                            </tr>
                            <tr class="RowStyle">
                                <td>
                                    No Records Found
                                </td>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <FooterStyle CssClass="RowStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />&nbsp;
                <asp:Button ID="btnPayment" runat="server" CssClass="button" Text="Pay Now" />
            </td>
        </tr>
    </table>
</asp:Content>
