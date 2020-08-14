using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class CautionMoneyReport
    {
        public CautionMoneyReport()
        {
        }

        public static DataTable GetSummaryReport(int batch_id, int CourseId, int StreamId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (batch_id > 0)
                    oDm.Add("@batch_id", SqlDbType.Int, batch_id);
                else
                    oDm.Add("@batch_id", SqlDbType.Int, DBNull.Value);

                if (CourseId > 0)
                    oDm.Add("@CourseId", SqlDbType.Int, CourseId);
                else
                    oDm.Add("@CourseId", SqlDbType.Int, DBNull.Value);

                if (StreamId > 0)
                    oDm.Add("@StreamId", SqlDbType.Int, StreamId);
                else
                    oDm.Add("@StreamId", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_CautionMoneySummaryReport");
            }
        }

        public static DataSet GetIndividualReport(int StudentId, int FeesHeadId, DateTime? FromDate, DateTime? ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StudentId", SqlDbType.Int, StudentId);

                if (FeesHeadId > 0)
                    oDm.Add("@FeesHeadId", SqlDbType.Int, FeesHeadId);
                else
                    oDm.Add("@FeesHeadId", SqlDbType.Int, DBNull.Value);

                if (FromDate != null)
                    oDm.Add("@FromDate", SqlDbType.DateTime, FromDate);
                else
                    oDm.Add("@FromDate", SqlDbType.DateTime, DBNull.Value);

                if (ToDate != null)
                    oDm.Add("@ToDate", SqlDbType.DateTime, ToDate);
                else
                    oDm.Add("@ToDate", SqlDbType.DateTime, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_CautionMoneyIndividualReport",ref ds,"table");
            }
        }
    }
}
