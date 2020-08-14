<%@ Page Title="Member Payment Excel Upload" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MemberPaymentExcelUpload.aspx.cs" Inherits="AccountsModule.Common.MemberPaymentExcelUpload" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openpopup(poplocation) {
            var popposition = 'left = 200, top=50, width=1000,align=center, height=600,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>Member Payment Excel Upload</h5>
    </div>
    <div style="width: 99%;">
        <br />
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="5%">Select File</td>
                <td width="10%">
                    <asp:FileUpload ID="fu" runat="server" CssClass="label" />
                </td>
                <td align="left">
                    <a href="../ExcelTemplates/Payment-Template.xlsx">Download Sample Template</a>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button" OnClick="btnUpload_Click" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h5>Upload validation error list</h5>
                    <div style="overflow-x: hidden; overflow-y: scroll; height: 200px; border: solid 1px #000;">
                        <asp:GridView ID="dgvError" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="false">
                            <Columns>
                                <asp:BoundField DataField="ROW_NO" HeaderText="Row No" ItemStyle-Width="40px" />
                                <asp:BoundField DataField="MESSAGE" HeaderText="Error Message" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h5>Upload list</h5>
                    <div>
                        <asp:GridView ID="dgvPaymentReport" runat="server" Width="100%" AutoGenerateColumns="false"
                            GridLines="Both" DataKeyNames="PaymentId,PaymentNo" AllowPaging="true" PageSize="15"
                            ShowFooter="true" OnPageIndexChanging="dgvPaymentReport_PageIndexChanging" OnRowDataBound="dgvPaymentReport_RowDataBound"
                            OnRowDeleting="dgvPaymentReport_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" ItemStyle-Wrap="false"
                                    FooterText="<b>Total</b>" />
                                <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Ledger" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="MemberCode" HeaderText="Member Code" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}"
                                    NullDisplayText="" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="AdmissionFeesPaymentAmount" HeaderText="Admission Fees"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="AdmissionFeesTaxPaymentAmount" HeaderText="Admission Fees Tax"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="RenewalFeesPaymentAmount" HeaderText="Renewal Fees" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="RenewalFeesTaxPaymentAmount" HeaderText="Renewal Fees Tax"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DevelopmentPaymentAmount" HeaderText="Development Fees"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentAmount" HeaderText="Total" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" FooterStyle-HorizontalAlign="Right" />
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download"
                        OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>


</asp:Content>
