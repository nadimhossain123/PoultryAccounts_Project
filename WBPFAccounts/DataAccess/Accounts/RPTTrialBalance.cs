using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class RPTTrialBalance
    {
        public RPTTrialBalance()
        {

        }

        public static DataTable GroupTypeGetAll(int GroupTypeLevel)
        {
            using (DataManager oDm = new DataManager())
            {
                if (GroupTypeLevel == 0)
                    oDm.Add("@GroupTypeLevel", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@GroupTypeLevel", SqlDbType.Int, GroupTypeLevel);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("spSelect_MstAccountsGroupType1");
            }
        }
    }
}
