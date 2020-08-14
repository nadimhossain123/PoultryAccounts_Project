using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class MembershipCategoryFeesConfig
    {
        public MembershipCategoryFeesConfig()
        {
        }

        public int MembershipCategoryId { get; set; }
        public string FeesXml { get; set; }
    }
}
