<%@ Page Title="Member Payment" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberPayment.aspx.cs" Inherits="AccountsModule.Common.MemberPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (parseFloat(document.getElementById('<%=txtPaymentAmount.ClientID %>').value.trim()) == 0) {
                alert("Payment amount should be greater than zero");
                return false;
            }
            else if (document.getElementById('<%=ddlCashBankLedger.ClientID %>').selectedIndex == 0) {
                alert("Please Select Cash/Bank Ledger");
                return false;
            }
            else {
                return confirm("Are you sure?");
            }
        }

        function CalculateTotalAmount() {
            var totalamt = 0;
            var value;

            var gv = document.getElementById('<%=dgvMemberOutstanding.ClientID%>');
            var rCount = gv.rows.length;
            var rowIdx = 1;

            for (rowIdx; rowIdx <= rCount - 1; rowIdx++) {
                var rowElement = gv.rows[rowIdx];
                value = rowElement.cells[3].getElementsByTagName("input")[0].value.trim();
                totalamt = totalamt + ((value == '') ? 0 : parseFloat(value));

                value = rowElement.cells[4].getElementsByTagName("input")[0].value.trim();
                totalamt = totalamt + ((value == '') ? 0 : parseFloat(value));
            }
            document.getElementById('<%=txtPaymentAmount.ClientID%>').value = totalamt.toFixed(2);
            document.getElementById('<%=hdnAmount.ClientID%>').value = totalamt.toFixed(2);
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            color: #333333;
            font-family: Tahoma;
            font-size: 8.5pt;
            padding-bottom: 3px;
            height: 37px;
        }
        .auto-style2 {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Payment</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 700px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td width="20%" class="auto-style1">
                            Payment No:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="textbox" Width="200px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Mobile No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox" Width="200px" MaxLength="10"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="label">
                            Member Name:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="450px"
                                AutoCompleteMode="SuggestAppend" DataValueField="MemberId" DataTextField="Name"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="dgvMemberOutstanding" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="Both" AllowPaging="false" DataKeyNames="FeesHeadId">
                                <Columns>
                                    <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head" />
                                    <asp:BoundField DataField="FeesOutstandingAmount" HeaderText="Fees Outstanding" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TaxOutstandingAmount" HeaderText="Tax Outstanding" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField HeaderText="Fees Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFeesPaymentAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("FeesPaymentAmount") %>'
                                                onkeyup="CalculateTotalAmount()" onkeypress="return AmountOnly('txtFeesPaymentAmount',this);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaxPaymentAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("TaxPaymentAmount") %>'
                                                onkeyup="CalculateTotalAmount()" onkeypress="return AmountOnly('txtTaxPaymentAmount',this);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Found
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Total Payment Amount:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="textbox" Width="90px"
                                Enabled="false" Style="text-align: right; padding-right: 8px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Payment Mode:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="150px">
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Bank</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem>Bank Draft</asp:ListItem>
                                <asp:ListItem>Online Payment</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Payment Date:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="textbox" Width="100px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtenderPaymentDate" runat="server" CssClass="cal_Theme1"
                                PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtPaymentDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Cash/Bank Ledger:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCashBankLedger" runat="server" CssClass="dropdownList" DataValueField="LedgerID"
                                DataTextField="LedgerName" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Label ID="lblOpeningBalance" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Narration:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Width="350px" Height="60px"
                                TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" 
                                onclick="btnPrint_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>        
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave"/>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField runat="server" ID="hdnAmount"/>
</asp:Content>
