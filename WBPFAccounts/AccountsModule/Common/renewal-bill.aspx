<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="renewal-bill.aspx.cs" Inherits="AccountsModule.Common.renewal_bill" %>

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
                    Kolkata - 700071, Ph.: 4017 5700, Fax : 2288 5525<br />
                    E-mail : wbpoultryfederation.yahoo.in
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: 16px; font-weight: bold;">
                    <u>Monthly Subscription/Renewal And Development Fee</u><br />
                    <br />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 100px;">
                    <b>Member No :</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblMemberCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 120px;">
                    <b>Member Name :</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <b>Individual Member / Associated Member,&nbsp;At</b>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 40px;">
                    <b>P.O.:</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblPO" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 40px;">
                    <b>P.A.C:</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 180px;">
                    <asp:Label ID="lblPAC" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 40px;">
                    <b>Block:</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblBlock" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 40px;">
                    <b>District:</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 200px;">
                    <b>We received with thanks for</b>
                </td>
                <td style="border-bottom: solid 1px black;" colspan="3">
                    <asp:Label ID="lblNarration" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 130px;">
                    <b>Admission Fee as</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblAdmissionFee" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 110px;">
                    <b>+ G.S.T. (18 %)</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblAdmissionGST" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 140px;">
                    <b>Development Fee as</b></td>
                <td style="border-bottom: solid 1px black;" colspan="3">
                    <asp:Label ID="lblDevelopmentFee" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 120px;">
                    <b>Renewal Fee as</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblSum" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 110px;">
                    <b>+ G.S.T. (18 %)</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblGST" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 50px;">
                    <b>= Total</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 150px;">
                    <b>By cash / Cheque No.</b>
                </td>
                <td style="border-bottom: solid 1px black;">
                    <asp:Label ID="lblChequeNo" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 90px;">
                    <b>Bank Dated:</b>
                </td>
                <td style="border-bottom: solid 1px black; width: 150px;">
                    <asp:Label ID="lblBankDated" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td align="left">
                    W.B.P.F form No.-
                </td>
                <td align="left">
                    Treasurer
                </td>
                <td align="center">
                    Collector
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
