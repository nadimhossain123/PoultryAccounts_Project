using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MemberGroup
    {
        public MemberGroup()
        {

        }

        public static void Save(Entity.Common.MemberGroup membergroup)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberGroupId", SqlDbType.Int, ParameterDirection.InputOutput, membergroup.MemberGroupId);
                oDm.Add("@pMemberGroupName", SqlDbType.VarChar, 50, membergroup.MemberGroupName);
                if (membergroup.Remarks == string.Empty)
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, string.Empty);
                else
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, membergroup.Remarks);


                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("MemberGroup_Save");

                membergroup.MemberGroupId = (int)oDm["@pMemberGroupId"].Value;
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteDataTable("MemberGroup_GetAll");
            }
        }

        public static Entity.Common.MemberGroup GetMemberGroupById(int memberGroupId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMemberGroupId", SqlDbType.Int, ParameterDirection.Input, memberGroupId);

                SqlDataReader dr = oDm.ExecuteReader("MemberGroup_GetById");

                Entity.Common.MemberGroup memberGroup = new Entity.Common.MemberGroup();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        memberGroup.MemberGroupId = memberGroupId;
                        memberGroup.MemberGroupName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        memberGroup.Remarks = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();

                    }
                }
                return memberGroup;
            }
        }

        public static void Delete(int memberGroupId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMemberGroupId", SqlDbType.Int, ParameterDirection.Input, memberGroupId);

                oDm.ExecuteNonQuery("MemberGroup_Delete");
            }
        }
    }
}