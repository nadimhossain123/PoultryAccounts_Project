<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AccountsGroup.aspx.cs" Inherits="CollegeERP.Accounts.AccountsGroup"
    Title="Acounts Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Img
        {
            cursor: pointer;
            width: 38px;
            height: 26px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function Validation() {
            if(document.getElementById('<% = txtGroupName.ClientID%>').value == "")
	        {	
	            alert("Please enter Group Name");
		        return false;
            }
            else if (document.getElementById('<% = ddlGroupType.ClientID%>').selectedIndex == 0)
	        {	
	            alert("Please select Group Type");
		        return false;
            }
            else if (document.getElementById('<% = ddlFirstAccountType.ClientID%>').selectedIndex == 0)
	        {	
	            alert("Please select First Account Type");
		        return false;
            }
	        else if (document.getElementById('<% = ddlSecondAccountType.ClientID%>').selectedIndex == 0)
	        {	
	            alert("Please select Second Account Type");
		        return false;
            }
	        else if (document.getElementById('<% = ddlThirdAccountType.ClientID%>').selectedIndex == 0)
	        {	
	            alert("Please select Third Account Type");
		        return false;
            }
	        else {return confirm('Are You Sure?');}

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Accounts Group</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                A/c Group</h6>
            <div style="width: 740px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Group Name<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            First A/C Type
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlFirstAccountType" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                Width="170px" OnSelectedIndexChanged="ddlFirstAccountType_SelectedIndexChanged"
                                DataValueField="GroupTypeID" DataTextField="GroupType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Main / Sub Group<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="dropdownList" Width="170px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="Main Group" Text="Main Group"></asp:ListItem>
                                <asp:ListItem Value="Sub Group" Text="Sub Group"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Second A/C Type
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlSecondAccountType" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                Width="170px" OnSelectedIndexChanged="ddlSecondAccountType_SelectedIndexChanged"
                                DataValueField="GroupTypeID" DataTextField="GroupType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Under Group
                        </td>
                        <td align="left" width="30%">
                            <asp:ComboBox ID="ddlUnderGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUnderGroup_SelectedIndexChanged"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="170px">
                            </asp:ComboBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Third A/C Type
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlThirdAccountType" runat="server" Width="170px" CssClass="dropdownList"
                                DataValueField="GroupTypeID" DataTextField="GroupType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                A/c Group Details</h6>
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="left" width="15%" class="label">
                        Group Name
                    </td>
                    <td align="left" width="25%" class="label">
                        <asp:TextBox ID="txtSearchVal" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click">
                        </asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gdAccountGr" runat="server" AllowPaging="True" AllowSorting="false"
                            PageSize="30" AutoGenerateColumns="False" Width="100%" DataKeyNames="GroupID"
                            GridLines="None" OnPageIndexChanging="gdAccountGr_PageIndexChanging" OnRowEditing="gdAccountGr_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="GroupName" HeaderText="Group Name"></asp:BoundField>
                                <asp:BoundField DataField="UnderGroupName" HeaderText="Parent Group"></asp:BoundField>
                                <asp:BoundField DataField="ActType1" HeaderText="First A/C"></asp:BoundField>
                                <asp:BoundField DataField="ActType2" HeaderText="Second A/C"></asp:BoundField>
                                <asp:BoundField DataField="ActType3" HeaderText="Third A/C"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
