<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="UpdateMemberRenewalFees.aspx.cs" Inherits="AccountsModule.Common.UpdateMemberRenewalFees" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript"> 
        function isNumberKey(evt)
        {
          var charCode = (evt.which) ? evt.which : evt.keyCode;
          if (charCode != 46 && charCode > 31 
            && (charCode < 48 || charCode > 57))
             return false;

          return true;
        }
        
        function Validation() {

            
            if (document.getElementById('<%=ddlFromMonth.ClientID %>').selectedIndex == 0) {
                alert('Please Select From Month');
                return false;
            }
            else if (document.getElementById('<%=ddlToMonth.ClientID %>').selectedIndex == 0) {
                alert('Please Select To Month');
                return false;
            }
            else if (document.getElementById('<%=ddlYear.ClientID %>').selectedIndex == 0) {
                alert('Please Select Year');
                return false;
            }
            else if (document.getElementById('<%=txtAmount.ClientID %>').value == '') {
                    alert('Please Enter New Amount');
                    return false;
            }
            else {
                return confirm('Are You Sure');
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Renewal Fees Update</h5>
    </div>
    <%--<asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 750px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label" height="20px">
                            Member:
                        </td>
                        <td height="20px" valign="top">
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="295px"
                                AutoCompleteMode="SuggestAppend" AutoPostBack="True" 
                                onselectedindexchanged="ddlMember_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            From Month:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="dropdownList" Width="300px"
                                DataValueField="Month" DataTextField="MonthName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            To Month:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="dropdownList" Width="200px"
                                DataValueField="Month" DataTextField="MonthName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Year:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="200px">
                                <asp:ListItem Value="0" Text="--SELECT YEAR--"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                           New Amount:
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="dropdownList" ID="txtAmount" Width="300px" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="fteTaxPercent" runat="server" TargetControlID="txtAmount"
                                FilterType="Custom" ValidChars="0123456789.">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Update" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        
                            <asp:GridView ID="dgvBill" runat="server" AllowPaging="false" 
                                AutoGenerateColumns="false" DataKeyNames="BillId" Width="100%">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            S.No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MemberName" HeaderText="MemberName" />
                                    <asp:BoundField DataField="MemberCode" HeaderText="MemberCode" />
                                    <asp:BoundField DataField="BillMonth" HeaderText="BillMonth" />
                                    <asp:BoundField DataField="Year" HeaderText="Year" />
                                    <asp:BoundField DataField="FinalAmount" HeaderText="Amount" />
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                            </asp:GridView>
                        
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
</asp:Content>
