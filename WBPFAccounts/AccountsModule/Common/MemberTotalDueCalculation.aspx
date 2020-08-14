<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MemberTotalDueCalculation.aspx.cs" Inherits="AccountsModule.Common.MemberTotalDueCalculation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
        function openpopup(poplocation) {
            var popposition = 'left = 50, top=50, width=1250,align=center, height=700,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>Member Total Due Report</h5>
    </div>
    <div style="width: 1325px;">
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td width="10%" align="left" class="label">State:
                        </td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="160px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="8%" align="left" class="label">District:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="160px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="7%" align="left" class="label">Block:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropdownList" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td width="7%" align="left" class="label">Membership Category:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlMembershipCategory" runat="server" CssClass="dropdownList"
                                Width="160px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="7%" align="left" class="label">Business Type:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBusinessType" runat="server"  CssClass="dropdownList"
                                Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td width="10%" align="left" class="label">Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td align="right">Mobile :</td>
                        <td align="left">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" MaxLength="20"
                                Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="8">
                            <asp:Button ID="btnSearch0" runat="server" CssClass="button"
                                OnClick="btnSearch_Click" Text="Search" Width="120px" />&nbsp;
                            <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" colspan="8">
                            <asp:Label ID="lblTotalMemberCount" runat="server"></asp:Label>
                        </td>
                        <td align="left" class="label" colspan="8">
                            <asp:Label ID="LblTotalDueAmount" runat="server"></asp:Label>
                        </td>

                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:Panel ID="Pnl1" runat="server" Width="1320px" ScrollBars="Horizontal">
                                <asp:GridView ID="dgvMemberMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="MemberId,IsActive,IsPriority" OnRowEditing="dgvMemberMaster_RowEditing" AllowPaging="true"
                                    PageSize="20" OnPageIndexChanging="dgvMemberMaster_PageIndexChanging" OnRowDataBound="dgvMemberMaster_RowDataBound"
                                    OnRowCommand="dgvMemberMaster_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="MemberName" HeaderText="Name" />
                                        <asp:BoundField DataField="StateName" HeaderText="State" />
                                        <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                        <asp:BoundField DataField="BlockName" HeaderText="Block" />
                                        <asp:BoundField DataField="CategoryName" HeaderText="Membership Category" />
                                        <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Outstanding">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnOutstanding" runat="server" Text="Outstanding" BackColor="SkyBlue" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       </Columns>
                                    <EmptyDataTemplate>
                                        <p>
                                            No Record Found
                                        </p>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:PostBackTrigger ControlID="btnDownload"/></Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content >
