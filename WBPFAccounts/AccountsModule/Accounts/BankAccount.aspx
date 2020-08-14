<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="BankAccount.aspx.cs" Inherits="CollegeERP.Accounts.BankAccount" Title="Bank Account" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<% = ddlBankName.ClientID%>').selectedIndex == 0) {
                alert("Please select Bank Name");
                return false;
            }
            else if (document.getElementById('<% = txtACNo.ClientID%>').value == '') {
                alert("Please enter A/c No.");
                return false;
            }
            else if (document.getElementById('<% = ddlACType.ClientID%>').selectedIndex == 0) {
                alert("Please select A/c Type");
                return false;
            }
            else if (document.getElementById('<% = txtAccountBalance.ClientID %>').value == '') {
                alert("Please enter Bank Account Balance");
                return false;
            }
            else if (document.getElementById('<% = txtOpBal.ClientID%>').value == '') {
                alert("Please enter Opening Balance");
                return false;
            }
            else {
                return confirm('Are You Sure?');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Bank Account</h5>
    </div>
    <asp:UpdatePanel ID="UpdatePanelEntry" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Bank A/c</h6>
            <table width="95%" align="center" class="table">
                <tbody>
                    <tr>
                        <td valign="top" align="center">
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            Bank Name :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBankName" CssClass="dropdownList" Width="160px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            Branch Name :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankBranch" CssClass="textbox" runat="server" Width="156px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px" class="label" valign="middle" align="right">
                                            A/C Name/No. :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtACNo" CssClass="textbox" runat="server" Width="156px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            A/C Type :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlACType" CssClass="dropdownList" runat="server" Width="160px">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="SAVINGS" Text="SAVINGS"></asp:ListItem>
                                                <asp:ListItem Value="CURRENT" Text="CURRENT"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="top" align="center">
                            <table style="width: 100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="label" align="right">
                                            A/C&nbsp;Opn.&nbsp;Date :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtACOpngDate" CssClass="textbox" runat="server" Width="140px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtACOpngDate"
                                                OnClientDateSelectionChanged="" Enabled="True">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="top" align="right">
                                            Bank&nbsp;Add.:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddress" CssClass="textbox" runat="server" TextMode="MultiLine"
                                                Width="140px" Height="20px" Style="resize: none;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="top" align="right">
                                            Operate By :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlOperatedBy" runat="server" CssClass="dropdownList" Width="146px" DataValueField="EmployeeId" DataTextField="FullName">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="top" align="center">
                            <table style="width: 100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="label" align="right">
                                            Bank&nbsp;Contact&nbsp;No.&nbsp;:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankContactNo" onkeypress="return AmountOnly('txtBankContactNo',this);"
                                                runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" align="right">
                                            &nbsp; Contact Person :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtContactPerson" CssClass="textbox" runat="server" Width="120px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" align="right">
                                            &nbsp;Mobile No. :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMobileNo" CssClass="textbox" onkeypress="return AmountOnly('txtMobileNo',this);"
                                                runat="server" Width="120px" MaxLength="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" align="right">
                                            Active :
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="True"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <h6 align="left" style="color: #00356A;">
                Bank Accounts Details</h6>
            <table width="95%" align="center" class="table">
                <tbody>
                    <tr>
                        <td style="width: 50%" valign="top">
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            A/c Group :
                                        </td>
                                        <td align="left">
                                            <asp:ComboBox ID="ddlGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                                Width="120px" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                            </asp:ComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px" class="label" valign="middle" align="right">
                                            A/c Sub Group :
                                        </td>
                                        <td style="height: 24px" align="left">
                                            <asp:ComboBox ID="ddlSubGoup" runat="server" CssClass="WindowsStyle" DataValueField="GroupID"
                                                DataTextField="GroupName" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                                CaseSensitive="false" Width="120px">
                                            </asp:ComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            Opening Balance (Bank):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAccountBalance" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            Cost Centre Applicable :
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="ChkCostCentreApplble" runat="server" Checked="True"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="width: 50%" valign="top">
                            <table style="width: 100%" id="Table6" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 21px" class="label" valign="middle" align="right">
                                            Opening Balance Date :
                                        </td>
                                        <td style="height: 21px" align="left">
                                            <asp:TextBox ID="txtOpBalDate" CssClass="textbox" runat="server" Width="120px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtOpBalDate"
                                                OnClientDateSelectionChanged="" Enabled="True">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px" class="label" valign="middle" align="right">
                                            Opening Balance (Book):
                                        </td>
                                        <td style="height: 21px" align="left">
                                            <asp:TextBox ID="txtOpBal" CssClass="textbox" onkeypress="return AmountOnly('txtOpBal',this)"
                                                runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" valign="middle" align="right">
                                            Opening Balance Type :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlOpBalType" CssClass="dropdownList" runat="server" Width="135px">
                                                <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                                <asp:ListItem Selected="True" Value="DR" Text="DR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" colspan="2">
                            <table border="0">
                                <tbody>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button"
                                                OnClientClick="return Validation()"></asp:Button>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="button">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <h6 align="left" style="color: #00356A;">
                Bank Accounts List</h6>
            <table class="table" width="100%">
                <tr>
                    <td valign="middle" align="center" width="100%">
                        <asp:GridView ID="gdBankAccount" runat="server" AllowPaging="true" PageSize="20"
                            DataKeyNames="BankID" AutoGenerateColumns="False" OnPageIndexChanging="gdBankAccount_PageIndexChanging"
                            Width="100%" AllowSorting="false" onrowediting="gdBankAccount_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="GeneralName" HeaderText="Bank Name"></asp:BoundField>
                                <asp:BoundField DataField="AccountNo" HeaderText="Account No"></asp:BoundField>
                                <asp:BoundField DataField="AccountType" HeaderText="Account Type"></asp:BoundField>
                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name"></asp:BoundField>
                                <asp:BoundField DataField="FinancialYear" HeaderText="Fin Year" />
                                <asp:BoundField DataField="ActOpeningDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="A/c Opening Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="AccountBalance" HeaderText="Opening Balance (Bank)" />
                                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance (Book)"></asp:BoundField>
                                <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance (Book)"></asp:BoundField>
                                <asp:BoundField DataField="Active" HeaderText="Active" ReadOnly="true" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditBank" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
