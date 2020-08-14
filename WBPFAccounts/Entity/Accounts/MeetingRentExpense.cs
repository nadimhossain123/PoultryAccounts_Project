using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class MeetingRentExpense
    {
        public int CBVHeaderID { get; set; }
        public int CashBankLedgerId { get; set; }
        public int LedgerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Narration { get; set; }
        public int CreatedBy { get; set; }
        public int DistrictId { get; set; }
        public string DistrictAmountXML { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Amount { get; set; }
    }
}
