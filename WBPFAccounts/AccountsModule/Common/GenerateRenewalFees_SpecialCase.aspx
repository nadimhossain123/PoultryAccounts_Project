<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterAdmin.Master" CodeBehind="GenerateRenewalFees_SpecialCase.aspx.cs" Inherits="AccountsModule.Common.GenerateRenewalFees_SpecialCase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validation() {
            if (document.getElementById("<%= ddlState.ClientID %>").selectedIndex == 0) {
                alert("Please Select State");
                return false;
            }
            else if (document.getElementById("<%= ddlDistrict.ClientID %>").selectedIndex == 0) {
                alert("Please Select District");
                return false;
            }
            else if (document.getElementById("<%= ddlBlock.ClientID %>").selectedIndex == 0) {
                alert("Please Select Block");
                return false;
            }
            else {
                return true;
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Bill Generate (Special Case for 2021-2022 )</h5>
    </div>
    <div style="width: 1200px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="10%" align="left" class="label">
                    State:
                </td>
                <td align="left" width="15%">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="8%" align="left" class="label">
                    District:
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                        Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="7%" align="left" class="label">
                    Block:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                    </asp:DropDownList>
                </td>
                
            </tr>
           
            <tr><td align="left" colspan="8"></td><td align="right" colspan="2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Generate" 
                        OnClick="btnSearch_Click" onClientClick="return Validation()"/>
                </td>

            </tr>
        </table>
       
        <br />
        <br />
    </div>
</asp:Content>
