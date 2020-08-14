<%@ Page Title="Fees Posting" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MonthlySubscriptionFeesGeneration.aspx.cs"
    Inherits="AccountsModule.Common.MonthlySubscriptionFeesGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlMonth.ClientID %>').selectedIndex == 0) {
                alert('Please Select Month');
                return false;
            }
            else if (document.getElementById('<%=ddlYear.ClientID %>').selectedIndex == 0) {
                alert('Please Select Year');
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
            Member Fees Generation</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 500px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Month:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="300px"
                                DataValueField="Month" DataTextField="MonthName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Year:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="300px">
                                <asp:ListItem Value="0" Text="--Select Year--"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Member:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlMember" runat="server" CssClass="WindowsStyle" Width="300px" AutoCompleteMode="SuggestAppend">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Generate" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
