<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SendSMS.aspx.cs" Inherits="AccountsModule.SMS.SendSMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtMssg.ClientID %>').value == '') {
                alert('Enter Message'); return false;
            }
            else {
                return confirm('Do you want to proceed?');
            }
        }

        function EnableButton() {
            var locked = document.getElementById('<%=Hidden1.ClientID %>').value;
            if (locked == "1") {
                var mobile = document.getElementById('<%=txtMobiles.ClientID %>').value.trim();
                if (mobile.length > 0)
                    document.getElementById('<%=btnSend.ClientID %>').style.display = 'block';
                else
                    document.getElementById('<%=btnSend.ClientID %>').style.display = 'none';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="title">
        <h5>
            <u>SEND SMS</u></h5>
    </div>
        <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
        </asp:ToolkitScriptManager>

        <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
            <ProgressTemplate>
                <div class="overlay">
                    <img style="top: 50%; left: 50%; position: relative;" src="images/ajax-loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <table width="75%" align="left">
                    <tr>
                        <td align="left" style="width: 15%">
                            <asp:Label ID="DateLbl" runat="server" CssClass="label" Text="Select a Date"></asp:Label>
                        </td>
                        <td align="left" style="width: 7%">
                            <asp:DropDownList ID="ddlDays" runat="server" CssClass="dropdownList" AutoPostBack="true">
                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 7%">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" AutoPostBack="true">
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 7%">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" AutoPostBack="true">
                                <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="15%" align="left">
                            <asp:Button ID="GetPriceBtn" runat="server" CssClass="button" Text="Get Price" OnClick="GetPriceBtn_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                Enter Mobile Numbers:<br />
                (Keep this field blank to fetch mobile numbers from database)
                <br />
                <asp:HiddenField ID="Hidden1" runat="server" />
                <asp:TextBox ID="txtMobiles" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                    onkeyup="EnableButton();"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="ChkIncludeGovtMembers" runat="server" Text="Include Govt Members"
                    TextAlign="Right" />
                <br />
                <br />
                Message Body:<br />
                <asp:TextBox ID="txtMssg" runat="server" TextMode="MultiLine" Width="600px" Height="200px"></asp:TextBox>
                <br />
                <asp:Button ID="btnSend" runat="server" Text="Send SMS" OnClick="btnSend_Click" OnClientClick="return Validation();" />
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
