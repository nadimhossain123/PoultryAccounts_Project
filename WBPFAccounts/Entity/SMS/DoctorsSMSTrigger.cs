using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.SMS
{
    public class DoctorsSMSTrigger
    {
        public int DoctorsSMSTriggerId { get; set; }
        public string Username { get; set; }
        public int GroupId { get; set; }
        public int NoofTrigger { get; set; }
        public string MessageBody { get; set; }
    }
}
