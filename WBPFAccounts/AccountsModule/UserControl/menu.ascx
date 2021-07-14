<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="CollegeERP.UserControl.menu" %>
<ul id="quick">
    <li id="liSettings" runat="server"><a href="" title="Settings"><span class="icon">
        <img src="/Images/cog.png" alt="Settings" /></span><span>Settings</span></a>
        <ul>
            <%--<li id="liCompanyConfig" runat="server"><a href="/Common/CollegeMaster.aspx">Company
                Configuration</a></li>--%>
            <li id="liCreateFnYear" runat="server"><a href="/Accounts/CreatefinYr.aspx">Create Financial
                Year</a></li>
            <%--<li id="liChangeCompany" runat="server" class="last"><a href="/Accounts/ChangeCompany.aspx">
                Change Company </a></li>--%>
        </ul>
    </li>
    <li id="liDevelopmentFee" runat="server"><a href="" title="DevelopmentFee"><span
        class="icon">
        <img src="/Images/cog.png" alt="Settings" /></span><span>Development Fee</span></a>
        <ul>
            <li id="liDevelopmentFeesGeneration" runat="server"><a href="/Common/MonthlyDevelopmentFeeGeneration.aspx">Fees Generation</a></li>
            <li id="liMonthlyDevelopmentBill" runat="server"><a href="/Common/MonthlyDevelopmentFeeBill.aspx">Monthly Development Fee Bill</a></li>
            <li id="liMemberDevelopmentFeesDetails" runat="server"><a href="/Common/MemberDevelopmentFeesDetails.aspx">Development Fee Invoice</a></li>
            <li id="liDevelopmentMemberReport" runat="server"><a href="/Common/DevelopmentFeeMemberReport.aspx">Development Fee Report</a></li>
            <li id="liDevelopmentFeeAdjustment" runat="server" ><a href="/Common/DevelopmentFeeAdjustment.aspx">Development Fee Adjustment </a></li>
            <li id="liDevelopmentFeePaymentReport" runat="server" ><a href="/Common/DevelopmentFeePaymentReport.aspx">Development Fee Payment Report</a></li>
            <li id="liDevelopmentFeeUpdate" runat="server" ><a href="/Common/UpdateMenberDevelopmentFees.aspx">Prev Month Development Fees Update </a></li>
            <li id="li2" runat="server"><a href="/Common/DevelopmentFeeUpdateReport.aspx">Development Fee Update Report</a></li>
            <li id="liRenewalFeeUpdate" runat="server"><a href="/Common/UpdateMemberRenewalFees.aspx">Prev Month Renewal Fees Update </a></li>
            <li id="li3" runat="server" class="last"><a href="/Common/RenewalFeeUpdateReport.aspx">Renewal Fee Update Report</a></li>
        </ul>
    </li>
    <li id="liMasters" runat="server"><a href="" title="Settings"><span class="icon">
        <img src="/Images/student.jpg" alt="Masters" /></span><span>Masters</span></a>
        <ul>
            <li id="liMembershipCategory" runat="server"><a href="/Common/AddEditMembershipCategory.aspx">Membership Category</a></li>
            <li id="liBusinessType" runat="server"><a href="/Common/AddEditBusinessType.aspx">Business
                Type</a></li>
            <li id="liGroup" runat="server"><a href="/Common/AddEditMemberGroup.aspx">Group</a></li>
            <li id="liState" runat="server"><a href="/Common/AddEditState.aspx">State</a></li>
            <li id="liDistrict" runat="server"><a href="/Common/AddEditDistrict.aspx">District</a></li>
            <li id="liBlock" runat="server" class="last"><a href="/Common/AddEditBlock.aspx">Block</a></li>
        </ul>
    </li>
    <li id="liEntries" runat="server"><a href="" title="Settings"><span class="icon">
        <img src="/Images/HR.jpg" alt="Entries" /></span><span>Entries</span></a>
        <ul>
            <li id="liMemberApproval" runat="server"><a href="/Common/MemberMasterApprove.aspx">Members Approval</a></li>
            <li id="liMemberDetail" runat="server"><a href="/Common/MemberMasterInfo.aspx">Member/SMS Member Detail (Payment Collection)</a></li>
            <li id="liAgentDetail" runat="server"><a href="/Common/AddEditAgent.aspx">Area Manager Detail</a></li>
            <li id="liAgentPaymentReport" runat="server"><a href="/Common/AgentPaymentReport.aspx">Area Manager Payment Report</a></li>
            <li id="liMemberFeesGeneration" runat="server"><a href="/Common/MonthlySubscriptionFeesGeneration.aspx">Member Fees Generation</a></li>
            <li id="liMemberPaymentEntry" runat="server"><a href="/Common/MemberPayment.aspx">Member Payment Entry</a></li>
            <li id="liMemberOutstandingReport" runat="server" visible="true"><a href="/Common/MemberOutstandingReport.aspx">Member Outstanding Report</a></li>
            <li id="liMemberPaymentReport" runat="server"><a href="/Common/MemberPaymentReport.aspx">Members Payment Detail</a></li>
            <li id="liMemberConsolidatedOutstandingReport" runat="server"><a href="/Common/MemberOutstandingReportConsolidated.aspx">Member Consolidated Outstanding Report</a></li>
            <li id="liMemberUptoDateReport" runat="server"><a href="/Common/MemberUptoDateReport.aspx">Member Upto Date Report</a></li>
            <li id="liMemberReport" runat="server"><a href="/Common/MemberReport.aspx">Member Report</a></li>
            <li id="liServiceTaxReport" runat="server"><a href="/Common/ServiceTaxReport.aspx">Service Tax Report</a></li>
            <li id="liMemberRenewalFeesDetails" runat="server"><a href="/Common/MemberRenewalFeesDetails.aspx">Member Renewal Fees Invoice</a></li>
            <li id="liMemberMonthlyBill" runat="server"><a href="/Common/MemberBillDetails.aspx">Member Monthly Bill</a></li>
            <li id="liOutstandingReportNew" runat="server"><a href="/Common/OutstandingReportNew.aspx">Member Outstanding Report new</a></li>
            <li id="liOutstandingReportNewForMedicine" runat="server"><a href="/Common/OutstandingReportNewForMedicine.aspx">Member Outstanding Report old For Medicine Representatives</a></li>
             <li id="li5" runat="server"><a href="/Common/GenerateRenewalFees_SpecialCase.aspx">Member Bill Genrate (Special Case Block Wise)</a></li>
            
            
            <li>
                <a href="#"><< SMS</a>
                <ul>
                    <li id="liAddNewSMSMember" runat="server"><a href="/Common/SMSMember.aspx">New SMS Members</a></li>
                    <li id="liSMSMemberDetail" runat="server" visible="false"><a href="/Common/SMSMemberList.aspx">SMS Members Detail</a></li>
                     <li id="liSMSPaymentReport" runat="server"><a href="/Common/SMSPaymentReport.aspx">SMS Payment Detail</a></li>
                    <li id="liSMSServiceTaxReport" runat="server"><a href="/Common/SMSServiceTaxReport.aspx">SMS Service Tax Report</a></li>
                    <li id="liAgentPaymentReportSMS" runat="server"><a href="/Common/AgentSMSPaymentReport.aspx">Area Manager SMS Payment Report</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><< Reports</a>
                <ul>
                    <li id="liDevelopmentFeesReportConsolidated" runat="server">
                        <a href="/Common/DevelopmentFeesReportConsolidated.aspx">Development Fees Report</a>
                    </li>
                    <li id="li1" runat="server">
                        <a href="/Common/FeeCollectionReport.aspx">Fees Collection Report</a>
                    </li>
                </ul>
            </li>
        </ul>
    </li>
    <li id="liFeeSettings" runat="server"><a href="" title="Accounts"><span class="icon">
        <img src="/Images/Ruppees.jpg" alt="Accounts" /></span><span>Fee Settings</span></a>
        <ul>
            <li id="liTax" runat="server"><a href="/Accounts/Tax.aspx">Tax Configuration</a></li>
            <li id="liFeesHeadMaster" runat="server"><a href="/Accounts/FeesHeadMaster.aspx">Fees
                Head Master</a></li>
            <li id="liMembershiptCategoryFeesConfig" runat="server"><a href="/Accounts/MembershipCategoryFeesConfig.aspx">Membership Category Fees Configuration</a></li>
            <li id="liMemberFeesConfig" runat="server"><a href="/Accounts/MemberFeesConfig.aspx">Member Development Fees Config</a></li>
            <li id="liMemberDiscountConfig" runat="server"><a href="/Accounts/MemberDiscountConfig.aspx">Member Discount Configuration</a></li>
            <li id="liRenewalFeeAdjustment" runat="server"><a href="/Common/RenewalFeeAdjustment.aspx">Renewal Fee Adjustment</a></li>
        </ul>
    </li>
    <li id="liAccounts" runat="server"><a href="" title="Accounts"><span class="icon">
        <img src="/Images/Ruppees.jpg" alt="Accounts" /></span><span>Accounts</span></a>
        <ul>
            <li id="liAccountsMaster" runat="server"><a href="#" class="childs">Masters</a>
                <ul>
                    <li id="liGroupType" runat="server"><a href="/Accounts/AccountGroupType.aspx">Accounts
                        Group Type</a></li>
                    <li id="liAccountsGroup" runat="server"><a href="/Accounts/AccountsGroup.aspx">Accounts
                        Group</a></li>
                    <li id="liGeneralLedger" runat="server"><a href="/Accounts/GeneralLedger.aspx">General
                        Ledger</a></li>
                    <li id="liCostCentre" runat="server"><a href="/Accounts/CostCentre.aspx">Cost Centre</a></li>
                    <li id="liBankAccount" runat="server"><a href="/Accounts/BankAccount.aspx">Bank Account</a></li>
                    <li id="liOpeningBalanceEntry" runat="server"><a href="/Accounts/OpeningBalanceEntry.aspx">Opening Balance Entry</a></li>
                </ul>
            </li>
            <li id="liCashBankVoucherEntry" runat="server"><a href="/Accounts/CashBankVoucher.aspx">Receipt/Payment Voucher</a></li>
            <li id="liContraVoucher" runat="server"><a href="/Accounts/ContraVoucher.aspx">Contra
                Voucher</a></li>
            <li id="liJournalVoucher" runat="server"><a href="/Accounts/JournalVoucher.aspx">Journal
                Voucher</a></li>
            <li id="liBankReconsiliation" runat="server" visible="false"><a href="/Accounts/BankReconsiliation.aspx">Bank Reconsiliation </a></li>
            <li id="liBillPayment" runat="server"><a href="/Accounts/PurchaseBillPayment.aspx">Purchase
                Bill Payment</a></li>
            <li id="liChangeFinYr" runat="server"><a href="/Accounts/ChangeFinYr.aspx">Change Financial
                Year</a></li>
            <li id="liDebitNote" runat="server"><a href="/Accounts/DebitNote.aspx">Debit Note</a></li>
            <li id="liMeetingRentVoucher" runat="server"><a href="/Accounts/MeetingRentVoucher.aspx">Meeting/Rent Voucher</a></li>
            <li id="liAccountsReport" runat="server"><a href="#" class="childs">Reports</a>
                <ul>
                    <li id="liRPTGeneralLedger" runat="server"><a href="/Accounts/RPTGeneralLedger.aspx">General Ledger</a></li>
                    <li id="liRPTGeneralLedgerBalance" runat="server"><a href="/Accounts/RPTGeneralLedgerBalance.aspx">General Ledger Balance</a></li>
                    <li id="liRPTLedger" runat="server"><a href="/Accounts/RPTLedger.aspx">Ledger Report</a></li>
                    <li id="liRPTBrs" runat="server" visible="false"><a href="/Accounts/RPTBrs.aspx">BRS
                        Report</a></li>
                    <li id="liRPTJournalRegister" runat="server"><a href="/Accounts/RPTJournalRegister.aspx">Journal Report</a></li>
                    <li id="liRPTCashBankVoucher" runat="server"><a href="/Accounts/RPTCashBankVoucher.aspx">Receipt/Payment Report</a></li>
                    <li id="liRPTUserBaseCashBankVoucher" runat="server"><a href="/Accounts/RPTUserBaseCashBankVoucher.aspx">Day End Report</a></li>
                    <li id="liRPTContraVoucher" runat="server"><a href="/Accounts/RPTContraVoucher.aspx">Contra Report</a></li>
                    <li id="liRPTBillPayment" runat="server"><a href="/Accounts/RPTPurchaseBillPayment.aspx">Bill Payment Report</a></li>
                    <li id="liRPTTrailBalance" runat="server"><a href="/Accounts/RPTTrailBalance.aspx">Trial
                        Balance</a></li>
                    <li id="liBalanceSheet" runat="server"><a href="/Accounts/RPTProfitLoss.aspx">P/L &
                        Balance Sheet</a></li>
                    <li id="liRPTCostCenterWiseReport" runat="server"><a href="/Accounts/RPTCostCenterWiseReport.aspx">Cost Center Wise Report</a></li>
                    <li id="liMeetingRentVoucherReport" runat="server"><a href="/Accounts/MeetingRentExpenseForDistrict.aspx">Meeting/Rent Report</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li id="liSMS" runat="server"><a href="" title="SMS"><span class="icon">
        <img src="/Images/SMS.png" alt="Purchase Order" /></span><span>SMS</span></a>
        <ul>
            <li id="liBirdPriceEntry" runat="server"><a href="/Common/AddEditBirdPrice.aspx">Bird
                Price Entry</a></li>
            <li id="liShowBirdPricePerMonth" runat="server"><a href="/Common/ShowBirdPricePerMonth.aspx">Bird Price Report</a></li>
            <li id="liSMSMember" runat="server"><a href="/Common/SMSMember.aspx">SMS Member</a></li>
            <li id="liSMSMemberExpireDetails" runat="server" ><a href="/Common/SMSMemberExpireDetails.aspx">SMS Member Expired List</a></li>
            <li id="liSMSAPIConfig" runat="server"><a href="/Common/SMSAPIConfig.aspx">SMS API Configuration</a></li>
            <li id="liSendSMS" runat="server"><a href="/Common/SendSMS.aspx">Send SMS</a></li>
            <li id="liSMSByMemberType" runat="server"><a href="/Common/SendSMSByMemberType.aspx"> Member Wise Send SMS</a></li>
            <li id="li4" runat="server"><a href="/Common/NECCMember.aspx">NECC Member</a></li>
            <li id="liNECCSendSMS" runat="server"><a href="/Common/NECCSendSMS.aspx"> NECC Send SMS</a></li>
            <%--<li id="liSendSMSToAllMember" runat="server"><a href="/Common/SendSMSToAllMember.aspx">Send SMS To All Member</a></li>--%>
            <li id="liMemberLogDetails" runat="server"><a href="/Common/MemberLogDetails.aspx">Member Log Details</a></li>
            <li id="liSMSReport" runat="server"><a href="/Common/SMSReport.aspx">SMS Report</a></li>
            <%--<li id="li1SendCustomeSMS" runat="server" class="last"><a href="/Common/CustomeSMS.aspx">
                Send Custome SMS</a></li>--%>
            <%--<li id="liSendSMS" runat="server"><a href="/Common/IFSendSms.aspx">Send SMS</a></li>
            <li id="liMemberSMSCategory" runat="server"><a href="/Common/IFsmsreport.aspx">SMS Report</a></li>
            <li id="liAddEditBirdPrice" runat="server"><a href="/Common/IFBirdPrice.aspx">Bird Price
                Entry</a></li>
            <li id="liShowBirdPricePerMonth" runat="server"><a href="/Common/BirdPriceReport.aspx">
                Bird Price/Month</a></li>
            <li id="liSMSSubscription" runat="server"><a href="/Common/IFMembers.aspx">Members</a></li>--%>
        </ul>
    </li>
    <li id="liPO" runat="server"><a href="" title="Purchase Order"><span class="icon">
        <img src="/Images/PO.jpg" alt="Purchase Order" /></span><span>Purchase</span></a>
        <ul>
            <li id="liPOEntry" runat="server" class="last"><a href="/PurchaseOrder/PurchaseOrderEntry.aspx">Purchase Bill Entry</a></li>
        </ul>
    </li>
</ul>
