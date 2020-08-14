<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintJournalVoucher.aspx.cs" Inherits="CollegeERP.Accounts.PrintJournalVoucher" Culture="hi-IN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Receipt</title>
    <style type="text/css">
        .text
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 12px;
            font-weight: normal;
            padding-left: 5px;
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
            font-size: 12px;
            font-weight: normal;
            padding: 12px 10px 12px 10px;
        }
        .headerstyle th
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 12px;
            font-weight: normal;
            padding: 3px 10px 2px 10px;
            border-bottom: solid 1px #000;
        }
        .footerstyle td
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 12px;
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
                <tr>
                    <td align="center" valign="top">
                        <asp:Image ID="img" runat="server" height="180px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrHeader" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrStudentName" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left">
                        <asp:GridView ID="dgvReceipt" runat="server" AutoGenerateColumns="false" GridLines="Vertical"
                            ShowFooter="true" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="<b>Particulars</b>" FooterText="Total:">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "GroupName")%><br />
                                        <%# DataBinder.Eval(Container.DataItem, "LedgerName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<b>Cost Center</b>">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CostCenterName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<b>Amount(Dr.)</b>">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "DRAmount", "{0:n}")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal ID="ltrTotalDr" runat="server" Mode="PassThrough"></asp:Literal>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<b>Amount(Cr.)</b>">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CRAmount", "{0:n}")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal ID="ltrTotalCr" runat="server" Mode="PassThrough"></asp:Literal>
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
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrFooter" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="text">
                        <asp:Literal ID="ltrSignature" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>
