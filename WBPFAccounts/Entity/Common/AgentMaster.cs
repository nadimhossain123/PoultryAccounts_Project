using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class AgentMaster
    {
        public AgentMaster()
        {
        }

        public int AgentId { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; }
        public string AgentPassword { get; set; }
        public int CreatedBy { get; set; }

    }
}
