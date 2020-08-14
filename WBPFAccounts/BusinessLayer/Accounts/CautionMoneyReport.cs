using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class CautionMoneyReport
    {
        public CautionMoneyReport()
        {
        }

        public DataTable GetSummaryReport(int batch_id, int CourseId, int StreamId)
        {
            return DataAccess.Accounts.CautionMoneyReport.GetSummaryReport(batch_id, CourseId, StreamId);
        }

        public DataSet GetIndividualReport(int StudentId, int FeesHeadId, DateTime? FromDate, DateTime? ToDate)
        {
            return DataAccess.Accounts.CautionMoneyReport.GetIndividualReport(StudentId, FeesHeadId, FromDate, ToDate);
        }
    }
}
