<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="member_development_bill.aspx.cs" Inherits="AccountsModule.Common.member_development_bill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .billDetails{
            font-size:14px;
        }
    </style>
</head>
<body onclick="window.print()">
    <form id="form1" runat="server">
    <table width='100%'>
        <tr>
            <td align="center">
                <b><span style="font-family: Calibri; font-size: 25px;"><u>WEST BENGAL POULTRY FEDERATION</u></span>
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
                    <b><span style="font-family: 'Times New Roman'; font-size: 20px;"><u>Monthly Developement Fees</u></span> </b>
                </h2>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                Bill No :
                <asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label>
            </td>
            <td align="right">
                Date:
                <asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                To,
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
            </td>
            <td align="right" valign="top">
                <b><u>Membership No.
                    <asp:Label ID="lblMemberCode" runat="server" Text=""></asp:Label></u></b>
            </td>
        </tr>
    </table>
    <table border="1" cellpadding="5" cellspacing="0" width="100%" style="border-collapse: collapse;" class="billDetails">
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
                Opening Balance<br /><br />
                <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
            </td>
            <td align="center" valign="top">
                Development Fees Opening Balance
                <br /><br />
                MONTHLY DEVELOPMENT FEES
                <%--DEVELOPMENT OF VARIOUS ACTIVITIES--%>
                <asp:Label ID="lblMonthlyRate" runat="server" Text=""></asp:Label>
            </td>
            <td align="right" valign="top">
                <b><asp:Label ID="lblOpeningBalance" runat="server" Text=""></asp:Label></b><br /><br />
                <asp:Label ID="lblFinalAmount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Rupees : <i><asp:Label ID="lblTotalInWords" runat="server"></asp:Label></i></b>
                <b style="border-left: 2px solid black;float:right">&nbsp Total :</b>
            </td>
            <td align="right">
                &nbsp;<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table width='100%' cellpadding="3" style="border: 1px solid black;">
        <tr>
            <td style="width: 50%" align="left">
                
            </td>
            <td align="right">
                E. &amp; O.E.
            </td>
        </tr>
        <tr>
            <td>
                <u><b>Conditions:</b></u> 
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
            <td colspan="2">
                2. If the bill is paid within 1st week of the month.
            </td>
        </tr>
        <tr>
            <td style="height: 20px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                Authorised signatory
            </td>
        </tr>
    </table>
    </form>
</body>
</html>