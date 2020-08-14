<%@ Page Title="SMS Payment" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="SMSPayment.aspx.cs" Inherits="AccountsModule.Common.SMSPayment" %>

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
            var checkbox;

            var gv = document.getElementById('<%=dgvSMS.ClientID%>');
            var rCount = gv.rows.length;
            var rowIdx = 1;

            for (rowIdx; rowIdx <= rCount - 1; rowIdx++) {
                var rowElement = gv.rows[rowIdx];

                checkbox = rowElement.cells[0].getElementsByTagName("input")[0];

                if (checkbox.checked) {
                    value = rowElement.cells[2].innerText;
                    totalamt = totalamt + ((value == '') ? 0 : parseFloat(value));

                    value = rowElement.cells[3].getElementsByTagName("input")[0].value.trim();
                    totalamt = totalamt + ((value == '') ? 0 : parseFloat(value));
                }
            }
            document.getElementById('<%=txtPaymentAmount.ClientID%>').value = totalamt.toFixed(2);
        }

        function CalculateSMSEndDate(PaymentDate)
        {
            var txtSMSEndDate = document.getElementById('<%=txtSubscriptionEndDate.ClientID%>');

            if (PaymentDate != "")
            {
                var from = PaymentDate.split("/");
                var f = new Date(from[2], from[1], from[0]);
                f.setFullYear(f.getFullYear() + 1);
                txtSMSEndDate.value = f.format("dd/MM/yyyy");
            }
            else
            {
                txtSMSEndDate.value = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            SMS Payment</h5>
    </div>
    <%--<asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>--%>
            <div style="width: 700px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td width="20%" class="label">
                            Payment No:
                        </td>
                        <td>
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
                           SMS Member Name:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlSMSMember" runat="server" CssClass="WindowsStyle" Width="450px"
                                AutoCompleteMode="SuggestAppend" DataValueField="SMSMemberId" DataTextField="MemberName"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSMSMember_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="dgvSMS" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="Both" AllowPaging="false" 
                                DataKeyNames="SMSCategoryId,FeesPaymentId" onrowdatabound="dgvSMS_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" onclick="CalculateTotalAmount()" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SMSCategoryName" HeaderText="SMS Category" />
                                    <%--<asp:BoundField DataField="SubscriptionStartDate" HeaderText="Subscription Start Date" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="" />--%>
                                    <%--<asp:BoundField DataField="SubscriptionEndDate" HeaderText="SMS Subscription End Date" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="" />--%>
                                    <asp:BoundField DataField="SMSAmount" HeaderText="SMS Amount" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <%--<asp:BoundField DataField="TaxAmount" HeaderText="Tax Amount" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" />--%>
                                    <asp:TemplateField HeaderText="Tax Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaxPaymentAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("TaxAmount") %>'
                                                onkeypress="return AmountOnly('txtTaxPaymentAmount',this);"></asp:TextBox>
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
                    <tr><td colspan="2"><br /></td></tr>
                    <tr>
                        <td class="label">
                            <b>Total Payment Amount:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="textbox" Width="90px" onkeydown="return false;"
                                 Style="text-align: right; padding-right: 8px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Payment Mode:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="110px">
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
                                 onkeydown="return false;" onblur="CalculateSMSEndDate(this.value.trim());"></asp:TextBox>
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
                            <asp:DropDownList ID="ddlCashBankLedger" runat="server" CssClass="dropdownList" 
                                DataValueField="LedgerID" DataTextField="LedgerName" Width="250px" 
                                AutoPostBack="true" 
                                onselectedindexchanged="ddlCashBankLedger_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;
                            <asp:Label ID="lblOpeningBalance" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>SMS Subscription End Date:</b></td>
                        <td>
                            <asp:TextBox ID="txtSubscriptionEndDate" runat="server" CssClass="textbox" 
                                Width="100px" onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSubscriptionEndDate_CalendarExtender" runat="server" 
                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                OnClientDateSelectionChanged="" PopupPosition="TopRight" 
                                TargetControlID="txtSubscriptionEndDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <b>Narration:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Height="60px" 
                                MaxLength="500" TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" OnClick="btnPrint_Click">
                                    </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
