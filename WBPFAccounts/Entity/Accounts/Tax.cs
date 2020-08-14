using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class Tax
    {
        private int taxId;
        private string taxHead;
        private decimal taxPercent;
        public int FeesHeadId { get; set; }
        public int LedgerId { get; set; }
        public int ParentLedgerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }

        public int TaxId
        {
            get { return taxId; }
            set { taxId = value; }
        }

        public decimal TaxPercent
        {
            get { return taxPercent; }
            set { taxPercent = value; }
        }

        public string TaxHead
        {
            get { return taxHead; }
            set { taxHead = value; }
        }

        public DateTime EffectiveFromDate { get; set; }
    }
}
