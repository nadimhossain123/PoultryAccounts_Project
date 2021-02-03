<%@ Page Title="Members Detail" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="MemberMasterInfo.aspx.cs" Inherits="AccountsModule.Common.MemberMasterInfo" %>

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
        <h5>Member/Non-member Details (Payment Collection)</h5>
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

                          <td align="right"><asp:DropDownList ID="ddlMonth" runat="server"  CssClass="dropdownList"
                                Width="160px">
                              <asp:listitem value="0" Text="MONTH-ALL"></asp:listitem>
                              <asp:listitem value="01" Text="JAN"></asp:listitem>
                              <asp:listitem value="02" Text="FEB"></asp:listitem>
                              <asp:listitem value="03" Text="MAR"></asp:listitem>
                              <asp:listitem value="04" Text="APR"></asp:listitem>
                              <asp:listitem value="05" Text="MAY"></asp:listitem>
                              <asp:listitem value="06" Text="JUN"></asp:listitem>
                              <asp:listitem value="07" Text="JULY"></asp:listitem>
                              <asp:listitem value="08" Text="AUG"></asp:listitem>
                              <asp:listitem value="09" Text="SEP"></asp:listitem>
                              <asp:listitem value="10" Text="OCT"></asp:listitem>
                              <asp:listitem value="10" Text=" NOV"></asp:listitem>
                              <asp:listitem value="12" Text="DEC"></asp:listitem>
                            </asp:DropDownList></td>
                        <td align="right"><asp:DropDownList ID="ddlYear" runat="server"  CssClass="dropdownList"
                                Width="160px">
                              <asp:listitem value="0" Text="YEAR-ALL"></asp:listitem>
                              <asp:listitem value="2021" Text="2021"></asp:listitem>
                              <asp:listitem value="2020" Text="2020"></asp:listitem>
                              <asp:listitem value="2019" Text="2019"></asp:listitem>
                              <asp:listitem value="2018" Text="2018"></asp:listitem>
                              <asp:listitem value="2017" Text="2017"></asp:listitem>
                              <asp:listitem value="2016" Text="2016"></asp:listitem>
                              <asp:listitem value="2015" Text="2015"></asp:listitem>
                              <asp:listitem value="2014" Text="2014"></asp:listitem>
                              <asp:listitem value="2013" Text="2013"></asp:listitem>
                            <asp:listitem value="2012" Text="2012"></asp:listitem>
                            <asp:listitem value="2011" Text="2011"></asp:listitem>
                            <asp:listitem value="2010" Text="2010"></asp:listitem>
                            </asp:DropDownList></td>
                       


                    </tr>
                    <tr>
                        <td align="right" colspan="8">
                            <asp:Button ID="btnSearch0" runat="server" CssClass="button"
                                OnClick="btnSearch_Click" Text="Search" Width="120px" />&nbsp;
                            <a href="SMSMemberList.aspx" class="button" style="text-decoration: none;">Go to SMS Member List (Payment Collection) >></a>&nbsp;
                            <a href="MemberPaymentExcelUpload.aspx" class="button" style="text-decoration: none;" id="lnkBulkUpload" runat="server">Bulk Upload</a>&nbsp;
                            <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" colspan="8">
                            <asp:Label ID="lblTotalMemberCount" runat="server"></asp:Label>
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
                                        <asp:BoundField DataField="MobileNo2" HeaderText="Alternative Mobile No (not for SMS)" />
                                        <asp:BoundField DataField="GSTNo" HeaderText="GST NO" />
                                        <asp:BoundField DataField="MonthlyFeesAmount" HeaderText="Monthly Fees" />
                                        <asp:BoundField DataField="DevelopmentFeeAmount" HeaderText="Development Fees" />
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Button ID="btnActivate" runat="server" CommandName="Activate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPriority" runat="server" CommandName="ChangePriority" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Outstanding">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnOutstanding" runat="server" Text="Outstanding" BackColor="SkyBlue" />
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnDevFeesOutstanding" runat="server" Text="Dev Outsta.." BackColor="YellowGreen" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPayment" runat="server" Text="Payment" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="SMS Payment" Visible="false">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSMSPayment" runat="server" Text="SMS Payment" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
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
</asp:Content>
