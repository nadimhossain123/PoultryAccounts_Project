<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestMail.aspx.cs" Inherits="AccountsModule.TestMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:textbox id="txtMailTo" runat="server"></asp:textbox>
            <asp:Button name="btnSend" Text="Send" runat="server" OnClick="btnSend_Click"/>
            
        </div>
        <div>
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
        </div>
    </form>
</body>
</html>
