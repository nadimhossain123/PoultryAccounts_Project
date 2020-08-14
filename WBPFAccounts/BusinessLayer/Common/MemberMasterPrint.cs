using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class MemberMasterPrint
    {
        public MemberMasterPrint()
        {
        }
        public DataTable MemberMaster_GetAll_ForPrint(Entity.Common.MemberMaster membermaster, int isMember = 2)
        {
            return DataAccess.Common.MemberMasterPrint.MemberMaster_GetAll_ForPrint(membermaster, isMember);
        }
    }
}
