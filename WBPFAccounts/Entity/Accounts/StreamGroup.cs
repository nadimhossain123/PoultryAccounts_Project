using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Entity.Accounts
{
    public class StreamGroup
    {
        public int intMode { get; set; }
        public int intCompanyId { get; set; }
        public string login_id { get; set; }
        public string studentID { get; set; }
        public int stream_type_id { get; set; }

        public int batch_ID { get; set; }
        public int courseID { get; set; }

        public int feesID { get; set; }
        public int feesHeaderID { get; set; }
        public int column_name { get; set; }
        public decimal  column_value { get; set; }

        //---------------Batch Insert
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string strBatchName { get; set; }

        public string StreamName { get; set; }
        public string Fees { get; set; }

        public string FeesName { get; set; }
        public string FeesHeadType { get; set; }
        public int AssestLedgerID_FK { get; set; }
        public int IncomeLedgerID_FK { get; set; }
        public bool IsRefundable { get; set; }
        public bool IsOneTimeApplicable { get; set; }
        public bool IsYearlyFees { get; set; }
        public bool IsMonthlyFees { get; set; }
        public bool IsTaxHead { get; set; }
        public int TAXDependencyHead { get; set; }
    }
}
