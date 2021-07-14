<%@ Page Title="Member Outstanding Report" Language="C#" MasterPageFile="~/MasterAdmin.Master" Culture="hi-IN"
    AutoEventWireup="true" CodeBehind="MemberOutstandingReport.aspx.cs" Inherits="AccountsModule.Common.MemberOutstandingReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SearchValidation() {
            if (document.getElementById('<%=ddlMember.ClientID%>').selectedIndex == 0) {
                alert("Please Select Member");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <style type="text/css">
        body{
            font-family:Calibri;
            font-size:14px;
        }
        h1{
            font-family:Calibri;
            font-size:30px;
        }
        h2{
            font-family:Calibri;
            font-size:18px;
        }
        @media print {
            @page { margin-top: 0; margin-bottom:0; margin-left:120px; padding-bottom:0; size: A4;max-height:100%; max-width:100%;}

            body {
                visibility: hidden;
                page-break-before: avoid;
            }

            #divPrintSection {
                visibility: visible;
            }

            #divPrintSection {
                position: absolute;
                left: 0;
                top: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Outstanding Report</h5>
    </div>
    <div style="width: 900px;">
        <br />
        <table width="98%" align="center">
            <tr>
                <td width="25%" class="label">
                    Member Name:
                </td>
                <td>
                    <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                Font-Bold="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="300px"
                        DataValueField="MemberId" DataTextField="MemberName">
                    </asp:ComboBox>
                </td>
            </tr>
            <tr><td colspan="2"><br /></td></tr>
            <tr>
                <td width="25%" class="label">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="150px" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="25%" class="label">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="150px" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Outstanding Report"
                         OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr style="display:none">
                <td colspan="2">
                    <asp:GridView ID="dgvMemberOutstanding" runat="server" Width="98%" AutoGenerateColumns="false"
                        GridLines="Both" AllowPaging="false" DataKeyNames="SL_NO" OnRowDataBound="dgvMemberOutstanding_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" NullDisplayText="" />
                            <asp:BoundField DataField="FEES_DEBIT" HeaderText="Fees (Dr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:BoundField DataField="TAX_DEBIT" HeaderText="Tax (Dr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:BoundField DataField="TOTAL_DEBIT" HeaderText="Total (Dr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:BoundField DataField="FEES_CREDIT" HeaderText="Fees (Cr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:BoundField DataField="TAX_CREDIT" HeaderText="Tax (Cr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:BoundField DataField="TOTAL_CREDIT" HeaderText="Total (Cr)" DataFormatString="{0:F2}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" NullDisplayText="" />
                            <asp:TemplateField HeaderText="Balance (Dr/Cr)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("BALANCE").ToString()%>&nbsp;<%# Convert.ToDecimal(Eval("BALANCE").ToString()) >= 0 ? "Dr" : "Cr" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Outstanding Record Found
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="display:none;">
                <td colspan="2" align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divPrintSection" style="margin-left:auto; margin-right:auto;">
                        <table width="100%" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2" align="center">
                                  <br /><h1><u>WEST BENGAL POULTRY FEDERATION</u></h1>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    46C, Chowringhee Road, Everest House, 11th Floor, Room No - C, Kolkata - 700071
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    Phone No. 033 - 40515700 / 40631307 / 22885525<br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <h2><u>MEMBERSHIP RENEWAL CONSOLIDATED SUMMARY</u></h2>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="60%">
                                    <asp:Label ID="lblMemberDetail" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                     <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><br /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="dgvReport" runat="server" Width="100%" AutoGenerateColumns="false" CellPadding="3" CellSpacing="3" AllowPaging="false" DataKeyNames="TOTAL_FIELD" OnRowDataBound="dgvReport_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="PERIOD" HeaderText="Period" />
                                            <asp:BoundField DataField="DESC" HeaderText="Description" />
                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><br /></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    E. & O.E.<br />
                                    For West Bengal Poultry Federation
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Conditions:<br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    1. Payments can be made by A/C payee cheque/NEFT/RTGS/Online Payment.
                                </td>
                                <td align="right">
                                    --------------------------------------------<br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    2. If the bill is paid within 1st week of the month.
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                                <td colspan="2" align="center">
                                    <input type="button" class="button" value="Print" onclick="javascript:window.print();" />
                                </td>
                            </tr>
        </table>
    </div>
</asp:Content>
