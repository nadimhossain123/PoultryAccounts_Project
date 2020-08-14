<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="True"
    CodeBehind="CashBankVoucher.aspx.cs" Inherits="CollegeERP.Accounts.CashBankVoucher"
    Title="Receipt/Payment Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function changeTab() {
            var tabBehavior = $get('<%=TabContainer1.ClientID%>').control;
            tabBehavior.set_activeTabIndex(0);
        }

        function Validation() {

            if (document.getElementById('<%=ddlType.ClientID %>').selectedIndex == 0) {
                alert('Please Select Voucher Type');
                document.getElementById('<%=ddlType.ClientID %>').focus();
                return false;
            }

            var gv = document.getElementById('<%=gvCshBnk.ClientID%>');
            var rowCount = gv.rows.length - 1;
            var VDate = document.getElementById('<%=txtVoucherDate.ClientID%>').value;

            if (VDate == '') {
                alert("Please Enter Voucher Date");
                return false;
            }
            else if (rowCount == 0) {
                alert("No Item to Save");
                return false;
            }
            else {
                return confirm('Are You Sure?');
            }
        }

        function openPopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 200, top=0, width=920,align=center, height=680,menubar=no, scrollbars=yes, resizable=no';
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
            Receipt/Payment Voucher</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel runat="server" ID="TabPanelEntry">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanelEntry">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="../Images/loading-bar.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanelEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <h6 align="left" style="color: #00356A;">
                            Receipt/Payment Voucher</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td align="left" class="label" width="10%">
                                    Voucher No. :
                                </td>
                                <td align="left" width="20%">
                                    <asp:TextBox ID="txtVchNo" CssClass="textbox_pink" runat="server" Width="190px" ReadOnly="true"></asp:TextBox><input
                                        id="hdnCBVHeaderID" type="hidden" runat="server" />
                                </td>
                                <td align="left" class="label" width="10%">
                                    Voucher Date :
                                </td>
                                <td align="left" width="20%">
                                    <asp:TextBox ID="txtVoucherDate" CssClass="textbox_pink" runat="server" Width="100px"></asp:TextBox><asp:CalendarExtender
                                        ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" PopupPosition="BottomRight"
                                        Format="dd MMM yyyy" TargetControlID="txtVoucherDate" OnClientDateSelectionChanged=""
                                        Enabled="True">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="label" width="10%">
                                    Cash/Bank Ledger:
                                </td>
                                <td align="left" width="20%">
                                    <asp:ComboBox ID="ddlSourceLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" AutoPostBack="true" Width="200px"
                                        OnSelectedIndexChanged="ddlSourceLedger_SelectedIndexChanged">
                                    </asp:ComboBox>
                                </td>
                                <td align="left" class="label" width="10%">
                                    Voucher Type :
                                </td>
                                <td align="left" width="20%">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="120px">
                                        <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                        <asp:ListItem Value="PAYMENT">PAYMENT</asp:ListItem>
                                        <asp:ListItem Value="RECEIVE">RECEIVE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td width="10%">
                                </td>
                                <td align="left" colspan="5" class="text">
                                    <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <h6 align="left" style="color: #00356A;">
                            Receipt/Payment Voucher Details</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td width="30%" align="left" class="label">
                                    Ledger Name
                                </td>
                                <td width="15%" align="left" class="label">
                                    Cost Center
                                </td>
                                <td width="10%" align="left" class="label">
                                    DR/CR
                                </td>
                                <td width="10%" align="left" class="label">
                                    Amt.
                                </td>
                                <td class="label" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" class="label" align="left">
                                    <asp:ComboBox ID="ddlLedg" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                        DataTextField="LedgerName" DataValueField="LedgerID" AutoCompleteMode="SuggestAppend"
                                        CaseSensitive="false" Width="240px">
                                    </asp:ComboBox>
                                </td>
                                <td width="15%" class="label" align="left">
                                    <asp:DropDownList ID="ddlCostCentre" runat="server" CssClass="dropdownList" Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td width="10%" class="label" align="left">
                                    <asp:DropDownList ID="ddlDRCR" runat="server" CssClass="dropdownList" Width="100px" Enabled="false">
                                        <asp:ListItem Value="RECEIVE" Text="DR"></asp:ListItem>
                                        <asp:ListItem Value="PAYMENT" Text="CR"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="10%" class="label" align="left">
                                    <asp:TextBox ID="txtAmt" CssClass="textbox" onkeypress="return AmountOnly('txtAmt',this);"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label" align="left">
                                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text="Add" CssClass="button">
                                    </asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    <asp:GridView ID="gvCshBnk" runat="server" AllowPaging="false" Width="100%" AutoGenerateColumns="False"
                                        OnRowEditing="gvCshBnk_RowEditing" OnRowDeleting="gvCshBnk_RowDeleting" ShowFooter="true" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="SrlNo" HeaderText="Sl No."></asp:BoundField>
                                            <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" FooterText="<b>Total</b>">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CostCenterName" HeaderText="Cost Center" FooterText="<b>Total</b>">
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:F2}" DataField="DRAmount" HeaderText="DR Amt">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:F2}" DataField="CRAmount" HeaderText="CR Amt">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnModify" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                                        runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnModify" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                                        runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table style="height: 10px; width: 100%;">
                                                <tr align="left" class="HeaderStyle">
                                                    <th scope="col">
                                                        No Records
                                                    </th>
                                                </tr>
                                                <tr class="RowStyle">
                                                    <td>
                                                        Sorry! No Records
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
                                <td align="left" class="label" colspan="5">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="label" colspan="5">
                                    On Account :
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="5">
                                    <asp:TextBox ID="txtAccount" CssClass="textbox" ReadOnly="true" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="label" colspan="5">
                                    Narration :
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="5">
                                    <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Width="870px" TextMode="MultiLine"
                                        Height="30px" Style="resize: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button"
                                        OnClientClick="return Validation()"></asp:Button>&#160;&nbsp;
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Reset"
                                        CssClass="button"></asp:Button>&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" OnClick="btnPrint_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Receipt/Payment Voucher Entry</b>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanelView">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelView" runat="server">
                    <ContentTemplate>
                        <table width="100%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <table border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="label" align="right">
                                                                        Voucher Type :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlVoucherType" CssClass="dropdownList" runat="server" Width="140px">
                                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                            <asp:ListItem Value="PAYMENT" Text="PAYMENT"></asp:ListItem>
                                                                            <asp:ListItem Value="RECEIVE" Text="RECEIVE"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label" nowrap align="right">
                                                                        C/B Book&#160;Name :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlLedgerVw" CssClass="dropdownList" runat="server" Width="140px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label" align="right">
                                                                        Voucher&#160;Date&#160;From :
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtVDateFromVW" CssClass="textbox" runat="server"></asp:TextBox><asp:CalendarExtender
                                                                            ID="CalendarExtender3" runat="server" CssClass="cal_Theme1" PopupPosition="BottomRight"
                                                                            Format="dd MMM yyyy" TargetControlID="txtVDateFromVW" OnClientDateSelectionChanged=""
                                                                            Enabled="True">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label" nowrap align="right">
                                                                        Voucher&#160;Date&#160;To :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtVDateToVW" CssClass="textbox" runat="server"></asp:TextBox><asp:CalendarExtender
                                                                            ID="CalendarExtender5" runat="server" CssClass="cal_Theme1" PopupPosition="BottomRight"
                                                                            Format="dd MMM yyyy" TargetControlID="txtVDateToVW" OnClientDateSelectionChanged=""
                                                                            Enabled="True">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label" align="right">
                                                                        Voucher&#160;No&#160;:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtVoucherNoVw" CssClass="textbox" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click">
                                                                        </asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvCBVView" runat="server" OnRowDataBound="gvCBVView_RowDataBound"
                                                            OnRowCommand="gvCBVView_RowCommand" AutoGenerateColumns="False" Width="100%"
                                                            GridLines="None" PageSize="20" OnPageIndexChanging="gvCBVView_PageIndexChanging"
                                                            OnRowDeleting="gvCBVView_RowDeleting" AllowPaging="True"
                                                            DataKeyNames="CBVHeaderID" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="VoucherDate"
                                                                    HeaderText="Date"></asp:BoundField>
                                                                <asp:BoundField DataField="CBVoucherNo" HeaderText="Voucher No"></asp:BoundField>
                                                                <asp:BoundField DataField="LedgerName" HeaderText="Book Name"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="Ledger Name(s)/Member Name">
                                                                    <ItemTemplate> <%# Server.HtmlDecode(Eval("TrnLedgerName").ToString()) %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TransactionType" HeaderText="Transaction Type" />
                                                                <asp:BoundField DataFormatString="{0:F2}" DataField="TotalAmount" HeaderText="Amount">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnEdit" CommandName="imgbtnEdit" ImageUrl="~/Images/edit_icon.gif"
                                                                            runat="server" CommandArgument='<%# Eval("CBVHeaderID") %>' /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnPrint" CommandName="imgbtnPrint" ImageUrl="~/Images/print.gif"
                                                                            Width="17px" Height="17px" runat="server" CommandArgument='<%# Eval("CBVHeaderID") + "," + Eval("CBVoucherNo") %>' /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                                                            runat="server" OnClientClick="return confirm('Are You Sure?')" /></ItemTemplate>
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
                                                                            Sorry! No Records Found.
                                                                        </td>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <PagerSettings Mode="Numeric" PageButtonCount="8" />
                                                            <HeaderStyle CssClass="HeaderStyle" />
                                                            <RowStyle CssClass="RowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Show Receipt/Payment Voucher</b>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
