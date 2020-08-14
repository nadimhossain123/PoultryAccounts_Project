using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class PaymentGateway
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public string MemberType { get; set; }
        public string OrderId { get; set; }
        public string TrackingId { get; set; }
        public string BankRefNo { get; set; }
        public string OrderStatus { get; set; }
        public string FailureMessage { get; set; }
        public string PaymentMode { get; set; }
        public string CardName { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }                         
    }
}
