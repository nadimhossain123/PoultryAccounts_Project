using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class FeesHeadMaster
    {
        public FeesHeadMaster()
        {
        }

        public int FeesHeadId { get; set; }
        public string FeesHeadName { get; set; }
        public string Frequency { get; set; }
        public int FeesType { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public string FeesHeadTaxMapXml { get; set; }
    }
}
