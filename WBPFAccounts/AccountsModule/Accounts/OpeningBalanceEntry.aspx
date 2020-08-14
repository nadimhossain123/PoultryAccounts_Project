<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="OpeningBalanceEntry.aspx.cs" Inherits="AccountsModule.Accounts.OpeningBalanceEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                alert("Please Select Ledger");
                return false;
            }
            if (document.getElementById('<%=ddlOpeningBalanceType.ClientID%>').selectedIndex == 0) {
                alert("Please Select Opening Balance Type");
                return false;
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value == '') {
                alert("Please Enter Opening Balance");
                return false;
            }
            return confirm('Are You Sure?');
        }
        function Validation2() {
            
            return confirm('Are You Sure?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Manage Opening Balance</h5>
    </div>
    <asp:UpdatePanel ID="UpdatePanelEntry" runat="server">
        <ContentTemplate>
            <table width="60%" align="center" class="table">
                <tr>
                    <td>
                        <uc3:Message ID="Message" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <h6 align="left" style="color: #00356A; margin-left: 100px">Opening Balance Entry</h6>
            <table width="40%" align="center" class="table">
                <tr>
                    <td align="left" class="label" width="30%">Ledger Name: <span class="req">*</span></td>
                    <td align="left" width="60%" style="padding-bottom: 10px">
                        <asp:DropDownList runat="server" ID="ddlLedger" CssClass="dropdownList" Width="95%" DataTextField="LedgerName" DataValueField="LedgerId"
                            OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td></td><td valign="top"><asp:Label runat="server" ID="lblActualOpeningBalance" Font-Bold="true"></asp:Label></td></tr>
                <tr>
                    <td align="left" class="label" width="30%">Opening Balance: <span class="req">*</span></td>
                    <td>
                        <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                            TargetControlID="txtOpeningBalance">
                        </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="40%" class="label">Opening Balance Type: <span class="req">*</span>
                    </td>
                    <td align="left" width="30%" style="padding-bottom: 10px">
                        <asp:DropDownList ID="ddlOpeningBalanceType" runat="server" CssClass="dropdownList" Width="100px">
                            <asp:ListItem Value="0" Text="--SELECT--"></asp:ListItem>
                            <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                            <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td colspan="5" align="center">
                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button"
                            OnClientClick="return Validation()" Width="80px"></asp:Button>&#160;&nbsp;
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Reset"
                                        CssClass="button" Width="80px"></asp:Button>&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;&#160;&nbsp;
                                    <asp:Button ID="btnCopyPrevClosing"  runat="server" Text="Copy Prev Year Closing" OnClientClick="return Validation2()" OnClick="btnCopyPrevClosing_Click"
                                        CssClass="button" BackColor="Red" Width="150px"></asp:Button>
                    </td>
                </tr>

            </table>
            <br />
            <hr />
            <br />
            <table width="85%" align="center" class="table">
                <tr>
                    <td align="left" class="label" width="10%">Ledger Name: </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        <asp:TextBox runat="server" ID="txtLedgerName" Width="200px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" Width="150px" CssClass="button" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp</td>
                </tr>
                <tr>
                    <td align="center" colspan="3" width="90%">
                        <asp:GridView ID="dgvOpeningBalance" runat="server" AllowSorting="false" AllowPaging="True"
                            AutoGenerateColumns="False" Width="100%" PageSize="30" DataKeyNames="OpBalId"
                            OnPageIndexChanging="dgvOpeningBalance_PageIndexChanging" OnRowEditing="dgvOpeningBalance_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="SL No.">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GroupType" HeaderText="Group Type"></asp:BoundField>
                                <asp:BoundField DataField="GroupName" HeaderText="Group Name"></asp:BoundField>
                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name"></asp:BoundField>
                                <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year"></asp:BoundField>
                                <asp:BoundField DataField="ActualOpeningBalance" HeaderText="Opening Balance (Actual)"></asp:BoundField>
                                <asp:BoundField DataField="ModifiedOpeningBalance" HeaderText="Opening Balance (Modified)"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                NO RECORDS FOUND!!!
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
