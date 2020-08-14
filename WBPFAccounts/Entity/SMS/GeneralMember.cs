using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.SMS
{
    public class GeneralMember
    {
        public GeneralMember()
        {
        }

        public int MemberId { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Village { get; set; }
        public string PostOffice { get; set; }
        public string PoliceStation { get; set; }
        public string PinCode { get; set; }
        public string BlockName { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string Code { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public int CategoryId { get; set; }
    }
}
