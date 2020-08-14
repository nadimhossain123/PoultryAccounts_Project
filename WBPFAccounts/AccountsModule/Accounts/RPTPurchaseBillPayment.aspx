<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTPurchaseBillPayment.aspx.cs" Inherits="CollegeERP.Accounts.RPTPurchaseBillPayment"
    Title="Purchase Bill Payent Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Purchase Bill Payment Report</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <table width="95%" align="center" class="table">
                <tr>
                    <td width="10%" align="left" class="label">
                        Select Supplier 
                    </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="dropdownList" Width="160px" DataValueField="LedgerID" DataTextField="LedgerName"></asp:DropDownList>
                    </td>
                    <td width="10%" align="left" class="label">
                        From
                    </td>
                    <td width="15%" align="left">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="140px">
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td width="10%" align="left" class="label">
                        To
                    </td>
                    <td width="15%" align="left">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="140px">
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" width="25%">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" 
                            onclick="btnSearch_Click" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="dgvPayment" runat="server" AutoGenerateColumns="false" Width="100%"
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt No" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Supplier" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="ModeOfPayment" HeaderText="Payment Mode" />
                                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No." />
                                <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date" />
                                <asp:BoundField DataField="DrawnOn" HeaderText="Drawn On" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
