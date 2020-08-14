<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrderEntry.aspx.cs" Inherits="CollegeERP.PurchaseOrder.PurchaseOrderEntry"
    Title="Purchase Order Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtBillNo.ClientID%>'), "Bill No", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtBillDate.ClientID%>'), "Bill Date", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtBillAmt.ClientID%>'), "Bill Amount", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSupplier.ClientID%>'), "Supplier A/C", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlPurchaseLedger.ClientID%>'), "Purchase A/C", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtNarration.ClientID%>'), "Narration", 0)) return false;
            return confirm('Are You Sure?');
        }       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Purchase Bill Entry</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel runat="server" ID="TabPanelEntry">
            <ContentTemplate>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <h6 align="left" style="color: #00356A;">
                PO Entry</h6>
            <div style="width: 820px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="12%" align="left" class="label">
                            Bill No<span class="req">*</span>
                        </td>
                        <td align="left" width="38%">
                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                        </td>
                        <td width="12%" align="left" class="label">
                            Bill Date<span class="req">*</span>
                        </td>
                        <td align="left" width="38%">
                            <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtBillDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="12%" align="left" class="label">
                            Bill Amount<span class="req">*</span>
                        </td>
                        <td align="left" width="38%">
                            <asp:TextBox ID="txtBillAmt" runat="server" CssClass="textbox_required" Width="160px"
                                onkeypress="return AmountOnly('txtBillAmt',this);"></asp:TextBox>
                        </td>
                        <td width="12%" align="left" class="label">
                            Supplier A/C<span class="req">*</span>
                        </td>
                        <td align="left" width="38%">
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="dropdownList" Width="160px" DataValueField="LedgerID" DataTextField="LedgerName"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                         <td width="12%" align="left" class="label">
                            Purchase A/C<span class="req">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlPurchaseLedger" runat="server" CssClass="dropdownList" Width="160px" DataValueField="LedgerID" DataTextField="LedgerName"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                         <td width="12%" align="left" class="label">
                            Narration<span class="req">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" TextMode="MultiLine" Height="60px" Width="200px"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                OnClientClick="return Validation()" onclick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                                onclick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                PO Details</h6>
            <div style="width: 820px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" class="label" width="10%">
                            Bill No
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtBillNoSearch" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="5%">
                            From
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="Right" Format="dd MMM yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" class="label" width="5%">
                            To
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="Right" Format="dd MMM yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" 
                                onclick="btnSearch_Click" />
                                
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvPO" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                GridLines="None" Width="100%" DataKeyNames="PurchaseBillId" 
                                onrowediting="dgvPO_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="BillNo" HeaderText="BillNo" />
                                    <asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
                                    <asp:BoundField DataField="TotalPaid" HeaderText="Total Paid" />
                                    <asp:BoundField DataField="Due" HeaderText="Due" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="Supplier" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="Numeric" PageButtonCount="8" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </ContentTemplate>
            <HeaderTemplate>
                <b>Purchase Bill Entry</b>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanelView">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelView" runat="server">
                    <ContentTemplate>
                        <table width="100%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvPurchaseBill" runat="server" 
                                                            OnRowCommand="gvPurchaseBill_RowCommand" AutoGenerateColumns="False" Width="100%"
                                                            GridLines="None" PageSize="25" OnPageIndexChanging="gvPurchaseBill_PageIndexChanging"
                                                            AllowPaging="True" AllowSorting="false" DataKeyNames="PurchaseBillId"  OnRowDeleting="gvPurchaseBill_RowDeleting">
                                                            <Columns>
                                                                <asp:BoundField DataField="BillNo" HeaderText="Bill No"></asp:BoundField>
                                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="BillDate"
                                                                    HeaderText="Bill Date"></asp:BoundField>
                                                               
                                                                <asp:BoundField DataFormatString="{0:F2}" DataField="BillAmount" HeaderText="Bill Amount">
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:BoundField>
                                                                <%--<asp:TemplateField>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnEdit" CommandName="imgbtnEdit" ImageUrl="~/Images/edit_icon.gif"
                                                                            runat="server" CommandArgument='<%# Eval("CBVHeaderID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnPrint" CommandName="imgbtnPrint" ImageUrl="~/Images/print.gif"
                                                                            Width="17px" Height="17px" runat="server" CommandArgument='<%# Eval("CBVHeaderID") + "," + Eval("CBVoucherNo") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                 <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                                                            runat="server" OnClientClick="return confirm('Are You Sure?')" CommandArgument='<%# Eval("PurchaseBillId") %>' />
                                                                    </ItemTemplate>
                                                                 </asp:TemplateField>
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
                                                                            Sorry! No Records Found.
                                                                        </td>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <PagerSettings Mode="Numeric" PageButtonCount="8" />
                                                            <HeaderStyle CssClass="HeaderStyle" />
                                                            <RowStyle CssClass="RowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <b>Show Purchase Bill</b>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
