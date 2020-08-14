using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class PurchaseBillPayment
    {
        public PurchaseBillPayment()
        {
        }

        public int PurchaseBillPaymentId { get; set; }
        public int SupplierLedgerId_FK { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDeducted { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ModeOfPayment { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string DrawnOn { get; set; }
        public int CreatedBy { get; set; }
        public string Narration { get; set; }

        public int CashBankLedgerID { get; set; }
        public string TransactionType { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinYrId { get; set; }
        public string XMLCashBankVoucherDetails { get; set; }
        public int CBVHeaderID { get; set; }
        public string PaymentDetailsXML { get; set; }
        public string DeductionDetailsXML { get; set; }

    }
}
