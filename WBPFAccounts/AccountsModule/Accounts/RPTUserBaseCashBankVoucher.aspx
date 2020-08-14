<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RPTUserBaseCashBankVoucher.aspx.cs" Inherits="CollegeERP.Accounts.RPTUserBaseCashBankVoucher"
    Title="Day End Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '')
                return ShowMsg("Enter Form Date");
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '')
                return ShowMsg("Enter To Date");
            else if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select User");
            else
                return true;            
        }
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
           <%-- User Base Receipt/Payment Voucher Report--%>
           Day End Report</h5>
    </div>
    <div style="width: 980px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="20%" align="left" class="label">
                    From
                </td>
                <td width="20%" align="left" class="label">
                    To
                </td>
                <td width="20%" align="left" class="label">
                    User
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td width="20%" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="170px"
                        DataValueField="EmployeeId" DataTextField="FullName">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="gvCBVDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        Width="100%" DataKeyNames="CBVHeaderID" ShowFooter="True" 
                        onrowdatabound="gvCBVDetails_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="VoucherDate" HeaderText="Date" DataFormatString="{0: dd/MM/yyyy}" />
                            <asp:BoundField DataField="CBVoucherNo" HeaderText="Voucher No" HtmlEncode="false" />
                            <asp:BoundField DataField="LedgerName" HeaderText="Cash/Bank Book" />
                            <%--<asp:BoundField DataField="Payment" HeaderText="Payment/Receive" HtmlEncode="false" />--%>
                            <asp:TemplateField  HeaderText="Payment/Receive"  >
                                    <ItemTemplate>
                                            <asp:Label ID="lblTran" runat="server" Text='<%#Eval("Payment") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                           <b> <asp:Label ID="lblTranTotal" runat="server" Text="Total Amount :" /></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            <%--<asp:BoundField DataField="DRAmount" HeaderText="Amount Dr(Cash)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" />--%>
                                
                                <asp:TemplateField  HeaderText="Amount Dr(Cash)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblDrAmount" runat="server" Text='<%#Eval("DRAmount") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            <b><asp:Label ID="lblDrCashtotal" runat="server" /></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                           <%-- <asp:BoundField DataField="CRAmount" HeaderText="Amount Cr(Cash)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" />--%>
                                 <asp:TemplateField  HeaderText="Amount Cr(Cash)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                            <asp:Label ID="lblCrAmount" runat="server" Text='<%#Eval("CRAmount") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            <b><asp:Label ID="lblCrCashtotal" runat="server" /></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            <%--<asp:BoundField DataField="DRAmountBnk" HeaderText="Amount Dr(Bank)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" />--%>
                                 <asp:TemplateField  HeaderText="Amount Dr(Bank)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                            <asp:Label ID="lblDrAmountBnk" runat="server" Text='<%#Eval("DRAmountBnk") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            <b><asp:Label ID="lblDrbanktotal" runat="server" /></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                           <%-- <asp:BoundField DataField="CRAmountBnk" HeaderText="Amount Cr(Bank)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" />--%>
                                <asp:TemplateField  HeaderText="Amount Cr(Bank)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                            <asp:Label ID="lblCrAmountBnk" runat="server" Text='<%#Eval("CRAmountBnk") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate >
                                            <b><asp:Label ID="lblCrbanktotal" runat="server"  /></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                        </Columns>
                        <FooterStyle CssClass="RowStyle"  />
                        <HeaderStyle CssClass="HeaderStyle"  />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" Visible="false"
                        OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                        OnClick="btnExportExcel_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
