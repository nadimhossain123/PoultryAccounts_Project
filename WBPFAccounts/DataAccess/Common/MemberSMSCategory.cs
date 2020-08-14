using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MemberSMSCategory
    {
        public MemberSMSCategory()
        { }

        public static int MemberSMSCategory_Save(Entity.Common.MemberSMSCategory memberSMSCategory)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, ParameterDirection.Input, memberSMSCategory.MemberSMSCategoryId);
                oDm.Add("@MemberSMSCategoryName", SqlDbType.VarChar, 100, memberSMSCategory.MemberSMSCategoryName);
                
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_MemberSMSCategory_Save");
            }
        }

        public static DataTable MemberSMSCategory_GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("usp_MemberSMSCategory_GetAll");
            }
        }

        public static Entity.Common.MemberSMSCategory MemberSMSCategory_GetById(int memberSMSCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, ParameterDirection.Input, memberSMSCategoryId);

                SqlDataReader dr = oDm.ExecuteReader("usp_MemberSMSCategory_GetById");

                Entity.Common.MemberSMSCategory memberSMSCategory = new Entity.Common.MemberSMSCategory();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        memberSMSCategory.MemberSMSCategoryId = memberSMSCategoryId;
                        memberSMSCategory.MemberSMSCategoryName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                    }
                }
                return memberSMSCategory;
            }
        }

        public static int MemberSMSCategory_Delete(int memberSMSCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@MemberSMSCategoryId", SqlDbType.Int, ParameterDirection.Input, memberSMSCategoryId);

                return oDm.ExecuteNonQuery("usp_MemberSMSCategory_Delete");
            }
        }
    }
}
