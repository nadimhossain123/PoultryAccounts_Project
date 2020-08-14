using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int MembershipCategoryId { get; set; }
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int SemNo { get; set; }
        public int FeesHeadId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime BillDate { get; set; }
        public int RowsAffected { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool YearlyBill { get; set; }
        public bool MonthlyBill { get; set; }
        public int MemberId { get; set; }
    }
}
