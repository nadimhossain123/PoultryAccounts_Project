<%@ Page Title="Service Tax Report" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ServiceTax_Report.aspx.cs" Inherits="AccountsModule.Accounts.ServiceTax_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="Tool1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Service Tax Report</h5>
    </div>
    <asp:TabContainer ID="tcGeneralLedger" runat="server" ActiveTabIndex="1" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="tpView" runat="server">
            <ContentTemplate>
                <br />
                <table class="table" width="95%" align="center">
                    <tr>
                        <td class="label" align="left" width="15%">
                            Fees Head
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlFeesHead" runat="server" class="dropdownList">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="15%">
                            From Date :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFromDate" CssClass="textbox" Width="180px" runat="server" MaxLength="12"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="15%">
                            To Date :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtToDate" CssClass="textbox" Width="180px" runat="server" MaxLength="12"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Literal ID="ltrPLHeading" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" width="100%">
                            <asp:GridView ID="gvServiceTax" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="SL" HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="MoneyReceiptNo" HeaderText="Money Receipt No." ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" HtmlEncode="false"/>
                                    <asp:BoundField DataField="AmountReceived" HeaderText="Amount Received" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="PayableTax" HeaderText="Service Tax" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                                    
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click"/>
                            &nbsp;
                            <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel" OnClick="btnExport_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Service Tax Report By Fees Head </b>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server">
            <ContentTemplate>
                <br />
                <table class="table" width="95%" align="center">
                    <tr>
                        <td class="label" align="left" width="15%">
                            Ledger
                        </td>
                        <td align="left">
                            <asp:ComboBox ID="ddlLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="250px">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="15%">
                            From Date :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFromDate2" CssClass="textbox" Width="245px" runat="server" MaxLength="12" style="margin-top:10px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate2"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="15%">
                            To Date :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtToDate2" CssClass="textbox" Width="245px" runat="server" MaxLength="12"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate2"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" width="15%">
                            Received A/C :
                        </td>
                        <td align="left">
                            <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="250px"
                                DataValueField="LedgerID" DataTextField="LedgerName" TabIndex="1">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch2" runat="server" CssClass="button" Text="Search" OnClick="btnSearch2_Click" style="margin-top:10px"/>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Literal ID="Literal1" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" width="100%">
                            <asp:GridView ID="gvServiceTax2" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="SL" HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="TransDate" HeaderText="Transation Date" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="DocumentNo" HeaderText="Voucher No." ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="ACDescription" HeaderText="Description" ItemStyle-HorizontalAlign="Left"
                                        HtmlEncode="false" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="CrAmount" HeaderText="Amount Received" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="PayableTax" HeaderText="Service Tax" DataFormatString="{0:n}"
                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnPrint2" runat="server" CssClass="button" Text="Print" OnClick="btnPrint2_Click" />
                            &nbsp;
                            <asp:Button ID="btnExport2" runat="server" CssClass="button" Text="Export To Excel" OnClick="btnExport2_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Service Tax Report By Ledger </b>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>&nbsp;
</asp:Content>
