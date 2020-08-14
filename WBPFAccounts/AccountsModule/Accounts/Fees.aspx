<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Fees.aspx.cs" Inherits="AccountsModule.Accounts.Fees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlMembershipCategory.ClientID%>'), "Membership Category", 0)) return false;
            return confirm('Are You Sure?');
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Fees
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 740px;">
                    <uc3:Message ID="Message" runat="server" />
                </div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Membership Category<span class="req">*</span>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList" Width="95%" >
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnktnFeeDetailsView" runat="server" 
                                    onclick="lnktnFeeDetailsView_Click">View</asp:LinkButton>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <h6 align="left" style="color: #00356A;">
                    Fees Details</h6>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left">
                                <%--<a rel="divAddFeesHead" class="poplight" onclick="popUp('350','divAddFeesHead')">New
                                    Fees Head </a>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--   <asp:Panel ID="panelfeesDetails" runat="server">
                                </asp:Panel>--%>
                                <asp:PlaceHolder ID="panelfeesDetails1" runat="server"></asp:PlaceHolder>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnStramSave" runat="server" Text="Save" CssClass="button" OnClick="btnStramSave_Click"
                                    OnClientClick="return Validation();" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>

</asp:Content>
