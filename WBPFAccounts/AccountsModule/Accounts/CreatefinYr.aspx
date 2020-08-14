<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CreatefinYr.aspx.cs" Inherits="CollegeERP.Accounts.CreatefinYr" Title="Financial Year" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    function Validation()
    {
        if(document.getElementById('<%=txtStartYr.ClientID%>').value.length != 4)
	     {	
		   alert("Please Enter a Valid Start Year");   
		   return false;
	     }
	     else if(document.getElementById('<%=txtEndYr.ClientID%>').value.length != 4)
	     {	
		   alert("Please Enter a Valid End Year");   
		   return false;
	     }
	     else if (parseFloat(document.getElementById('<%=txtStartYr.ClientID%>').value) >= parseFloat(document.getElementById('<%=txtEndYr.ClientID%>').value))
	     {
	         alert("Please Enter a Valid Financial Year Range");   
		    return false;
	     }
	     else {return true;}
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Create Financial Year</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 720px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Start Year<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtStartYr" runat="server" CssClass="textbox_required" Width="140px"
                                MaxLength="4"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtStartYr">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td align="left" width="20%" class="label">
                            End Year<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtEndYr" runat="server" CssClass="textbox_required" Width="140px"
                                MaxLength="4"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Numbers" TargetControlID="txtEndYr">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Active
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="chkActive" runat="server" />
                        </td>
                        <td align="left" width="20%" class="label">
                        </td>
                        <td align="left" width="30%">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" OnClientClick="return Validation()"
                                OnClick="btnAdd_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gdFinancialYr" runat="server" AllowSorting="false" AllowPaging="false"
                                AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="FinYear" HeaderText="Financial Year" />
                                    <asp:BoundField DataField="Syear" HeaderText="Start Year"></asp:BoundField>
                                    <asp:BoundField DataField="EYear" HeaderText="End Year"></asp:BoundField>
                                    <asp:CheckBoxField DataField="Active" HeaderText="Is Active"></asp:CheckBoxField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
