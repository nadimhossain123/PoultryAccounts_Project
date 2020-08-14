<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPTShowGrid.aspx.cs" Inherits="CollegeERP.Accounts.RPTShowGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report : </title>
    <style type="text/css">
        body
        {
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 11px;
        }
        .header
        {
            font-size: 14px;
            font-weight:bold;
        }
        .GridViewStyle
        {
            table-layout: auto;
            border-collapse: collapse;
            border: solid 1px #d3d3d3;
        }
        .HeaderStyle, .PagerStyle, .FooterStyle, .EmptyTableHeader /*Common Styles*/
        {
            background-color: #fff;
            height: 35px;
            border: solid 1px #D3D3D3;
        }
        .HeaderStyle th
        {
            padding: 1px;
            color: #000;
            text-align: left;
            font-weight: bold;
            font-size: 12px;
            padding-left: 7px;
            padding-right: 5px;
            vertical-align: middle;
        }
        .PagerStyle table
        {
            text-align: center;
            margin: auto;
        }
        .PagerStyle table td
        {
            border: 0px;
            padding: 5px;
        }
        .PagerStyle td
        {
            border-top: #CCCCCC 2px solid;
        }
        .PagerStyle a
        {
            color: #fff;
            text-decoration: none;
            padding: 2px 10px 2px 10px;
            border: solid 1px #CCCCCC;
        }
        .PagerStyle span
        {
            font-weight: bold;
            color: #fff;
            text-decoration: none;
            padding: 2px 10px 2px 10px;
        }
        .RowStyle td, .AltRowStyle td
        {
            background-color: #fff;
            font-weight: normal;
            color: #000;
            vertical-align: middle;
            border: solid 1px #D3D3D3;
            padding: 3px 7px 3px 7px;
            height: 15px;
            cursor: pointer;
        }
    </style>
</head>
<body style="cursor: pointer;" onclick="window.print()">
    <form id="form1" runat="server">
    
        <div style="width: 950px;" align="left">
            <table border="0" width="100%" id="Content">
                <tr>
                    <td align="center">
                        <%--<img src="../Images/ReportHeader.png" height="150px" />--%>
                        <%--<asp:Image ID="imgHeader" runat="server" Height="150px"   />--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" width="100%" class="header">
                        <asp:Label ID="lblReportHeader" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="100%">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" width="100%">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="100%">
                        <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    
    </form>
</body>
</html>
