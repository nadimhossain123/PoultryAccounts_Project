using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class MemberFeesConfig
    {
        public MemberFeesConfig()
        {
        }

        public static void Save(Entity.Common.MemberFeesConfig config)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, config.MemberId);
                oDm.Add("@pFeesXml", SqlDbType.Xml, config.FeesXml);
                oDm.Add("@pParticularsXml", SqlDbType.Xml, config.ParticularsXml);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberFeesConfig_Save");
            }
        }

        public static DataTable GetAll(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberFeesConfig_GetAll");
            }
        }

        public static DataTable MemberDevelopmentFeeGetAll(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_MemberDevelopmentFee_GetAll");
            }
        }

        public static DataSet MemberRenewalFeeGetAll(int MemberId,int FinYrId, int FromMonth, int ToMonth)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pFinYrId", SqlDbType.Int, FinYrId);
                if(FromMonth==0) oDm.Add("@pFromMonth", SqlDbType.Int, DBNull.Value);
                else oDm.Add("@pFromMonth", SqlDbType.Int, FromMonth);
                if(ToMonth==0) oDm.Add("@pToMonth", SqlDbType.Int, DBNull.Value);
                else oDm.Add("@pToMonth", SqlDbType.Int, ToMonth);
                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_MemberRenewalFee_GetAll",ref ds,"Table");
            }
        }

        public static DataSet MemberDevelopmentFeeAllMonthGetAll(int MemberId, int FinYrId, int FromMonth, int ToMonth)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.Add("@pFinYrId", SqlDbType.Int, FinYrId);
                if (FromMonth == 0) oDm.Add("@pFromMonth", SqlDbType.Int, DBNull.Value);
                else oDm.Add("@pFromMonth", SqlDbType.Int, FromMonth);
                if (ToMonth == 0) oDm.Add("@pToMonth", SqlDbType.Int, DBNull.Value);
                else oDm.Add("@pToMonth", SqlDbType.Int, ToMonth);
                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_MemberDevelopmentFeeAllMonth_GetAll",ref ds,"Table");
            }
        }
    }
}
