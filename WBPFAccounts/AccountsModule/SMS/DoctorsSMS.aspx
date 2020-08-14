<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DoctorsSMS.aspx.cs" Inherits="AccountsModule.SMS.DoctorsSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtMsg.ClientID %>').value == '') {
                alert('Message Body is Blank'); return false;
            }
            else if (document.getElementById('<%=Hidden1.ClientID %>').value == '1') {
                return confirm('Message has been sent for today. Do you want to send it again?');
            }
            else {
                return confirm('Are You Sure?');
            }
        }
        function Preview(textarea) {
            document.getElementById('<%=txtMsgPreview.ClientID %>').value = textarea.value;
        }
        function Count() {

            var str = document.getElementById('<%=txtMsg.ClientID %>').value;
            var n = str.length;
            var z = Math.ceil(n / 158);
            document.getElementById('<%=lblCharacter.ClientID %>').innerHTML = n;
            document.getElementById('<%=txtCredit.ClientID %>').value = z;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="Hidden1" runat="server" />
    <div class="title">
        <h5>
            <u>SEND SMS TO DOCTOR</u></h5>
    </div>
    <table width="80%" align="center">
        
        <tr>
            <td align="center" colspan="6" style="height: 20px">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="80%" align="center">
        <tr>
            <td align="left" class="label" colspan="2">
                Group :
            </td>
        </tr>
        <tr>
            <td align="left" class="label" colspan="2">
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="dropdownList" Width="200px">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="1">DOCTORS</asp:ListItem>
                    <asp:ListItem Value="2">PARAMEDICAL</asp:ListItem>
                    <asp:ListItem Value="3">ASSISTANT</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="label" colspan="2">
                Enter Mobile Numbers:<br />
                (Keep this field blank to fetch mobile numbers from database)
            </td>
        </tr>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <tr>
            <td align="left">
                <asp:TextBox ID="txtMobNo" runat="server" CssClass="textbox" onkeyup="EnableButton();"
                    Width="550px" Height="120px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="left" class="Preview" width="40%">
                <%--Unlocked Button--%>
                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ChckedChanged" AutoPostBack="True"
                    Visible="false" />
            </td>
        </tr>
        <tr>
            <td align="left" class="label" colspan="2">
                Message Body:
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <table width="100%" align="center">
                    <tr>
                        <td align="left" width="60%">
                            <asp:TextBox ID="txtMsg" runat="server" CssClass="textbox" Width="550px" Height="160px"
                                TextMode="MultiLine" onkeyup="Preview(this); return Count();"></asp:TextBox>
                        </td>
                        <td align="left" class="Preview" width="40%">
                            <asp:TextBox ID="txtMsgPreview" runat="server" CssClass="textbox" TextMode="MultiLine"
                                Height="90px" Width="200px" ReadOnly="true">
                            </asp:TextBox>
                            <br />
                            Total Character:
                            <asp:Label ID="lblCharacter" runat="server"></asp:Label><br />
                            Total Credit:
                            <asp:TextBox ID="txtCredit" runat="server" BackColor="#F7E4BE" BorderStyle="None"
                                Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSendSMS" runat="server" CssClass="button" Text="Send SMS" OnClientClick="javascript:return Validation();"
                    OnClick="btnSendSMS_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
