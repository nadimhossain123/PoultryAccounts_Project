<%@ Page Title="Member Consolidated Ledger Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberConsolidatedOutstandingReport.aspx.cs"
    Inherits="AccountsModule.Accounts.MemberConsolidatedOutstandingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Consolidated Ledger Report</h5>
    </div>
    <br />
    <div style="width: 100%">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                
                <td width="20%" class="label">
                    From Date:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox"
                        Width="140px">
                    </asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" class="label">
                    To Date:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox"
                        Width="140px">
                    </asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="20%" class="label">
                    Membership Category:
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList" Width="140px" >
                    </asp:DropDownList>
                </td>
                <td width="20%" class="label">
                    Block:
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    District:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="140px" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    State:
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="140px" onselectedindexchanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td class="label">
                    Subscription Month:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubscriptionMonth" runat="server" CssClass="dropdownList" Width="140px">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="January">January</asp:ListItem>
                        <asp:ListItem Value="February">February</asp:ListItem>
                        <asp:ListItem Value="March">March</asp:ListItem>
                        <asp:ListItem Value="April">April</asp:ListItem>
                        <asp:ListItem Value="May">May</asp:ListItem>
                        <asp:ListItem Value="June">June</asp:ListItem>
                        <asp:ListItem Value="July">July</asp:ListItem>
                        <asp:ListItem Value="August">August</asp:ListItem>
                        <asp:ListItem Value="September">September</asp:ListItem>
                        <asp:ListItem Value="October">October</asp:ListItem>
                        <asp:ListItem Value="November">November</asp:ListItem>
                        <asp:ListItem Value="December">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label">
                    Subscription Year:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubscriptionYear" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td class="label">
                    Fees Head:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFeesHead" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="id" DataTextField="fees">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Report" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="dgvBill" runat="server" Width="100%" ShowFooter="true" GridLines="None"
                        AutoGenerateColumns="false" AllowPaging="false" CellPadding="0" CellSpacing="0" OnRowDataBound="dgvBill_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sl" HeaderText="SL." />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" HtmlEncode="false" />
                            <asp:BoundField DataField="MemberCode" HeaderText="Code" FooterText="<b>Total</b>" />
                            <asp:TemplateField HeaderText="OPBAL Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpBalAmt" runat="server" Text='<%#Bind("Opening","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotOpBalAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillAmt" runat="server" Text='<%#Bind("BillAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotBillAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Tax Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillTaxAmt" runat="server" Text='<%#Bind("BillTaxAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotTaxBillAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmt" runat="server" Text='<%#Bind("PaidAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotPaidAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax Paid Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaxPaidAmt" runat="server" Text='<%#Bind("PaidTaxAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotTaxPaidAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Main Due Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueAmt" runat="server" Text='<%#Bind("DueAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotDueAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax Due Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaxDueAmt" runat="server" Text='<%#Bind("TaxDueAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotTaxDueAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Bill Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalBill" runat="server" Text='<%#Bind("TotalBillAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalBill" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Paid Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalPaid" runat="server" Text='<%#Bind("TotalPaymentAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalPaid" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Due Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalDue" runat="server" Text='<%#Bind("TotalDueAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalDue" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr class="HeaderStyle">
                                    <th>
                                        No Records Found
                                    </th>
                                </tr>
                                <tr class="RowStyle">
                                    <td>
                                        No Records Found
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <FooterStyle CssClass="RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
