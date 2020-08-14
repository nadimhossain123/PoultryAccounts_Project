using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class MemberDiscountConfig
    {
        public MemberDiscountConfig()
        {
        }

        public static void Save(Entity.Common.MemberDiscountConfig config)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, config.MemberId);
                oDm.Add("@pDiscountConfigXml", SqlDbType.Xml, config.DiscountConfigXml);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberDiscountConfig_Save");
            }
        }

        public static DataTable GetAll(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberDiscountConfig_GetAll");
            }
        }
    }
}
