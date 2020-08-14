using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.SMS
{
    public class DoctorMaster
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public string MobileNo { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
    }
}
