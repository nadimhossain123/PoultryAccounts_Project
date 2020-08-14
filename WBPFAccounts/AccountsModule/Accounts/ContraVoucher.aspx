<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ContraVoucher.aspx.cs" Inherits="CollegeERP.Accounts.ContraVoucher"
    Title="Contra Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function changeTab() {
            var tabBehavior = $get('<%=TabContainer1.ClientID%>').control;
            tabBehavior.set_activeTabIndex(0);
        }

        function Validation() {
            if (document.getElementById('<%=txtVoucherDate.ClientID%>').value == '') {
                alert("Please Enter Voucher Date");
                return false;
            }
            else if (document.getElementById('<% = txtAmount.ClientID%>').value == '') {
                alert('Enter Amount');
                return false;
            }
            else if (parseFloat(document.getElementById('<% = txtAmount.ClientID%>').value) == 0) {
                alert('Enter Valid Amount');
                return false;
            }
            else return confirm('Are You Sure?');
        }

        function OnChangeToOther(amt) {
            document.getElementById('<% = txtAmountCopy.ClientID%>').value = amt;
        }

        function openPopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';
            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Contra Voucher</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="TabPanel2" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelVoucherEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <h6 align="left" style="color: #00356A;">
                            Contra Voucher Information</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td width="10%" class="label" align="left">
                                    Voucher No. :
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox ID="txtVchNo" CssClass="textbox_pink" runat="server" Width="180px" Enabled="false"></asp:TextBox><%--ontextchanged="txtVchNo_TextChanged"--%>
                                </td>
                                <td width="10%" class="label" align="center">
                                    Voucher Date :
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="textbox_pink" Width="120px">
                                    </asp:TextBox><%--ontextchanged="txtVoucherDate_TextChanged"--%>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVoucherDate"
                                        OnClientDateSelectionChanged="" Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                                <td width="10%" class="label" align="left">
                                    Cheque Date. :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Width="120px">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDate"
                                        OnClientDateSelectionChanged="" Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td width="10%" class="label" align="left">
                                    Cheque No. :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                </td>
                                <td width="10%" class="label" align="center">
                                    Drawn On. :
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtDrawnOn" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left" colspan="5">
                                    If Cheque No is left blank,<br />
                                    Then mode of transaction will be considered as 'CASH'
                                </td>
                            </tr>
                        </table>
                        <h6 align="left" style="color: #00356A;">
                            Particulars</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td class="label" align="left" width="40%">
                                    Cash/Bank Book
                                </td>
                                <td class="label" align="left" width="5%">
                                    Dr/Cr
                                </td>
                                <td class="label" align="left">
                                    Amount
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="40%">
                                    <asp:ComboBox ID="ddlLedger" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                        DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                        Width="380px" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                    </asp:ComboBox>
                                </td>
                                <td align="left" width="8%">
                                    <asp:DropDownList ID="ddlDrCr" runat="server" CssClass="dropdownList" Width="50px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDrCr_SelectedIndexChanged">
                                        <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                                        <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAmount" CssClass="textbox" Style="text-align: right; padding-right: 5px;"
                                        onkeypress="return AmountOnly('txtAmount',this);" runat="server" Width="100px"
                                        onblur="OnChangeToOther(this.value)" onkeyup="OnChangeToOther(this.value)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" class="text">
                                    <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="40%">
                                    <asp:ComboBox ID="ddlParentLedger" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                        DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                        Width="380px" OnSelectedIndexChanged="ddlParentLedger_SelectedIndexChanged">
                                    </asp:ComboBox>
                                </td>
                                <td align="center" width="8%" class="label">
                                    <asp:Literal ID="ltrDrCr" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAmountCopy" CssClass="textbox" ReadOnly="true" runat="server"
                                        Style="text-align: right; padding-right: 5px;" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" class="text">
                                    <asp:Literal ID="ltrParentLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtNarration" CssClass="textbox" runat="server" TextMode="MultiLine"
                                        Width="81%" Height="45px" Style="resize: none;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table align="center" width="95%" class="table">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button"
                                            OnClientClick="return Validation();"></asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="button">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Contra Voucher Information Entry</strong>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <ContentTemplate>
                <asp:UpdatePanel runat="server" ID="UpdatePanelVoucherSearch">
                    <ContentTemplate>
                        <table width="98%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>To Cash/Bank Book</td>
                                                    <td>From</td>
                                                    <td>Voucher No. :</td>
                                                    <td>Voucher Date :</td>
                                                    <td>To :</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ComboBox ID="ddlLedgerSearch" runat="server" CssClass="WindowsStyle" 
                                                            DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                                            Width="200px" >
                                                        </asp:ComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:ComboBox ID="ddlParentLedgerSearch" runat="server" CssClass="WindowsStyle" 
                                                            DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                                            Width="200px">
                                                        </asp:ComboBox>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchNoSearch" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearch" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearch"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearchTo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearchTo"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                                                            CssClass="button"></asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="grdvwtrnsctnsearch" runat="server" Width="100%" AllowPaging="false"
                                            GridLines="None" AllowSorting="false" DataKeyNames="CVHeaderID" AutoGenerateColumns="False"
                                            OnRowEditing="grdvwtrnsctnsearch_RowEditing" OnRowDataBound="grdvwtrnsctnsearch_RowDataBound"
                                            OnRowDeleting="grdvwtrnsctnsearch_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="CVoucherNo" HeaderText="Voucher No."></asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" DataField="CVoucherDate" HeaderText="Voucher Date">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Book" />
                                                <asp:BoundField DataField="ParentLedgerName" HeaderText="From Cash/Bank Book" />
                                                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" />
                                                <asp:BoundField DataField="DrAmount" HeaderText="Amount(Dr)" DataFormatString="{0:F2}" />
                                                <asp:BoundField DataField="CrAmount" HeaderText="Amount(Cr)" DataFormatString="{0:F2}" />
                                                <asp:BoundField DataField="CVNarration" HeaderText="Narration"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnedit" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                                            runat="server" OnClientClick="return confirm('Do you want to delete this voucher?')" />
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
                                            <PagerStyle CssClass="PagerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right">
                                        <asp:Button ID="btnPrintReport" runat="server" CssClass="button" Text="Print" OnClick="btnPrintReport_Click" />&nbsp;
                                        <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                                            OnClick="btnExportExcel_Click" />
                                    </td>
                                </tr>--%>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Show Contra Voucher Information</strong>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
