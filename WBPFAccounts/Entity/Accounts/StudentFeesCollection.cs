using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class StudentFeesCollection
    {
        public StudentFeesCollection()
        {
        }

        public int PaymentId { get; set; }
        public string MoneyReceiptNo { get; set; }
        public int StudentId { get; set; }
        public int SemNo { get; set; }
        public decimal Amount { get; set; }
        public int CashBankLedgerID { get; set; }
        public string TransactionType { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string DrawnOn { get; set; }
        public int CreatedBy { get; set; }
        public string PaymentDetailsXML { get; set; }
        public string XMLCashBankVoucherDetails { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinYrId { get; set; }

        public string FeesBookNumber { get; set; }
        public int CBVHeaderId { get; set; }
        public string Narration { get; set; }
        public bool IsRefund { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }


    }
}
