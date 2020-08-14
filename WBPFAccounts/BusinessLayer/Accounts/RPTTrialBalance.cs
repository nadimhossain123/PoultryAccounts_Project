using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class RPTTrialBalance
    {
        public RPTTrialBalance()
        {

        }

        public DataTable GroupTypeGetAll(int GroupTypeLevel)
        {
            return DataAccess.Accounts.RPTTrialBalance.GroupTypeGetAll(GroupTypeLevel);
        }
    }
}
