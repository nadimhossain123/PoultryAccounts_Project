using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class MemberPayment
    {
        public MemberPayment()
        {
        }

        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Narration { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserType { get; set; }
        public string FeesXml { get; set; }
        public int CashBankLedgerId { get; set; }
        public bool? IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool IsExcelUpload { get; set; }
    }
}
