<%@ Page Title="Development Fees Outstanding Report" Language="C#" MasterPageFile="~/MasterAdmin.Master" Culture="hi-IN"
    AutoEventWireup="true" CodeBehind="DevelopmentFeesOutstandingReport.aspx.cs" Inherits="AccountsModule.Common.DevelopmentFeesOutstandingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            font-family: Calibri;
            font-size: 14px;
        }

        h1 {
            font-family: Calibri;
            font-size: 30px;
        }

        h2 {
            font-family: Calibri;
            font-size: 18px;
        }

        @media print {
            @page {
                margin-top: 0;
                margin-bottom: 0;
                margin-left: 120px;
                padding-bottom: 0;
                size: A4;
                max-height: 100%;
                max-width: 100%;
            }

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
        <h5>Development Fees Outstanding Report</h5>
    </div>
    <div style="width: 900px;">

        <div id="divPrintSection" style="margin-left: auto; margin-right: auto;">
            <table width="100%" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <h1><u>WEST BENGAL POULTRY FEDERATION</u></h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">46C, Chowringhee Road, Everest House, 11th Floor, Room No - C, Kolkata - 700071
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">Phone No. 033 - 40515700 / 40631307 / 22885525<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <h2><u>DEVELOPMENT FEES SUMMARY</u></h2>
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
                    <td colspan="2">
                        <br />
                    </td>
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
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">E. & O.E.<br />
                        For West Bengal Poultry Federation
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Conditions:<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>1. Payments should be made by A/C payee cheque only.
                    </td>
                    <td align="right">--------------------------------------------<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">2. If the bill is paid within 1st week of the month.
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <input type="button" class="button" value="Print" onclick="javascript: window.print();" />
        </div>
    </div>
</asp:Content>
