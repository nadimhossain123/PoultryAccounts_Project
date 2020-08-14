<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMSMemberSubscription.aspx.cs"
    Inherits="AccountsModule.Common.SMSMemberSubscription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 700px;">
            <uc3:Message ID="Message" runat="server" />
            <br />
            <table width="100%" align="center">
                <tr>
                    <td width="20%" class="label">
                        From Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="textbox" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                        DataKeyNames="SMSSubscriptionId" AllowPaging="true" PageSize="20" >
                        <Columns>
                            <asp:TemplateField ShowHeader="false">
                                <HeaderTemplate>
                                    S.No.</HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member/Non-member Name" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="SubscriptionStartDate" HeaderText="SubscriptionStartDate" />
                            <asp:BoundField DataField="SubscriptionEndDate" HeaderText="SubscriptionEndDate" />
                            <%--<asp:TemplateField ItemStyle-Width="30px" HeaderText="SMS Payment">
                                <ItemTemplate>
                                    <asp:Button ID="btnSMSSubscription" runat="server" Text="SMS Subscription" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:BoundField DataField="IsMember" HeaderText="Member" />
                            <asp:BoundField DataField="IsGovtMember" HeaderText="Govt." />
                            <asp:TemplateField ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:Button ID="btnBlock" runat="server" CssClass="button" Text="Block" OnClick="btnBlock_Click" />
                                </ItemTemplate>
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
