<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="PurchaseBillPayment.aspx.cs" Inherits="CollegeERP.Accounts.PurchaseBillPayment"
    Title="Purchase Bill Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
      function Validation()
       {
          var Amount=document.getElementById('<%=txtTotalAmt.ClientID%>').value;
          if (document.getElementById('<%=ddlCashBankLedger.ClientID%>').selectedIndex == 0)
          {
             alert("Select Cash/Bank Ledger");
             return false;
          }
          else if (Amount == '' || parseFloat(Amount) == 0)
          {
             alert('Please Enter Valid Amount'); 
             return false;
          }
          else 
          {
             return confirm('Are You Sure?');
          }
       }
       
       function CheckAmount(Obj) {
          var row = Obj.parentNode.parentNode;
          var rowIndex = row.rowIndex - 1;
          var Due = row.cells[4].innerHTML;
                    
          var Amt= (Obj.value == '') ? '0' : Obj.value;
          if (parseFloat(Amt) > parseFloat(Due)) {
              alert("Amout Should Not Be More Than Due");
              Obj.focus();
          }
          TotalAmount();
      }

      function TotalAmount() {
            var amt = 0;
            var gv = document.getElementById('<%=dgvBill.ClientID%>');
            var rowIdx = 1;
            for (rowIdx; rowIdx <= gv.rows.length -1 ; rowIdx++) {
                var rowElement = gv.rows[rowIdx];
                var txtBox = rowElement.cells[5].getElementsByTagName("input")[0];
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value));
             
            }
            document.getElementById('<%=txtTotalAmt.ClientID%>').value = amt.toFixed(2);
        }
        
       function DeductionValidation()
       {
            if (document.getElementById('<%=ddlDeductionLedger.ClientID%>').selectedIndex == 0)
            {
                alert("Select Deduction Ledger");
                return false;
            }
            else if (document.getElementById('<%=txtDeductionAmount.ClientID%>').value == '' || parseFloat(document.getElementById('<%=txtDeductionAmount.ClientID%>').value) == 0)
            {
                alert("Enter Deduction Amount");
                return false;
            }
            else
            {
                return true;
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
        .style1
        {
            color: #000000;
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 10px;
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Purchase Bill Payment</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Payment Details</h6>
            <div style="width: 950px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="10%" align="left" class="label">
                            Cash/Bank A/C:
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlCashBankLedger" runat="server" CssClass="dropdownList" Width="200px"
                                AutoPostBack="true" DataValueField="LedgerID" DataTextField="LedgerName" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="label" width="10%">
                            Payment Mode :
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="120px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                                <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="label" width="10%">
                            Ch. No :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="120px" MaxLength="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                        </td>
                        <td align="left" colspan="5" class="text">
                            <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left" class="label">
                            Supplier A/C:
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlSupplierLedger" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="200px" DataValueField="LedgerID" DataTextField="LedgerName" OnSelectedIndexChanged="ddlSupplierLedger_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="label" width="10%">
                            Drawn On :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtDrawnOn" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="10%">
                            Ch. Date :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Width="120px" onkeydown="return false;">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left" class="label">
                            Payment Date:
                        </td>
                        <td align="left" colspan="5" style="padding-top: 10px;">
                            <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="textbox_pink" Width="120px"
                                onkeydown="return false;">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtPaymentDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                Outstanding Bill Details</h6>
            <div style="width: 950px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="dgvBill" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="PurchaseBillId">
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="SL" ItemStyle-Width="20px" />
                                    <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                    <asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                    <asp:BoundField DataField="DueAmt" HeaderText="Outstanding" DataFormatString="{0:F2}"
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
                                                Sorry! No Outstanding Biils Found.
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
                        <td align="left" class="style1">
                            Total
                        </td>
                        <td align="right" width="10px">
                            <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox_yellow" Text="0.00"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <h6 align="left" style="color: #00356A;">
                Tax Deduction Details</h6>
            <div style="width: 950px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="10%" align="left" class="label">
                            Deduction A/C:
                        </td>
                        <td width="30%" align="left">
                            <asp:DropDownList ID="ddlDeductionLedger" runat="server" CssClass="dropdownList"
                                Width="220px" DataValueField="LedgerID" DataTextField="LedgerName">
                            </asp:DropDownList>
                        </td>
                        <td width="10%" align="left" class="label">
                            Amount:
                        </td>
                        <td width="15%" align="left">
                            <asp:TextBox ID="txtDeductionAmount" runat="server" CssClass="textbox" Width="100px"
                                onkeypress="return AmountOnly('txtDeductionAmount',this);"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClientClick="return DeductionValidation();"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="dgvDeduction" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="DeductionLedgerId" OnRowDeleting="dgvDeduction_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete_icon.gif"
                                                CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DeductionHead" HeaderText="Deduction A/C" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" />
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
                                                Hooray! No Deduction.
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
                        <td align="left" class="style1">
                            Total
                        </td>
                        <td align="right" width="10px">
                            <asp:TextBox ID="txtTotalDeductionAmt" runat="server" CssClass="textbox_yellow" Text="0.00"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 950px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="10%" class="label">
                            Narration
                        </td>
                        <td align="left" colspan="7">
                            <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" CssClass="textbox"
                                Width="750px" Height="50px" Style="resize: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />&nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
