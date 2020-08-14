<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ShowBirdPricePerMonth.aspx.cs" Inherits="AccountsModule.SMS.ShowBirdPricePerMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            <u>Bird Price Per Month</u></h5>
    </div>
    <table width="95%" align="center">
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="DateLbl" runat="server" CssClass="label" Text="Select Month"></asp:Label>
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
    <asp:Panel ID="Panel" runat="server" Width="820px" Height="500px" ScrollBars="Both">
        <asp:Label ID="lblmssg" runat="server" Text="Sorry!!!! no records found.." Visible="false"></asp:Label>
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
            <EmptyDataTemplate>
                No Fees Head Found
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <br />
    </asp:Panel>
</asp:Content>
