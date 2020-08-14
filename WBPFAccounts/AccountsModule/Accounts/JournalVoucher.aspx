<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="JournalVoucher.aspx.cs" Inherits="CollegeERP.Accounts.JournalVoucher"
    Title="Journal Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function ValidateRequired(id, caption, type) {
            var strVal = document.getElementById(id).value;
            if (strVal.trim() == "" || strVal == "0") {
                boolVar = false;
                if (type == "txt") {
                    alert(caption + " cannot be blank!")
                    //document.write("<div id = 'div"+document.getElementById(e)+"'>"+caption + " cannot be blank!"+"</div>");
                }
                else if (type == "ddl") {
                    alert("Please Select " + caption + "!")
                }
                document.getElementById(id).focus()
            }
            else {
                boolVar = true;
                //document.getElementById(id).className=style;
            }
            return boolVar;
        }

        function validate() {
            if (!ValidateRequired("txtVchNo", "Voucher No.", "txt"))
                return false;
            else if (!ValidateRequired("txtVchDate", "Voucher Date", "txt"))
                return false;
            else if (!ValidateRequired("ddlledgernm", "Ledger Name", "ddl"))
                return false;
            else if (!ValidateRequired("txtByTo", "By/To", "txt"))
                return false;
            else if (!ValidateRequired("txtClblAmt", "Closing Balance", "txt"))
                return false;
            else if (!ValidateRequired("txtDRAmt", "DR Amount", "txt"))
                return false;
            else if (!ValidateRequired("txtCRAmt", "CR Amount", "txt"))
                return false;

            if (confirm("Are you sure to Proceed?")) {
                return true;
            }
            return false;
        }

        function changeTab() {
            var tabBehavior = $get('<%=TabContainer1.ClientID%>').control;
            //Set the Currently Visible Tab 
            tabBehavior.set_activeTabIndex(0);
        }
        function CheckDrCrValidation() {
            var drcheck = document.getElementById('<%=txtblncdr.ClientID%>').value;
            var crdtcheck = document.getElementById('<%=txtblnccr.ClientID%>').value;
            if (document.getElementById('<%=txtVchDate.ClientID%>').value = "") {
                alert("Please enter Voucher Date!");
                return false;
            }
            else if (parseFloat(drcheck) != parseFloat(crdtcheck)) {
                alert("Please make DR And CR Balance should be same!");
                return false;
            }
            else {
                return true;
            }
        }
        
         function Confirmationmessage()
        {
         if (document.getElementById('<%=txtVchDate.ClientID%>').value == '')
            {
                alert("Please Enter Voucher Date");
                return false;
            }
         else {return confirm('Are You Sure??');}    				            
      } 
      
      function openPopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
        {                
            var popposition='left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
            var NewWindow = window.open(poplocation,'',popposition);                
            if (NewWindow.focus!=null){                        
            NewWindow.focus();                
        }        
     }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Journal Voucher</h5>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="TabPanel2" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelVoucherEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <h6 align="left" style="color: #00356A;">
                            Journal Voucher Information</h6>
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td style="width: 10%" class="label" valign="middle" align="left">
                                        Voucher No. :
                                    </td>
                                    <td style="width: 20%" valign="middle" align="left">
                                        <asp:TextBox ID="txtVchNo" CssClass="textbox_pink" runat="server" Width="180px"
                                            Enabled="false"></asp:TextBox>
                                        <input id="hdnJournalID" type="hidden" value="0" runat="server" />
                                        <input id="hdndtlid" type="hidden" value="0" runat="server" />
                                    </td>
                                    <td style="width: 15%" class="label" valign="middle" align="center">
                                        Voucher Date :
                                    </td>
                                    <td style="width: 15%" valign="middle" align="left">
                                        <asp:TextBox ID="txtVchDate" runat="server" CssClass="textbox_pink" Width="120px">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDate"
                                            OnClientDateSelectionChanged="" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <h6 align="left" style="color: #00356A;">
                            Transaction</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td class="label" valign="top">
                                    By/To
                                </td>
                                <td class="label" valign="top">
                                    Ledger Name
                                </td>
                                <td class="label" valign="top">
                                    Cur Balance
                                    <input id="hdnCCApp" type="hidden" runat="server" />
                                </td>
                                <td class="label" valign="top">
                                    Cost Center
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Reference No.
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Amount Dr
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Amount Cr
                                </td>
                                <td style="width: 5%;" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlbyto" runat="server" CssClass="dropdownList" Width="40px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlbyto_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">To</asp:ListItem>
                                        <asp:ListItem>By</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="top">
                                    <asp:ComboBox ID="ddlledgernm" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                        DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                        Width="240px" OnSelectedIndexChanged="ddlledgernm_SelectedIndexChanged" DataTextField="LedgerName"
                                        DataValueField="LedgerID">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbalance" CssClass="textbox" ReadOnly="true" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcostcntr" runat="server" CssClass="dropdownList" AutoPostBack="false"
                                        Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrfno" CssClass="textbox" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamnrdr" onkeypress="return AmountOnly('txtamnrdr',this);" runat="server"
                                        CssClass="textbox" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamnrcr" onkeypress="return AmountOnly('txtamnrcr',this);" runat="server"
                                        CssClass="textbox" Width="120px">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btntsctnadd" OnClick="btntsctnadd_Click" runat="server" Text="Add"
                                        CssClass="button"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:GridView ID="grdvwtrnsctn" runat="server" Width="100%" AllowSorting="false"
                                            AllowPaging="false" AutoGenerateColumns="False" OnRowEditing="grdvwtrnsctn_RowEditing">
                                            <Columns>
                                                <asp:BoundField DataField="Serial No" HeaderText="Serial No."></asp:BoundField>
                                                <asp:BoundField DataField="By/To" HeaderText="By/To"></asp:BoundField>
                                                <asp:BoundField DataField="Ledger Name" HeaderText="Ledger Name"></asp:BoundField>
                                                <asp:BoundField DataField="Cost Center" HeaderText="Cost Center"></asp:BoundField>
                                                <asp:BoundField DataField="Reference No" HeaderText="Reference No."></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Amount Dr"
                                                    HeaderText="Amount DR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Amount Cr"
                                                    HeaderText="Amount CR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Ledger ID" Visible="False" HeaderText="Ledger ID"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtntrnpopulate" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False"></asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td style="width: 57%; height: 26px">
                                    </td>
                                    <td style="width: 15%; height: 26px; text-align: right" class="label">
                                        Balance Amount
                                    </td>
                                    <td style="width: 10%; height: 26px">
                                        <asp:TextBox ID="txtblncdr" CssClass="textbox" onkeypress="return AmountOnly('txtblncdr',this);"
                                            runat="server" Width="120px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; height: 26px">
                                        <asp:TextBox ID="txtblnccr" CssClass="textbox" onkeypress="return AmountOnly('txtblnccr',this);"
                                            runat="server" Width="120px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="width: 8%; height: 26px">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="95%">
                            <tbody>
                                <tr>
                                    <td style="width: 7%; text-align: right" class="label">
                                        Narration :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNarration" CssClass="textbox" runat="server" TextMode="MultiLine"
                                            Width="91%" Height="30px" Style="resize: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table align="center" width="95%" class="table">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="button">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Journal Voucher Information Entry</strong>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <ContentTemplate>
                <asp:UpdatePanel runat="server" ID="UpdatePanelVoucherSearch">
                    <ContentTemplate>
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="label" valign="middle" align="right">
                                                        Voucher No. :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchNoSearch" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                                        <input id="Hidden1" type="hidden" value="0" runat="server" />
                                                    </td>
                                                    <td class="label" valign="middle" align="right">
                                                        Voucher Date :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearch" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearch"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td class="label" valign="middle" align="right">
                                                        To :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearchTo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearchTo"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                                                            CssClass="button"></asp:Button>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="grdvwtrnsctnsearch" runat="server" Width="100%" AllowPaging="false"
                                            GridLines="None" AllowSorting="false" OnRowCommand="grdvwtrnsctnsearch_RowCommand"
                                            OnRowDataBound="grdvwtrnsctnsearch_RowDataBound" AutoGenerateColumns="False"
                                            CellPadding="0">
                                            <Columns>
                                                <asp:BoundField DataField="JVoucherNo" HeaderText="Voucher No."></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="JVoucherDate"
                                                    HeaderText="Voucher Date"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataField="JVNarration" HeaderText="Narration">
                                                </asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:F2}" DataField="DRAmount" HeaderText="Amount DR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:F2}" DataField="CRAmount" HeaderText="Amount CR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtneditsearch" CommandName="imgbtneditsearch" CommandArgument='<%# Eval("JVHeaderID")%>'
                                                            ImageUrl="~/Images/edit_icon.gif" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtndeletesearch" CommandName="imgbtndeletesearch" ImageUrl="~/Images/delete_icon.gif" CommandArgument='<%# Eval("JVHeaderID")%>'
                                                            runat="server" OnClientClick="return confirm('Do you want to delete this voucher?')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" Width="17px"
                                                            Height="17px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table style="height: 10px; width: 100%;">
                                                    <tr align="left" class="HeaderStyle">
                                                        <th scope="col">
                                                            No Records Found
                                                        </th>
                                                    </tr>
                                                    <tr class="RowStyle">
                                                        <td>
                                                            Sorry! No Records Found.
                                                        </td>
                                                </table>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Show Journal Voucher Information</strong>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
