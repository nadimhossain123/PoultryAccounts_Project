<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountsModule.ResponseHandler" CodeBehind="ccavResponseHandler.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Response</title>
    <%--<link href="/Styles/reset.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/style.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/blue.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/Control.css" type="text/css" rel="Stylesheet" />
    <script src="/Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <!--[if IE]><script language="javascript" type="text/javascript" src="resources/scripts/excanvas.min.js"></script><![endif]-->
    <script src="/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.ui.selectmenu.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.flot.min.js" type="text/javascript"></script>
    <script src="/Scripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script src="/Scripts/tiny_mce/jquery.tinymce.js" type="text/javascript"></script>
    <!-- scripts (custom) -->
    <script src="/Scripts/smooth.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.menu.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.chart.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.table.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.form.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.dialog.js" type="text/javascript"></script>
    <script src="/Scripts/smooth.autocomplete.js" type="text/javascript"></script>
    <script src="/Scripts/ssjscript.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>--%>

    <link href="/Styles/reset.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/style.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/blue.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/login-box.css" type="text/css" rel="Stylesheet" />
    <link href="Styles/Control.css" type="text/css" rel="Stylesheet" />
    <%--jQuery--%>
</head>
<body>
    <form id="form2" runat="server">
        <div id="header">
            <!-- logo -->
            <div id="logo">
                <h1>
                    <%--<img src="/Images/logo.png" alt="Smooth Admin" />--%>
                    <img src="/Images/logo2.png" alt="Smooth Admin" height="50px" style="margin: -5px 0px 10px 0px" />
                </h1>
            </div>
            <br />
            <br />
            <br />
            <ul id="user">
            </ul>
        </div>
        <center>
            <div id="content">

                <%--<div id="right">--%>
                <div class="box" align="center">
                    <center>
                        <div>
                            <div class="title">
                                <h5>Payment Summary</h5>
                            </div>

                            <div style="width: 700px;">
                                <uc3:Message id="Message" runat="server" />
                                <br />
                                <table width="100%" align="center">
                                    <tr>
                                        <td width="20%" class="label">Billing Name:
                                        </td>
                                        <td>
                                            <asp:Label ID="billing_name" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Order Id:
                                        </td>
                                        <td>
                                            <asp:Label ID="order_id" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Tracking Id :
                                        </td>
                                        <td>
                                            <asp:Label ID="tracking_id" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Bank Ref No :
                                        </td>
                                        <td>
                                            <asp:Label ID="bank_ref_no" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Order Status :
                                        </td>
                                        <td>
                                            <asp:Label ID="order_status" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Failure Message :
                                        </td>
                                        <td>
                                            <asp:Label ID="failure_message" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Payment Mode :
                                        </td>
                                        <td>
                                            <asp:Label ID="payment_mode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Transaction Date :
                                        </td>
                                        <td>
                                            <asp:Label ID="trans_date" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="label">Amount :
                                        </td>
                                        <td>
                                            <asp:Label ID="mer_amount" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <asp:Label runat="server" ID="lblResponse" Font-Bold="true"></asp:Label>
                                </table>
                            </div>
                            <div>
                                <a href="../MemberDefault.aspx">Go To Home </a>
                            </div>
                        </div>
                    </center>
                </div>
                <%--</div>--%>

                <div id="footer">
                    <p>Copyright &copy; 2011-2019 WBPF Link. All Rights Reserved.</p>
                </div>
            </div>
        </center>
    </form>
    <%--<form id="form1" runat="server">
        <div id="header">
            <!-- logo -->
            <div id="logo">
                <h1>
                    <img src="/Images/logo2.png" alt="Smooth Admin" height="50px" style="margin: -5px 0px 10px 0px" /></h1>
            </div>
            <div>
                <div class="title">
                    <h5>Payment Summary</h5>
                </div>

                <div style="width: 700px;">
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td width="20%" class="label">Order Id:
                            </td>
                            <td>
                                <asp:Label ID="order_id" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">Tracking Id :
                            </td>
                            <td>
                                <asp:Label ID="tracking_id" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">Bank Ref No :
                            </td>
                            <td>
                                <asp:Label ID="bank_ref_no" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">Order Status :
                            </td>
                            <td>
                                <asp:Label ID="order_status" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">Failure Message :
                            </td>
                            <td>
                                <asp:Label ID="failure_message" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">payment_mode :
                            </td>
                            <td>
                                <asp:Label ID="payment_mode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">Trans Date :
                            </td>
                            <td>
                                <asp:Label ID="trans_date" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" class="label">mer_amount :
                            </td>
                            <td>
                                <asp:Label ID="mer_amount" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <asp:Label runat="server" ID="lblResponse" Font-Bold="true"></asp:Label>
                    </table>
                </div>
            </div>
        </div>
    </form>--%>
</body>
</html>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>

<%--</asp:Content>--%>
