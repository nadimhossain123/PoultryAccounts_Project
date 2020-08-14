using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Accounts
{
    public class StudentOpeningBal
    {
        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int StudentId { get; set; }
        public int SemNo { get; set; }
        public decimal BillAmount { get; set; }
        public int CreatedBy { get; set; }
        public string DrORCr { get; set; }
        public string OpeningBalXML { get; set; }
        public int BillId { get; set; }
    }
}
