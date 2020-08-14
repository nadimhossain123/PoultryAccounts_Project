using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class MemberDiscountConfig
    {
        public MemberDiscountConfig()
        {
        }

        public int MemberId { get; set; }
        public string DiscountConfigXml { get; set; }
    }
}
