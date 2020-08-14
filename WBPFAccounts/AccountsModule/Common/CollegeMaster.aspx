<%@ Page Title="College Details" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CollegeMaster.aspx.cs" Inherits="CollegeERP.Common.CollegeMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtCompanyName.ClientID%>').value == '') {
                alert("Please Enter Company Name");
                return false;
            }
            else if (document.getElementById('<%=txtAddress.ClientID%>').value == '') {
                alert("Please Enter Company Address");
                return false;
            }

            else if (document.getElementById('<%=txtPhone.ClientID%>').value == '') {
                alert("Please Enter Phone No");
                return false;
            }
            else if (document.getElementById('<%=txtEmail.ClientID%>').value != '' && CheckEmail() == false) {
                alert("Please Enter Valid Email ID");
                return false;
            }
            else if (document.getElementById('<%=txtPANNo.ClientID%>').value == '') {
                alert("Please Enter PAN No");
                return false;
            }
            
            else { return true; }
        }
        
        function CheckEmail() {
            var email = document.getElementById('<%=txtEmail.ClientID %>');
            var filter = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!filter.test(email.value)) {
                return false;
            }
            else
            { return true; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
		<h5>Company Details</h5>
    </div>
         
<asp:UpdatePanel ID="Up1" runat="server">
  <ContentTemplate>
  <div style="width:740px;">
       <uc3:Message ID="Message" runat="server" />
        <br />   
      <table width="100%" align="center" class="table">
        <tr>
           <td align="left" width="20%" class="label">Company<span class="req">*</span></td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
                
           </td>
           <td align="left" width="20%" class="label">Alias</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtAlias" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>  
        </tr>
        
        <tr>
           <td align="left" width="20%" class="label">Address<span class="req">*</span></td>
           <td align="left" colspan="3">
             <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox_required" Width="550px" Height="40px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
           </td>
             
        </tr>
        
        <tr>
           <td align="left" width="20%" class="label">Phone No<span class="req">*</span></td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox_required" Width="180px" MaxLength="15"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">Fax No</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>
        
        <tr>
           <td align="left" width="20%" class="label">Email</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">P Tax No</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtPTaxNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>    
        
        
        <tr>
           <td align="left" width="20%" class="label">Tan No</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtTanNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">PF No</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtPFNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>   
        
       
        
        <tr>
           <td align="left" width="20%" class="label">ESI No</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtESINo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">CIN No</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtCINNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>  
        
        
        <tr>
           <td align="left" width="20%" class="label">Vat Regn No.</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtVatRegnNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">CST Regn No.</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtCSTRegnNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>  
        
        
        <tr>
           <td align="left" width="20%" class="label">ST Regn No.</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtSTRegnNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">Website</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>
        
        <tr>
           <td align="left" width="20%" class="label">Contact Person</td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
           <td align="left" width="20%" class="label">Contact Person No</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtContactPersonNo" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
           </td>
        </tr>
        <tr>
           <td align="left" width="20%" class="label">Company Type</td>
           <td align="left" width="30%">
                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="dropdownList" Width="180px">
                    <asp:ListItem Value="None" Text="None"></asp:ListItem>
                    <asp:ListItem Value="MBA" Text="MBA"></asp:ListItem>
                    <asp:ListItem Value="Engineering" Text="Engineering"></asp:ListItem>
                </asp:DropDownList>
           </td>
           <td align="left" width="20%" class="label">PAN No<span class="req">*</span></td>
           <td align="left" width="30%">
                <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
           </td>
        </tr>
         
    </table>
    
    <br />
    <table width="100%" align="center">
        <tr>
            <td width="30%" align="left"></td>
            <td width="70%" align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" onclick="btnSave_Click" 
                    OnClientClick="javascript:return Validation()"  />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"    
                    />
            </td>
        </tr>
    </table>
    
    
    <br />
        
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvCompany" runat="server" AutoGenerateColumns="false" 
                        Width="100%" AllowPaging="false" 
                         DataKeyNames="CompanyId" onrowediting="dgvCompany_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                            <asp:BoundField DataField="CompanyPhoneNo" HeaderText="Phone No" />
                            <asp:BoundField DataField="CompanyPhoneFax" HeaderText="Fax No" />
                            <asp:BoundField DataField="CompanyEmailId" HeaderText="Email" />
                            <asp:BoundField DataField="CompanyContactPersonName" HeaderText="Contact Person" />
                            <asp:BoundField DataField="CompanyContactPersonNo" HeaderText="Contact No" />
                            <asp:BoundField DataField="CompanyType" HeaderText="Company Type" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        
                    <HeaderStyle CssClass="HeaderStyle"  />
	                <RowStyle CssClass="RowStyle" />
	                <EmptyDataRowStyle CssClass="EditRowStyle" />
	                <AlternatingRowStyle CssClass="AltRowStyle" />
	                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView> 
                </td>
            </tr>
        </table>
    
    
    
   </div> 
  </ContentTemplate>
</asp:UpdatePanel>   
</asp:Content>
