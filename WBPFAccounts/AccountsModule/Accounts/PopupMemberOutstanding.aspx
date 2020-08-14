<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMemberOutstanding.aspx.cs"
    Inherits="AccountsModule.Accounts.PopupMemberOutstanding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" href="../Styles/reset.css" type="text/css" rel="Stylesheet" runat="server" />
    <link id="Link2" href="../Styles/style.css" type="text/css" rel="Stylesheet" runat="server" />
    <link id="Link3" href="../Styles/blue.css" type="text/css" rel="Stylesheet" runat="server" />
    <link id="Link4" href="../Styles/Control.css" type="text/css" rel="Stylesheet" runat="server" />
    <script src="../Scripts/ssjscript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <!--[if IE]><script language="javascript" type="text/javascript" src="resources/scripts/excanvas.min.js"></script><![endif]-->
    <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.selectmenu.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../Scripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script src="../Scripts/tiny_mce/jquery.tinymce.js" type="text/javascript"></script>
    <!-- scripts (custom) -->
    <script src="../Scripts/smooth.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.menu.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.chart.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.table.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.form.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.dialog.js" type="text/javascript"></script>
    <script src="../Scripts/smooth.autocomplete.js" type="text/javascript"></script>
    <script src="../Scripts/ssjscript.js" type="text/javascript"></script>
    <%--jQuery--%>
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="toolScript1" runat="server">
        </asp:ToolkitScriptManager>
        <center>
            <div id="content">
                <div class="box" align="center">
                    <center>
                        <div class="Skybox">
                            <div class="title">
                                <h5>
                                    Member Ledger Report</h5>
                            </div>
                            <uc3:Message ID="Message" runat="server" />
                            <br />
                            <table width="85%" align="center" class="table">
                                <tr>
                                    <td width="15%" align="left" class="label">
                                        Member Name
                                    </td>
                                    <td width="4%" align="left" class="label">
                                        From
                                    </td>
                                    <td align="left" class="label">
                                        &nbsp;To
                                    </td>
                                    <td width="4%" align="left" class="label">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="12%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" align="left" class="label">
                                        <%--<asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                    DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                    Width="300px" DataValueField="MemberId" DataTextField="MemberName">
                </asp:ComboBox>--%>
                                        <asp:DropDownList ID="ddlMember" runat="server" Width="300px" DataValueField="MemberId"
                                            DataTextField="MemberName" Enabled="false" CssClass="dropdownList">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="label">
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="125px" CssClass="textbox" MaxLength="12"
                                            onkeydown="return false;"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                                            OnClientDateSelectionChanged="" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td width="4%" align="left" class="label">
                                        <asp:TextBox ID="txtToDate" runat="server" Width="125px" CssClass="textbox" MaxLength="12"
                                            onkeydown="return false;"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                                            OnClientDateSelectionChanged="" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Report" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="85%" align="center" class="table">
                                <tr>
                                    <td align="left" class="table">
                                        <asp:Literal ID="ltrStudentInfo" runat="server" Mode="PassThrough"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="dgvBill" runat="server" Width="100%" ShowFooter="true" GridLines="None"
                                            FooterStyle-Font-Bold="true" AutoGenerateColumns="false" AllowPaging="false"
                                            OnRowDataBound="dgvBill_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="DocumentNo" HeaderText="Document No" FooterText="<b>*** Closing Balance ***</b>" />
                                                <%--<asp:BoundField DataField="DocumentDate" HeaderText="Date" />--%>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentDate" runat="server" Text='<%#Bind("DocumentDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DocumentType" HeaderText="Type" />
                                                <asp:BoundField DataField="FeesType" HeaderText="Fees Type" />
                                                <asp:BoundField DataField="MainAmt" HeaderText="Main Amt" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="TaxAmt" HeaderText="Tax Amt" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="TotBill" HeaderText="Tot Bill" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="MainRcvd" HeaderText="Main Rcvd" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="TaxRcvd" HeaderText="Tax Rcvd" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="TotRecd" HeaderText="Tot Rcvd" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="Balance" HeaderText="Balance Amt" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" />
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
                                                            No Records Found
                                                        </td>
                                                </table>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <FooterStyle CssClass="RowStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                                        <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />&nbsp;
                                        <%--<asp:Button ID="btnPayment" runat="server" CssClass="button" Text="Pay Now" Visible="false"/>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </center>
                </div>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
