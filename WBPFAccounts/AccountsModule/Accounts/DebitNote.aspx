<%@ Page Title="Debit Note" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DebitNote.aspx.cs" Inherits="CollegeERP.Accounts.DebitNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            var gv = document.getElementById('<%=dgvBill.ClientID%>');
            var rowCount = gv.rows.length - 1;

            if (document.getElementById('<%=txtVoucherDate.ClientID%>').value == '') {
                alert("Please Enter Voucher Date");
                return false;
            }
            else if (document.getElementById('<%=ddlSupplierLedger.ClientID%>').selectedIndex == 0) {
                aler("Please Select Supplier");
                return false;
            }
            else if (rowCount == 0) {
                alert("No Due Bill Exists");
                return false;
            }
            else if (parseFloat(document.getElementById('<%=txtTotalAmt.ClientID%>').value) == 0) {
                alert("Total Amount Should Be Greater Than Zero");
                return false;
            }
            else {
                return confirm('Do you want to save?');
            }
        }

        function CheckAmount(Obj) {
            var row = Obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var Due = row.cells[4].innerHTML;

            var Amt = (Obj.value == '') ? '0' : Obj.value;
            if (parseFloat(Amt) > parseFloat(Due)) {
                alert("Amout Should Not Be More Than Due");
                Obj.focus();
            }
            TotalAmount();
        }

        function TotalAmount() {

            var amt = 0;
            var gv = document.getElementById('<%=dgvBill.ClientID%>');
            var rCount = gv.rows.length;
            var rowIdx = 1;
            for (rowIdx; rowIdx < rCount; rowIdx++) {
                var rowElement = gv.rows[rowIdx];
                var txtBox = rowElement.cells[5].getElementsByTagName("input")[0];
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value));
            }
            document.getElementById('<%=txtTotalAmt.ClientID%>').value = amt.toFixed(2);
        }


        function openPopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';
            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Debit Note</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="TabPanel2" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelVoucherEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <div style="width: 950px">
                            <table width="95%" align="center" class="table">
                                <tr>
                                    <td width="12%" class="label" align="left">
                                        Voucher No. :
                                    </td>
                                    <td align="left" width="35%">
                                        <asp:TextBox ID="txtVchNo" CssClass="textbox_pink" runat="server" Width="180px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td width="12%" class="label" align="center">
                                        Voucher Date :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="textbox_pink" Width="120px">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVoucherDate"
                                            OnClientDateSelectionChanged="" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="12%" class="label" align="left">
                                        Supplier :
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddlSupplierLedger" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                            Width="180px" DataValueField="LedgerID" DataTextField="LedgerName" OnSelectedIndexChanged="ddlSupplierLedger_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="95%" align="center" class="table">
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:GridView ID="dgvBill" runat="server" Width="100%" AutoGenerateColumns="false"
                                            GridLines="None" AllowPaging="false" DataKeyNames="PurchaseBillId">
                                            <Columns>
                                                <asp:BoundField DataField="SrNo" HeaderText="SL" />
                                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                                <asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" DataFormatString="{0:F2}"
                                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                                <asp:BoundField DataField="DueAmt" HeaderText="Due Amount" DataFormatString="{0:F2}"
                                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text="0.00" onkeyup="CheckAmount(this)"
                                                            onblur="CheckAmount(this)" onkeypress="return AmountOnly('txtAmount',this);"></asp:TextBox>
                                                    </ItemTemplate>
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
                                                            Sorry! No Due Biils Found.
                                                        </td>
                                                </table>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr class="RowStyle">
                                    <td align="left" class="label">
                                        Total
                                    </td>
                                    <td align="left" width="90px">
                                        <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox_yellow" Text="0.00"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="95%" align="center" class="table">
                                <tr>
                                    <td width="15%" align="left" class="label">
                                        Narration :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" CssClass="textbox"
                                            Width="750px" Height="50px" Style="resize: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center" width="95%" class="table">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation();"
                                            OnClick="btnSave_Click"></asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Debit Note Entry</strong>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <ContentTemplate>
                <asp:UpdatePanel runat="server" ID="UpdatePanelVoucherSearch">
                    <ContentTemplate>
                        <table width="90%" align="center" class="table">
                            <tr>
                                <td class="label" valign="middle" align="right">
                                    Voucher No. :
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtVchNoSearch" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                </td>
                                <td class="label" valign="middle" align="right">
                                    Voucher Date :
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtVchDateSearch" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearch"
                                        OnClientDateSelectionChanged="" Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="label" valign="middle" align="right">
                                    To :
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtVchDateSearchTo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearchTo"
                                        OnClientDateSelectionChanged="" Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click">
                                    </asp:Button>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="8">
                                    <asp:GridView ID="dgvDebitNote" runat="server" Width="100%" AllowPaging="false" GridLines="None"
                                        DataKeyNames="DNHeaderID" AutoGenerateColumns="False" OnRowDataBound="dgvDebitNote_RowDataBound"
                                        OnRowDeleting="dgvDebitNote_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="DNVoucherNo" HeaderText="Voucher No."></asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" DataField="DNVoucherDate" HeaderText="Voucher Date">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LedgerName" HeaderText="Supplier" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="Amount" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="DNNarration" HeaderText="Narration"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                                        runat="server" OnClientClick="return confirm('Do you want to delete the voucher?')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" Width="17px"
                                                        Height="17px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Debit Note List</strong>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
