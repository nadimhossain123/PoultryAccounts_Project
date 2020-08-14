<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddEditMember.aspx.cs" Inherits="AccountsModule.SMS.AddEditMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CalculateEndDate() {
            var field = document.getElementById('<%=txtStartDate.ClientID %>');
            var field1 = document.getElementById('<%=txtEndDate.ClientID %>');
            if (field.value != '' && isValidDate(field.value) == true) {
                var s = field.value;
                s = s.replace(/0*(\d*)/gi, "$1");
                var dateArray = s.split(/[\.|\/|-]/);
                dateArray[2] = parseInt(dateArray[2]) + 1;
                field1.value = dateArray[0] + '/' + dateArray[1] + '/' + dateArray[2];
                field1.focus();

            }
            else {
                alert('Enter Start Date in DD/MM/YYYY Format');
                field.focus();
            }
        }
        function Validation() {
            if (document.getElementById('<%=txtName.ClientID %>').value == '') {
                alert('Enter Member name'); return false;
            }

            else if (document.getElementById('<%=txtMobNo.ClientID %>').value == '') {
                alert('Enter Mobile No'); return false;
            }
            else if (document.getElementById('<%=txtStartDate.ClientID %>').value == '') {
                alert('Enter Start Date'); return false;
            }
            else if (document.getElementById('<%=txtEndDate.ClientID %>').value == '') {
                alert('Enter End Date'); return false;
            }

            else if (isValidDate(document.getElementById('<%=txtStartDate.ClientID %>').value) == false) {
                alert('Enter Start Date in DD/MM/YYYY Format'); return false;
            }
            else if (isValidDate(document.getElementById('<%=txtEndDate.ClientID %>').value) == false) {
                alert('Enter End Date in DD/MM/YYYY Format'); return false;
            }

            else { return true; }

        }

        function QuickValidation() {
            if (document.getElementById('<%=txtMobNoforQuickSearch.ClientID %>').value.length != 10) {
                alert('Mobile No Should be 10-digit No'); return false;
            }
            else { return true; }
        }
        function isValidDate(s) {
            // format D(D)/M(M)/(YY)YY
            var dateFormat = /^\d{1,4}[\.|\/|-]\d{1,2}[\.|\/|-]\d{1,4}$/;

            if (dateFormat.test(s)) {
                // remove any leading zeros from date values
                s = s.replace(/0*(\d*)/gi, "$1");
                var dateArray = s.split(/[\.|\/|-]/);

                // correct month value
                dateArray[1] = dateArray[1] - 1;

                // correct year value
                if (dateArray[2].length < 4) {
                    // correct year value
                    dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
                }

                var testDate = new Date(dateArray[2], dateArray[1], dateArray[0]);
                if (testDate.getDate() != dateArray[0] || testDate.getMonth() != dateArray[1] || testDate.getFullYear() != dateArray[2]) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }


        function isNumber(n) {
            if (n == '') { n = '0'; }
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        function ActivateNow(field) {
            if (field.title == 'Activated') {
                return false;
            }
            else if (field.title == 'Activate Now') {
                return confirm('Do you want to activate ' + field.innerHTML + ' ?');
            }
        }
        function SearchValidation() {
            var field = document.getElementById('<%=txtExpirationDate.ClientID%>');
            if (field.value != '' && isValidDate(field.value) == false) {
                alert('Enter Expiration Date In DD/MM/YYYY Format');
                field.focus();
                return false;
            }

            else { return true; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            <u>ADD NEW MEMBER</u></h5>
    </div>
    <table width="90%" align="center">
        <tr>
            <td colspan="2" align="center">
                <asp:Literal ID="ltrMsg" runat="server" Mode="PassThrough"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Member Name:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Location:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtLocation" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Start Date:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox" Width="250px" onblur="javascript:CalculateEndDate()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                End Date:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Mobile No:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtMobNo" runat="server" CssClass="textbox" Width="250px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Registration Type:
            </td>
            <td align="left" width="70%">
                <asp:DropDownList ID="ddlRegType" runat="server" CssClass="dropdownList">
                    <asp:ListItem Text="PAID" Value="2"></asp:ListItem>
                    <asp:ListItem Text="FREE" Value="3"></asp:ListItem>
                    <asp:ListItem Text="GOVT" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Registration No:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Voucher Details:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtVoucherDetails" runat="server" CssClass="textbox" Width="250px"
                    Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
            </td>
            <td align="left" width="70%">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="javascript:return Validation()"
                    OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="left">
                <br />
                <b>Quick Search by Mobile No</b><hr />
            </td>
        </tr>
        <tr>
            <td align="left" width="30%" class="label">
                Mobile No:
            </td>
            <td align="left" width="70%">
                <asp:TextBox ID="txtMobNoforQuickSearch" runat="server" CssClass="textbox" Width="250px"
                    MaxLength="10"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnQuickSearch" runat="server" CssClass="button" Text="Quick Search"
                    OnClientClick="javascript:return QuickValidation()" OnClick="btnQuickSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <br />
                <b>Search</b><hr />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <table width="100%" align="left">
                    <tr>
                        <td align="left" class="label" width="15%">
                            Member Name
                        </td>
                        <td align="left" class="label" width="15%">
                            Mobile No
                        </td>
                        <td align="left" class="label" width="15%">
                            Registration Type
                        </td>
                        <td align="left" class="label" width="15%">
                            Expiration(DD/MM/YYYY)
                        </td>
                        <td align="left" class="label" width="15%">
                        </td>
                        <td align="left" class="label" width="7%">
                        </td>
                        <td align="left" class="label" width="18%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtSearchMob" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="15%">
                            <asp:DropDownList ID="ddlRegTypeSearch" runat="server">
                                <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                <asp:ListItem Text="PAID" Value="2"></asp:ListItem>
                                <asp:ListItem Text="FREE" Value="3"></asp:ListItem>
                                <asp:ListItem Text="GOVT" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtExpirationDate" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="15%">
                            <asp:DropDownList ID="ddlExpiration" runat="server">
                                <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Expired" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="7%">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click"
                                OnClientClick="javascript:return SearchValidation();" />
                        </td>
                        <td align="left" width="18%">
                            <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:GridView ID="dgvMember" runat="server" Width="100%" AllowPaging="true" PageSize="100"
                    CellSpacing="2" GridLines="Both" DataKeyNames="MemberId" AutoGenerateColumns="false"
                    OnPageIndexChanging="dgvMember_PageIndexChanging" OnRowDataBound="dgvMember_RowDataBound"
                    OnRowDeleting="dgvMember_RowDeleting" OnRowEditing="dgvMember_RowEditing" OnRowCommand="dgvMember_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Literal ID="ltrSl" runat="server" Mode="PassThrough"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="High Priority" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkPriority" runat="server" AutoPostBack="true" OnCheckedChanged="ChkPriority_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Member Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMemberName" runat="server" Text='<%#Bind("MemberName") %>'
                                    CommandName="Activate" OnClientClick="javascript:return ActivateNow(this);"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Literal ID="ltrIsExpired" runat="server" Text='<%#Bind("IsExpired") %>'>
                                </asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                        <asp:BoundField DataField="RegistrationType" HeaderText="Registration Type" />
                        <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No " />
                        <asp:BoundField DataField="VoucherDetails" HeaderText="Voucher" />
                        <asp:CommandField ShowEditButton="true" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EmptyDataTemplate>
                        No Fees Head Found
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
