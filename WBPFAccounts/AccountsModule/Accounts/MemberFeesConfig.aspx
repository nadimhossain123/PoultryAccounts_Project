<%@ Page Title="Fees" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="True"
    CodeBehind="MemberFeesConfig.aspx.cs" Inherits="AccountsModule.Accounts.MemberFeesConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CalculateTotalAmount(input_field, max_value) {
            var totalamt = 0;
            var value;
            var gv = document.getElementById('<%=dgvDevelopmentFee.ClientID%>');
            var rCount = gv.rows.length;
            var rowIdx = 1;

            for (rowIdx; rowIdx <= rCount - 1; rowIdx++) {
                if (rowIdx == 1) {
                    if (gv.rows[1].cells[3].getElementsByTagName("input")[0].value > max_value) {
                        gv.rows[1].cells[4].getElementsByTagName("input")[0].value = gv.rows[1].cells[3].getElementsByTagName("input")[0].value * 0.10;
                    }
                    else {
                        gv.rows[1].cells[4].getElementsByTagName("input")[0].value = 0;
                    }
                }
                if (rowIdx == 2)
                    gv.rows[2].cells[4].getElementsByTagName("input")[0].value = gv.rows[2].cells[3].getElementsByTagName("input")[0].value * 0.10;
                if (rowIdx == 3) {
                    if (gv.rows[3].cells[3].getElementsByTagName("input")[0].value > max_value) {
                        gv.rows[3].cells[4].getElementsByTagName("input")[0].value = gv.rows[3].cells[3].getElementsByTagName("input")[0].value * 0.02;
                    }
                    else {
                        gv.rows[3].cells[4].getElementsByTagName("input")[0].value = 0;
                    }
                }
                if (rowIdx == 4)
                    gv.rows[4].cells[4].getElementsByTagName("input")[0].value = gv.rows[4].cells[3].getElementsByTagName("input")[0].value * 0.02;
                if (rowIdx == 5) {
                    if (gv.rows[5].cells[3].getElementsByTagName("input")[0].value > max_value) {
                        gv.rows[5].cells[4].getElementsByTagName("input")[0].value = gv.rows[5].cells[3].getElementsByTagName("input")[0].value * 2.00;
                    }
                    else {
                        gv.rows[5].cells[4].getElementsByTagName("input")[0].value = 0;
                    }
                }
                if (rowIdx == 6)
                    gv.rows[6].cells[4].getElementsByTagName("input")[0].value = gv.rows[6].cells[3].getElementsByTagName("input")[0].value * 0.01;
                if (rowIdx == 7)
                    gv.rows[7].cells[4].getElementsByTagName("input")[0].value = gv.rows[7].cells[3].getElementsByTagName("input")[0].value * 1.0;
                if (rowIdx == 8)
                    gv.rows[8].cells[4].getElementsByTagName("input")[0].value = gv.rows[8].cells[3].getElementsByTagName("input")[0].value * 1.0;


                var rowElement = gv.rows[rowIdx];
                value = rowElement.cells[4].getElementsByTagName("input")[0].value.trim();
                totalamt = totalamt + ((value == '') ? 0 : parseFloat(value));
            }
            var gvFee = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var FeerowElement = gvFee.rows[1];
            FeerowElement.cells[1].getElementsByTagName("input")[0].value = totalamt;
        }

        function ShowMsg(str) {
            alert(str);
            return false;
        }

        function Validation() {
            if (!validateTextBox())
                return ShowMsg('Please enter all fields');
            else
                return confirm('Do You Want to Save?');
        }

        function validateTextBox() {
            var flag = 0;
            var textboxes = new Array();
            var gridview = document.getElementById('<%=dgvDevelopmentFee.ClientID %>');
            textboxes = gridview.getElementsByTagName('input');
            for (var i = 0; i < textboxes.length; i++) {
                if (textboxes.item(i).value == "")
                {
                    flag = 1;
                    break; //break the loop as there is no need to check further.
                }
            }
            if (flag == 1) {
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Fees Configuration
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 1050px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <tr>
                            <td>
                                <asp:GridView ID="dgvDevelopmentFee" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                    DataKeyNames="MemberDevelopmentFeeId,ParticularsId" GridLines="None" Width="100%"
                                    OnRowDataBound="dgvDevelopmentFee_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ParticularsName" HeaderText="Particulars" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="UnitTypeName" HeaderText="Unit Type" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Capacity" ItemStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCapacity" runat="server" CssClass="textbox" Enabled="true" Style="text-align: right;
                                                    padding-right: 8px;" Text='<%#Bind("Capacity") %>' Width="120px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftbtxtCapacity" runat="server" FilterType="Custom"
                                                    TargetControlID="txtCapacity" ValidChars="0123456789">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fee Amount" ItemStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFee" runat="server" CssClass="textbox" Enabled="false" Style="text-align: right;
                                                    padding-right: 8px;" Text='<%#Bind("FeeAmount") %>' Width="120px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftbFee" runat="server" FilterType="Custom" TargetControlID="txtFee"
                                                    ValidChars="0123456789.">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Narration" ItemStyle-Width="250px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Width="250px" Height="45px" TextMode="MultiLine" style="resize:none" Text='<%#Bind("Narration") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataTemplate>
                                        No Fees Head Found
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="dgvFeesHead" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                    DataKeyNames="FeesHeadId" GridLines="None" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="FeesHeadName" HeaderText="Fees Head" />
                                        <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Enabled=false Style="text-align: right;
                                                    padding-right: 8px;" Text='<%#Bind("Amount") %>' Width="120px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftbAmount" runat="server" FilterType="Custom" TargetControlID="txtAmount"
                                                    ValidChars="0123456789.">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataTemplate>
                                        No Fees Head Found
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click" OnClientClick="return Validation()"
                                    Width="120px" Text="Save" />
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
