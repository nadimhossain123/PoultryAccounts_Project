<%@ Page Title="Bird Price/Month" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="ShowBirdPricePerMonth.aspx.cs" Inherits="AccountsModule.Common.ShowBirdPricePerMonth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="50%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Select month: <span class="req">*</span>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="140px">
                        <asp:ListItem Value="0">MONTH</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                    <asp:Button ID="btnGetPrice" runat="server" Text="Get Price" CssClass="button" OnClick="btnGetPrice_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel" runat="server" Width="90%" Height="600px" ScrollBars="Both">
        <asp:GridView ID="dgvMonthlyPrice" runat="server" AllowPaging="false" AllowSorting="false"
            AutoGenerateColumns="false" Width="100%" CssClass="GridViewStyle" OnRowDataBound="dgvMonthlyPrice_RowDataBound">
            <Columns>
                <asp:BoundField DataField="DistName" HeaderText="Dist Name" />
                <asp:TemplateField HeaderText="1">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy1" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0} &Month={1} &DistricName={2}",1,Eval("Month"),Eval("DistName")) %>'
                            Text='<%#Bind("1") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="2">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy2" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",2,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("2") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="3">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy3" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",3,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("3") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="4">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy4" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",4,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("4") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="5">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy5" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",5,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("5") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="6">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy6" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",6,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("6") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="7">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy7" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",7,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("7") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="8">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy8" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",8,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("8") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="9">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy9" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",9,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("9") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="10">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy10" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",10,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("10") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="11">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy11" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",11,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("11") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="12">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy12" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",12,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("12") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="13">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy13" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",13,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("13") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="14">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy14" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",14,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("14") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="15">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy15" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",15,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("15") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="16">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy16" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",16,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("16") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="17">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy17" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",17,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("17") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="18">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy18" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",18,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("18") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="19">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy19" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",19,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("19") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="20">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy20" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",20,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("20") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="21">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy21" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",21,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("21") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="22">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy22" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",22,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("22") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="23">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy23" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",23,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("23") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="24">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy24" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",24,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("24") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="25">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy25" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",25,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("25") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="26">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy26" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",26,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("26") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="27">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy27" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",27,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("27") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="28">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy28" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",28,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("28") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="29">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy29" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",29,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("29") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="30">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy30" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",30,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("30") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="31">
                    <ItemTemplate>
                        <asp:HyperLink ID="hy31" runat="server" CssClass="Hyper" NavigateUrl='<%# String.Format("Diagram.aspx?Id={0}&Month={1} &DistricName={2}",31,Eval("Month"), Eval("DistName")) %>'
                            Text='<%#Bind("31") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Avg">
                    <ItemTemplate>
                        <asp:Literal ID="Avg" runat="server" Text='<%#Bind("Avg") %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <EmptyDataTemplate>
                Sorry!!! No record found.
            </EmptyDataTemplate>
        </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
