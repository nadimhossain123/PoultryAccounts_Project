using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class MemberFeesConfig
    {
        public MemberFeesConfig()
        {
        }

        public int MemberId { get; set; }
        public string FeesXml { get; set; }
        public string ParticularsXml { get; set; }
    }
}
