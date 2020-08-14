<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpMemberListPrint.aspx.cs"
    Inherits="AccountsModule.Common.PopUpMemberListPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member List Print</title>
</head>
<body onclick="window.print()">
    <form id="form1" runat="server">
    <div style="font-size: 13px; font-family: Segoe UI Symbol">
        <asp:DataList runat="server" ID="grdMemberPrint" RepeatDirection="Horizontal" RepeatColumns="3">
            <ItemTemplate>
                <div style="margin:0px 10px 15px 0px">
                    <%# DataBinder.Eval(Container.DataItem, "MemberName") %>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "Address") %>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "Phone") %>
                    <br />
                </div>
            </ItemTemplate>
            <HeaderStyle CssClass="HeaderStyle" />
        </asp:DataList>
    </div>
    </form>
</body>
</html>
