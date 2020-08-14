<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DoctorMaster.aspx.cs" Inherits="AccountsModule.SMS.DoctorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<% = ddlGroup.ClientID%>').value == 0) {
                alert("Please Select Group");
                document.getElementById('<% = ddlGroup.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtName.ClientID %>').value == '') {
                alert('Enter Name');
                document.getElementById('<%=txtName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtMobileNo.ClientID %>').value == '') {
                alert('Enter Mobile No');
                document.getElementById('<%=txtMobileNo.ClientID %>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title">
        <h5>
            <u>Add New Doctor</u></h5>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="background-color: Red; color: White;">
            <td colspan="5" align="center">
                <asp:Literal ID="ltrMsg" runat="server" Mode="PassThrough"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Group :<span class="style1">*</span>
            </td>
            <td>
                District :
            </td>
            <td>
                Block :
            </td>
            <td>
                Name :<span class="style1">*</span>
            </td>
            <td>
                Mobile No :<span class="style1">*</span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="dropdownList">
                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                    <asp:ListItem Value="1">DOCTORS</asp:ListItem>
                    <asp:ListItem Value="2">PARAMEDICAL</asp:ListItem>
                    <asp:ListItem Value="3">ASSISTANT</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" DataTextField="DistrictName" CssClass="dropdownList"
                    DataValueField="DistrictId" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlBlock" runat="server" DataTextField="BlockName" CssClass="dropdownList"
                    DataValueField="BlockId">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="99" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox" MaxLength="10" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" Width="120px"
                    OnClick="btnSave_Click" OnClientClick="javascript:return Validation();" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="dgvDoctors" runat="server" Width="100%" AllowPaging="true" PageSize="30"
                    AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" DataKeyNames="DoctorId"
                    OnPageIndexChanging="dgvDoctors_PageIndexChanging" OnRowDeleting="dgvDoctors_RowDeleting"
                    OnRowEditing="dgvDoctors_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl" ItemStyle-Width="15px">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
                        <asp:BoundField DataField="GroupName" HeaderText="GroupName" />
                        <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" />
                        <asp:BoundField DataField="BlockName" HeaderText="BlockName" />
                        <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" />
                        <asp:CommandField ShowEditButton="true" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
