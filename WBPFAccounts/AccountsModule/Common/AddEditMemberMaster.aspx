<%@ Page Title="Add/Edit Member Master" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="AddEditMemberMaster.aspx.cs" Inherits="AccountsModule.Common.AddEditMemberMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ValidateExtension(fileUpload) {
            var allowedFiles = [".doc", ".docx", ".pdf", ".jpg", ".jpeg", ".png", ".gif", ".txt", ".xls", ".xlsx"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (!regex.test(fileUpload.value.toLowerCase())) {
                alert("Please upload files having extensions: " + allowedFiles.join(', ') + " only");
                fileUpload.value = '';
                return false;
            }
            return true;
        }

        function IsValidEmail(s) {
            var filter = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!filter.test(s)) {
                return false;
            }
            else
            { return true; }
        }

        function Validation() {
            if (document.getElementById("<%=txtMemberName.ClientID %>").value.trim() == '') {
                alert('Please Enter Member Name');
                return false;
            }
            if (document.getElementById("<%=txtVillageOrStreet.ClientID %>").value == '') {
                alert('Please Enter Village/Street');
                return false;
            }
            if (document.getElementById("<%=txtPO.ClientID %>").value.trim() == '') {
                alert('Please Enter P.O.');
                return false;
            }
            if (document.getElementById("<%=txtPS.ClientID %>").value.trim() == '') {
                alert('Please Enter P.S.');
                return false;
            }
            if (document.getElementById("<%=txtPIN.ClientID %>").value.trim() == '') {
                alert('Please Enter PIN');
                return false;
            }
            if (document.getElementById("<%=ddlState.ClientID %>").selectedIndex == 0) {
                alert('Please Select State');
                return false;
            }
            if (document.getElementById("<%=ddlDistrict.ClientID %>").selectedIndex == 0) {
                alert('Please Select District');
                return false;
            }
            if (document.getElementById("<%=ddlBlock.ClientID %>").selectedIndex == 0) {
                alert('Please Select Block');
                return false;
            }
            if (document.getElementById("<%=ddlMembershipCategory.ClientID %>").selectedIndex == 0) {
                alert('Please Select Membership Category');
                return false;
            }
            if (document.getElementById("<%=chkMember.ClientID %>").checked == true && document.getElementById("<%=ddlMemberGroup.ClientID %>").selectedIndex == 0) {
                alert("Please Select Member Group");
                return false;
            }
            if (document.getElementById("<%=chkMember.ClientID %>").checked == true && document.getElementById("<%=ddlBusinessType.ClientID %>").selectedIndex == 0) {
                alert("Please Select Business Type");
                return false;
            }
            if (document.getElementById("<%=txtMembershipDate.ClientID %>").value.trim() == '') {
                alert('Please Enter Membership Date');
                return false;
            }
            if (document.getElementById("<%=txtEffectiveDate.ClientID %>").value.trim() == '') {
                alert('Please Enter Effective Date');
                return false;
            }
            if (document.getElementById("<%=txtMembershipCategoryEffectiveDate.ClientID %>").value.trim() == '') {
                alert('Please Enter Membership Category Effective Date');
                return false;
            }
            if (document.getElementById("<%=txtEmail.ClientID %>").value.trim() != '' && !IsValidEmail(document.getElementById("<%=txtEmail.ClientID %>").value.trim())) {
                alert("Invalid Email Address");
                return false;
            }
            else {
                return confirm("Are You Sure?");
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Member Registration</h5>
    </div>
    <%--<asp:UpdatePanel ID="UP1" runat="server"><ContentTemplate>--%>
    <div style="margin: 0px 20px">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="70%" align="left" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Member/Non-member Code
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtMemberCode" CssClass="textbox" Enabled="false" runat="server"
                        Width="260px" Text="AUTO GENERATED" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Member/Non-member Name <span class="req">*</span>
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="txtMemberName" CssClass="textbox" runat="server" Width="260px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Is Member
                </td>
                <td align="left" width="60%">
                    <asp:CheckBox ID="chkMember" runat="server" AutoPostBack="true" OnCheckedChanged="chkMember_CheckedChanged"
                        Checked="true" />
                </td>
                <td align="left" width="20%" class="label">
                    Is Executive Member(Yes/No)
                </td>
                <td align="left" width="60%">
                    <asp:CheckBox ID="chkExecutiveMember" runat="server"/>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Is Govt. Member
                </td>
                <td align="left" width="60%">
                    <asp:CheckBox ID="chkIsGovtMember" runat="server" />
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <br />
        <b style="float: left; padding: 10px 0px 2px 10px;">Address Details</b>
        <div style="clear: both">
        </div>
        <hr class="style-one" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="50%" style="float: left" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Village/Street <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtVillageOrStreet" runat="server" CssClass="textbox" Width="180px"
                                TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            P.O. <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPO" runat="server" CssClass="textbox" Width="180px" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            P.S. <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPS" runat="server" CssClass="textbox" Width="180px" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            PIN <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPIN" runat="server" CssClass="textbox" Width="180px" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            State <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlState" runat="server" Width="192px" CssClass="dropdownList"
                                Height="28px" Style="margin-bottom: 4px;" TabIndex="9" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            District <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="192px" CssClass="dropdownList"
                                Height="28px" Style="margin-bottom: 4px;" TabIndex="8" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Block <span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlBlock" runat="server" Width="192px" CssClass="dropdownList"
                                Height="28px" Style="margin-bottom: 4px; margin-top: -6px" TabIndex="7">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="50%" class="table" style="float: right">
            <tr>
                <td align="left" width="20%" class="label">
                    Plot No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtPlotNo" runat="server" CssClass="textbox" Width="180px" TabIndex="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Khaitan No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtKhaitanNo" runat="server" CssClass="textbox" Width="180px" TabIndex="11"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Mouza
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtMouza" runat="server" CssClass="textbox" Width="180px" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    JL No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtJLNo" runat="server" CssClass="textbox" Width="180px" TabIndex="13"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <b style="float: left; padding: 10px 0px 2px 10px;">Personal Details</b>
        <div style="clear: both">
        </div>
        <hr class="style-one" />
        <table width="50%" style="float: left" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Mobile No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtMobileNo" CssClass="textbox" runat="server" Width="180px" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Alternative Mobile No. (not for SMS)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtMobileNo2" CssClass="textbox" runat="server" Width="180px" TabIndex="15" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Phone No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox" Width="180px" TabIndex="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Email
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="180px" CssClass="textbox" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    VAT No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtVATNo" runat="server" CssClass="textbox" Width="180px" TabIndex="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    PAN No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox" Width="180px" TabIndex="17"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    LIC No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtLICNo" runat="server" CssClass="textbox" Width="180px" TabIndex="18"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    GST No.
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtGSTNo" runat="server" CssClass="textbox" Width="180px" TabIndex="18"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="50%" class="table" style="float: right">
            <tr>
                <td align="left" width="20%" class="label">
                    Photo
                </td>
                <td align="left" width="30%">
                    <asp:Image ID="ImgPhoto" runat="server" Width="100px" Height="100px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Select Photo
                </td>
                <td align="left" width="30%">
                    <asp:FileUpload ID="uploadImage" runat="server" />
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <b style="float: left; padding: 10px 0px 2px 10px;">Capacity</b>
        <div style="clear: both">
        </div>
        <hr class="style-one" />
        <table width="50%" style="float: left" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Layer Capacity (Nos)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtLayerCapacityNos" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="19"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtLayerCapacityNos_FilteredTextBoxExtender" runat="server"
                        Enabled="True" TargetControlID="txtLayerCapacityNos" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Breeder Capacity (Nos)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtBreederCapacityNos" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="20"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtBreederCapacityNos_FilteredTextBoxExtender" runat="server"
                        Enabled="True" TargetControlID="txtBreederCapacityNos" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Chicken Seller Daily Sales (Nos)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtChickenSellerDailySalesNos" runat="server" CssClass="textbox"
                        Width="180px" TabIndex="21"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtChickenSellerDailySalesNos_FilteredTextBoxExtender"
                        runat="server" Enabled="True" TargetControlID="txtChickenSellerDailySalesNos"
                        FilterMode="ValidChars" ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Feed Producer Daily Sales (MT)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtFeedProducerDailySalesMT" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="22"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtFeedProducerDailySalesMT_FilteredTextBoxExtender"
                        runat="server" Enabled="True" TargetControlID="txtFeedProducerDailySalesMT" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Broiler Capacity (Nos)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtBroilerCapacityNos" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="23"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtBroilerCapacityNos_FilteredTextBoxExtender" runat="server"
                        Enabled="True" TargetControlID="txtBroilerCapacityNos" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
        <table width="50%" class="table" style="float: right">
            <tr>
                <td align="left" width="20%" class="label">
                    Egg Seller Daily Sales (Nos)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtEggSellerDailySalesNos" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="24"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtEggSellerDailySalesNos_FilteredTextBoxExtender"
                        runat="server" Enabled="True" TargetControlID="txtEggSellerDailySalesNos" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Chicken Seller Daily Sales (Kgs)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtChickenSellerDailySalesKgs" runat="server" CssClass="textbox"
                        Width="180px" TabIndex="25"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtChickenSellerDailySalesKgs_FilteredTextBoxExtender"
                        runat="server" Enabled="True" TargetControlID="txtChickenSellerDailySalesKgs"
                        FilterMode="ValidChars" ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Feed Seller Daily Sales (MT)
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtFeedSellerDailySalesMT" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="26"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtFeedSellerDailySalesMT_FilteredTextBoxExtender"
                        runat="server" Enabled="True" TargetControlID="txtFeedSellerDailySalesMT" FilterMode="ValidChars"
                        ValidChars="0123456789.">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Other Category
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtOtherCategory" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="27"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Remarks
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Width="180px" Height="40px"
                        TextMode="MultiLine" Style="resize: none" TabIndex="28"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <b style="float: left; padding: 10px 0px 2px 10px;">Business Details</b>
        <div style="clear: both">
        </div>
        <hr class="style-one" />
        <table width="50%" style="float: left" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Company Name
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textbox" Width="180px"
                        TabIndex="29"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Website
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" Width="180px" TabIndex="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Membership Category <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMembershipCategory" runat="server" Width="210px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="31">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Member Group
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMemberGroup" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="32">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Membership Category Effective Date <span class="req">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtMembershipCategoryEffectiveDate" runat="server" CssClass="textbox" Width="180px"
                        onkeydown="return false" TabIndex="37"></asp:TextBox>
                    <asp:CalendarExtender ID="calMembershipCategoryEffectiveDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtMembershipCategoryEffectiveDate"
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
        <table width="50%" class="table" style="float: right">
            <tr>
                <td align="left" width="20%" class="label">
                    Business Type
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlBusinessType" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="33">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Membership Date <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtMembershipDate" runat="server" CssClass="textbox" Width="180px"
                        Enabled="false" onkeydown="return false" TabIndex="34"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderMembershipDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtMembershipDate"
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Effective Date <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textbox" Width="180px"
                        Enabled="false" onkeydown="return false" TabIndex="35"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtenderEffectiveDate" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtEffectiveDate"
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Narration
                </td>
                <td>
                    <asp:TextBox ID="txtNarration" runat="server" CssClass="textbox" Width="250px" Height="40px" TextMode="MultiLine" style="resize:none;"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td align="left" width="20%" class="label">
                    Member SMS Category
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMemberSMSCategory" runat="server" Width="192px" CssClass="dropdownList"
                        Height="28px" Style="margin-bottom: 4px;" TabIndex="32">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <div style="clear: both">
        </div>
        <b style="float: left; padding: 10px 0px 2px 10px;">Document Details</b>
        <div style="clear: both">
        </div>
        <hr class="style-one" />
        <table width="50%" style="float: left;" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Document 1
                </td>
                <td align="left" width="30%">
                    <div>
                        <asp:TextBox ID="txtDocName_1" runat="server" CssClass="textbox" Width="190px" MaxLength="100"></asp:TextBox>
                    </div>
                    <div>
                        <asp:FileUpload ID="fileUploadDocument_1" runat="server" CssClass="label" onchange="ValidateExtension(this);" />
                    </div>
                    <div>
                        <asp:Label ID="lblDocFile_1" runat="server" CssClass="label"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Document 2
                </td>
                <td align="left" width="30%">
                    <div>
                        <asp:TextBox ID="txtDocName_2" runat="server" CssClass="textbox" Width="190px" MaxLength="100"></asp:TextBox>
                    </div>
                    <div>
                        <asp:FileUpload ID="fileUploadDocument_2" runat="server" CssClass="label" onchange="ValidateExtension(this);" />
                    </div>
                    <div>
                        <asp:Label ID="lblDocFile_2" runat="server" CssClass="label"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label" valign="middle">
                    Document 3
                </td>
                <td align="left" width="30%">
                    <div>
                        <asp:TextBox ID="txtDocName_3" runat="server" CssClass="textbox" Width="190px" MaxLength="100"></asp:TextBox>
                    </div>
                    <div>
                        <asp:FileUpload ID="fileUploadDocument_3" runat="server" CssClass="label" onchange="ValidateExtension(this);" />
                    </div>
                    <div>
                        <asp:Label ID="lblDocFile_3" runat="server" CssClass="label"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                        OnClick="btnSave_Click" TabIndex="37" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="button" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <%--</ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>
