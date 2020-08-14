<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyReceipt.aspx.cs" Inherits="AccountsModule.Accounts.MoneyReceipt" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Money Receipt</title>
    <style type="text/css">
        .text
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 13px;
            font-weight: normal;
            padding-left: 10px;
        }
        .smalltext
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 11px;
            font-weight: normal;
            padding-left: 10px;
        }
        .rowstyle td
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 13px;
            font-weight: normal;
            padding: 3px 10px 3px 10px;
        }
        .headerstyle th
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 13px;
            font-weight: normal;
            padding: 2px 10px 2px 10px;
            border-bottom: solid 1px #000;
        }
        .footerstyle td
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 13px;
            font-weight: normal;
            padding: 3px 10px 3px 10px;
            border-top: solid 1px #000;
        }
    </style>
</head>
<body title="Click on White Area for Print" onclick="window.print()">
    <form id="form1" runat="server">
    <center>
        <div style="padding: 1px 3px 10px 3px; width: 900px;">
            <table width="100%" align="center" cellpadding="0">
                <%--<tr>
                    <td align="center" valign="top">
                        <img src="../Images/ReportHeader.png" height="180px" />
                    </td>
                </tr>--%>
                <tr>
                    <td align="center" class="text"><asp:Literal ID="ltrHeader" runat="server" Mode="PassThrough" 
                                meta:resourcekey="ltrReceiptNoResource1"></asp:Literal><br /></td>
                </tr>
                <tr>
                    <td align="right">
                        <div class="text">
                            <asp:Literal ID="ltrReceiptNo" runat="server" Mode="PassThrough" 
                                meta:resourcekey="ltrReceiptNoResource1"></asp:Literal><br />
                            <asp:Literal ID="ltrPaymentDate" runat="server" Mode="PassThrough" 
                                meta:resourcekey="ltrPaymentDateResource1"></asp:Literal>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrName" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrNameResource1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrStudentCode" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrStudentCodeResource1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrMembershipCategory" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrMembershipCategory1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrOther" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrOtherResource1"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left">
                        <asp:GridView ID="dgvBill" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
                            ShowFooter="True" Width="100%" meta:resourcekey="dgvBillResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="<b>Fees</b>" FooterText="<b>TOTAL</b>" 
                                    meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "fees")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<b>Amount</b>" 
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal ID="ltrTotalAmt" runat="server" Mode="PassThrough" 
                                            meta:resourcekey="ltrTotalAmtResource1"></asp:Literal>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <AlternatingRowStyle CssClass="rowstyle" />
                            <HeaderStyle CssClass="headerstyle" />
                            <FooterStyle CssClass="footerstyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrAmtInWord" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrAmtInWordResource1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrReceiptMode" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrReceiptModeResource1"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrBankName" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrBankNameResource1"></asp:Literal>
                    </td>
                </tr>
                 <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrNarration" runat="server" Mode="PassThrough" 
                            ></asp:Literal>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="smalltext" width="70%" colspan="2">
                        <asp:Literal ID="ltrFooter" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrFooterResource1"></asp:Literal>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="smalltext" width="70%" colspan="2">
                        <i> <asp:Label ID="lblText" runat="server" Text="*Subject to Realisation of Cheque. All disputes are subject to Chandannagar Jurisdiction
                            only."></asp:Label></i>
                    </td>
                    <td align="center"  class="text">
                        <%--<asp:Literal ID="ltrSignature" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrSignatureResource1"></asp:Literal> style="border-top: solid 1px #000;"--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="width:100%"></td>
                </tr>
                 <tr>
                    <td colspan="3" style="width:100%; height:50px"></td>
                </tr>
                </table>
                <table width="100%" align="center">
                
                <tr>
                <% if (Refund == 1)
                   { 
                   %>
                  
                    <td align="center" style="border-top: dotted 1px #000" class="text" width="35%">
                     
                         <asp:Literal ID="ltrPaidBy" runat="server" Mode="PassThrough"></asp:Literal>
                     </td>
                   <%                       
                    }
                   else
                   {%>
                   
                    <td align="center"  class="text" width="30%">
                     
                         <asp:Literal ID="Literal1" runat="server" Mode="PassThrough"></asp:Literal>
                     </td>
                    
                   <%
                   }
                        %>
                     <td style="width:5%"></td>
                     <td align="center"  class="text" width="20%" style="border-top: dotted 1px #000">
                     Approved By
                     </td>
                     <td style="width:5%"></td>
                     <td align="center" style="border-top: dotted 1px #000;" class="text" width="35%">
                        <asp:Literal ID="ltrSignature" runat="server" Mode="PassThrough" 
                            meta:resourcekey="ltrSignatureResource1"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>
