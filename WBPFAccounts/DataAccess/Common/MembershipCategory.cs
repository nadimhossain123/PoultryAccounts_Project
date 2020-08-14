using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MembershipCategory
    {
        public MembershipCategory()
        {

        }

        public static void Save(Entity.Common.MembershipCategory membershipcategory)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, ParameterDirection.InputOutput, membershipcategory.MembershipCategoryId);
                oDm.Add("@pCategoryName", SqlDbType.VarChar, 50, membershipcategory.CategoryName);
                if (membershipcategory.CategoryRemarks == string.Empty)
                    oDm.Add("@pCategoryRemarks", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pCategoryRemarks", SqlDbType.VarChar, 200, membershipcategory.CategoryRemarks);
                oDm.Add("@pSMSApplicable", SqlDbType.Bit, membershipcategory.SMSApplicable);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("MembershipCategory_Save");

                membershipcategory.MembershipCategoryId = (int)oDm["@pMembershipCategoryId"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("MembershipCategory_GetAll");
            }
        }

        public static Entity.Common.MembershipCategory GetMembershipCategoryById(int membershipCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, ParameterDirection.Input, membershipCategoryId);

                SqlDataReader dr = oDm.ExecuteReader("MembershipCategory_GetById");

                Entity.Common.MembershipCategory membershipCategory = new Entity.Common.MembershipCategory();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        membershipCategory.MembershipCategoryId = membershipCategoryId;
                        membershipCategory.CategoryName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        membershipCategory.CategoryRemarks = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        membershipCategory.SMSApplicable = Convert.ToBoolean((dr[3] == DBNull.Value) ? null : dr[3].ToString());

                    }
                }
                return membershipCategory;
            }
        }

        public static void Delete(int membershipCategoryId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMembershipCategoryId", SqlDbType.Int, ParameterDirection.Input, membershipCategoryId);

                oDm.ExecuteNonQuery("MembershipCategory_Delete");
            }
        }
    }
}