using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLayer.Accounts
{
    public class OpeningBalanceMaster
    {
        public int OpeningBalanceSave(Entity.Accounts.OpeningBalanceMaster OpBal)
        {
            return DataAccess.Accounts.OpeningBalanceMaster.Save(OpBal);
        }

        public DataTable GetAll()
        {
            return DataAccess.Accounts.OpeningBalanceMaster.GetAll();
        }

        public Entity.Accounts.OpeningBalanceMaster GetById(int OpBalId)
        {
            return DataAccess.Accounts.OpeningBalanceMaster.GetById(OpBalId);
        }
    }
}
