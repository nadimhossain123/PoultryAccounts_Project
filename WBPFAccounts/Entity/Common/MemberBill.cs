using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class MemberBill
    {
        public MemberBill()
        {
        }

        public int Month { get; set; }
        public int Year { get; set; }
        public int MemberId { get; set; }
        public int CreatedBy { get; set; }

        public int MonthTo { get; set; }

        public int Amount { get; set; }
    }
}
