<%@ Page Title="Member Fees Collection" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberFeesCollection.aspx.cs" Inherits="AccountsModule.Accounts.MemberFeesCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validation() {
            var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var rowCount = gv.rows.length - 1;

            if (document.getElementById('<%=txtVoucherDate.ClientID%>').value == '')
                return ShowMsg("Please Enter Voucher Date");
            else if (rowCount == 0)
                return ShowMsg("No Payment To Save");
            else if (parseFloat(document.getElementById('<%=txtTotalAmt.ClientID%>').value) == 0)
                return ShowMsg("Zero Amount is Not Allowed");
            // else if (document.getElementById('<%=txtFeesBookNo.ClientID%>').value == '')
            //     return ShowMsg("Please Enter Fees Book Number");
            else
                return confirm('Do you want to save?');
        }

        function ShowMsg(str) {
            alert(str);
            return false;
        }

        function TotalAmount() {
            var amt = 0;
            var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var rCount = gv.rows.length;
            var rowIdx = 1;
            for (rowIdx; rowIdx <= rCount - 1; rowIdx++) {
                var rowElement = gv.rows[rowIdx];
                var txtBox = rowElement.cells[4].getElementsByTagName("input")[0];
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value));
//                if (rowElement.cells[5].getElementsByTagName("input")[0]) {
//                    var txtBox2 = rowElement.cells[5].getElementsByTagName("input")[0];
//                    amt = amt + ((txtBox2.value == '') ? 0 : parseFloat(txtBox2.value));
//                }
            }
            document.getElementById('<%=txtTotalAmt.ClientID%>').value = amt.toFixed(2);
        }

        function moveEnter(rowIndex) {
            if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
                var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
                var rCount = gv.rows.length;

                if (rowIndex < rCount) {
                    var rowElement = gv.rows[parseInt(rowIndex + 1)];
                    rowElement.cells[4].getElementsByTagName("input")[0].focus();
                }

                event.preventDefault();
            }

            TotalAmount();

        }

        function openPopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';
            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }

        function setFocuss() {
            document.getElementById('<%=btnSave.ClientID%>').focus();
            return true;
        }
    </script>
    <style type="text/css">
        .text
        {
            font-family: Helvetica;
            font-size: 13px;
            font-weight: normal;
            padding: 7px 0 0 15px;
        }
        .style1
        {
            width: 28%;
        }
        .style2
        {
            width: 223px;
        }
        .divWaiting
        {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Accounting Voucher Creation For Members</h5>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="../Images/loading-bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <table width="75%" align="center" class="table">
                <tbody>
                    <tr>
                        <td align="left" class="label" width="10%">
                            Money Receipt No :
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox_pink" Enabled="false"
                                Width="250px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="10%">
                            Voucher Date :
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="textbox_pink" Width="250px">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVoucherDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="10%">
                            Account (Dr) :
                        </td>
                        <td align="left" width="20%">
                            <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="250px"
                                DataValueField="LedgerID" DataTextField="LedgerName" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged"
                                TabIndex="1">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                        </td>
                        <td align="left" colspan="5" class="text">
                            <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <h6 align="left" style="color: #00356A;">
                Member Particulars</h6>
            <table width="75%" align="center" class="table">
                <tr>
                    <td width="15%" align="left" class="label">
                        <asp:Label ID="lblMemberName" runat="server" Text="Member Name (Cr) :"></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="340px" Font-Bold="true"
                            TabIndex="2">
                        </asp:ComboBox>
                        <asp:Label ID="lblDropout" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Unpaid List"
                            OnClick="btnSearch_Click" Style="margin-top: 10px" TabIndex="3" />
                    </td>
                </tr>
            </table>
            <table width="75%" align="center">
                <tr>
                    <td align="center" width="15%" valign="top">
                        <asp:Image ID="ImgPhoto" runat="server" ImageUrl="~/Images/Male.JPG" Width="120px"
                            Height="130px" />
                    </td>
                    <td align="center" valign="top" colspan="2">
                        <asp:GridView ID="dgvFeesHead" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false"
                            AllowPaging="false" DataKeyNames="id,AssestLedgerID_FK" OnRowDataBound="dgvFeesHead_RowDataBound"
                            TabIndex="4">
                            <Columns>
                                <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                <asp:BoundField DataField="BillAmount" HeaderText="Bill" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Due" HeaderText="Due" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Advance" HeaderText="Advance" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px" />
                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text="0.00" onkeyup="TotalAmount()"
                                            onkeypress="return AmountOnly('txtAmount',this);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Non Taxable Amount" ItemStyle-Width="50px" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmountNonTaxable" runat="server" CssClass="textbox_green" Text="0.00" onkeyup="TotalAmount()"
                                        onkeypress="return AmountOnly('txtAmount',this);" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                                            Sorry! No Payment.
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
                    <td width="15%">
                    </td>
                    <td align="left" class="label">
                        Total
                    </td>
                    <td align="left" width="98px">
                        <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox_yellow" Text="0.00"
                            Enabled="false" TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="75%" align="center" class="table">
                <tr>
                    <td align="left" width="15%" valign="top" class="label">
                        Narration:
                    </td>
                    <td align="left" width="80%" colspan="3">
                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" CssClass="textbox"
                            Width="750px" Height="40px" Style="resize: none;"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td align="left" class="label" width="15%">
                        Mode Of Payment :
                    </td>
                    <td align="left" width="85%" colspan="3">
                        <asp:DropDownList ID="ddlReceiptMode" runat="server" CssClass="dropdownList" Width="120px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptMode_SelectedIndexChanged"
                            onblur="javascript:return setFocuss()">
                            <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                            <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none">
                    <td align="left" class="label" width="15%">
                        Ch. No :
                    </td>
                    <td align="left" width="35%">
                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="120px" MaxLength="6"
                            TabIndex="12"></asp:TextBox>
                    </td>
                    <td align="left" class="label" width="15%">
                        Ch. Date :
                    </td>
                    <td align="left" width="35%">
                        <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Width="120px" TabIndex="13">
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr style="display: none">
                    <td width="10%" align="left" class="label">
                        Fees Book No
                        <%--<span class="req">*</span>--%>
                    </td>
                    <td align="left" width="20%" style="padding-top: 10px;">
                        <asp:TextBox ID="txtFeesBookNo" runat="server" CssClass="textbox" Width="120px" TabIndex="14"></asp:TextBox>
                    </td>
                    <td width="10%" align="left" class="label">
                        Drawn On :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDrawnOn" runat="server" CssClass="textbox" Width="120px" TabIndex="15"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" class="table" width="60%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" OnClientClick="return Validation()"
                            OnClick="btnSave_Click" Text="Save" TabIndex="16" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" TabIndex="17" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                            Text="Reset" TabIndex="18" />
                        &nbsp;
                        <%--<asp:Button ID="Button1" runat="server" CssClass="button" Text="Print" OnClick="return ClearText()" />--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
