<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sms-receipt.aspx.cs" Inherits="AccountsModule.Accounts.sms_receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="cursor: pointer; font-family: Calibri; font-size: 16px;" onclick="window.print()"
    onload="window.print()">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="left" colspan="2">
                    SL No.-
                    <asp:Label ID="lblSLNo" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" colspan="2">
                    Date :-
                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 28px; font-weight: bold;" align="center">
                    <u>West Bengal Poultry Federation</u>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    46/C Chowringhee Road, Everest Building 11th Floor, Room No C,
                    <br />
                    Kolkata - 700071, Ph.: 40515700, Fax : 2288 5525<br />
                    E-mail : wbpoultryfederation.yahoo.in
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: 16px; font-weight: bold;">
                    <u>Miscellaneous Collection Receipt</u><br />
                    <br />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 110px;">
                    <b>Mr./Miss/M/s. :</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 50px;">
                    <b>P.O.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 200px;">
                    <asp:Label ID="lblPO" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 60px;">
                    <b>District</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" style="width: 200px;">
                    <b>we received with thanks for</b>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <%--<tr>
                <td>
                    <b>1. Registration for suppliers from outside state (yearly fees)</b>
                </td>
                <td align="right">
                    <b>Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>2. Registration for Register traders / wholesalers from Outside State (yearly fees)</b>
                </td>
                <td align="right">
                    <b>Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                </td>
            </tr>--%>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gvCshBnk" runat="server" AllowPaging="false" Width="100%" AutoGenerateColumns="False"
                        ShowFooter="true" GridLines="None" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="SrlNo" HeaderText="Sl No."></asp:BoundField>
                            <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" FooterText="<b>Total</b>">
                            </asp:BoundField>
                            <%--<asp:BoundField DataFormatString="{0:F2}" DataField="DRAmount" HeaderText="DR Amt">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataFormatString="{0:F2}" DataField="CRAmount" HeaderText="Amount">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <FooterStyle CssClass="RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <b>3. Registration mobile message for rate (yearly fees)</b>
                </td>
                <td align="right">
                    <b>Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                    <asp:Label ID="lblMessageRate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>4. G.S.T (18 %)</b>
                </td>
                <td align="right">
                    <b>Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                    <asp:Label ID="lblMessageTax" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>5. Others</b>
                </td>
                <td align="right">
                    <b>Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                    <asp:Label ID="lblOthers" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <b>Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Rs.</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 100px;">
                    <asp:Label ID="lblMessageTotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 160px;">
                    <b>By Cash / Cheque No. </b>
                </td>
                <td style="border-bottom: solid 1px black;">
                </td>
                <td style="width: 100px;">
                    <b>Bank, Dated</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 400px;">
                    Federation is not liable for non delivery message problem
                    <br />
                    for any type of mobile net-work disturbance.
                </td>
                <td colspan="2">
                    <b>Treasurer</b>
                </td>
                <td>
                    <b>Collector</b>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
