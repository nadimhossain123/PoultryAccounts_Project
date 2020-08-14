using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class FeesHeadMaster
    {
        public FeesHeadMaster()
        {
        }

        public int Save(Entity.Common.FeesHeadMaster FeesHead)
        {
            return DataAccess.Common.FeesHeadMaster.Save(FeesHead);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.FeesHeadMaster.GetAll();
        }

        public DataSet GetAllById(int FeesHeadId)
        {
            return DataAccess.Common.FeesHeadMaster.GetAllById(FeesHeadId);
        }
    }
}
