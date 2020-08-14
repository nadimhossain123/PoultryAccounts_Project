using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public void GenerateSemFees(Entity.Common.SemFeesGeneration SemFees)
        {
            DataAccess.Common.SemFeesGeneration.GenerateSemFees(SemFees);
        }

        public void GenerateYearlyFees(Entity.Common.SemFeesGeneration SemFees)
        {
            DataAccess.Common.SemFeesGeneration.GenerateYearlyFees(SemFees);
        }

        public DataTable GetConsolidated_StudentOutstandingReport(Entity.Common.SemFeesGeneration SemFees)
        {
            return DataAccess.Common.SemFeesGeneration.GetConsolidated_StudentOutstandingReport(SemFees);
        }

        public int GenerateMemberFees_Manual(Entity.Common.SemFeesGeneration SemFees)
        {
            return DataAccess.Common.SemFeesGeneration.GenerateMemberFees_Manual(SemFees);
        }

        public DataTable SMSFeeReceipt(string VoucherNo)
        {
            return DataAccess.Common.SemFeesGeneration.SMSFeeReceipt(VoucherNo);
        }

    }
}
