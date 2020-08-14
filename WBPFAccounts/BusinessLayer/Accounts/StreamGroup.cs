using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class StreamGroup
    {
        public DataTable GetParentStream(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.GetParentStream(stremGroup);
        }
        public DataTable GetFeesHead(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.GetFeesHead(stremGroup);
        }
        public DataSet GetLoad(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.GetLoad(stremGroup);
        }
        public int SaveHeader(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.SaveHeader(stremGroup);
        }
        public int SaveDetails(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.SaveDetails(stremGroup);
        }
        public int SaveBatch(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.SaveBatch(stremGroup);
        }

        public DataTable GetOtherFeesHead()
        {
            return DataAccess.Accounts.StreamGroup.GetOtherFeesHead();
        }

        public int SaveFeesHead(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.SaveFeesHead(stremGroup);
        }

        public DataTable GetAllFeesHead()
        {
            return DataAccess.Accounts.StreamGroup.GetAllFeesHead();
        }

        public DataTable GetAllFeesHeadById(int feesId)
        {
            return DataAccess.Accounts.StreamGroup.GetAllFeesHeadById(feesId);
        }

        public DataTable AllFees(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.AllFees(stremGroup);
        }
        public DataTable FeesBasedOnID(Entity.Accounts.StreamGroup stremGroup)
        {
            return DataAccess.Accounts.StreamGroup.FeesBasedOnID(stremGroup);
        }

        public DataTable GetAllHostelFeesHead()
        {
            return DataAccess.Accounts.StreamGroup.GetAllHostelFeesHead();
        }

        public DataTable GetAllSemesterFeesHead()
        {
            return DataAccess.Accounts.StreamGroup.GetAllSemesterFeesHead();
        }
    }
}
