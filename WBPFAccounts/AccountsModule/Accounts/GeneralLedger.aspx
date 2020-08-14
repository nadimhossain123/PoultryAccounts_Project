<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="GeneralLedger.aspx.cs" Inherits="CollegeERP.Accounts.GeneralLedger"
    Title="General Ledger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtLedgerName.ClientID%>'), "Ledger Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlLedgerType.ClientID%>'), "Ledger Type", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtOpeningBalance.ClientID%>'), "Opening Balance", 1)) return false;
            return confirm('Are You Sure?');
        }
    </script>
    <style type="text/css">
        .text
        {
            font-family: Helvetica;
            font-size: 13px;
            font-weight: normal;
            padding: 7px 0 0 15px;
        }
        .divWaiting
        {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="../Images/loading-bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="title">
        <h5>
            General Ledger</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                General Ledger Entry</h6>
            <div style="width: 840px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Ledger Name<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtLedgerName" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance Date
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOpBalDate" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtOpBalDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Ledger Type<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlLedgerType" runat="server" CssClass="dropdownList" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="CASH">CASH</asp:ListItem>
                                <asp:ListItem Value="NONCASH">NON CASH</asp:ListItem>
                                <asp:ListItem Value="CUST">CUSTOMER</asp:ListItem>
                                <asp:ListItem Value="SUP">SUPPLIER</asp:ListItem>
                                <asp:ListItem Value="PUR">PURCHASE</asp:ListItem>
                                <asp:ListItem Value="REIM">REIMBURSEMENT</asp:ListItem>
                                <asp:ListItem Value="TAX">TAX</asp:ListItem>
                                <asp:ListItem Value="OTH">OTHER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                                TargetControlID="txtOpeningBalance">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Group<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:ComboBox ID="ddlGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="150px" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance Type<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlOpeningBalanceType" CssClass="dropdownList" Width="150px"
                                runat="server">
                                <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Sub Group<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:ComboBox ID="ddlSubGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                DataValueField="GroupID" DataTextField="GroupName" Width="150px">
                            </asp:ComboBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Cost Center Applicable
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="chkCostCenter" runat="server"></asp:CheckBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Is Active?
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="chkIsActive" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation()"
                                OnClick="btnSave_Click"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                General Ledger Details</h6>
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="left" width="10%" class="label">
                        Ledger Name
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtLedgerNameVw" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" class="label">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gdGenLedger" runat="server" AllowSorting="false" AllowPaging="true"
                            DataKeyNames="LedgerID" PageSize="15" AutoGenerateColumns="False" Width="100%"
                            OnPageIndexChanging="gdGenLedger_PageIndexChanging" OnRowEditing="gdGenLedger_RowEditing"
                            OnRowDataBound="gdGenLedger_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name"></asp:BoundField>
                                <asp:BoundField DataField="LedgerType" HeaderText="Ledger Type"></asp:BoundField>
                                <asp:BoundField DataField="MAINGROUP" HeaderText="Main Group"></asp:BoundField>
                                <asp:BoundField DataField="SUBGROUP" HeaderText="Sub Group"></asp:BoundField>
                                <asp:BoundField DataField="OpeningDate" HeaderText="OP. Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance"></asp:BoundField>
                                <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance"></asp:BoundField>
                                <asp:BoundField DataField="CostCenterApplied" HeaderText="Cost Center"></asp:BoundField>
                                <asp:BoundField DataField="Active" HeaderText="Active"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
