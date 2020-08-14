using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public static class Utility
    {
        //SETTINGS
        public const string SETTINGS = "100";
        public const string COMPANY_CONFIG = "101";
        public const string CREATE_FINYR = "102";
        public const string DATABASE_BACKUP_RESTORE = "103";
        public const string LEAVE_APPLICATION_CONFIG = "104";
        public const string CHANGE_COMPANY = "105";
        
        //STUDENT
        public const string STUDENT = "200";
        public const string ALL_STUDENT = "201";
        public const string STUDENT_BTECH = "202";
        public const string STUDENT_MBA = "203";
        public const string LIBRARY_FINE = "204";
        public const string TEACHER_SUBJECT_MAPPING = "205";
        public const string STUDENT_ATTENDANCE = "206";
        public const string STUDENT_ATTENDANCE_REPORT = "207";
        public const string FEES = "208";
        public const string ALL_FEES = "209";
        public const string APPROVE_STUDENT = "210";
        public const string SEM_FEES_GENERATION = "211";
        public const string STUDENT_MTECH = "212";
        public const string FEES_HEAD_MASTER = "213";
        public const string STREAM_MASTER = "214";
        public const string STUDENT_PROMOTION = "215";
        public const string NEW_STUDENT_ADMISSION_REPORT = "216";
        public const string USER_BASE_APPROVED_STUDENT_LIST = "217";
        public const string HOSTEL_FEES_CONFIGURATION = "218";
        public const string HOSTEL_BILL_GENERATION = "219";
        public const string STUDENT_CREDIT_BILL_ENTRY = "220";
        public const string STUDENT_DROPOUT = "221";
        public const string APPROVE_HOSTEL = "222";
        public const string APPROVE_HOSTEL_LIST = "223";
        public const string STUDENT_SINGLE_BILL_ENTRY = "224";
        public const string STUDENT_SECTION_MAPPING = "225";
        public const string STUDENT_ELECTIVE_SUBJECT_MAPPING = "226";
        public const string ATTENDANCE_UPDATE = "227";
        public const string LIBRARY_FINE_DELETE_BUTTON = "228";
        public const string STUDENT_DIPLOMA = "229";
        public const string ATTENDANCE_DELETE = "230";

        //PAYROLL
        public const string PAYROLL = "300";
        public const string EMPLOYEE_SALARY_SETTING = "301";
        public const string LOAN_SETTING = "302";
        public const string MONTHLY_SALARY_GENERATION = "303";
        public const string MONTHLY_PAYSLIP_GENERATION = "304";

        public const string ITAX = "305";
        public const string GENERATE_FORM16 = "306";
        public const string ITAX_EMPLOYEE_CNTR = "307";
        public const string ITAX_INVESTMENT_HEAD = "308";
        public const string ITAX_PREV_EMPLHEAD = "309";
        public const string ITAX_SECTION_MASTER = "310";
        public const string PF_REGISTER = "311";
        public const string ESI_REGISTER = "312";
        public const string PTAX_REGISTER = "313";
        public const string ITAX_EMPLOYEE_PREVEMPL_DETAILS = "314";
        public const string ALL_EMPLOYEES_IT_CONTRIBUTION="315";
        public const string VIEW_FORM16 = "316";
        public const string ALL_EMPLOYEES_FORM16 = "317";
        public const string INDIVIDUAL_SALARY_DETAILS = "318";
        public const string STATUTORY_SALARY_CONFIG = "319";
        public const string HOLIDAY_CONFIG = "320";
        public const string PTAX_CONFIG = "321";
        public const string PAYBAND_MASTER = "322";
        public const string EMPLOYEE_DEDUCTION_SETTING = "323";
        public const string ALL_EMPLOYEES_PAYSLIP = "324";
        public const string EMPLOYEE_ADDITIONAL_HEAD_SETTING = "325";
        public const string SALARY_HEAD_UPDATION = "326";
        public const string EMPLOYEE_INCREMENT_LIST = "327";
        public const string EMPLOYEE_INCREMENT_REPORT = "328";
        public const string PTAX_CHALLAN = "329";
        public const string TDS_CHALLAN = "330";

        //HR
        public const string HR = "400";
        public const string CLAIM = "401";
        public const string CLAIM_SETTING = "402";
        public const string CLAIM_APPLY = "403";
        public const string CLAIM_APPROVE = "404";
        public const string CLAIM_APPROVE_BY_DIRECTOR = "405";

        public const string LEAVE = "406";
        public const string LEAVE_SETTING = "407";
        public const string LEAVE_APPLY = "408";
        public const string LEAVE_APPROVE = "409";
        public const string LEAVE_APPROVE_BY_DIRECTOR = "410";


        public const string EMPLOYEE_SETTING = "411";
        public const string ROLE_ACCESS_LEVEL = "412";
        public const string IMPORT_EMPLOYEE_ATTENDANCE = "413";
        public const string LEAVE_MANAGER_CHANGE = "414";
        public const string EMPLOYEE_LEAVE_REPORT = "415";
        public const string EMPLOYEE_LEAVE_BALANCE_REPORT = "416";
        public const string DIRECT_LEAVE_ENTRY = "417";
        public const string LEAVE_DELETE = "418";
        public const string EMPLOYEE_DETAIL = "419";
        public const string EMP_INDIVIDUAL_REPORT = "420";
        public const string LEAVE_STOCK_UPDATE = "421";

        //ACCOUNTS
        public const string ACCOUNTS = "500";
        public const string ACCOUNT_GROUPTYPE = "501";
        public const string ACCOUNT_GROUP = "502";
        public const string GENERAL_LEDGER = "503";
        public const string COST_CENTER = "504";
        public const string BANK_ACCOUNT = "505";
        public const string RECEIPT_PAYMENT_VOUCHER = "506";
        public const string JOURNAL_VOUCHER = "507";
        public const string CONTRA_VOUCHER = "508";
        public const string BANK_RECONSILATION = "509";
        public const string TRIAL_BALANCE = "510";
        public const string STUDENT_FEES_COLLECTION = "511";
        public const string STUDENT_PAYMENT_DETAILS = "512";
        public const string EXPENSE_REIMBURSEMENT = "513";
        public const string PURCHASE_BILL_PAYMENT = "514";
        public const string CHANGE_FINYR = "515";
        public const string REPORT_GENERAL_LEDGER = "516";
        public const string REPORT_GENERAL_LEDGER_BALANCE = "517";
        public const string REPORT_LEDGER = "518";
        public const string REPORT_JOURNAL_VOUCHER = "519";
        public const string REPORT_RECEIPT_PAYMENT_VOUCHER = "520";
        public const string REPORT_BILL_PAYMENT = "521";
        public const string REPORT_TRIAL_BALANCE = "522";
        public const string REPORT_BALANCE_SHEET = "523";
        public const string STUDENT_OUTSTANDING_REPORT = "524";
        public const string REPORT_BRS = "525";
        public const string REPORT_CONTRA_VOUCHER = "526";
        public const string REPORT_USER_BASE_RECEIPT_PAYMENT_VOUCHER = "527";
        public const string STUDENT_ADVANCE_REFUND = "528";
        public const string STUDENT_CAUTION_MONEY_REFUND = "529";
        public const string STUDENT_OPENING_BALANCE = "530";
        public const string FUND_TRANSFER = "531";
        public const string STUDENT_JOURNAL = "532";
        public const string DEBIT_NOTE = "533";
        public const string COST_CENTER_WISE_REPORT = "534";
        public const string CAUTION_MONEY_SUMMARY_REPORT = "535";
        public const string CAUTION_MONEY_INDIVIDUAL_REPORT = "536";
        public const string STUDENT_CONSOLIDATED_OUTSTANDING_REPORT = "537";
        public const string STUDENT_FEES_COLLECTION_REPORT = "538";

        //PURCHASE ORDER
        public const string PURCHASE_ORDER = "600";
        public const string PURCHASE_ORDER_ENTRY = "601";

        //ACTION ROLES
        public const string DELETE_VOUCHER = "701";
    }
}
