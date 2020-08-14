using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class MembershipCategoryFeesConfig
    {
        public MembershipCategoryFeesConfig()
        {
        }

        public static void Save(Entity.Common.MembershipCategoryFeesConfig config)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, config.MembershipCategoryId);
                oDm.Add("@pFeesXml", SqlDbType.Xml, config.FeesXml);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MembershipCategoryFeesConfig_Save");
            }
        }

        public static DataTable GetAll(int MembershipCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, MembershipCategoryId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MembershipCategoryFeesConfig_GetAll");
            }
        }
    }
}
