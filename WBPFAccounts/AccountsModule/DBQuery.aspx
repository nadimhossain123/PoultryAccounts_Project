<%@ Page Language="C#" AutoEventWireup="true" Inherits="DBQuery" Codebehind="DBQuery.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtQuery" runat="server" Height="343px" TextMode="MultiLine" 
            Width="917px"></asp:TextBox>
    
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Execute Query" Width="186px" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Execute DDL" Width="156px" />
    <br />
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
&nbsp;<asp:GridView ID="GridView1" runat="server" Width="910px">
    </asp:GridView>
    </form>
</body>
</html>
