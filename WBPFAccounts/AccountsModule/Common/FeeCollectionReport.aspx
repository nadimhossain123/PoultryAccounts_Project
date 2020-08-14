<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="FeeCollectionReport.aspx.cs" Inherits="AccountsModule.Common.FeeCollectionReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SearchValidation() {
            if (document.getElementById('<% = txtFromDate.ClientID%>').value.trim() == '') {
                alert("Please Enter From Date");
                return false;
            }
            if (document.getElementById('<% = txtToDate.ClientID%>').value.trim() == '') {
                alert("Please Enter To Date");
                return false;
            }
            else return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Fee Collection Report</h5>
    </div>
    <%--<asp:UpdatePanel ID="UP1" runat="server"><ContentTemplate>--%>
    <div style="margin: 0px 20px">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="80%" align="center" class="table">
            <tr>
                <td align="left" width="15%" class="label">District <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="192px" CssClass="dropdownList">
                    </asp:DropDownList>
                </td>
                <td align="left" width="15%" class="label">From Date
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderStartDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" width="15%" class="label">To Date
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="150px"
                        OnClick="btnSearch_Click" OnClientClick="return SearchValidation()" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp</td>
            </tr>
            <tr>
                <td colspan="7" align="center">
                    <asp:GridView ID="dgvFeeCollectionReport" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="" CellPadding="4" ShowFooter="true" OnRowDataBound="dgvFeeCollectionReport_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SL No" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="BusinessTypeName" HeaderText="Business Type" ItemStyle-HorizontalAlign="Left" />--%>
                            <asp:TemplateField HeaderText="Business Type" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusinessTypeName" Text='<%#Bind("BusinessTypeName") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalText" Text="Total" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission Fee" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmissionFee" Text='<%#Bind("AdmissionFee") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalAdmissionFee" Text="" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission Fee Tax" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmissionFeeTax" Text='<%#Bind("AdmissionFeeTax") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalAdmissionFeeTax" Text="" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Renewal Fee" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblRenewalFee" Text='<%#Bind("RenewalFee") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalRenewalFee" Text="" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Renewal Fee Tax" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblRenewalFeeTax" Text='<%#Bind("RenewalFeeTax") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalRenewalFeeTax" Text="" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Development Fee" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblDevelopmentFee" Text='<%#Bind("DevelopmentFee") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDevelopmentFee" Text="" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="AdmissionFee" HeaderText="Admission Fee" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AdmissionFeeTax" HeaderText="Admission Fee Tax" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RenewalFee" HeaderText="Renewal Fee" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RenewalFeeTax" HeaderText="Renewal Fee Tax" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DevelopmentFee" HeaderText="Development Fee" ItemStyle-HorizontalAlign="Center" />--%>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="7">
                    <%--<asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;--%>
                <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                    OnClick="btnExportExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
