<%@ Page Title="SMS Payment Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="SMSPaymentReport.aspx.cs" Inherits="AccountsModule.Common.SMSPaymentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openpopup(poplocation) {
            var popposition = 'left = 200, top=50, width=1000,align=center, height=600,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>SMS Payment Detail</h5>
    </div>
    <div style="width: 99%;">
        <br />
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="14%" class="label">From Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderFromDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="14%" class="label">To Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderToDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="label">Payment No:
                </td>
                <td>
                    <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="textbox" Width="290px"></asp:TextBox>
                </td>
                <td class="label">Payment Mode:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="200px">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Bank</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>Bank Draft</asp:ListItem>
                        <asp:ListItem>Online Payment</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">Cash/Bank Ledger:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCashBankLedger" runat="server" CssClass="dropdownList" Width="300px"
                        DataValueField="LedgerId" DataTextField="LedgerName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">SMS Member Name:
                </td>
                <td colspan="3">
                    <asp:ComboBox ID="ddlSMSMember" runat="server" CssClass="WindowsStyle" Width="300px"
                        DropDownStyle="DropDown" Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        DataValueField="SMSMemberId" DataTextField="MemberName">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show SMS Payment Report"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" align="center">
            <tr>
                <td>
                    <asp:Panel ID="Pnl" runat="server" Width="1200px" ScrollBars="Horizontal">
                        <asp:GridView ID="dgvPaymentReport" runat="server" Width="100%" AutoGenerateColumns="false"
                            GridLines="Both" DataKeyNames="PaymentId,IsApproved,PaymentNo" AllowPaging="true" PageSize="50" ShowFooter="true" OnPageIndexChanging="dgvPaymentReport_PageIndexChanging"
                            OnRowDataBound="dgvPaymentReport_RowDataBound" OnRowCommand="dgvPaymentReport_RowCommand" OnRowDeleting="dgvPaymentReport_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-Wrap="false" FooterText="<b>Total</b>" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="ParentMemberName" HeaderText="Parent Member" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="ReadyBirdPriceSMSAmount" HeaderText="Bird Price SMS Amount"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="ReadyBirdPriceSMSTaxAmount" HeaderText="Bird Price SMS Tax Amount"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentAmount" HeaderText="Total Amount" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-Wrap="true" />
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:Button ID="btnApprove" runat="server" CssClass="button" Text="Approve" CommandName="Approve" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Report"
                        OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
