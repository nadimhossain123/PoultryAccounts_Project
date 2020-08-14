<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MeetingRentExpenseForDistrict.aspx.cs" Inherits="AccountsModule.Accounts.MeetingRentExpenseReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function openPopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';
            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
        }
        function SearchValidation() {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == '') {
                alert("Please Choose From Date");
                return false;
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == '') {
                alert("Please Choose To Date");
                return false;
            }
            else return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Meeting / Rent Expense Report</h5>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanelVoucherSearch">
        <ContentTemplate>
            <table width="85%" align="center" class="table">
                <tr>
                    <td class="label" valign="middle" align="left">Ledger Name :</td>
                    <td class="label" valign="middle" align="left">District Name :</td>
                    <td class="label" valign="middle" align="left">From Date :</td>
                    <td class="label" valign="middle" align="left">To :</td>
                    <td class="label"></td>
                    <td class="label"></td>
                </tr>
                <tr>
                    <td valign="middle" align="left">
                        <asp:DropDownList ID="ddlLedgerS" runat="server" CssClass="dropdownList" AutoPostBack="true"
                            Width="240px" DataTextField="LedgerName" DataValueField="LedgerID">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="dropdownList"
                            DataTextField="DistrictName" DataValueField="DistrictId">
                        </asp:DropDownList>
                    </td>
                    <td valign="middle" align="left">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td valign="middle" align="left">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                            CssClass="button" OnClientClick="return SearchValidation()"></asp:Button>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Button ID="btnDownload" OnClick="btnDownload_Click" runat="server" Text="Download"
                            CssClass="button"></asp:Button></td>
                </tr>
                <br />
                <tr>
                    <td align="center" colspan="6">
                        <asp:GridView ID="grdReport" runat="server" Width="70%" AllowPaging="true" PageSize="30"
                            GridLines="None" AllowSorting="false" AutoGenerateColumns="False" CellPadding="0"
                            OnRowDataBound="grdReport_RowDataBound" OnPageIndexChanging="grdReport_PageIndexChanging">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="SL No." ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblLedgerName" Text='<%#Bind("DistrictName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" Text="TOTAL:"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrictAmount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table style="height: 10px; width: 100%;">
                                    <tr align="left" class="HeaderStyle">
                                        <th scope="col">No Records Found</th>
                                    </tr>
                                    <tr class="RowStyle">
                                        <td>Sorry! No Records Found.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
