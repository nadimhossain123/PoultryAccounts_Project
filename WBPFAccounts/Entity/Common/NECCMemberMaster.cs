using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class NECCMemberMaster
    {
        public int NECCMemberId { get; set; }

        public string MemberName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public bool IsActive { get; set; }

        public string Remarks { get; set; }

    }
}
