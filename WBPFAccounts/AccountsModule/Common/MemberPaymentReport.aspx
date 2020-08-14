<%@ Page Title="Member Payment Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberPaymentReport.aspx.cs" Inherits="AccountsModule.Common.MemberPaymentReport" %>

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
        <h5>Member Payment Detail</h5>
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
                <td class="label">Member Name:
                </td>
                <td >
                    <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="300px"
                        DropDownStyle="DropDown" Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                        DataValueField="MemberId" DataTextField="MemberName">
                    </asp:ComboBox>
                </td>
                <td align="left" width="20%" class="label">
                    Business Type:
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlBusinessType" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="33">
                    </asp:DropDownList>
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
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Payment Report"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" align="center">
            <tr>
                <td>
                    <asp:Panel ID="Pnl" runat="server" Width="85%" ScrollBars="Horizontal">
                        <asp:GridView ID="dgvPaymentReport" runat="server" Width="100%" AutoGenerateColumns="false"
                            GridLines="Both" DataKeyNames="PaymentId,IsApproved,PaymentNo" AllowPaging="false" PageSize="50"
                            ShowFooter="true" OnPageIndexChanging="dgvPaymentReport_PageIndexChanging" OnRowDataBound="dgvPaymentReport_RowDataBound"
                            OnRowDeleting="dgvPaymentReport_RowDeleting" OnRowCommand="dgvPaymentReport_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-Wrap="false"
                                    FooterText="<b>Total</b>" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}"
                                    NullDisplayText="" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="AdmissionFeesPaymentAmount" HeaderText="Admission Fees"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="AdmissionFeesTaxPaymentAmount" HeaderText="Admission Fees Tax"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="RenewalFeesPaymentAmount" HeaderText="Renewal Fees" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="RenewalFeesTaxPaymentAmount" HeaderText="Renewal Fees Tax"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DevelopmentPaymentAmount" HeaderText="Development Fees"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentAmount" HeaderText="Total" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="OrderStatus" HeaderText="Payment Status" ItemStyle-Wrap="false" />
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
                <td align="right" colspan="1">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Report"
                        OnClick="btnDownload_Click" />
                </td><td colspan="3"></td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
