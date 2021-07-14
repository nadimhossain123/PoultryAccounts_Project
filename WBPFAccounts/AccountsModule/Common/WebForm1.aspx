<%@ Page Language='C#' AutoEventWireup='true' CodeBehind="WebForm1.aspx.cs" Inherits="NCCPayroll.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onclick="window.print()">
    <form id="form1" runat="server">
    <table width='100%'>
        <tr>
            <td align="center">
                <b><span style="font-family: Calibri; font-size: 44px;"><u>WEST BENGAL POULTRY FEDERATION</u></span>
                    </b>
            </td>
        </tr>
        <tr>
            <td align="center">
                46C, Chowringhee Road, 11th Floor, Room No - C, Kolkata - 700071
            </td>
        </tr>
        <tr>
            <td align="center">
                Phone No. 033 - 65229085 / 40175700
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width='100%' cellpadding="3" style="border: 1px solid black;">
        <tr>
            <td colspan="2" align="center">
                <h2>
                    <b><u>Reimbursement of Monthly Subscription / Renewal fees</u> </b>
                </h2>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                Bill No : 
                <asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label>
            </td>
            <td align="right">
                Date: <asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                To
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label>
                
                <br />
                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
            </td>
            <td align="right" valign="top">
                <b><u>Membership No. <asp:Label ID="lblMemberCode" runat="server" Text=""></asp:Label></u></b>
            </td>
        </tr>
    </table>
    <table border="1" cellpadding="5" cellspacing="0" width="100%" style="border-collapse: collapse;">
        <tr>
            <td style='width: 33%' align="center">
                <b><u>Bill Period</u></b>
            </td>
            <td style='width: 53%' align="center">
                <b><u>Description</u></b>
            </td>
            <td style='width: 13%' align="center">
                <b><u>Amount</u></b>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
            </td>
            <td>
                Monthly Subscription / Renewal fees @ Rs <asp:Label ID="lblMonthlyRate" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
            <td align="right" valign="top">
                <asp:Label ID="lblFinalAmount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
       <%-- <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                G.S.T @ 18%
            </td>
            <td align="right">
                <asp:Label ID="lblServiceTax" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" align="right">
               <b>Total : </b>
            </td>
            <td align="right">
                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table width='100%' cellpadding="3" style="border: 1px solid black;">
        <tr>
            <td style="width: 50%">
                &nbsp;
            </td>
            <td align="right">
                E. &amp; O.E.
            </td>
        </tr>
        <tr>
            <td>
                <u><b>Conditions:</u> </b>
            </td>
            <td align="right">
                For West Bengal Poultry Federation
            </td>
        </tr>
        <tr>
            <td colspan="2">
                1. Payments can be made by A/C payee cheque/NEFT/RTGS/Online Payment.
            </td>
        </tr>
        <tr>
            <td style="height: 120px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                Authorised signatory<br /><br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
