using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class MemberDocument
    {
        public MemberDocument()
        {
        }

        public static void Save(Entity.Common.MemberDocument memberDocument)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMode", SqlDbType.VarChar, 10, memberDocument.Mode);
                oDm.Add("@pMemberId", SqlDbType.Int, memberDocument.MemberId);
                oDm.Add("@pDocName_1", SqlDbType.VarChar, 100, memberDocument.DocName_1);
                oDm.Add("@pDocFile_1", SqlDbType.VarChar, 50, memberDocument.DocFile_1);
                oDm.Add("@pDocName_2", SqlDbType.VarChar, 100, memberDocument.DocName_2);
                oDm.Add("@pDocFile_2", SqlDbType.VarChar, 50, memberDocument.DocFile_2);
                oDm.Add("@pDocName_3", SqlDbType.VarChar, 100, memberDocument.DocName_3);
                oDm.Add("@pDocFile_3", SqlDbType.VarChar, 50, memberDocument.DocFile_3);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_MemberDocument_Save");
            }
        }

        public static Entity.Common.MemberDocument GetAllById(int MemberId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pMemberId", SqlDbType.Int, MemberId);
                oDm.CommandType=CommandType.StoredProcedure;
                Entity.Common.MemberDocument memberDocument = new Entity.Common.MemberDocument();

                SqlDataReader dr;
                dr = oDm.ExecuteReader("usp_MemberDocument_GetAllById");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        memberDocument.DocName_1 = dr["DocName_1"].ToString();
                        memberDocument.DocFile_1 = dr["DocFile_1"].ToString();
                        memberDocument.DocName_2 = dr["DocName_2"].ToString();
                        memberDocument.DocFile_2 = dr["DocFile_2"].ToString();
                        memberDocument.DocName_3 = dr["DocName_3"].ToString();
                        memberDocument.DocFile_3 = dr["DocFile_3"].ToString();
                    }
                }
                return memberDocument;
            }
        }
    }
}
